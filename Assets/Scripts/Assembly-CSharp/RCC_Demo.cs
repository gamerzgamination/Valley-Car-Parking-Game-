using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("BoneCracker Games/Realistic Car Controller/UI/Demo Manager")]
public class RCC_Demo : MonoBehaviour
{
	[Header("Spawnable Cars")]
	public RCC_CarControllerV3[] selectableVehicles;

	private int selectedCarIndex;

	private int selectedBehaviorIndex;

	public void SelectVehicle(int index)
	{
		selectedCarIndex = index;
	}

	public void Spawn()
	{
		RCC_CarControllerV3[] array = Object.FindObjectsOfType<RCC_CarControllerV3>();
		Vector3 vector = default(Vector3);
		Quaternion rotation = default(Quaternion);
		if (array != null && array.Length > 0)
		{
			RCC_CarControllerV3[] array2 = array;
			foreach (RCC_CarControllerV3 rCC_CarControllerV in array2)
			{
				if (!rCC_CarControllerV.AIController && rCC_CarControllerV.canControl)
				{
					vector = rCC_CarControllerV.transform.position;
					rotation = rCC_CarControllerV.transform.rotation;
					break;
				}
			}
		}
		if (vector == Vector3.zero && (bool)Object.FindObjectOfType<RCC_Camera>())
		{
			vector = Object.FindObjectOfType<RCC_Camera>().transform.position;
			rotation = Object.FindObjectOfType<RCC_Camera>().transform.rotation;
		}
		rotation.x = 0f;
		rotation.z = 0f;
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j].canControl && !array[j].AIController)
			{
				Object.Destroy(array[j].gameObject);
			}
		}
		GameObject gameObject = Object.Instantiate(selectableVehicles[selectedCarIndex].gameObject, vector + Vector3.up, rotation);
		gameObject.GetComponent<RCC_CarControllerV3>().canControl = true;
		if ((bool)Object.FindObjectOfType<RCC_Camera>())
		{
			Object.FindObjectOfType<RCC_Camera>().SetPlayerCar(gameObject);
		}
		if ((bool)Object.FindObjectOfType<RCC_CustomizerExample>())
		{
			Object.FindObjectOfType<RCC_CustomizerExample>().car = gameObject.GetComponent<RCC_CarControllerV3>();
			Object.FindObjectOfType<RCC_CustomizerExample>().CheckUIs();
		}
	}

	public void SelectBehavior(int index)
	{
		selectedBehaviorIndex = index;
	}

	public void InitBehavior()
	{
		switch (selectedBehaviorIndex)
		{
		case 0:
			RCC_Settings.Instance.behaviorType = RCC_Settings.BehaviorType.Simulator;
			RestartScene();
			break;
		case 1:
			RCC_Settings.Instance.behaviorType = RCC_Settings.BehaviorType.Racing;
			RestartScene();
			break;
		case 2:
			RCC_Settings.Instance.behaviorType = RCC_Settings.BehaviorType.SemiArcade;
			RestartScene();
			break;
		case 3:
			RCC_Settings.Instance.behaviorType = RCC_Settings.BehaviorType.Drift;
			RestartScene();
			break;
		case 4:
			RCC_Settings.Instance.behaviorType = RCC_Settings.BehaviorType.Fun;
			RestartScene();
			break;
		case 5:
			RCC_Settings.Instance.behaviorType = RCC_Settings.BehaviorType.Custom;
			RestartScene();
			break;
		}
	}

	public void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
