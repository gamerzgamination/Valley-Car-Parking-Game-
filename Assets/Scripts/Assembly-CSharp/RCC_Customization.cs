using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RCC_Customization : MonoBehaviour
{
	public static void SetCustomizationMode(RCC_CarControllerV3 car, bool state)
	{
		if (!car)
		{
			Debug.LogError("Player Car is not selected for customization!");
			return;
		}
		if (state)
		{
			car.canControl = false;
			if ((bool)UnityEngine.Object.FindObjectOfType<RCC_Camera>())
			{
				RCC_Camera rCC_Camera = UnityEngine.Object.FindObjectOfType<RCC_Camera>();
				rCC_Camera.ChangeCamera(RCC_Camera.CameraMode.ORBIT);
				rCC_Camera.useOnlyWhenHold = true;
			}
			return;
		}
		SetSmokeParticle(car, state: false);
		SetExhaustFlame(car, state: false);
		car.canControl = true;
		if ((bool)UnityEngine.Object.FindObjectOfType<RCC_Camera>())
		{
			UnityEngine.Object.FindObjectOfType<RCC_Camera>().ChangeCamera(RCC_Camera.CameraMode.TPS);
			UnityEngine.Object.FindObjectOfType<RCC_Camera>().useOnlyWhenHold = false;
		}
	}

	public static void UpdateRCC(RCC_CarControllerV3 car)
	{
		car.sleepingRigid = false;
	}

	public static void SetSmokeParticle(RCC_CarControllerV3 car, bool state)
	{
		car.PreviewSmokeParticle(state);
	}

	public static void SetSmokeColor(RCC_CarControllerV3 car, int indexOfGroundMaterial, Color color)
	{
		RCC_WheelCollider[] componentsInChildren = car.GetComponentsInChildren<RCC_WheelCollider>();
		RCC_WheelCollider[] array = componentsInChildren;
		foreach (RCC_WheelCollider rCC_WheelCollider in array)
		{
			for (int j = 0; j < rCC_WheelCollider.allWheelParticles.Count; j++)
			{
				rCC_WheelCollider.allWheelParticles[j].startColor = color;
			}
		}
	}

	public static void SetHeadlightsColor(RCC_CarControllerV3 car, Color color)
	{
		RCC_Light[] componentsInChildren = car.GetComponentsInChildren<RCC_Light>();
		car.lowBeamHeadLightsOn = true;
		RCC_Light[] array = componentsInChildren;
		foreach (RCC_Light rCC_Light in array)
		{
			if (rCC_Light.lightType == RCC_Light.LightType.HeadLight)
			{
				rCC_Light.GetComponent<Light>().color = color;
			}
		}
	}

	public static void SetExhaustFlame(RCC_CarControllerV3 car, bool state)
	{
		RCC_Exhaust[] componentsInChildren = car.GetComponentsInChildren<RCC_Exhaust>();
		RCC_Exhaust[] array = componentsInChildren;
		foreach (RCC_Exhaust rCC_Exhaust in array)
		{
			rCC_Exhaust.previewFlames = state;
		}
	}

	public static void SetFrontCambers(RCC_CarControllerV3 car, float camberAngle)
	{
		RCC_WheelCollider[] componentsInChildren = car.GetComponentsInChildren<RCC_WheelCollider>();
		RCC_WheelCollider[] array = componentsInChildren;
		foreach (RCC_WheelCollider rCC_WheelCollider in array)
		{
			if (rCC_WheelCollider == car.FrontLeftWheelCollider || rCC_WheelCollider == car.FrontRightWheelCollider)
			{
				rCC_WheelCollider.camber = camberAngle;
			}
		}
		UpdateRCC(car);
	}

	public static void SetRearCambers(RCC_CarControllerV3 car, float camberAngle)
	{
		RCC_WheelCollider[] componentsInChildren = car.GetComponentsInChildren<RCC_WheelCollider>();
		RCC_WheelCollider[] array = componentsInChildren;
		foreach (RCC_WheelCollider rCC_WheelCollider in array)
		{
			if (rCC_WheelCollider != car.FrontLeftWheelCollider && rCC_WheelCollider != car.FrontRightWheelCollider)
			{
				rCC_WheelCollider.camber = camberAngle;
			}
		}
		UpdateRCC(car);
	}

	public static void ChangeWheels(RCC_CarControllerV3 car, GameObject wheel)
	{
		for (int i = 0; i < car.allWheelColliders.Length; i++)
		{
			if ((bool)car.allWheelColliders[i].wheelModel.GetComponent<MeshRenderer>())
			{
				car.allWheelColliders[i].wheelModel.GetComponent<MeshRenderer>().enabled = false;
			}
			IEnumerator enumerator = car.allWheelColliders[i].wheelModel.GetComponentInChildren<Transform>().GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform = (Transform)enumerator.Current;
					transform.gameObject.SetActive(value: false);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = enumerator as IDisposable) != null)
				{
					disposable.Dispose();
				}
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(wheel, car.allWheelColliders[i].wheelModel.position, car.allWheelColliders[i].wheelModel.rotation, car.allWheelColliders[i].wheelModel);
			if (car.allWheelColliders[i].wheelModel.localPosition.x > 0f)
			{
				gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
			}
		}
		UpdateRCC(car);
	}

	public static void SetFrontSuspensionsTargetPos(RCC_CarControllerV3 car, float targetPosition)
	{
		JointSpring suspensionSpring = car.FrontLeftWheelCollider.wheelCollider.suspensionSpring;
		suspensionSpring.targetPosition = 1f - targetPosition;
		car.FrontLeftWheelCollider.wheelCollider.suspensionSpring = suspensionSpring;
		JointSpring suspensionSpring2 = car.FrontRightWheelCollider.wheelCollider.suspensionSpring;
		suspensionSpring2.targetPosition = 1f - targetPosition;
		car.FrontRightWheelCollider.wheelCollider.suspensionSpring = suspensionSpring2;
		UpdateRCC(car);
	}

	public static void SetRearSuspensionsTargetPos(RCC_CarControllerV3 car, float targetPosition)
	{
		JointSpring suspensionSpring = car.RearLeftWheelCollider.wheelCollider.suspensionSpring;
		suspensionSpring.targetPosition = 1f - targetPosition;
		car.RearLeftWheelCollider.wheelCollider.suspensionSpring = suspensionSpring;
		JointSpring suspensionSpring2 = car.RearRightWheelCollider.wheelCollider.suspensionSpring;
		suspensionSpring2.targetPosition = 1f - targetPosition;
		car.RearRightWheelCollider.wheelCollider.suspensionSpring = suspensionSpring2;
		UpdateRCC(car);
	}

	public static void SetFrontSuspensionsDistances(RCC_CarControllerV3 car, float distance)
	{
		if (distance <= 0f)
		{
			distance = 0.05f;
		}
		car.FrontLeftWheelCollider.wheelCollider.suspensionDistance = distance;
		car.FrontRightWheelCollider.wheelCollider.suspensionDistance = distance;
		UpdateRCC(car);
	}

	public static void SetRearSuspensionsDistances(RCC_CarControllerV3 car, float distance)
	{
		if (distance <= 0f)
		{
			distance = 0.05f;
		}
		car.RearLeftWheelCollider.wheelCollider.suspensionDistance = distance;
		car.RearRightWheelCollider.wheelCollider.suspensionDistance = distance;
		if (car.ExtraRearWheelsCollider != null && car.ExtraRearWheelsCollider.Length > 0)
		{
			RCC_WheelCollider[] extraRearWheelsCollider = car.ExtraRearWheelsCollider;
			foreach (RCC_WheelCollider rCC_WheelCollider in extraRearWheelsCollider)
			{
				rCC_WheelCollider.wheelCollider.suspensionDistance = distance;
			}
		}
		UpdateRCC(car);
	}

	public static void SetDrivetrainMode(RCC_CarControllerV3 car, RCC_CarControllerV3.WheelType mode)
	{
		car._wheelTypeChoise = mode;
		UpdateRCC(car);
	}

	public static void SetGearShiftingThreshold(RCC_CarControllerV3 car, float targetValue)
	{
		car.gearShiftingThreshold = targetValue;
		UpdateRCC(car);
	}

	public static void SetClutchThreshold(RCC_CarControllerV3 car, float targetValue)
	{
		car.clutchInertia = targetValue;
		UpdateRCC(car);
	}

	public static void SetSteeringSensitivity(RCC_CarControllerV3 car, bool state)
	{
		car.steerAngleSensitivityAdjuster = state;
		UpdateRCC(car);
	}

	public static void SetCounterSteering(RCC_CarControllerV3 car, bool state)
	{
		car.applyCounterSteering = state;
		UpdateRCC(car);
	}

	public static void SetNOS(RCC_CarControllerV3 car, bool state)
	{
		car.useNOS = state;
		UpdateRCC(car);
	}

	public static void SetTurbo(RCC_CarControllerV3 car, bool state)
	{
		car.useTurbo = state;
		UpdateRCC(car);
	}

	public static void SetUseExhaustFlame(RCC_CarControllerV3 car, bool state)
	{
		car.useExhaustFlame = state;
		UpdateRCC(car);
	}

	public static void SetRevLimiter(RCC_CarControllerV3 car, bool state)
	{
		car.useRevLimiter = state;
		UpdateRCC(car);
	}

	public static void SetClutchMargin(RCC_CarControllerV3 car, bool state)
	{
		car.useClutchMarginAtFirstGear = state;
		UpdateRCC(car);
	}

	public static void SetFrontSuspensionsSpringForce(RCC_CarControllerV3 car, float targetValue)
	{
		JointSpring suspensionSpring = car.FrontLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring;
		suspensionSpring.spring = targetValue;
		car.FrontLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring = suspensionSpring;
		car.FrontRightWheelCollider.GetComponent<WheelCollider>().suspensionSpring = suspensionSpring;
		UpdateRCC(car);
	}

	public static void SetRearSuspensionsSpringForce(RCC_CarControllerV3 car, float targetValue)
	{
		JointSpring suspensionSpring = car.RearLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring;
		suspensionSpring.spring = targetValue;
		car.RearLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring = suspensionSpring;
		car.RearRightWheelCollider.GetComponent<WheelCollider>().suspensionSpring = suspensionSpring;
		UpdateRCC(car);
	}

	public static void SetFrontSuspensionsSpringDamper(RCC_CarControllerV3 car, float targetValue)
	{
		JointSpring suspensionSpring = car.FrontLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring;
		suspensionSpring.damper = targetValue;
		car.FrontLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring = suspensionSpring;
		car.FrontRightWheelCollider.GetComponent<WheelCollider>().suspensionSpring = suspensionSpring;
		UpdateRCC(car);
	}

	public static void SetRearSuspensionsSpringDamper(RCC_CarControllerV3 car, float targetValue)
	{
		JointSpring suspensionSpring = car.RearLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring;
		suspensionSpring.damper = targetValue;
		car.RearLeftWheelCollider.GetComponent<WheelCollider>().suspensionSpring = suspensionSpring;
		car.RearRightWheelCollider.GetComponent<WheelCollider>().suspensionSpring = suspensionSpring;
		UpdateRCC(car);
	}

	public static void SetMaximumSpeed(RCC_CarControllerV3 car, float targetValue)
	{
		car.maxspeed = Mathf.Clamp(targetValue, 10f, 300f);
		UpdateRCC(car);
	}

	public static void SetMaximumTorque(RCC_CarControllerV3 car, float targetValue)
	{
		car.engineTorque = Mathf.Clamp(targetValue, 500f, 50000f);
		UpdateRCC(car);
	}

	public static void SetMaximumBrake(RCC_CarControllerV3 car, float targetValue)
	{
		car.brakeTorque = Mathf.Clamp(targetValue, 0f, 50000f);
		UpdateRCC(car);
	}

	public static void RepairCar(RCC_CarControllerV3 car)
	{
		car.repairNow = true;
	}

	public static void SetESP(RCC_CarControllerV3 car, bool state)
	{
		car.ESP = state;
	}

	public static void SetABS(RCC_CarControllerV3 car, bool state)
	{
		car.ABS = state;
	}

	public static void SetTCS(RCC_CarControllerV3 car, bool state)
	{
		car.TCS = state;
	}

	public static void SetSH(RCC_CarControllerV3 car, bool state)
	{
		car.steeringHelper = state;
	}

	public static void SetSHStrength(RCC_CarControllerV3 car, float value)
	{
		car.steeringHelper = true;
		car.steerHelperLinearVelStrength = value;
		car.steerHelperAngularVelStrength = value;
	}

	public static void SetCameraOrbitOnDrag(BaseEventData data)
	{
		RCC_Camera rCC_Camera = UnityEngine.Object.FindObjectOfType<RCC_Camera>();
		if ((bool)rCC_Camera)
		{
			if (rCC_Camera.cameraMode != RCC_Camera.CameraMode.ORBIT)
			{
				rCC_Camera.ChangeCamera(RCC_Camera.CameraMode.ORBIT);
			}
			rCC_Camera.useOnlyWhenHold = true;
			rCC_Camera.OnDrag(data);
		}
	}

	public static void SetTransmission(bool automatic)
	{
		RCC_Settings.Instance.useAutomaticGear = automatic;
	}

	public static void SaveStats(RCC_CarControllerV3 car)
	{
		PlayerPrefs.SetFloat(car.transform.name + "_FrontCamber", car.FrontLeftWheelCollider.camber);
		PlayerPrefs.SetFloat(car.transform.name + "_RearCamber", car.RearLeftWheelCollider.camber);
		PlayerPrefs.SetFloat(car.transform.name + "_FrontSuspensionsDistance", car.FrontLeftWheelCollider.wheelCollider.suspensionDistance);
		PlayerPrefs.SetFloat(car.transform.name + "_RearSuspensionsDistance", car.RearLeftWheelCollider.wheelCollider.suspensionDistance);
		PlayerPrefs.SetFloat(car.transform.name + "_FrontSuspensionsSpring", car.FrontLeftWheelCollider.wheelCollider.suspensionSpring.spring);
		PlayerPrefs.SetFloat(car.transform.name + "_RearSuspensionsSpring", car.RearLeftWheelCollider.wheelCollider.suspensionSpring.spring);
		PlayerPrefs.SetFloat(car.transform.name + "_FrontSuspensionsDamper", car.FrontLeftWheelCollider.wheelCollider.suspensionSpring.damper);
		PlayerPrefs.SetFloat(car.transform.name + "_RearSuspensionsDamper", car.RearLeftWheelCollider.wheelCollider.suspensionSpring.damper);
		PlayerPrefs.SetFloat(car.transform.name + "_MaximumSpeed", car.maxspeed);
		PlayerPrefs.SetFloat(car.transform.name + "_MaximumBrake", car.brakeTorque);
		PlayerPrefs.SetFloat(car.transform.name + "_MaximumTorque", car.engineTorque);
		PlayerPrefs.SetString(car.transform.name + "_DrivetrainMode", car._wheelTypeChoise.ToString());
		PlayerPrefs.SetFloat(car.transform.name + "_GearShiftingThreshold", car.gearShiftingThreshold);
		PlayerPrefs.SetFloat(car.transform.name + "_ClutchingThreshold", car.clutchInertia);
		PlayerPrefsX.SetBool(car.transform.name + "_CounterSteering", car.applyCounterSteering);
		RCC_Light[] componentsInChildren = car.GetComponentsInChildren<RCC_Light>();
		foreach (RCC_Light rCC_Light in componentsInChildren)
		{
			if (rCC_Light.lightType == RCC_Light.LightType.HeadLight)
			{
				PlayerPrefsX.SetColor(car.transform.name + "_HeadlightsColor", rCC_Light.GetComponentInChildren<Light>().color);
				break;
			}
		}
		PlayerPrefsX.SetColor(car.transform.name + "_WheelsSmokeColor", car.RearLeftWheelCollider.allWheelParticles[0].startColor);
		PlayerPrefsX.SetBool(car.transform.name + "_ABS", car.ABS);
		PlayerPrefsX.SetBool(car.transform.name + "_ESP", car.ESP);
		PlayerPrefsX.SetBool(car.transform.name + "_TCS", car.TCS);
		PlayerPrefsX.SetBool(car.transform.name + "_SH", car.steeringHelper);
		PlayerPrefsX.SetBool(car.transform.name + "NOS", car.useNOS);
		PlayerPrefsX.SetBool(car.transform.name + "Turbo", car.useTurbo);
		PlayerPrefsX.SetBool(car.transform.name + "ExhaustFlame", car.useExhaustFlame);
		PlayerPrefsX.SetBool(car.transform.name + "SteeringSensitivity", car.steerAngleSensitivityAdjuster);
		PlayerPrefsX.SetBool(car.transform.name + "RevLimiter", car.useRevLimiter);
		PlayerPrefsX.SetBool(car.transform.name + "ClutchMargin", car.useClutchMarginAtFirstGear);
	}

	public static void LoadStats(RCC_CarControllerV3 car)
	{
		SetFrontCambers(car, PlayerPrefs.GetFloat(car.transform.name + "_FrontCamber", car.FrontLeftWheelCollider.camber));
		SetRearCambers(car, PlayerPrefs.GetFloat(car.transform.name + "_RearCamber", car.RearLeftWheelCollider.camber));
		SetFrontSuspensionsDistances(car, PlayerPrefs.GetFloat(car.transform.name + "_FrontSuspensionsDistance", car.FrontLeftWheelCollider.wheelCollider.suspensionDistance));
		SetRearSuspensionsDistances(car, PlayerPrefs.GetFloat(car.transform.name + "_RearSuspensionsDistance", car.RearLeftWheelCollider.wheelCollider.suspensionDistance));
		SetFrontSuspensionsSpringForce(car, PlayerPrefs.GetFloat(car.transform.name + "_FrontSuspensionsSpring", car.FrontLeftWheelCollider.wheelCollider.suspensionSpring.spring));
		SetRearSuspensionsSpringForce(car, PlayerPrefs.GetFloat(car.transform.name + "_RearSuspensionsSpring", car.RearLeftWheelCollider.wheelCollider.suspensionSpring.spring));
		SetFrontSuspensionsSpringDamper(car, PlayerPrefs.GetFloat(car.transform.name + "_FrontSuspensionsDamper", car.FrontLeftWheelCollider.wheelCollider.suspensionSpring.damper));
		SetRearSuspensionsSpringDamper(car, PlayerPrefs.GetFloat(car.transform.name + "_RearSuspensionsDamper", car.RearLeftWheelCollider.wheelCollider.suspensionSpring.damper));
		SetMaximumSpeed(car, PlayerPrefs.GetFloat(car.transform.name + "_MaximumSpeed", car.maxspeed));
		SetMaximumBrake(car, PlayerPrefs.GetFloat(car.transform.name + "_MaximumBrake", car.brakeTorque));
		SetMaximumTorque(car, PlayerPrefs.GetFloat(car.transform.name + "_MaximumTorque", car.engineTorque));
		switch (PlayerPrefs.GetString(car.transform.name + "_DrivetrainMode", car._wheelTypeChoise.ToString()))
		{
		case "FWD":
			car._wheelTypeChoise = RCC_CarControllerV3.WheelType.FWD;
			break;
		case "RWD":
			car._wheelTypeChoise = RCC_CarControllerV3.WheelType.RWD;
			break;
		case "AWD":
			car._wheelTypeChoise = RCC_CarControllerV3.WheelType.AWD;
			break;
		}
		SetGearShiftingThreshold(car, PlayerPrefs.GetFloat(car.transform.name + "_GearShiftingThreshold", car.gearShiftingThreshold));
		SetClutchThreshold(car, PlayerPrefs.GetFloat(car.transform.name + "_ClutchingThreshold", car.clutchInertia));
		SetCounterSteering(car, PlayerPrefsX.GetBool(car.transform.name + "_CounterSteering", car.applyCounterSteering));
		SetABS(car, PlayerPrefsX.GetBool(car.transform.name + "_ABS", car.ABS));
		SetESP(car, PlayerPrefsX.GetBool(car.transform.name + "_ESP", car.ESP));
		SetTCS(car, PlayerPrefsX.GetBool(car.transform.name + "_TCS", car.TCS));
		SetSH(car, PlayerPrefsX.GetBool(car.transform.name + "_SH", car.steeringHelper));
		SetNOS(car, PlayerPrefsX.GetBool(car.transform.name + "NOS", car.useNOS));
		SetTurbo(car, PlayerPrefsX.GetBool(car.transform.name + "Turbo", car.useTurbo));
		SetUseExhaustFlame(car, PlayerPrefsX.GetBool(car.transform.name + "ExhaustFlame", car.useExhaustFlame));
		SetSteeringSensitivity(car, PlayerPrefsX.GetBool(car.transform.name + "SteeringSensitivity", car.steerAngleSensitivityAdjuster));
		SetRevLimiter(car, PlayerPrefsX.GetBool(car.transform.name + "RevLimiter", car.useRevLimiter));
		SetClutchMargin(car, PlayerPrefsX.GetBool(car.transform.name + "ClutchMargin", car.useClutchMarginAtFirstGear));
		if (PlayerPrefs.HasKey(car.transform.name + "_WheelsSmokeColor"))
		{
			SetSmokeColor(car, 0, PlayerPrefsX.GetColor(car.transform.name + "_WheelsSmokeColor"));
		}
		if (PlayerPrefs.HasKey(car.transform.name + "_HeadlightsColor"))
		{
			SetHeadlightsColor(car, PlayerPrefsX.GetColor(car.transform.name + "_HeadlightsColor"));
		}
		UpdateRCC(car);
	}

	public static void ResetStats(RCC_CarControllerV3 car, RCC_CarControllerV3 defaultCar)
	{
		SetFrontCambers(car, defaultCar.FrontLeftWheelCollider.camber);
		SetRearCambers(car, defaultCar.RearLeftWheelCollider.camber);
		SetFrontSuspensionsDistances(car, defaultCar.FrontLeftWheelCollider.wheelCollider.suspensionDistance);
		SetRearSuspensionsDistances(car, defaultCar.RearLeftWheelCollider.wheelCollider.suspensionDistance);
		SetFrontSuspensionsSpringForce(car, defaultCar.FrontLeftWheelCollider.wheelCollider.suspensionSpring.spring);
		SetRearSuspensionsSpringForce(car, defaultCar.RearLeftWheelCollider.wheelCollider.suspensionSpring.spring);
		SetFrontSuspensionsSpringDamper(car, defaultCar.FrontLeftWheelCollider.wheelCollider.suspensionSpring.damper);
		SetRearSuspensionsSpringDamper(car, defaultCar.RearLeftWheelCollider.wheelCollider.suspensionSpring.damper);
		SetMaximumSpeed(car, defaultCar.maxspeed);
		SetMaximumBrake(car, defaultCar.brakeTorque);
		SetMaximumTorque(car, defaultCar.engineTorque);
		switch (defaultCar._wheelTypeChoise.ToString())
		{
		case "FWD":
			car._wheelTypeChoise = RCC_CarControllerV3.WheelType.FWD;
			break;
		case "RWD":
			car._wheelTypeChoise = RCC_CarControllerV3.WheelType.RWD;
			break;
		case "AWD":
			car._wheelTypeChoise = RCC_CarControllerV3.WheelType.AWD;
			break;
		}
		SetGearShiftingThreshold(car, defaultCar.gearShiftingThreshold);
		SetClutchThreshold(car, defaultCar.clutchInertia);
		SetCounterSteering(car, defaultCar.applyCounterSteering);
		SetABS(car, defaultCar.ABS);
		SetESP(car, defaultCar.ESP);
		SetTCS(car, defaultCar.TCS);
		SetSH(car, defaultCar.steeringHelper);
		SetNOS(car, defaultCar.useNOS);
		SetTurbo(car, defaultCar.useTurbo);
		SetUseExhaustFlame(car, defaultCar.useExhaustFlame);
		SetSteeringSensitivity(car, defaultCar.steerAngleSensitivityAdjuster);
		SetRevLimiter(car, defaultCar.useRevLimiter);
		SetClutchMargin(car, defaultCar.useClutchMarginAtFirstGear);
		SetSmokeColor(car, 0, Color.white);
		SetHeadlightsColor(car, Color.white);
		SaveStats(car);
		UpdateRCC(car);
	}
}
