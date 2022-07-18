using UnityEngine;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/Dashboard Inputs")]
public class RCC_DashboardInputs : MonoBehaviour
{
	private RCC_Settings RCCSettingsInstance;

	public RCC_CarControllerV3 carController;

	public GameObject RPMNeedle;

	public GameObject KMHNeedle;

	public GameObject turboGauge;

	public GameObject NOSGauge;

	public GameObject BoostNeedle;

	public GameObject NoSNeedle;

	private float RPMNeedleRotation;

	private float KMHNeedleRotation;

	private float BoostNeedleRotation;

	private float NoSNeedleRotation;

	internal float RPM;

	internal float KMH;

	internal int direction = 1;

	internal float Gear;

	internal bool changingGear;

	internal bool NGear;

	internal bool ABS;

	internal bool ESP;

	internal bool Park;

	internal bool Headlights;

	internal RCC_CarControllerV3.IndicatorsOn indicators;

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

	private void OnEnable()
	{
		RCC_CarControllerV3.OnRCCPlayerSpawned += RCC_CarControllerV3_OnRCCSpawned;
	}

	private void RCC_CarControllerV3_OnRCCSpawned(RCC_CarControllerV3 RCC)
	{
		GetVehicle(RCC);
	}

	private void Update()
	{
		if (RCCSettings.uiType == RCC_Settings.UIType.None)
		{
			base.gameObject.SetActive(value: false);
			base.enabled = false;
		}
		else
		{
			GetValues();
		}
	}

	public void GetVehicle(RCC_CarControllerV3 rcc)
	{
		carController = rcc;
		RCC_UIDashboardButton[] array = Object.FindObjectsOfType<RCC_UIDashboardButton>();
		RCC_UIDashboardButton[] array2 = array;
		foreach (RCC_UIDashboardButton rCC_UIDashboardButton in array2)
		{
			rCC_UIDashboardButton.Check();
		}
	}

	private void GetValues()
	{
		if (!carController || !carController.canControl || carController.AIController)
		{
			return;
		}
		if ((bool)NOSGauge)
		{
			if (carController.useNOS)
			{
				if (!NOSGauge.activeSelf)
				{
					NOSGauge.SetActive(value: true);
				}
			}
			else if (NOSGauge.activeSelf)
			{
				NOSGauge.SetActive(value: false);
			}
		}
		if ((bool)turboGauge)
		{
			if (carController.useTurbo)
			{
				if (!turboGauge.activeSelf)
				{
					turboGauge.SetActive(value: true);
				}
			}
			else if (turboGauge.activeSelf)
			{
				turboGauge.SetActive(value: false);
			}
		}
		RPM = carController.engineRPM;
		KMH = carController.speed;
		direction = carController.direction;
		Gear = carController.currentGear;
		changingGear = carController.changingGear;
		NGear = carController.NGear;
		ABS = carController.ABSAct;
		ESP = carController.ESPAct;
		Park = ((carController.handbrakeInput > 0.1f) ? true : false);
		Headlights = carController.lowBeamHeadLightsOn || carController.highBeamHeadLightsOn;
		indicators = carController.indicatorsOn;
		if ((bool)RPMNeedle)
		{
			RPMNeedleRotation = carController.engineRPM / 50f;
			RPMNeedle.transform.eulerAngles = new Vector3(RPMNeedle.transform.eulerAngles.x, RPMNeedle.transform.eulerAngles.y, 0f - RPMNeedleRotation);
		}
		if ((bool)KMHNeedle)
		{
			if (RCCSettings.units == RCC_Settings.Units.KMH)
			{
				KMHNeedleRotation = carController.speed;
			}
			else
			{
				KMHNeedleRotation = carController.speed * 0.62f;
			}
			KMHNeedle.transform.eulerAngles = new Vector3(KMHNeedle.transform.eulerAngles.x, KMHNeedle.transform.eulerAngles.y, 0f - KMHNeedleRotation);
		}
		if ((bool)BoostNeedle)
		{
			BoostNeedleRotation = carController.turboBoost / 30f * 270f;
			BoostNeedle.transform.eulerAngles = new Vector3(BoostNeedle.transform.eulerAngles.x, BoostNeedle.transform.eulerAngles.y, 0f - BoostNeedleRotation);
		}
		if ((bool)NoSNeedle)
		{
			NoSNeedleRotation = carController.NoS / 100f * 270f;
			NoSNeedle.transform.eulerAngles = new Vector3(NoSNeedle.transform.eulerAngles.x, NoSNeedle.transform.eulerAngles.y, 0f - NoSNeedleRotation);
		}
	}

	private void OnDisable()
	{
		RCC_CarControllerV3.OnRCCPlayerSpawned -= RCC_CarControllerV3_OnRCCSpawned;
	}
}
