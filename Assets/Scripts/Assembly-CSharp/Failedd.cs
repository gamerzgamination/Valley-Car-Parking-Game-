using UnityEngine;

public class Failedd : MonoBehaviour
{
	public bool isDestructable;

	public bool isfalseonly;

	public float DestroyAfter;

	private void OnEnable()
	{
		if (isfalseonly)
		{
			Invoke("OFF", DestroyAfter);
		}
	}

	public void OFF()
	{
		base.gameObject.SetActive(value: false);
		Gmanager.gm.new_fail();
	}
}
