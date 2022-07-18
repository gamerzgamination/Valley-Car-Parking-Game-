using UnityEngine;
using UnityEngine.UI;

public class TutorialListner : MonoBehaviour
{
    public GameObject[] steps;
    public string [] stepDescriptionHeadingVal;
    public string [] stepDescriptionBodyVal;
    public AudioClip [] stepVoiceover;

    public GameObject descriptionObj;
    public Text descriptionHeadingTxt;
    public Text descriptionBodyTxt;

    private int curIndex = 0;

    private void OnEnable()
    {
      //  Toolbox.GameManager.Add_ActiveUI(this.gameObject);
    }

    private void OnDisable()
    {
       // Toolbox.GameManager.Remove_ActiveUI(this.gameObject);
    }
    private void Start()
    {
        if (steps.Length <= 0)
            End();
        else
        {
        //    Toolbox.GameplayController.Level3DAudioSource.enabled = false;
        //    Toolbox.GameplayController.TutorialCamera.SetActive(true);

            Toolbox.Soundmanager.Set_MusicVolume((Toolbox.Soundmanager.musicSource.volume - 0.5f));

            curIndex = 0;
            ShowStep(curIndex);
        }


    }

    public void ShowStep(int _index) {

        if (_index >= steps.Length) {

            End();
            return;
        }

        steps[_index].SetActive(true);
        descriptionHeadingTxt.text = stepDescriptionHeadingVal[_index];
        descriptionBodyTxt.text = stepDescriptionBodyVal[_index];

        Toolbox.Soundmanager.PlaySoundAfterStop(stepVoiceover[_index]);
    }

    public void OnPress_Okay() {

        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.FBAnalytic_EventDesign("Tutorial_Press_Okay");
        Toolbox.GameManager.Analytics_DesignEvent("Tutorial_Press_Okay");
        steps[curIndex].SetActive(false);
        curIndex++;
        ShowStep(curIndex);
    }

    void End() {

        Toolbox.Soundmanager.Set_SoundVolume(Toolbox.DB.Prefs.SoundVolume);
        Toolbox.Soundmanager.Set_MusicVolume(Toolbox.DB.Prefs.MusicVolume);
      //  Toolbox.GameplayController.Level3DAudioSource.enabled = true;
       // Toolbox.GameplayController.TutorialCamera.SetActive(false);

    //    Toolbox.HUDListner.StartTimer_Status(true);
        this.gameObject.SetActive(false);
        Time.timeScale = 1;

    }
}
