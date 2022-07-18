using UnityEngine;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/Mobile/Mobile Buttons")]
public class RCC_MobileButtons : MonoBehaviour
{
	private RCC_Settings RCCSettingsInstance;

	public RCC_CarControllerV3[] carControllers;

	public RCC_UIController gasButton;

	public RCC_UIController brakeButton;

	public RCC_UIController leftButton;

	public RCC_UIController rightButton;

	public RCC_UISteeringWheelController steeringWheel;

	public RCC_UIController handbrakeButton;

	public RCC_UIController NOSButton;

	public GameObject gearButton;

	private float gasInput;

	private float brakeInput;

	private float leftInput;

	private float rightInput;

	private float steeringWheelInput;

	private float handbrakeInput;

	private float NOSInput = 1f;

	private float gyroInput;

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
		if (RCCSettings.controllerType != RCC_Settings.ControllerType.Mobile)
		{
			if ((bool)gasButton)
			{
				gasButton.gameObject.SetActive(value: false);
			}
			if ((bool)leftButton)
			{
				leftButton.gameObject.SetActive(value: false);
			}
			if ((bool)rightButton)
			{
				rightButton.gameObject.SetActive(value: false);
			}
			if ((bool)brakeButton)
			{
				brakeButton.gameObject.SetActive(value: false);
			}
			if ((bool)steeringWheel)
			{
				steeringWheel.gameObject.SetActive(value: false);
			}
			if ((bool)handbrakeButton)
			{
				handbrakeButton.gameObject.SetActive(value: false);
			}
			if ((bool)NOSButton)
			{
				NOSButton.gameObject.SetActive(value: false);
			}
			if ((bool)gearButton)
			{
				gearButton.gameObject.SetActive(value: false);
			}
			base.enabled = false;
		}
	}

	private void OnEnable()
	{
		RCC_CarControllerV3.OnRCCPlayerSpawned += RCC_CarControllerV3_OnRCCSpawned;
		GetVehicles();
	}

	private void RCC_CarControllerV3_OnRCCSpawned(RCC_CarControllerV3 RCC)
	{
		if (!RCC.AIController)
		{
			GetVehicles();
		}
	}

	public void GetVehicles()
	{
		carControllers = Object.FindObjectsOfType<RCC_CarControllerV3>();
	}

	private void Update()
	{
		if (RCCSettings.useSteeringWheelForSteering)
		{
			if (RCCSettings.useAccelerometerForSteering)
			{
				RCCSettings.useAccelerometerForSteering = false;
			}
			if (!steeringWheel.gameObject.activeInHierarchy)
			{
				steeringWheel.gameObject.SetActive(value: true);
			}
			if (leftButton.gameObject.activeInHierarchy)
			{
				leftButton.gameObject.SetActive(value: false);
			}
			if (rightButton.gameObject.activeInHierarchy)
			{
				rightButton.gameObject.SetActive(value: false);
			}
		}
		if (RCCSettings.useAccelerometerForSteering)
		{
			if (RCCSettings.useSteeringWheelForSteering)
			{
				RCCSettings.useSteeringWheelForSteering = false;
			}
			brakeButton.transform.position = leftButton.transform.position;
			if (steeringWheel.gameObject.activeInHierarchy)
			{
				steeringWheel.gameObject.SetActive(value: false);
			}
			if (leftButton.gameObject.activeInHierarchy)
			{
				leftButton.gameObject.SetActive(value: false);
			}
			if (rightButton.gameObject.activeInHierarchy)
			{
				rightButton.gameObject.SetActive(value: false);
			}
		}
		if (!RCCSettings.useAccelerometerForSteering && !RCCSettings.useSteeringWheelForSteering)
		{
			if ((bool)steeringWheel && steeringWheel.gameObject.activeInHierarchy)
			{
				steeringWheel.gameObject.SetActive(value: false);
			}
			if (!leftButton.gameObject.activeInHierarchy)
			{
				leftButton.gameObject.SetActive(value: true);
			}
			if (!rightButton.gameObject.activeInHierarchy)
			{
				rightButton.gameObject.SetActive(value: true);
			}
		}
		gasInput = GetInput(gasButton);
		brakeInput = GetInput(brakeButton);
		leftInput = GetInput(leftButton);
		rightInput = GetInput(rightButton);
		if ((bool)steeringWheel)
		{
			steeringWheelInput = steeringWheel.input;
		}
		if (RCCSettings.useAccelerometerForSteering)
		{
			gyroInput = Input.acceleration.x * RCCSettings.gyroSensitivity;
		}
		else
		{
			gyroInput = 0f;
		}
		handbrakeInput = GetInput(handbrakeButton);
		NOSInput = Mathf.Clamp(GetInput(NOSButton) * 2.5f, 1f, 2.5f);
		for (int i = 0; i < carControllers.Length; i++)
		{
			if (carControllers[i].canControl && !carControllers[i].AIController)
			{
				carControllers[i].gasInput = gasInput;
				carControllers[i].brakeInput = brakeInput;
				carControllers[i].steerInput = 0f - leftInput + rightInput + steeringWheelInput + gyroInput;
				carControllers[i].handbrakeInput = handbrakeInput;
				carControllers[i].boostInput = NOSInput;
			}
		}
	}

	private float GetInput(RCC_UIController button)
	{
		if (button == null)
		{
			return 0f;
		}
		return button.input;
	}

	public void ChangeCamera()
	{
		if ((bool)Object.FindObjectOfType<RCC_Camera>())
		{
			Object.FindObjectOfType<RCC_Camera>().ChangeCamera();
		}
	}

	public void ChangeController(int index)
	{
		switch (index)
		{
		case 0:
			RCCSettings.useAccelerometerForSteering = false;
			RCCSettings.useSteeringWheelForSteering = false;
			break;
		case 1:
			RCCSettings.useAccelerometerForSteering = true;
			RCCSettings.useSteeringWheelForSteering = false;
			break;
		case 2:
			RCCSettings.useAccelerometerForSteering = false;
			RCCSettings.useSteeringWheelForSteering = true;
			break;
		}
	}

	private void OnDisable()
	{
		RCC_CarControllerV3.OnRCCPlayerSpawned -= RCC_CarControllerV3_OnRCCSpawned;
	}
}
