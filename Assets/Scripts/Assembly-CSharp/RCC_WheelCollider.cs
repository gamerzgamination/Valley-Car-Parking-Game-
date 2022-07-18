using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Main/Wheel Collider")]
[RequireComponent(typeof(WheelCollider))]
public class RCC_WheelCollider : MonoBehaviour
{
	private RCC_Settings RCCSettingsInstance;

	private RCC_GroundMaterials RCCGroundMaterialsInstance;

	private WheelCollider _wheelCollider;

	private RCC_CarControllerV3 carController;

	private Rigidbody rigid;

	private List<RCC_WheelCollider> allWheelColliders = new List<RCC_WheelCollider>();

	public Transform wheelModel;

	private float wheelRotation;

	public float camber;

	internal float wheelRPMToSpeed;

	private RCC_Skidmarks skidmarks;

	private float startSlipValue = 0.25f;

	private int lastSkidmark = -1;

	private float wheelSlipAmountForward;

	private float wheelSlipAmountSideways;

	internal float totalSlip;

	private float orgForwardStiffness = 1f;

	private float orgSidewaysStiffness = 1f;

	public WheelFrictionCurve forwardFrictionCurve;

	public WheelFrictionCurve sidewaysFrictionCurve;

	private AudioSource audioSource;

	private AudioClip audioClip;

	internal List<ParticleSystem> allWheelParticles = new List<ParticleSystem>();

	internal ParticleSystem.EmissionModule emission;

	internal float tractionHelpedSidewaysStiffness = 1f;

	private float minForwardStiffness = 0.75f;

	private float maxForwardStiffness = 1f;

	private float minSidewaysStiffness = 0.75f;

	private float maxSidewaysStiffness = 1f;

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

	private RCC_GroundMaterials RCCGroundMaterials
	{
		get
		{
			if (RCCGroundMaterialsInstance == null)
			{
				RCCGroundMaterialsInstance = RCC_GroundMaterials.Instance;
			}
			return RCCGroundMaterialsInstance;
		}
	}

	public WheelCollider wheelCollider
	{
		get
		{
			if (_wheelCollider == null)
			{
				_wheelCollider = GetComponent<WheelCollider>();
			}
			return _wheelCollider;
		}
		set
		{
			_wheelCollider = value;
		}
	}

	private RCC_GroundMaterials physicsMaterials => RCCGroundMaterials;

	private RCC_GroundMaterials.GroundMaterialFrictions[] physicsFrictions => RCCGroundMaterials.frictions;

