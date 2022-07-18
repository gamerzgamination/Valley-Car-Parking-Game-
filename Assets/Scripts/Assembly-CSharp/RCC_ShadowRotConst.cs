using UnityEngine;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Misc/Shadow Rotation Const")]
public class RCC_ShadowRotConst : MonoBehaviour
{
	private Transform root;

	private void Start()
	{
		root = GetComponentInParent<RCC_CarControllerV3>().transform;
	}

	private void Update()
	{
		base.transform.rotation = Quaternion.Euler(90f, root.eulerAngles.y, 0f);
	}
}
