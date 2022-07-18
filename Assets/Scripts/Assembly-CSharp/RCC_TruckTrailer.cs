using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/Truck Trailer")]
[RequireComponent(typeof(Rigidbody))]
public class RCC_TruckTrailer : MonoBehaviour
{
	private RCC_CarControllerV3 carController;

	private Rigidbody rigid;

	public Transform COM;

	public WheelCollider[] wheelColliders;

	private List<WheelCollider> leftWheelColliders = new List<WheelCollider>();

	private List<WheelCollider> rightWheelColliders = new List<WheelCollider>();

	public float antiRoll = 50000f;

	private void Start()
	{
		rigid = GetComponent<Rigidbody>();
		carController = base.transform.GetComponentInParent<RCC_CarControllerV3>();
		rigid.interpolation = RigidbodyInterpolation.None;
		rigid.interpolation = RigidbodyInterpolation.Interpolate;
		antiRoll = carController.antiRollFrontHorizontal;
		for (int i = 0; i < wheelColliders.Length; i++)
		{
			if (wheelColliders[i].transform.localPosition.x < 0f)
			{
				leftWheelColliders.Add(wheelColliders[i]);
			}
			else
			{
				rightWheelColliders.Add(wheelColliders[i]);
			}
		}
		base.gameObject.SetActive(value: false);
		base.gameObject.SetActive(value: true);
	}

	private void FixedUpdate()
	{
		rigid.centerOfMass = base.transform.InverseTransformPoint(COM.transform.position);
		AntiRollBars();
		WheelCollider[] array = wheelColliders;
		foreach (WheelCollider wheelCollider in array)
		{
			wheelCollider.motorTorque = carController._gasInput * (carController.engineTorque / 10f);
		}
	}

	public void AntiRollBars()
	{
		for (int i = 0; i < leftWheelColliders.Count; i++)
		{
			float num = 1f;
			float num2 = 1f;
			WheelHit hit;
			bool groundHit = leftWheelColliders[i].GetGroundHit(out hit);
			if (groundHit)
			{
				num = (0f - leftWheelColliders[i].transform.InverseTransformPoint(hit.point).y - leftWheelColliders[i].radius) / leftWheelColliders[i].suspensionDistance;
			}
			bool groundHit2 = rightWheelColliders[i].GetGroundHit(out hit);
			if (groundHit2)
			{
				num2 = (0f - rightWheelColliders[i].transform.InverseTransformPoint(hit.point).y - rightWheelColliders[i].radius) / rightWheelColliders[i].suspensionDistance;
			}
			float num3 = (num - num2) * antiRoll;
			if (groundHit)
			{
				rigid.AddForceAtPosition(leftWheelColliders[i].transform.up * (0f - num3), leftWheelColliders[i].transform.position);
			}
			if (groundHit2)
			{
				rigid.AddForceAtPosition(rightWheelColliders[i].transform.up * num3, rightWheelColliders[i].transform.position);
			}
		}
	}
}
