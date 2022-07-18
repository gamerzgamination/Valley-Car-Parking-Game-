using UnityEngine;

public class RateUsListner : MonoBehaviour
{

    private void OnEnable()
    {
       // Toolbox.GameManager.Add_ActiveUI(this.gameObject);
    }

    private void OnDisable()
    {
       // Toolbox.GameManager.Remove_ActiveUI(this.gameObject);
    }
    private void Start()
    {
      //  Toolbox.DB.Prefs.AppRated = true;
    }

    void Close() {
        Toolbox.HUDListner.CompletePanel.SetActive(true);
     //   Toolbox.GameManager.InstantiateUI_LevelComplete();
         this.gameObject.SetActive(false);
      //  Destroy(this.gameObject);
    }

    #region ButtonListners

    public void OnPress_Rate()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.Analytics_DesignEvent("RateUs_Pressed");
        Toolbox.GameManager.FBAnalytic_EventDesign("RateUs_Pressed");
        Toolbox.DB.Prefs.AppRated = true;

        Application.OpenURL(Toolbox.GameManager.Get_RateUsLink());
        Close();
    }

    public void OnPress_Close()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.FBAnalytic_EventDesign("RateUs_Closed");
        Toolbox.GameManager.Analytics_DesignEvent("RateUs_Closed");

        Close();
    }

    #endregion
}
