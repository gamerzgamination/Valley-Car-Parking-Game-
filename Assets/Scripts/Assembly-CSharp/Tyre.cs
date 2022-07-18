using UnityEngine;

public class Tyre : MonoBehaviour
{
	public static Tyre Ty;

	private void Start()
	{
		Ty = this;
	}

	private void OnTriggerStay(Collider coll)
	{
		if (coll.gameObject.tag == "ptha")
		{
			Gmanager.gm.player.GetComponent<RCC_CarControllerV3>().engineTorque = 600f;
		}
	}

	private void OnTriggerExit(Collider coll)
	{
		if (coll.gameObject.tag == "ptha")
		{
			Gmanager.gm.player.GetComponent<RCC_CarControllerV3>().engineTorque = 150f;
		}
	}
}
