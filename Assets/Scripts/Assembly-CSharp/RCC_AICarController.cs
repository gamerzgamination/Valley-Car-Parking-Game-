using UnityEngine;
using UnityEngine.AI;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/AI/AI Controller")]
public class RCC_AICarController : MonoBehaviour
{
	public enum AIType
	{
		FollowWaypoints,
		ChasePlayer
	}

	private RCC_CarControllerV3 carController;

	private Rigidbody rigid;

	public RCC_AIWaypointsContainer waypointsContainer;

	public int currentWaypoint;

	public Transform targetChase;

	public AIType _AIType;

	public LayerMask obstacleLayers = -1;

	public int wideRayLength = 20;

	public int tightRayLength = 20;

	public int sideRayLength = 3;

	private float rayInput;

	private bool raycasting;

	private float resetTime;

	private float steerInput;

	private float gasInput;

	private float brakeInput;

	public bool limitSpeed;

	public float maximumSpeed = 100f;

	public bool smoothedSteer = true;

	private float maximumSpeedInBrakeZone;

	private bool inBrakeZone;

	public int lap;

	public int totalWaypointPassed;

	public int nextWaypointPassRadius = 40;

	public bool ignoreWaypointNow;

	private NavMeshAgent navigator;

	private GameObject navigatorObject;

	private void Awake()
	{
		carController = GetComponent<RCC_CarControllerV3>();
		carController.AIController = true;
		rigid = GetComponent<Rigidbody>();
		if (!waypointsContainer)
		{
			waypointsContainer = Object.FindObjectOfType(typeof(RCC_AIWaypointsContainer)) as RCC_AIWaypointsContainer;
		}
		navigatorObject = new GameObject("Navigator");
		navigatorObject.transform.parent = base.transform;
		navigatorObject.transform.localPosition = Vector3.zero;
		navigatorObject.AddComponent<NavMeshAgent>();
		navigator = navigatorObject.GetComponent<NavMeshAgent>();
		navigator.radius = 1f;
		navigator.speed = 1f;
		navigator.angularSpeed = 1000f;
		navigator.height = 1f;
		navigator.avoidancePriority = 50;
	}

	private void Update()
	{
		navigator.transform.localPosition = new Vector3(0f, carController.FrontLeftWheelCollider.transform.localPosition.y, carController.FrontLeftWheelCollider.transform.localPosition.z);
	}

	private void FixedUpdate()
	{
		if (carController.canControl)
		{
			Navigation();
			FixedRaycasts();
			FeedRCC();
			Resetting();
		}
	}

	private void Navigation()
	{
		if (_AIType == AIType.FollowWaypoints && !waypointsContainer)
		{
			Debug.LogError("Waypoints Container Couldn't Found!");
			base.enabled = false;
			return;
		}
		if (_AIType == AIType.FollowWaypoints && (bool)waypointsContainer && waypointsContainer.waypoints.Count < 1)
		{
			Debug.LogError("Waypoints Container Doesn't Have Any Waypoints!");
			base.enabled = false;
			return;
		}
		if (_AIType == AIType.ChasePlayer && !targetChase)
		{
			Debug.LogError("Target Chase Couldn't Found!");
			base.enabled = false;
			return;
		}
		Vector3 vector = base.transform.InverseTransformPoint(new Vector3(waypointsContainer.waypoints[currentWaypoint].position.x, base.transform.position.y, waypointsContainer.waypoints[currentWaypoint].position.z));
		float num = Mathf.Clamp(base.transform.InverseTransformDirection(navigator.desiredVelocity).x * 1.5f, -1f, 1f);
		if (_AIType == AIType.FollowWaypoints)
		{
			if (navigator.isOnNavMesh)
			{
				navigator.SetDestination(waypointsContainer.waypoints[currentWaypoint].position);
			}
		}
		else if (navigator.isOnNavMesh)
		{
			navigator.SetDestination(targetChase.position);
		}
		if (carController.direction == 1)
		{
			if (!ignoreWaypointNow)
			{
				steerInput = Mathf.Clamp(num + rayInput, -1f, 1f);
			}
			else
			{
				steerInput = Mathf.Clamp(rayInput, -1f, 1f);
			}
		}
		else
		{
			steerInput = Mathf.Clamp(0f - num - rayInput, -1f, 1f);
		}
		if (!inBrakeZone)
		{
			if (carController.speed >= 25f)
			{
				brakeInput = Mathf.Lerp(0f, 0.85f, Mathf.Abs(steerInput));
			}
			else
			{
				brakeInput = 0f;
			}
		}
		else
		{
			brakeInput = Mathf.Lerp(0f, 1f, (carController.speed - maximumSpeedInBrakeZone) / maximumSpeedInBrakeZone);
		}
		if (!inBrakeZone)
		{
			if (carController.speed >= 10f)
			{
				if (!carController.changingGear)
				{
					gasInput = Mathf.Clamp(1f - (Mathf.Abs(num / 10f) - Mathf.Abs(rayInput / 10f)), 0.75f, 1f);
				}
				else
				{
					gasInput = 0f;
				}
			}
			else if (!carController.changingGear)
			{
				gasInput = 1f;
			}
			else
			{
				gasInput = 0f;
			}
		}
		else if (!carController.changingGear)
		{
			gasInput = Mathf.Lerp(1f, 0f, carController.speed / maximumSpeedInBrakeZone);
		}
		else
		{
			gasInput = 0f;
		}
		if (_AIType == AIType.FollowWaypoints && vector.magnitude < (float)nextWaypointPassRadius)
		{
			currentWaypoint++;
			totalWaypointPassed++;
			if (currentWaypoint >= waypointsContainer.waypoints.Count)
			{
				currentWaypoint = 0;
				lap++;
			}
		}
	}

