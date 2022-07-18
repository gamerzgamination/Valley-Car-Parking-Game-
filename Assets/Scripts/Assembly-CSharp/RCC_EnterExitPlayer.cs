using UnityEngine;

public class RCC_EnterExitPlayer : MonoBehaviour
{
	public enum PlayerType
	{
		FPS,
		TPS
	}

	private RCC_Settings RCCSettingsInstance;

	public PlayerType playerType;

	public GameObject rootOfPlayer;

	public float maxRayDistance = 2f;

	public float rayHeight = 1f;

	public GameObject TPSCamera;

	private bool showGui;

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
		if (!rootOfPlayer)
		{
			rootOfPlayer = base.transform.root.gameObject;
		}
		GameObject gameObject = Object.FindObjectOfType<RCC_Camera>().gameObject;
		gameObject.SetActive(value: false);
	}

	private void Update()
	{
		Vector3 direction = base.transform.TransformDirection(Vector3.forward);
		if (Physics.Raycast(new Vector3(base.transform.position.x, base.transform.position.y + ((playerType != PlayerType.TPS) ? 0f : rayHeight), base.transform.position.z), direction, out var hitInfo, maxRayDistance))
		{
			if ((bool)hitInfo.transform.GetComponentInParent<RCC_EnterExitCar>())
			{
				showGui = true;
				if (Input.GetKeyDown(RCCSettings.enterExitVehicleKB))
				{
					hitInfo.transform.GetComponentInParent<RCC_CarControllerV3>().SendMessage("Act", rootOfPlayer, SendMessageOptions.DontRequireReceiver);
				}
			}
			else
			{
				showGui = false;
			}
		}
		else
		{
			showGui = false;
		}
	}

	private void OnGUI()
	{
		if (showGui && RCCSettings.controllerType == RCC_Settings.ControllerType.Keyboard)
		{
			GUI.Label(new Rect((float)Screen.width - (float)Screen.width / 1.7f, (float)Screen.height - (float)Screen.height / 1.2f, 800f, 100f), "Press ''" + RCCSettings.enterExitVehicleKB.ToString() + "'' key to Get In");
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay(new Vector3(base.transform.position.x, base.transform.position.y + ((playerType != PlayerType.TPS) ? 0f : rayHeight), base.transform.position.z), base.transform.forward * maxRayDistance);
	}

	private void OnEnable()
	{
		if ((bool)TPSCamera)
		{
			TPSCamera.SetActive(value: true);
		}
	}

	private void OnDisable()
	{
		if ((bool)TPSCamera)
		{
			TPSCamera.SetActive(value: false);
		}
	}
}
