using System;
using UnityEngine;

public class Desi_rcc : MonoBehaviour
{
	public GameObject main_canvas;

	public CanvasGroup main_2;

	public static Desi_rcc desi;

	private void Start()
	{
		desi = this;
	}

	private void start_point()
	{
		main_canvas.SetActive(value: false);
	}

	private void end_point()
	{
		main_canvas.SetActive(value: true);
		main_2.alpha = 1f;
		main_2.interactable = true;
		try
		{
			if (PlayerPrefs.GetInt("cameradone") == 0)
			{
				Gmanager.gm.camera_trainning();
			}
		}
		catch (Exception)
		{
		}
	}

	private void distance_plus()
	{
	}
}
