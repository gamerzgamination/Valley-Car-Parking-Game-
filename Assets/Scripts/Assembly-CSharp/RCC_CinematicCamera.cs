using UnityEngine;

public class RCC_CinematicCamera : MonoBehaviour
{
	internal Transform currentCar;

	private RCC_Camera rccCamera;

	public GameObject pivot;

	private Vector3 targetPosition;

	public float targetFOV = 60f;

	private void Awake()
	{
		if (!pivot)
		{
			pivot = new GameObject("Pivot");
			pivot.transform.SetParent(base.transform, worldPositionStays: false);
			pivot.transform.localPosition = Vector3.zero;
			pivot.transform.localRotation = Quaternion.identity;
		}
	}

	private void Update()
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
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(base.transform.eulerAngles.x, currentCar.transform.eulerAngles.y + 180f, base.transform.eulerAngles.z), Time.deltaTime * 3f);
		targetPosition = currentCar.position;
		targetPosition -= base.transform.rotation * Vector3.forward * 10f;
		base.transform.position = targetPosition;
	}
}
