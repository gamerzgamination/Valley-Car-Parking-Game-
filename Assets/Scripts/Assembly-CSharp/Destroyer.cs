using System;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
	public bool isDestructable;

	public bool isfalseonly;

	public float DestroyAfter;

	private void OnEnable()
	{
		if (isDestructable)
		{
			Invoke("Destruct", DestroyAfter);
		}
		if (isfalseonly)
		{
			Invoke("OFF", DestroyAfter);
		}
	}

	public void Destruct()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void OFF()
	{
		try
		{
			if (Application.loadedLevel == 2 && MainMenu.menu.free_coins_panel.activeSelf)
			{
				if (PlayerPrefs.GetInt("TwitterClicked") == 0)
				{
					MainMenu.menu.twitter_btn.interactable = true;
				}
				else
				{
					MainMenu.menu.twitter_btn.interactable = false;
				}
				if (PlayerPrefs.GetInt("FacebookClicked") == 0)
				{
					MainMenu.menu.fb_btn2.interactable = true;
				}
				else
				{
					MainMenu.menu.fb_btn2.interactable = false;
				}
				if (PlayerPrefs.GetInt("SHARECLICEKD") == 0)
				{
					MainMenu.menu.share_btn.interactable = true;
				}
				else
				{
					MainMenu.menu.share_btn.interactable = false;
				}
				if (PlayerPrefs.GetInt("YOUTUBEClicked") == 0)
				{
					MainMenu.menu.yt_btn.interactable = true;
					MainMenu.menu.yt_btn_2.interactable = true;
				}
				else
				{
					MainMenu.menu.yt_btn.interactable = false;
					MainMenu.menu.yt_btn_2.interactable = false;
				}
			}
			if (Application.loadedLevel >= 3 && Application.loadedLevel <= 7)
			{
				if (Gmanager.gm.level_complete.activeSelf)
				{
					Gmanager.gm.inapp_banner_show();
				}
				else if (Gmanager.gm.level_fail.activeSelf)
				{
					Gmanager.gm.inapp_banner_show();
				}
			}
		}
		catch (Exception)
		{
		}
		base.gameObject.SetActive(value: false);
	}
}
