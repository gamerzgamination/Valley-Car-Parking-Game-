using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Async_Loading : MonoBehaviour {

	public Image loadingSlider;
    private string scenename;
	bool loadCheck;
	public Text loadingText;
	public float LoadTime;

	// Use this for initialization
	void Start () 
	{
		Time.timeScale = 1f;
		scenename = Toolbox.DB.Prefs.LastSelectedscenename;
		print("scenename :"+ Toolbox.DB.Prefs.LastSelectedscenename);
		StartCoroutine(LoadScene());
    }

	public void loadButton()
	{
		StartCoroutine(LoadScene());
	}

    private void Update()
    {
		print("Time :"+Time.timeScale);
    }
    IEnumerator LoadScene()
	{
	 	yield return null;
          AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scenename);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
		Debug.Log("Pro :" + asyncOperation.progress);
		//When the load is still in progress, output the Text and progress bar
		print("isDone :"+asyncOperation.isDone);
		while (!asyncOperation.isDone)
		{
			//Output the current progress
			loadingSlider.fillAmount = loadingSlider.fillAmount * Time.deltaTime *LoadTime;
			string percent = (asyncOperation.progress * 100).ToString ("F0");
			loadingText.text = string.Format ("{0}%", percent);
			print("IsDone :"+ loadingSlider.fillAmount +" text :"+ loadingText.text);
			// Check if the load has finished
			if (asyncOperation.progress >= 0.9f )
			{
				//Change the Text to show the Scene is ready
				asyncOperation.allowSceneActivation = true;
			}
			yield return null;
		}
	}

}
