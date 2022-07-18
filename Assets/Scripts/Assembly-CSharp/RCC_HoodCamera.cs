using System.Collections;
using UnityEngine;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/Camera/Hood Camera")]
public class RCC_HoodCamera : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine("FixShakeDelayed");
	}

	public void FixShake()
	{
		StartCoroutine("FixShakeDelayed");
	}

	private IEnumerator FixShakeDelayed()
	{
		if (!GetComponent<Rigidbody>())
		{
			yield return null;
		}
		yield return new WaitForFixedUpdate();
		GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
		yield return new WaitForFixedUpdate();
		GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
	}
}
