using UnityEngine;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Camera/Auto Camera Config")]
public class RCC_CameraConfig : MonoBehaviour
{
	public bool automatic = true;

	private Bounds combinedBounds;

	public float distance = 10f;

	public float height = 5f;
	public Transform CameraTarget;
	public static RCC_CameraConfig ins;

	private void Awake()
	{
		ins = this;
		if (automatic)
		{
			Quaternion rotation = base.transform.rotation;
			base.transform.rotation = Quaternion.identity;
			distance = MaxBoundsExtent(base.transform) * 1.2f;
			height = MaxBoundsExtent(base.transform) * 0.5f;
			if (height < 1f)
			{
				height = 1f;
			}
			base.transform.rotation = rotation;
		}
		//if (MainMenu.current_mode == 1)
		//{
		//	height = 5.5f;
		//	distance = 1.5f;
		//}
		//else if (MainMenu.current_mode == 2)
		//{
		//	height = 7.5f;
		//	distance = 0.27f;
		//}
	}

	public void SetCameraSettings()
	{
		RCC_Camera rCC_Camera = Object.FindObjectOfType<RCC_Camera>();
		if ((bool)rCC_Camera)
		{
			rCC_Camera.TPSDistance = distance;
			rCC_Camera.TPSHeight = height;
		}
	}

	public static float MaxBoundsExtent(Transform obj)
	{
		Renderer[] componentsInChildren = obj.GetComponentsInChildren<Renderer>();
		Bounds bounds = default(Bounds);
		bool flag = false;
		Renderer[] array = componentsInChildren;
		foreach (Renderer renderer in array)
		{
			if (!(renderer is TrailRenderer)  && !(renderer is ParticleSystemRenderer))
			{
				if (!flag)
				{
					flag = true;
					bounds = renderer.bounds;
				}
				else
				{
					bounds.Encapsulate(renderer.bounds);
				}
			}
		}
		return Mathf.Max(bounds.extents.x, bounds.extents.y, bounds.extents.z);
	}
}
