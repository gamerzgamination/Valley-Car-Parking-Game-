using UnityEngine;

public class came : MonoBehaviour
{
	public Transform playerCar;

	private Rigidbody playerRigid;

	private Camera cam;

	public float distance = 6f;

	public float height = 2f;

	public float heightOffset = 0.75f;

	public float heightDamping = 2f;

	public float rotationDamping = 3f;

	public bool useSmoothRotation = true;

	public float minimumFOV = 50f;

	public float maximumFOV = 70f;

	public float maximumTilt = 15f;

	private float tiltAngle;

	private void Start()
	{
		if ((bool)playerCar)
		{
			playerRigid = playerCar.GetComponent<Rigidbody>();
			cam = GetComponent<Camera>();
		}
	}

	private void Update()
	{
		if ((bool)playerCar)
		{
			if (playerRigid != playerCar.GetComponent<Rigidbody>())
			{
				playerRigid = playerCar.GetComponent<Rigidbody>();
			}
			tiltAngle = Mathf.Lerp(tiltAngle, Mathf.Clamp(0f - playerCar.InverseTransformDirection(playerRigid.velocity).x, -35f, 35f), Time.deltaTime * 2f);
			if (!cam)
			{
				cam = GetComponent<Camera>();
			}
			cam.fieldOfView = Mathf.Lerp(minimumFOV, maximumFOV, playerRigid.velocity.magnitude * 3f / 150f);
		}
	}

	private void LateUpdate()
	{
		if ((bool)playerCar && (bool)playerRigid)
		{
			float num = playerRigid.transform.InverseTransformDirection(playerRigid.velocity).z * 3f;
			float b = playerCar.eulerAngles.y;
			float num2 = playerCar.position.y + height;
			float y = base.transform.eulerAngles.y;
			float y2 = base.transform.position.y;
			if (useSmoothRotation)
			{
				rotationDamping = Mathf.Lerp(0f, 3f, playerRigid.velocity.magnitude * 3f / 40f);
			}
			if (num < -10f)
			{
				b = playerCar.eulerAngles.y + 180f;
			}
			y = Mathf.LerpAngle(y, b, rotationDamping * Time.deltaTime);
			y2 = Mathf.Lerp(y2, num2 + Mathf.Lerp(-1f, 0f, playerRigid.velocity.magnitude * 3f / 20f), heightDamping * Time.deltaTime);
			Quaternion quaternion = Quaternion.Euler(0f, y, 0f);
			base.transform.position = playerCar.position;
			base.transform.position -= quaternion * Vector3.forward * distance;
			base.transform.position = new Vector3(base.transform.position.x, y2, base.transform.position.z);
			base.transform.LookAt(new Vector3(playerCar.position.x, playerCar.position.y + heightOffset, playerCar.position.z));
			base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, base.transform.eulerAngles.y, Mathf.Clamp(tiltAngle, -10f, 10f));
		}
	}
}
