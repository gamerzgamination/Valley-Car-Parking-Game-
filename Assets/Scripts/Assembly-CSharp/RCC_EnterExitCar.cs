using System.Collections;
using UnityEngine;

public class RCC_EnterExitCar : MonoBehaviour
{
	private RCC_Settings RCCSettingsInstance;

	private RCC_CarControllerV3 carController;

	private GameObject carCamera;

	private GameObject player;

	private GameObject dashboard;

	public Transform getOutPosition;

	public bool isPlayerIn;

	private bool opened;

	private float waitTime = 1f;

	private bool temp;

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

	private void Awake()
	{
		carController = GetComponent<RCC_CarControllerV3>();
		carCamera = Object.FindObjectOfType<RCC_Camera>().gameObject;
		if ((bool)Object.FindObjectOfType<RCC_DashboardInputs>())
		{
			dashboard = Object.FindObjectOfType<RCC_DashboardInputs>().gameObject;
		}
		if (!getOutPosition)
		{
			GameObject gameObject = new GameObject("Get Out Position");
			gameObject.transform.SetParent(base.transform);
			gameObject.transform.localPosition = new Vector3(-1.5f, 0f, 0f);
			gameObject.transform.localRotation = Quaternion.identity;
			getOutPosition = gameObject.transform;
		}
	}

	private void Start()
	{
		if ((bool)dashboard)
		{
			StartCoroutine("DisableDashboard");
		}
	}

	private IEnumerator DisableDashboard()
	{
		yield return new WaitForEndOfFrame();
		dashboard.SetActive(value: false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(RCCSettings.enterExitVehicleKB) && opened && !temp)
		{
			GetOut();
			opened = false;
			temp = false;
		}
		if (!isPlayerIn)
		{
			carController.canControl = false;
		}
	}

	private IEnumerator Act(GameObject fpsplayer)
	{
		player = fpsplayer;
		if (!opened && !temp)
		{
			GetIn();
			opened = true;
			temp = true;
			yield return new WaitForSeconds(waitTime);
			temp = false;
		}
	}

	private void GetIn()
	{
		isPlayerIn = true;
		carCamera.SetActive(value: true);
		if ((bool)carCamera.GetComponent<RCC_Camera>())
		{
			carCamera.GetComponent<RCC_Camera>().cameraSwitchCount = 10;
			carCamera.GetComponent<RCC_Camera>().ChangeCamera();
		}
		carCamera.transform.GetComponent<RCC_Camera>().SetPlayerCar(base.gameObject);
		player.transform.SetParent(base.transform);
		player.transform.localPosition = Vector3.zero;
		player.transform.localRotation = Quaternion.identity;
		player.SetActive(value: false);
		carController.canControl = true;
		if ((bool)dashboard)
		{
			dashboard.SetActive(value: true);
			dashboard.GetComponent<RCC_DashboardInputs>().GetVehicle(carController);
		}
		if (!carController.engineRunning)
		{
			SendMessage("StartEngine");
		}
		Cursor.lockState = CursorLockMode.None;
	}

	private void GetOut()
	{
		isPlayerIn = false;
		player.transform.SetParent(null);
		player.transform.position = getOutPosition.position;
		player.transform.rotation = getOutPosition.rotation;
		player.transform.rotation = Quaternion.Euler(0f, player.transform.eulerAngles.y, 0f);
		carCamera.SetActive(value: false);
		player.SetActive(value: true);
		carController.canControl = false;
		if (!RCCSettings.keepEnginesAlive)
		{
			carController.engineRunning = false;
		}
		if ((bool)dashboard)
		{
			dashboard.SetActive(value: false);
		}
		Cursor.lockState = CursorLockMode.Locked;
	}
}