	private void Awake()
	{
		carController = GetComponentInParent<RCC_CarControllerV3>();
		rigid = carController.GetComponent<Rigidbody>();
		if (!RCCSettings.dontUseSkidmarks)
		{
			if ((bool)Object.FindObjectOfType(typeof(RCC_Skidmarks)))
			{
				skidmarks = Object.FindObjectOfType(typeof(RCC_Skidmarks)) as RCC_Skidmarks;
			}
			else
			{
				skidmarks = Object.Instantiate(RCCSettings.skidmarksManager, Vector3.zero, Quaternion.identity);
			}
		}
		wheelCollider.mass = rigid.mass / 15f;
		forwardFrictionCurve = wheelCollider.forwardFriction;
		sidewaysFrictionCurve = wheelCollider.sidewaysFriction;
		switch (RCCSettings.behaviorType)
		{
		case RCC_Settings.BehaviorType.SemiArcade:
			forwardFrictionCurve = SetFrictionCurves(forwardFrictionCurve, 0.2f, 2f, 2f, 2f);
			sidewaysFrictionCurve = SetFrictionCurves(sidewaysFrictionCurve, 0.25f, 2f, 2f, 2f);
			wheelCollider.forceAppPointDistance = Mathf.Clamp(wheelCollider.forceAppPointDistance, 0.35f, 1f);
			break;
		case RCC_Settings.BehaviorType.Drift:
			forwardFrictionCurve = SetFrictionCurves(forwardFrictionCurve, 0.25f, 1f, 0.8f, 0.5f);
			sidewaysFrictionCurve = SetFrictionCurves(sidewaysFrictionCurve, 0.4f, 1f, 0.5f, 0.75f);
			wheelCollider.forceAppPointDistance = Mathf.Clamp(wheelCollider.forceAppPointDistance, 0.1f, 1f);
			if (carController._wheelTypeChoise == RCC_CarControllerV3.WheelType.FWD)
			{
				Debug.LogError("Current behavior mode is ''Drift'', but your vehicle named " + carController.name + " was FWD. You have to use RWD, AWD, or BIASED to rear wheels. Setting it to *RWD* now. ");
				carController._wheelTypeChoise = RCC_CarControllerV3.WheelType.RWD;
			}
			break;
		case RCC_Settings.BehaviorType.Fun:
			forwardFrictionCurve = SetFrictionCurves(forwardFrictionCurve, 0.2f, 2f, 2f, 2f);
			sidewaysFrictionCurve = SetFrictionCurves(sidewaysFrictionCurve, 0.25f, 2f, 2f, 2f);
			wheelCollider.forceAppPointDistance = Mathf.Clamp(wheelCollider.forceAppPointDistance, 0.75f, 2f);
			break;
		case RCC_Settings.BehaviorType.Racing:
			forwardFrictionCurve = SetFrictionCurves(forwardFrictionCurve, 0.2f, 1f, 0.8f, 0.75f);
			sidewaysFrictionCurve = SetFrictionCurves(sidewaysFrictionCurve, 0.3f, 1f, 0.25f, 0.75f);
			wheelCollider.forceAppPointDistance = Mathf.Clamp(wheelCollider.forceAppPointDistance, 0.25f, 1f);
			break;
		case RCC_Settings.BehaviorType.Simulator:
			forwardFrictionCurve = SetFrictionCurves(forwardFrictionCurve, 0.2f, 1f, 0.8f, 0.75f);
			sidewaysFrictionCurve = SetFrictionCurves(sidewaysFrictionCurve, 0.25f, 1f, 0.5f, 0.75f);
			wheelCollider.forceAppPointDistance = Mathf.Clamp(wheelCollider.forceAppPointDistance, 0.1f, 1f);
			break;
		}
		orgForwardStiffness = forwardFrictionCurve.stiffness;
		orgSidewaysStiffness = sidewaysFrictionCurve.stiffness;
		wheelCollider.forwardFriction = forwardFrictionCurve;
		wheelCollider.sidewaysFriction = sidewaysFrictionCurve;
		if (RCCSettings.useSharedAudioSources)
		{
			if (!carController.transform.Find("All Audio Sources/Skid Sound AudioSource"))
			{
				audioSource = RCC_CreateAudioSource.NewAudioSource(carController.gameObject, "Skid Sound AudioSource", 5f, 50f, 0f, audioClip, loop: true, playNow: true, destroyAfterFinished: false);
			}
			else
			{
				audioSource = carController.transform.Find("All Audio Sources/Skid Sound AudioSource").GetComponent<AudioSource>();
			}
		}
		else
		{
			audioSource = RCC_CreateAudioSource.NewAudioSource(carController.gameObject, "Skid Sound AudioSource", 5f, 50f, 0f, audioClip, loop: true, playNow: true, destroyAfterFinished: false);
			audioSource.transform.position = base.transform.position;
		}
		if (!RCCSettings.dontUseAnyParticleEffects)
		{
			for (int i = 0; i < RCCGroundMaterials.frictions.Length; i++)
			{
				GameObject gameObject = Object.Instantiate(RCCGroundMaterials.frictions[i].groundParticles, base.transform.position, base.transform.rotation);
				emission = gameObject.GetComponent<ParticleSystem>().emission;
				emission.enabled = false;
				gameObject.transform.SetParent(base.transform, worldPositionStays: false);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				allWheelParticles.Add(gameObject.GetComponent<ParticleSystem>());
			}
		}
	}

	private void Start()
	{
		allWheelColliders = carController.allWheelColliders.ToList();
		allWheelColliders.Remove(this);
	}

	private WheelFrictionCurve SetFrictionCurves(WheelFrictionCurve curve, float extremumSlip, float extremumValue, float asymptoteSlip, float asymptoteValue)
	{
		WheelFrictionCurve result = curve;
		result.extremumSlip = extremumSlip;
		result.extremumValue = extremumValue;
		result.asymptoteSlip = asymptoteSlip;
		result.asymptoteValue = asymptoteValue;
		return result;
	}