	private void Resetting()
	{
		if (carController.speed <= 5f && base.transform.InverseTransformDirection(rigid.velocity).z < 1f)
		{
			resetTime += Time.deltaTime;
		}
		if (resetTime >= 2f)
		{
			carController.direction = -1;
		}
		if (resetTime >= 4f || carController.speed >= 25f)
		{
			carController.direction = 1;
			resetTime = 0f;
		}
	}

	private void FixedRaycasts()
	{
		Vector3 vector = base.transform.TransformDirection(new Vector3(0f, 0f, 1f));
		Vector3 vector2 = new Vector3(base.transform.localPosition.x, carController.FrontLeftWheelCollider.transform.position.y, base.transform.localPosition.z);
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		bool flag6 = false;
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		Debug.DrawRay(vector2, Quaternion.AngleAxis(25f, base.transform.up) * vector * wideRayLength, Color.white);
		Debug.DrawRay(vector2, Quaternion.AngleAxis(-25f, base.transform.up) * vector * wideRayLength, Color.white);
		Debug.DrawRay(vector2, Quaternion.AngleAxis(7f, base.transform.up) * vector * tightRayLength, Color.white);
		Debug.DrawRay(vector2, Quaternion.AngleAxis(-7f, base.transform.up) * vector * tightRayLength, Color.white);
		Debug.DrawRay(vector2, Quaternion.AngleAxis(90f, base.transform.up) * vector * sideRayLength, Color.white);
		Debug.DrawRay(vector2, Quaternion.AngleAxis(-90f, base.transform.up) * vector * sideRayLength, Color.white);
		if (Physics.Raycast(vector2, Quaternion.AngleAxis(25f, base.transform.up) * vector, out var hitInfo, wideRayLength, obstacleLayers) && !hitInfo.collider.isTrigger && hitInfo.transform.root != base.transform)
		{
			Debug.DrawRay(vector2, Quaternion.AngleAxis(25f, base.transform.up) * vector * wideRayLength, Color.red);
			num = Mathf.Lerp(-0.5f, 0f, hitInfo.distance / (float)wideRayLength);
			flag2 = true;
		}
		else
		{
			num = 0f;
			flag2 = false;
		}
		if (Physics.Raycast(vector2, Quaternion.AngleAxis(-25f, base.transform.up) * vector, out hitInfo, wideRayLength, obstacleLayers) && !hitInfo.collider.isTrigger && hitInfo.transform.root != base.transform)
		{
			Debug.DrawRay(vector2, Quaternion.AngleAxis(-25f, base.transform.up) * vector * wideRayLength, Color.red);
			num4 = Mathf.Lerp(0.5f, 0f, hitInfo.distance / (float)wideRayLength);
			flag5 = true;
		}
		else
		{
			num4 = 0f;
			flag5 = false;
		}
		if (Physics.Raycast(vector2, Quaternion.AngleAxis(7f, base.transform.up) * vector, out hitInfo, tightRayLength, obstacleLayers) && !hitInfo.collider.isTrigger && hitInfo.transform.root != base.transform)
		{
			Debug.DrawRay(vector2, Quaternion.AngleAxis(7f, base.transform.up) * vector * tightRayLength, Color.red);
			num3 = Mathf.Lerp(-1f, 0f, hitInfo.distance / (float)tightRayLength);
			flag = true;
		}
		else
		{
			num3 = 0f;
			flag = false;
		}
		if (Physics.Raycast(vector2, Quaternion.AngleAxis(-7f, base.transform.up) * vector, out hitInfo, tightRayLength, obstacleLayers) && !hitInfo.collider.isTrigger && hitInfo.transform.root != base.transform)
		{
			Debug.DrawRay(vector2, Quaternion.AngleAxis(-7f, base.transform.up) * vector * tightRayLength, Color.red);
			num2 = Mathf.Lerp(1f, 0f, hitInfo.distance / (float)tightRayLength);
			flag4 = true;
		}
		else
		{
			num2 = 0f;
			flag4 = false;
		}
		if (Physics.Raycast(vector2, Quaternion.AngleAxis(90f, base.transform.up) * vector, out hitInfo, sideRayLength, obstacleLayers) && !hitInfo.collider.isTrigger && hitInfo.transform.root != base.transform)
		{
			Debug.DrawRay(vector2, Quaternion.AngleAxis(90f, base.transform.up) * vector * sideRayLength, Color.red);
			num5 = Mathf.Lerp(-1f, 0f, hitInfo.distance / (float)sideRayLength);
			flag3 = true;
		}
		else
		{
			num5 = 0f;
			flag3 = false;
		}
		if (Physics.Raycast(vector2, Quaternion.AngleAxis(-90f, base.transform.up) * vector, out hitInfo, sideRayLength, obstacleLayers) && !hitInfo.collider.isTrigger && hitInfo.transform.root != base.transform)
		{
			Debug.DrawRay(vector2, Quaternion.AngleAxis(-90f, base.transform.up) * vector * sideRayLength, Color.red);
			num6 = Mathf.Lerp(1f, 0f, hitInfo.distance / (float)sideRayLength);
			flag6 = true;
		}
		else
		{
			num6 = 0f;
			flag6 = false;
		}
		if (flag2 || flag5 || flag || flag4 || flag3 || flag6)
		{
			raycasting = true;
		}
		else
		{
			raycasting = false;
		}
		if (raycasting)
		{
			rayInput = num + num2 + num3 + num4 + num5 + num6;
		}
		else
		{
			rayInput = 0f;
		}
		if (raycasting && Mathf.Abs(rayInput) > 0.5f)
		{
			ignoreWaypointNow = true;
		}
		else
		{
			ignoreWaypointNow = false;
		}
	}

