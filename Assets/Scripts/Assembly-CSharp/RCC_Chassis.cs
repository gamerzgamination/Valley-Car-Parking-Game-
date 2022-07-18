using System.Collections;
using UnityEngine;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/Chassis")]
public class RCC_Chassis : MonoBehaviour
{
	private RCC_Settings RCCSettingsInstance;

	private Rigidbody mainRigid;

	private float chassisVerticalLean = 4f;

	private float chassisHorizontalLean = 4f;

	private float horizontalLean;

	private float verticalLean;

	private RCC_Settings RCCSettings
	{
		get
		{
			if (RCCSettingsInstance == null)
			{
				RCCSettingsInstance = RCC_Settings.Instance;
			}
			return RCCSettingsInstance;
		}
	}

	private void Start()
	{
		mainRigid = GetComponentInParent<RCC_CarControllerV3>().GetComponent<Rigidbody>();
		chassisVerticalLean = GetComponentInParent<RCC_CarControllerV3>().chassisVerticalLean;
		chassisHorizontalLean = GetComponentInParent<RCC_CarControllerV3>().chassisHorizontalLean;
		if (!RCCSettings.dontUseChassisJoint)
		{
			ChassisJoint();
		}
	}

	private void OnEnable()
	{
		if (!RCCSettings.dontUseChassisJoint)
		{
			StartCoroutine("ReEnable");
		}
	}

	private IEnumerator ReEnable()
	{
		if (!GetComponent<ConfigurableJoint>())
		{
			yield return null;
		}
		GameObject _joint = GetComponentInParent<ConfigurableJoint>().gameObject;
		_joint.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
		yield return new WaitForFixedUpdate();
		_joint.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
	}

	private void ChassisJoint()
	{
		GameObject gameObject = new GameObject("Colliders");
		gameObject.transform.SetParent(GetComponentInParent<RCC_CarControllerV3>().transform, worldPositionStays: false);
		Transform[] componentsInChildren = GetComponentInParent<RCC_CarControllerV3>().chassis.GetComponentsInChildren<Transform>();
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			if (!transform.gameObject.activeSelf || !transform.GetComponent<Collider>())
			{
				continue;
			}
			if (transform.childCount >= 1)
			{
				Transform[] componentsInChildren2 = transform.GetComponentsInChildren<Transform>();
				Transform[] array2 = componentsInChildren2;
				foreach (Transform transform2 in array2)
				{
					if (transform2 != transform)
					{
						transform2.SetParent(base.transform);
					}
				}
			}
			GameObject gameObject2 = Object.Instantiate(transform.gameObject, transform.transform.position, transform.transform.rotation);
			gameObject2.transform.SetParent(gameObject.transform, worldPositionStays: true);
			gameObject2.transform.localScale = transform.lossyScale;
			Component[] components = gameObject2.GetComponents(typeof(Component));
			Component[] array3 = components;
			foreach (Component component in array3)
			{
				if (!(component is Transform) && !(component is Collider))
				{
					Object.Destroy(component);
				}
			}
		}
		GameObject gameObject3 = Object.Instantiate(RCCSettings.chassisJoint, Vector3.zero, Quaternion.identity);
		gameObject3.transform.SetParent(mainRigid.transform, worldPositionStays: false);
		gameObject3.GetComponent<ConfigurableJoint>().connectedBody = mainRigid;
		gameObject3.GetComponent<ConfigurableJoint>().autoConfigureConnectedAnchor = false;
		base.transform.SetParent(gameObject3.transform, worldPositionStays: false);
		Collider[] componentsInChildren3 = GetComponentsInChildren<Collider>();
		Collider[] array4 = componentsInChildren3;
		foreach (Collider obj in array4)
		{
			Object.Destroy(obj);
		}
		GetComponentInParent<Rigidbody>().centerOfMass = new Vector3(mainRigid.centerOfMass.x, mainRigid.centerOfMass.y + 1f, mainRigid.centerOfMass.z);
	}

	private void FixedUpdate()
	{
		if (RCCSettings.dontUseChassisJoint)
		{
			LegacyChassis();
		}
	}

	private void LegacyChassis()
	{
		verticalLean = Mathf.Clamp(Mathf.Lerp(verticalLean, mainRigid.angularVelocity.x * chassisVerticalLean, Time.fixedDeltaTime * 5f), -3f, 3f);
		horizontalLean = Mathf.Clamp(Mathf.Lerp(horizontalLean, base.transform.InverseTransformDirection(mainRigid.angularVelocity).y * (float)((base.transform.InverseTransformDirection(mainRigid.velocity).z >= 0f) ? 1 : (-1)) * chassisHorizontalLean, Time.fixedDeltaTime * 5f), -3f, 3f);
		if (!float.IsNaN(verticalLean) && !float.IsNaN(horizontalLean) && !float.IsInfinity(verticalLean) && !float.IsInfinity(horizontalLean) && !Mathf.Approximately(verticalLean, 0f) && !Mathf.Approximately(horizontalLean, 0f))
		{
			Quaternion localRotation = Quaternion.Euler(verticalLean, base.transform.localRotation.y + mainRigid.angularVelocity.z, horizontalLean);
			base.transform.localRotation = localRotation;
		}
	}
}