	private void Update()
	{
		if (!carController.enabled)
		{
			return;
		}
		if (!carController.sleepingRigid)
		{
			WheelAlign();
			WheelCamber();
		}
		switch (carController._wheelTypeChoise)
		{
		case RCC_CarControllerV3.WheelType.FWD:
			if (this == carController.FrontLeftWheelCollider || this == carController.FrontRightWheelCollider)
			{
				ApplyMotorTorque(carController.engineTorque);
			}
			break;
		case RCC_CarControllerV3.WheelType.RWD:
			if (this == carController.RearLeftWheelCollider || this == carController.RearRightWheelCollider)
			{
				ApplyMotorTorque(carController.engineTorque);
			}
			break;
		case RCC_CarControllerV3.WheelType.AWD:
			ApplyMotorTorque(carController.engineTorque / 2f);
			break;
		case RCC_CarControllerV3.WheelType.BIASED:
			if (this == carController.FrontLeftWheelCollider || this == carController.FrontRightWheelCollider)
			{
				ApplyMotorTorque(carController.engineTorque * (100f - carController.biasedWheelTorque) / 100f);
			}
			if (this == carController.RearLeftWheelCollider || this == carController.RearRightWheelCollider)
			{
				ApplyMotorTorque(carController.engineTorque * carController.biasedWheelTorque / 100f);
			}
			break;
		}
		if (carController.ExtraRearWheelsCollider.Length > 0 && carController.applyEngineTorqueToExtraRearWheelColliders)
		{
			for (int i = 0; i < carController.ExtraRearWheelsCollider.Length; i++)
			{
				if (this == carController.ExtraRearWheelsCollider[i])
				{
					ApplyMotorTorque(carController.engineTorque);
				}
			}
		}
		if (this == carController.FrontLeftWheelCollider || this == carController.FrontRightWheelCollider)
		{
			ApplySteering();
		}
		if (carController.handbrakeInput > 0.1f)
		{
			if (this == carController.RearLeftWheelCollider || this == carController.RearRightWheelCollider)
			{
				ApplyBrakeTorque(carController.brakeTorque * 1.5f * carController.handbrakeInput);
			}
		}
		else if (this == carController.FrontLeftWheelCollider || this == carController.FrontRightWheelCollider)
		{
			ApplyBrakeTorque(carController.brakeTorque * Mathf.Clamp(carController._brakeInput, 0f, 1f));
		}
		else
		{
			ApplyBrakeTorque(carController.brakeTorque * (Mathf.Clamp(carController._brakeInput, 0f, 1f) / 2f));
		}
		if (!carController.ESP)
		{
			return;
		}
		if (carController.underSteering)
		{
			if (this == carController.RearLeftWheelCollider)
			{
				ApplyBrakeTorque(carController.brakeTorque * carController.ESPStrength * Mathf.Clamp(0f - carController.frontSlip, 0f, float.PositiveInfinity));
			}
			if (this == carController.RearRightWheelCollider)
			{
				ApplyBrakeTorque(carController.brakeTorque * carController.ESPStrength * Mathf.Clamp(carController.frontSlip, 0f, float.PositiveInfinity));
			}
		}
		if (carController.overSteering)
		{
			if (this == carController.FrontLeftWheelCollider)
			{
				ApplyBrakeTorque(carController.brakeTorque * carController.ESPStrength * Mathf.Clamp(0f - carController.rearSlip, 0f, float.PositiveInfinity));
			}
			if (this == carController.FrontRightWheelCollider)
			{
				ApplyBrakeTorque(carController.brakeTorque * carController.ESPStrength * Mathf.Clamp(carController.rearSlip, 0f, float.PositiveInfinity));
			}
		}
	}

	private void FixedUpdate()
	{
		if (carController.enabled)
		{
			wheelRPMToSpeed = wheelCollider.rpm * wheelCollider.radius / 2.9f * rigid.transform.lossyScale.y;
			SkidMarks();
			Frictions();
			Audio();
		}
	}

