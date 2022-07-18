using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse drag Orbit with zoom")]
public class ob : MonoBehaviour
{
	public Transform target;

	public bool autoRotateOn;

	public bool autoRotateReverse;

	public float autoRotateSpeed = 1f;

	private float originalAutoRotateSpeed;

	public float autoRotateSpeedFast = 5f;

	private float autoRotateValue = 1f;

	public float distance = 1.5f;

	public float xSpeed = 1f;

	public float ySpeed = 1f;

	public float yMinLimit = -20f;

	public float yMaxLimit = 80f;

	public float distanceMin = 1f;

	public float distanceMax = 3f;

	public float smoothTime = 2f;

	public float autoTimer = 5f;

	public float rotationYAxis;

	private float rotationYyAxis;

	private float rotationcyAxis;

	public float rotationXAxis;

	private float velocityX;

	private float velocityY;

	private bool faster;

	private bool rkeyActive;

	public static ob obi;

	private void Start()
	{
		obi = this;
		rkeyActive = autoRotateOn;
		autoRotateValue = 1f;
		Vector3 eulerAngles = base.transform.eulerAngles;
		if (MainMenu.currentlevel == 3 && MainMenu.current_mode == 1)
		{
			rotationXAxis = 30f;
			rotationYAxis = 7f;
		}
		else if (MainMenu.currentlevel <= 10 && MainMenu.current_mode == 1)
		{
			rotationXAxis = 56f;
			rotationYAxis = 7f;
		}
		else
		{
			rotationXAxis = 70f;
		}
		originalAutoRotateSpeed = autoRotateSpeed;
	}

	private void Update()
	{
		if (autoRotateOn)
		{
			velocityX += autoRotateSpeed * autoRotateValue * Time.deltaTime;
		}
	}

	private void LateUpdate()
	{
		if (target != null)
		{
			float num = Input.GetAxis("Mouse X");
			float num2 = Input.GetAxis("Mouse Y");
		
            if (Input.touchCount > 0 && Toolbox.HUDListner.istouched)
            {
                num = Input.touches[0].deltaPosition.x / 4f;
                num2 = Input.touches[0].deltaPosition.y / 4f;
            }
            if (Input.GetMouseButton(0) && Toolbox.HUDListner.istouched)
            {
                velocityX += xSpeed * num * 0.05f;
                velocityY -= ySpeed * num2 * 0.02f;
            }
            rotationYAxis += velocityX;
			rotationXAxis += velocityY;
			rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
			Quaternion quaternion = Quaternion.Euler(rotationXAxis, rotationYAxis + target.eulerAngles.y, 0f);
			Quaternion quaternion2 = quaternion;
			Vector3 vector = new Vector3(0f, 0f, 0f - distance);
			Vector3 position = quaternion2 * vector + target.position;
			base.transform.rotation = quaternion2;
			base.transform.position = position;
			velocityX = Mathf.Lerp(velocityX, 0f, Time.deltaTime * smoothTime);
			velocityY = Mathf.Lerp(velocityY, 0f, Time.deltaTime * smoothTime);
		}
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -90f)
		{
			angle += 90f;
		}
		if (angle > 90f)
		{
			angle -= 90f;
		}
		return Mathf.Clamp(angle, min, max);
	}
}
