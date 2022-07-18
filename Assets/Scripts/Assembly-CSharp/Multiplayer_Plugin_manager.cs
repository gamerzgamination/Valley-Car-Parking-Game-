using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Multiplayer_Plugin_manager : MonoBehaviour
{
	public RawImage icon_image;

	public Text name_text;

	public static Multiplayer_Plugin_manager instance;

	public Text status_text;

	public GameObject signInButtonText;

	private void OnEnable()
	{
		try
		{
		}
		catch (Exception)
		{
		}
	}

	public void SignInCallback(bool success)
	{
		try
		{
			if (success)
			{
				name_text.text = Social.localUser.userName.ToString();
				PlayerPrefs.SetString("Name", name_text.text);
				StartCoroutine(UpdateImg());
			}
		}
		catch (Exception)
		{
		}
	}

	private IEnumerator UpdateImg()
	{
		while (icon_image.texture == null)
		{
			try
			{
				icon_image.texture = Social.localUser.image;
				icon_image.gameObject.GetComponent<RawImage>().color = new Color(255f, 255f, 255f, 255f);
			}
			catch (Exception)
			{
				yield break;
			}
			yield return new WaitForSeconds(0.25f);
		}
	}
}