	public void WheelAlign()
	{
		if (!wheelModel)
		{
			Debug.LogError(base.transform.name + " wheel of the " + carController.transform.name + " is missing wheel model. This wheel is disabled");
			base.enabled = false;
			return;
		}
		Vector3 vector = wheelCollider.transform.TransformPoint(wheelCollider.center);
		wheelCollider.GetGroundHit(out var hit);
		if (Physics.Raycast(vector, -wheelCollider.transform.up, out var hitInfo, (wheelCollider.suspensionDistance + wheelCollider.radius) * base.transform.localScale.y) && !hitInfo.transform.IsChildOf(carController.transform) && !hitInfo.collider.isTrigger)
		{
			wheelModel.transform.position = hitInfo.point + wheelCollider.transform.up * wheelCollider.radius * base.transform.localScale.y;
			float num = (0f - wheelCollider.transform.InverseTransformPoint(hit.point).y - wheelCollider.radius) / wheelCollider.suspensionDistance;
			Debug.DrawLine(hit.point, hit.point + wheelCollider.transform.up * (hit.force / rigid.mass), (!((double)num <= 0.0)) ? Color.white : Color.magenta);
			Debug.DrawLine(hit.point, hit.point - wheelCollider.transform.forward * hit.forwardSlip * 2f, Color.green);
			Debug.DrawLine(hit.point, hit.point - wheelCollider.transform.right * hit.sidewaysSlip * 2f, Color.red);
		}
		else
		{
			wheelModel.transform.position = vector - wheelCollider.transform.up * wheelCollider.suspensionDistance * base.transform.localScale.y;
		}
		wheelRotation += wheelCollider.rpm * 6f * Time.deltaTime;
		wheelModel.transform.rotation = wheelCollider.transform.rotation * Quaternion.Euler(wheelRotation, wheelCollider.steerAngle, wheelCollider.transform.rotation.z);
	}

	public void WheelCamber()
	{
		Vector3 euler = ((!(wheelCollider.transform.localPosition.x < 0f)) ? new Vector3(wheelCollider.transform.localEulerAngles.x, wheelCollider.transform.localEulerAngles.y, camber) : new Vector3(wheelCollider.transform.localEulerAngles.x, wheelCollider.transform.localEulerAngles.y, 0f - camber));
		Quaternion localRotation = Quaternion.Euler(euler);
		wheelCollider.transform.localRotation = localRotation;
	}

	private void SkidMarks()
	{
		wheelCollider.GetGroundHit(out var hit);
		wheelSlipAmountSideways = Mathf.Abs(hit.sidewaysSlip);
		wheelSlipAmountForward = Mathf.Abs(hit.forwardSlip);
		totalSlip = wheelSlipAmountSideways + wheelSlipAmountForward / 2f;
		if (!skidmarks)
		{
			return;
		}
		if (wheelSlipAmountSideways > startSlipValue || wheelSlipAmountForward > startSlipValue * 2f)
		{
			Vector3 pos = hit.point + 2f * rigid.velocity * Time.deltaTime;
			if (rigid.velocity.magnitude > 1f)
			{
				lastSkidmark = skidmarks.AddSkidMark(pos, hit.normal, wheelSlipAmountSideways / 2f + wheelSlipAmountForward / 2f, lastSkidmark);
			}
			else
			{
				lastSkidmark = -1;
			}
		}
		else
		{
			lastSkidmark = -1;
		}
	}

