using UnityEngine;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Camera/Fixed Camera")]
public class RCC_FixedCamera : MonoBehaviour
{
	public Transform currentCar;

	private RCC_Camera rccCamera;

	private Vector3 targetPosition;

	private Vector3 smoothedPosition;

	public float maxDistance = 50f;

	private float distance;

	public float minimumFOV = 20f;

	public float maximumFOV = 60f;

	public bool canTrackNow;

	private float timer;

	public float updateInSeconds = 0.05f;

	private void Start()
	{
		rccCamera = Object.FindObjectOfType<RCC_Camera>();
	}

	private void LateUpdate()
	{
		if (canTrackNow)
		{
			if (!rccCamera)
			{
				rccCamera = Object.FindObjectOfType<RCC_Camera>();
				return;
			}
			if (!currentCar)
			{
				currentCar = rccCamera.playerCar;
				return;
			}
			CheckCulling();
			targetPosition = currentCar.position;
			targetPosition += currentCar.rotation * Vector3.forward * (currentCar.GetComponent<RCC_CarControllerV3>().speed * 0.05f);
			smoothedPosition = Vector3.Lerp(smoothedPosition, targetPosition, Time.deltaTime * 5f);
			distance = Vector3.Distance(base.transform.position, currentCar.position);
			rccCamera.targetFieldOfView = Mathf.Lerp((!(distance > maxDistance / 10f)) ? 70f : maximumFOV, minimumFOV, distance * 1.5f / maxDistance);
			base.transform.LookAt(smoothedPosition);
			base.transform.Translate(-currentCar.forward * currentCar.GetComponent<RCC_CarControllerV3>().speed / 50f * Time.deltaTime);
		}
	}

	private void CheckCulling()
	{
		timer += Time.deltaTime;
		if (!(timer < updateInSeconds))
		{
			timer = 0f;
			if ((Physics.Linecast(currentCar.position, base.transform.position, out var hitInfo) && !hitInfo.transform.IsChildOf(currentCar) && !hitInfo.collider.isTrigger) || distance >= maxDistance)
			{
				ChangePosition();
			}
		}
	}

	private void ChangePosition()
	{
		float num = Random.Range(-15f, 15f);
		if (Physics.Raycast(currentCar.position, Quaternion.AngleAxis(num, currentCar.up) * currentCar.forward, out var hitInfo, maxDistance) && !hitInfo.transform.IsChildOf(currentCar) && !hitInfo.collider.isTrigger)
		{
			base.transform.position = hitInfo.point;
			base.transform.LookAt(currentCar.position + new Vector3(0f, Mathf.Clamp(num, 0.5f, 5f), 0f));
			base.transform.position += base.transform.rotation * Vector3.forward * 5f;
		}
		else
		{
			base.transform.position = currentCar.position + new Vector3(0f, Mathf.Clamp(num, 0f, 5f), 0f);
			base.transform.position += Quaternion.AngleAxis(num, currentCar.up) * currentCar.forward * (maxDistance * 0.9f);
		}
	}
}
