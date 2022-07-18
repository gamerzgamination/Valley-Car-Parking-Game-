using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
	private bool isRewinding;

	public float recordTime = 5f;

	public List<PointInTime> pointsInTime;

	private Rigidbody rb;

	private void Start()
	{
		pointsInTime = new List<PointInTime>();
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (Gmanager.gm.forcam)
		{
			StartRewind();
		}
		if (!Gmanager.gm.forcam)
		{
			StopRewind();
		}
	}

	private void FixedUpdate()
	{
		if (isRewinding)
		{
			Rewind();
		}
		else
		{
			Record();
		}
	}

	private void Rewind()
	{
		if (pointsInTime.Count > 0)
		{
			PointInTime pointInTime = pointsInTime[5];
			base.transform.position = pointInTime.position;
			base.transform.rotation = pointInTime.rotation;
			pointsInTime.RemoveAt(0);
		}
		else
		{
			StopRewind();
		}
	}

	private void Record()
	{
		if ((float)pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
		{
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		}
		pointsInTime.Insert(0, new PointInTime(base.transform.position, base.transform.rotation));
	}

	public void StartRewind()
	{
		isRewinding = true;
		rb.isKinematic = true;
	}

	public void StopRewind()
	{
		isRewinding = false;
		rb.isKinematic = false;
	}
}