	private void Frictions()
	{
		wheelCollider.GetGroundHit(out var hit);
		bool flag = false;
		for (int i = 0; i < physicsFrictions.Length; i++)
		{
			if (!(hit.point != Vector3.zero) || !(hit.collider.sharedMaterial == physicsFrictions[i].groundMaterial))
			{
				continue;
			}
			flag = true;
			forwardFrictionCurve.stiffness = physicsFrictions[i].forwardStiffness;
			sidewaysFrictionCurve.stiffness = physicsFrictions[i].sidewaysStiffness * tractionHelpedSidewaysStiffness;
			if (RCCSettings.behaviorType == RCC_Settings.BehaviorType.Drift)
			{
				Drift();
			}
			wheelCollider.forwardFriction = forwardFrictionCurve;
			wheelCollider.sidewaysFriction = sidewaysFrictionCurve;
			wheelCollider.wheelDampingRate = physicsFrictions[i].damp;
			if (!RCCSettings.dontUseAnyParticleEffects)
			{
				emission = allWheelParticles[i].emission;
			}
			audioClip = physicsFrictions[i].groundSound;
			if (!RCCSettings.dontUseAnyParticleEffects)
			{
				if (wheelSlipAmountSideways > physicsFrictions[i].slip || wheelSlipAmountForward > physicsFrictions[i].slip)
				{
					emission.enabled = true;
				}
				else
				{
					emission.enabled = false;
				}
			}
		}
		if (!flag && physicsMaterials.useTerrainSplatMapForGroundFrictions)
		{
			for (int j = 0; j < physicsMaterials.terrainSplatMapIndex.Length; j++)
			{
				if (!(hit.point != Vector3.zero) || !(hit.collider.sharedMaterial == physicsMaterials.terrainPhysicMaterial) || TerrainSurface.GetTextureMix(base.transform.position) == null || !(TerrainSurface.GetTextureMix(base.transform.position)[j] > 0.5f))
				{
					continue;
				}
				flag = true;
				forwardFrictionCurve.stiffness = physicsFrictions[physicsMaterials.terrainSplatMapIndex[j]].forwardStiffness;
				sidewaysFrictionCurve.stiffness = physicsFrictions[physicsMaterials.terrainSplatMapIndex[j]].sidewaysStiffness * tractionHelpedSidewaysStiffness;
				if (RCCSettings.behaviorType == RCC_Settings.BehaviorType.Drift)
				{
					Drift();
				}
				wheelCollider.forwardFriction = forwardFrictionCurve;
				wheelCollider.sidewaysFriction = sidewaysFrictionCurve;
				wheelCollider.wheelDampingRate = physicsFrictions[physicsMaterials.terrainSplatMapIndex[j]].damp;
				if (!RCCSettings.dontUseAnyParticleEffects)
				{
					emission = allWheelParticles[physicsMaterials.terrainSplatMapIndex[j]].emission;
				}
				audioClip = physicsFrictions[physicsMaterials.terrainSplatMapIndex[j]].groundSound;
				if (!RCCSettings.dontUseAnyParticleEffects)
				{
					if (wheelSlipAmountSideways > physicsFrictions[physicsMaterials.terrainSplatMapIndex[j]].slip || wheelSlipAmountForward > physicsFrictions[physicsMaterials.terrainSplatMapIndex[j]].slip)
					{
						emission.enabled = true;
					}
					else
					{
						emission.enabled = false;
					}
				}
			}
		}
		if (!flag)
		{
			forwardFrictionCurve.stiffness = orgForwardStiffness;
			sidewaysFrictionCurve.stiffness = orgSidewaysStiffness * tractionHelpedSidewaysStiffness;
			if (RCCSettings.behaviorType == RCC_Settings.BehaviorType.Drift)
			{
				Drift();
			}
			wheelCollider.forwardFriction = forwardFrictionCurve;
			wheelCollider.sidewaysFriction = sidewaysFrictionCurve;
			wheelCollider.wheelDampingRate = physicsFrictions[0].damp;
			if (!RCCSettings.dontUseAnyParticleEffects)
			{
				emission = allWheelParticles[0].emission;
			}
			audioClip = physicsFrictions[0].groundSound;
			if (!RCCSettings.dontUseAnyParticleEffects)
			{
				if (wheelSlipAmountSideways > physicsFrictions[0].slip || wheelSlipAmountForward > physicsFrictions[0].slip)
				{
					emission.enabled = true;
				}
				else
				{
					emission.enabled = false;
				}
			}
		}
		if (RCCSettings.dontUseAnyParticleEffects)
		{
			return;
		}
		for (int k = 0; k < allWheelParticles.Count; k++)
		{
			if (!(wheelSlipAmountSideways > startSlipValue) && !(wheelSlipAmountForward > startSlipValue))
			{
				emission = allWheelParticles[k].emission;
				emission.enabled = false;
			}
		}
	}

