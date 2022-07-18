using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Camera/Main Camera")]
public class RCC_Camera : MonoBehaviour
{
	public enum CameraMode
	{
		TPS,
		FPS,
		WHEEL,
		FIXED,
		CINEMATIC,
		TOP,
		ORBIT
	}

	private RCC_Settings RCCSettingsInstance;

	public Transform playerCar;

	private Rigidbody playerRigid;

	private Camera cam;

	public GameObject pivot;

	public CameraMode cameraMode;

	public bool useTopCameraMode;

	public bool useHoodCameraMode = true;

	public bool useWheelCameraMode = true;

	public bool useFixedCameraMode = true;

	public bool useOrbitCameraMode;

	public bool useCinematicCameraMode;

	public bool useOrthoForTopCamera = true;

	public Vector3 topCameraAngle = new Vector3(45f, 45f, 0f);

	private float distanceOffset;

	public float maximumZDistanceOffset = 10f;

	public float topCameraDistance = 100f;

	private Vector3 targetPosition;

	private Vector3 pastFollowerPosition = Vector3.zero;

	private Vector3 pastTargetPosition = Vector3.zero;

	public float TPSDistance = 6f;

	public float TPSHeight = 1.5f;

	public float TPSHeightDamping = 5f;

	public float TPSRotationDamping = 3f;

	internal float targetFieldOfView = 60f;

	public float TPSMinimumFOV = 50f;

	public float TPSMaximumFOV = 60f;

	public float hoodCameraFOV = 65f;

	public float wheelCameraFOV = 60f;

	public float minimumOrtSize = 10f;

	public float maximumOrtSize = 20f;

	public float orbitCameraFOV = 50f;

	public float maximumTilt = 15f;

	private float tiltAngle;

	internal int cameraSwitchCount;

	private RCC_HoodCamera hoodCam;

	private RCC_WheelCamera wheelCam;

	private RCC_FixedCamera fixedCam;

	private RCC_CinematicCamera cinematicCam;

	private float speed;

	private Vector3 collisionVector = Vector3.zero;

	private Vector3 collisionPos = Vector3.zero;

	private Quaternion collisionRot = Quaternion.identity;

	private float index;

	private float orbitX;

	private float orbitY;

	private float smoothedOrbitX;

	private float smoothedOrbitY;

	public bool useSmoothOrbit;

	public bool useOnlyWhenHold;

	public float minOrbitY = -20f;

	public float maxOrbitY = 80f;

	public float orbitXSpeed = 10f;

	public float orbitYSpeed = 10f;

	private Vector3 orbitPosition;

	private Quaternion orbitRotation = Quaternion.identity;

	private float orgTimeScale = 1f;

	public static RCC_Camera ins;

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

	public Transform _playerCar
	{
		get
		{
			return playerCar;
		}
		set
		{
			playerCar = value;
			GetPlayerCar();
		}
	}

	private void Awake()
	{
		ins = this;
		cam = GetComponentInChildren<Camera>();
		orgTimeScale = Time.timeScale;
		if (MainMenu.current_mode == 1)
		{
			TPSHeight = 5.5f;
			TPSDistance = 1.5f;
		}
		else if (MainMenu.current_mode == 2)
		{
			TPSHeight = 7.5f;
			TPSDistance = 0.27f;
		}
	}

	private void GetPlayerCar()
	{
		if ((bool)playerCar)
		{
			if ((bool)playerCar.GetComponent<RCC_CameraConfig>())
			{
				TPSDistance = playerCar.GetComponent<RCC_CameraConfig>().distance;
				TPSHeight = playerCar.GetComponent<RCC_CameraConfig>().height;
			}
			ChangeCamera(CameraMode.TPS);
			playerRigid = playerCar.GetComponent<Rigidbody>();
			hoodCam = playerCar.GetComponentInChildren<RCC_HoodCamera>();
			wheelCam = playerCar.GetComponentInChildren<RCC_WheelCamera>();
			fixedCam = Object.FindObjectOfType<RCC_FixedCamera>();
			cinematicCam = Object.FindObjectOfType<RCC_CinematicCamera>();
		}
	}

	public void SetPlayerCar(GameObject player)
	{
		playerCar = player.transform;
		GetPlayerCar();
	}