	private void FeedRCC()
	{
		if (carController.direction == 1)
		{
			if (!limitSpeed)
			{
				carController.gasInput = gasInput;
			}
			else
			{
				carController.gasInput = gasInput * Mathf.Clamp01(Mathf.Lerp(10f, 0f, carController.speed / maximumSpeed));
			}
		}
		else
		{
			carController.gasInput = 0f;
		}
		if (smoothedSteer)
		{
			carController.steerInput = Mathf.Lerp(carController.steerInput, steerInput, Time.deltaTime * 20f);
		}
		else
		{
			carController.steerInput = steerInput;
		}
		if (carController.direction == 1)
		{
			carController.brakeInput = brakeInput;
		}
		else
		{
			carController.brakeInput = gasInput;
		}
	}

	private void OnTriggerEnter(Collider col)
	{
		if ((bool)col.gameObject.GetComponent<RCC_AIBrakeZone>())
		{
			inBrakeZone = true;
			maximumSpeedInBrakeZone = col.gameObject.GetComponent<RCC_AIBrakeZone>().targetSpeed;
		}
	}

	private void OnTriggerExit(Collider col)
	{
		if ((bool)col.gameObject.GetComponent<RCC_AIBrakeZone>())
		{
			inBrakeZone = false;
			maximumSpeedInBrakeZone = 0f;
		}
	}
}