	private void Drift()
	{
		Vector3 vector = base.transform.InverseTransformDirection(rigid.velocity);
		float num = vector.x * vector.x / 100f;
		if (wheelCollider == carController.FrontLeftWheelCollider.wheelCollider || wheelCollider == carController.FrontRightWheelCollider.wheelCollider)
		{
			forwardFrictionCurve.extremumValue = Mathf.Clamp(1f - num, 0.1f, maxForwardStiffness);
			forwardFrictionCurve.asymptoteValue = Mathf.Clamp(0.75f - num / 2f, 0.1f, minForwardStiffness);
		}
		else
		{
			forwardFrictionCurve.extremumValue = Mathf.Clamp(1f - num, 0.75f, maxForwardStiffness);
			forwardFrictionCurve.asymptoteValue = Mathf.Clamp(0.75f - num / 2f, 0.75f, minForwardStiffness);
		}
		if (wheelCollider == carController.FrontLeftWheelCollider.wheelCollider || wheelCollider == carController.FrontRightWheelCollider.wheelCollider)
		{
			sidewaysFrictionCurve.extremumValue = Mathf.Clamp(1f - num / 1f, 0.5f, maxSidewaysStiffness);
			sidewaysFrictionCurve.asymptoteValue = Mathf.Clamp(0.75f - num / 2f, 0.5f, minSidewaysStiffness);
		}
		else
		{
			sidewaysFrictionCurve.extremumValue = Mathf.Clamp(1f - num, 0.45f, maxSidewaysStiffness);
			sidewaysFrictionCurve.asymptoteValue = Mathf.Clamp(0.75f - num / 2f, 0.45f, minSidewaysStiffness);
		}
	}

	private void Audio()
	{
		if (RCCSettings.useSharedAudioSources && isSkidding())
		{
			return;
		}
		if (totalSlip > startSlipValue)
		{
			if (audioSource.clip != audioClip)
			{
				audioSource.clip = audioClip;
			}
			if (!audioSource.isPlaying)
			{
				audioSource.Play();
			}
			if (rigid.velocity.magnitude > 1f)
			{
				audioSource.volume = Mathf.Lerp(audioSource.volume, Mathf.Lerp(0f, 1f, totalSlip - startSlipValue), Time.deltaTime * 5f);
				audioSource.pitch = Mathf.Lerp(1f, 0.8f, audioSource.volume);
			}
			else
			{
				audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime * 5f);
			}
		}
		else
		{
			audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime * 5f);
			if (audioSource.volume <= 0.05f && audioSource.isPlaying)
			{
				audioSource.Stop();
			}
		}
	}

	private bool isSkidding()
	{
		for (int i = 0; i < allWheelColliders.Count; i++)
		{
			if (allWheelColliders[i].totalSlip > totalSlip)
			{
				return true;
			}
		}
		return false;
	}

	private void ApplyMotorTorque(float torque)
	{
		if (carController.TCS)
		{
			wheelCollider.GetGroundHit(out var hit);
			if (Mathf.Abs(wheelCollider.rpm) >= 100f)
			{
				if (hit.forwardSlip > 0.25f)
				{
					carController.TCSAct = true;
					torque -= Mathf.Clamp(torque * hit.forwardSlip * carController.TCSStrength, 0f, carController.engineTorque);
				}
				else
				{
					carController.TCSAct = false;
					torque += Mathf.Clamp(torque * hit.forwardSlip * carController.TCSStrength, 0f - carController.engineTorque, 0f);
				}
			}
			else
			{
				carController.TCSAct = false;
			}
		}
		if (OverTorque())
		{
			torque = 0f;
		}
		wheelCollider.motorTorque = torque * (1f - carController.clutchInput) * carController._boostInput * carController._gasInput * (carController.engineTorqueCurve[carController.currentGear].Evaluate(wheelRPMToSpeed * (float)carController.direction) * (float)carController.direction);
		carController.ApplyEngineSound(wheelCollider.motorTorque);
	}

	public void ApplySteering()
	{
		if (carController.applyCounterSteering && carController.currentGear != 0)
		{
			wheelCollider.steerAngle = Mathf.Clamp(carController.steerAngle * (carController._steerInput + carController.driftAngle), 0f - carController.steerAngle, carController.steerAngle);
		}
		else
		{
			wheelCollider.steerAngle = Mathf.Clamp(carController.steerAngle * carController._steerInput, 0f - carController.steerAngle, carController.steerAngle);
		}
	}

	private void ApplyBrakeTorque(float brake)
	{
		if (carController.ABS && carController.handbrakeInput <= 0.1f)
		{
			wheelCollider.GetGroundHit(out var hit);
			if (Mathf.Abs(hit.forwardSlip) * Mathf.Clamp01(brake) >= carController.ABSThreshold)
			{
				carController.ABSAct = true;
				brake = 0f;
			}
			else
			{
				carController.ABSAct = false;
			}
		}
		wheelCollider.brakeTorque = brake;
	}

	private bool OverTorque()
	{
		if (carController.speed > carController.maxspeed || !carController.engineRunning)
		{
			return true;
		}
		return false;
	}
}