	private void Update()
	{
		if (!playerCar || !playerRigid)
		{
			GetPlayerCar();
			return;
		}
		speed = Mathf.Lerp(speed, playerCar.InverseTransformDirection(playerRigid.velocity).z * 3.6f, Time.deltaTime * 3f);
		if (index > 0f)
		{
			index -= Time.deltaTime * 5f;
		}
		cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFieldOfView, Time.deltaTime * 2f);
		if (Input.GetKey(RCCSettings.slowMotionKB))
		{
			Time.timeScale = 0.2f;
		}
		if (Input.GetKeyUp(RCCSettings.slowMotionKB))
		{
			Time.timeScale = orgTimeScale;
		}
	}

	private void LateUpdate()
	{
		if ((bool)playerCar && (bool)playerRigid && playerCar.gameObject.activeSelf)
		{
			switch (cameraMode)
			{
			case CameraMode.TPS:
				TPS();
				break;
			case CameraMode.FPS:
				FPS();
				break;
			case CameraMode.WHEEL:
				WHEEL();
				break;
			case CameraMode.FIXED:
				FIXED();
				break;
			case CameraMode.CINEMATIC:
				CINEMATIC();
				break;
			case CameraMode.TOP:
				TOP();
				break;
			case CameraMode.ORBIT:
				ORBIT();
				break;
			}
			if (Input.GetKeyDown(RCCSettings.changeCameraKB))
			{
				ChangeCamera();
			}
		}
	}

	public void ChangeCamera()
	{
		cameraSwitchCount++;
		if (cameraSwitchCount >= 7)
		{
			cameraSwitchCount = 0;
			cameraMode = CameraMode.TPS;
		}
		switch (cameraSwitchCount)
		{
		case 0:
			cameraMode = CameraMode.TPS;
			break;
		case 1:
			if (useHoodCameraMode && (bool)hoodCam)
			{
				cameraMode = CameraMode.FPS;
			}
			else
			{
				ChangeCamera();
			}
			break;
		case 2:
			if (useWheelCameraMode && (bool)wheelCam)
			{
				cameraMode = CameraMode.WHEEL;
			}
			else
			{
				ChangeCamera();
			}
			break;
		case 3:
			if (useFixedCameraMode && (bool)fixedCam)
			{
				cameraMode = CameraMode.FIXED;
			}
			else
			{
				ChangeCamera();
			}
			break;
		case 4:
			if (useCinematicCameraMode)
			{
				cameraMode = CameraMode.CINEMATIC;
			}
			else
			{
				ChangeCamera();
			}
			break;
		case 5:
			if (useTopCameraMode)
			{
				cameraMode = CameraMode.TOP;
			}
			else
			{
				ChangeCamera();
			}
			break;
		case 6:
			if (useOrbitCameraMode)
			{
				cameraMode = CameraMode.ORBIT;
			}
			else
			{
				ChangeCamera();
			}
			break;
		}
		ResetCamera();
	}

	public void ChangeCamera(CameraMode mode)
	{
		cameraMode = mode;
		ResetCamera();
	}

	private void FPS()
	{
	}

	private void WHEEL()
	{
	}

	private void TPS()
	{
		targetFieldOfView = Mathf.Lerp(TPSMinimumFOV, TPSMaximumFOV, Mathf.Abs(speed) / 150f);
		targetFieldOfView += 5f * Mathf.Cos(1f * index);
		tiltAngle = Mathf.Lerp(0f, maximumTilt * (float)(int)Mathf.Clamp(0f - playerCar.InverseTransformDirection(playerRigid.velocity).x, -1f, 1f), Mathf.Abs(playerCar.InverseTransformDirection(playerRigid.velocity).x) / 50f);
		float b = playerCar.eulerAngles.y;
		float b2 = playerCar.position.y + TPSHeight;
		float y = base.transform.eulerAngles.y;
		float y2 = base.transform.position.y;
		TPSRotationDamping = Mathf.Lerp(0f, 5f, Mathf.Abs(speed) / 40f);
		if (speed < -1f)
		{
			b = playerCar.eulerAngles.y + 180f;
		}
		y = Mathf.LerpAngle(y, b, TPSRotationDamping * Time.deltaTime);
		y2 = Mathf.Lerp(y2, b2, TPSHeightDamping * Time.deltaTime);
		Quaternion quaternion = Quaternion.Euler(0f, y, 0f);
		base.transform.position = playerCar.position;
		base.transform.position -= quaternion * Vector3.forward * TPSDistance;
		base.transform.position = new Vector3(base.transform.position.x, y2, base.transform.position.z);
		base.transform.LookAt(new Vector3(playerCar.position.x, playerCar.position.y + 1f, playerCar.position.z));
		base.transform.eulerAngles = new Vector3(base.transform.eulerAngles.x, base.transform.eulerAngles.y, Mathf.Clamp(tiltAngle, -10f, 10f));
		collisionPos = Vector3.Lerp(collisionPos, Vector3.zero, Time.deltaTime);
		if (Time.deltaTime != 0f)
		{
			collisionRot = Quaternion.Lerp(collisionRot, Quaternion.identity, Time.deltaTime);
		}
		pivot.transform.localPosition = Vector3.Lerp(pivot.transform.localPosition, collisionPos, Time.deltaTime);
		pivot.transform.localRotation = Quaternion.Lerp(pivot.transform.localRotation, collisionRot, Time.deltaTime);
	}

	private void FIXED()
	{
		if (fixedCam.transform.parent != null)
		{
			fixedCam.transform.SetParent(null);
		}
	}

	private void TOP()
	{
		if ((bool)playerCar && (bool)playerRigid)
		{
			targetPosition = playerCar.position;
			targetPosition += playerCar.rotation * Vector3.forward * distanceOffset;
			cam.orthographic = useOrthoForTopCamera;
			distanceOffset = Mathf.Lerp(0f, maximumZDistanceOffset, Mathf.Abs(speed) / 100f);
			targetFieldOfView = Mathf.Lerp(minimumOrtSize, maximumOrtSize, Mathf.Abs(speed) / 100f);
			cam.orthographicSize = targetFieldOfView;
			base.transform.position = SmoothApproach(pastFollowerPosition, pastTargetPosition, targetPosition, Mathf.Clamp(0.5f, Mathf.Abs(speed), float.PositiveInfinity));
			base.transform.rotation = Quaternion.Euler(topCameraAngle);
			pastFollowerPosition = base.transform.position;
			pastTargetPosition = targetPosition;
			pivot.transform.localPosition = new Vector3(0f, 0f, 0f - topCameraDistance);
		}
	}

	private void ORBIT()
	{
		if (!useOnlyWhenHold)
		{
			if (useSmoothOrbit)
			{
				smoothedOrbitX += Input.GetAxis("Mouse X") * (orbitXSpeed * 10f) * Time.deltaTime;
				smoothedOrbitY -= Input.GetAxis("Mouse Y") * (orbitYSpeed * 10f) * Time.deltaTime;
				smoothedOrbitY = Mathf.Clamp(smoothedOrbitY, minOrbitY, maxOrbitY);
				orbitX = Mathf.Lerp(orbitX, smoothedOrbitX, Time.deltaTime * 10f);
				orbitY = Mathf.Lerp(orbitY, smoothedOrbitY, Time.deltaTime * 10f);
			}
			else
			{
				orbitX += Input.GetAxis("Mouse X") * (orbitXSpeed * 10f) * Time.deltaTime;
				orbitY -= Input.GetAxis("Mouse Y") * (orbitYSpeed * 10f) * Time.deltaTime;
			}
		}
		orbitY = Mathf.Clamp(orbitY, minOrbitY, maxOrbitY);
		orbitRotation = Quaternion.Euler(orbitY, orbitX, 0f);
		orbitPosition = orbitRotation * new Vector3(0f, 0f, 0f - TPSDistance) + playerCar.position;
		base.transform.rotation = orbitRotation;
		base.transform.position = orbitPosition;
	}

	private void CINEMATIC()
	{
		if (cinematicCam.transform.parent != null)
		{
			cinematicCam.transform.SetParent(null);
		}
		targetFieldOfView = cinematicCam.targetFOV;
	}

	public void OnDrag(BaseEventData data)
	{
		PointerEventData pointerEventData = data as PointerEventData;
		orbitX += pointerEventData.delta.x * orbitXSpeed * 0.02f;
		orbitY -= pointerEventData.delta.y * orbitYSpeed * 0.02f;
		orbitY = Mathf.Clamp(orbitY, minOrbitY, maxOrbitY);
		orbitRotation = Quaternion.Euler(orbitY, orbitX, 0f);
		orbitPosition = orbitRotation * new Vector3(0f, 0f, 0f - TPSDistance) + playerCar.position;
	}

	public void Collision(Collision collision)
	{
		if (base.enabled && cameraMode == CameraMode.TPS)
		{
			Vector3 relativeVelocity = collision.relativeVelocity;
			relativeVelocity *= 1f - Mathf.Abs(Vector3.Dot(base.transform.up, collision.contacts[0].normal));
			float num = Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, relativeVelocity.normalized));
			if (relativeVelocity.magnitude * num >= 5f)
			{
				collisionVector = base.transform.InverseTransformDirection(relativeVelocity) / 30f;
				collisionPos -= collisionVector * 5f;
				collisionRot = Quaternion.Euler(new Vector3((0f - collisionVector.z) * 50f, (0f - collisionVector.y) * 50f, (0f - collisionVector.x) * 50f));
				targetFieldOfView = cam.fieldOfView - Mathf.Clamp(collision.relativeVelocity.magnitude, 0f, 15f);
				index = Mathf.Clamp(collision.relativeVelocity.magnitude / 5f, 0f, 10f);
			}
		}
	}

	private void ResetCamera()
	{
		if ((bool)fixedCam)
		{
			fixedCam.canTrackNow = false;
		}
		tiltAngle = 0f;
		collisionPos = Vector3.zero;
		collisionRot = Quaternion.identity;
		pivot.transform.localPosition = collisionPos;
		pivot.transform.localRotation = collisionRot;
		cam.orthographic = false;
		switch (cameraMode)
		{
		case CameraMode.TPS:
			base.transform.SetParent(null);
			targetFieldOfView = TPSMaximumFOV;
			break;
		case CameraMode.FPS:
			base.transform.SetParent(hoodCam.transform, worldPositionStays: false);
			base.transform.localPosition = Vector3.zero;
			base.transform.localRotation = Quaternion.identity;
			targetFieldOfView = hoodCameraFOV;
			hoodCam.FixShake();
			break;
		case CameraMode.WHEEL:
			base.transform.SetParent(wheelCam.transform, worldPositionStays: false);
			base.transform.localPosition = Vector3.zero;
			base.transform.localRotation = Quaternion.identity;
			targetFieldOfView = wheelCameraFOV;
			break;
		case CameraMode.FIXED:
			base.transform.SetParent(fixedCam.transform, worldPositionStays: false);
			base.transform.localPosition = Vector3.zero;
			base.transform.localRotation = Quaternion.identity;
			targetFieldOfView = 60f;
			fixedCam.currentCar = playerCar;
			fixedCam.canTrackNow = true;
			break;
		case CameraMode.CINEMATIC:
			base.transform.SetParent(cinematicCam.pivot.transform, worldPositionStays: false);
			base.transform.localPosition = Vector3.zero;
			base.transform.localRotation = Quaternion.identity;
			targetFieldOfView = 30f;
			cinematicCam.currentCar = playerCar;
			break;
		case CameraMode.TOP:
			base.transform.SetParent(null);
			targetFieldOfView = minimumOrtSize;
			pivot.transform.localPosition = Vector3.zero;
			pivot.transform.localRotation = Quaternion.identity;
			targetPosition = playerCar.position;
			targetPosition += playerCar.rotation * Vector3.forward * distanceOffset;
			base.transform.position = playerCar.position;
			pastFollowerPosition = playerCar.position;
			pastTargetPosition = targetPosition;
			break;
		case CameraMode.ORBIT:
			base.transform.SetParent(null);
			targetFieldOfView = orbitCameraFOV;
			orbitX = 0f;
			orbitY = 0f;
			smoothedOrbitX = 0f;
			smoothedOrbitY = 0f;
			break;
		}
	}

	private Vector3 SmoothApproach(Vector3 pastPosition, Vector3 pastTargetPosition, Vector3 targetPosition, float delta)
	{
		if (float.IsNaN(delta) || float.IsInfinity(delta) || pastPosition == Vector3.zero || pastTargetPosition == Vector3.zero || targetPosition == Vector3.zero)
		{
			return base.transform.position;
		}
		float num = Time.deltaTime * delta;
		Vector3 vector = (targetPosition - pastTargetPosition) / num;
		Vector3 vector2 = pastPosition - pastTargetPosition + vector;
		return targetPosition - vector + vector2 * Mathf.Exp(0f - num);
	}
}
