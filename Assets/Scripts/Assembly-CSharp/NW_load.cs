using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NW_load : MonoBehaviour
{
	public float delay;

	private void Start()
	{
		try
		{
			Invoke("Load_Scene", delay);
		}
		catch (Exception)
		{
		}
	}

	private void Load_Scene()
	{
		try
		{
			SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
		}
		catch (Exception)
		{
		}
	}
}
