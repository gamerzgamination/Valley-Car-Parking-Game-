//using GameAnalyticsSDK;
//using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PauseListner : MonoBehaviour
{
    public List<AudioSource> soundsSources;
    bool restartPressed = false;
   // public ConsoliAdsBannerView consoliAdsBannerView = new ConsoliAdsBannerView();

    private void OnEnable()
    {
      //  soundsSources = new List<AudioSource>();
        try
        {
            //if (AdsManager.Instance)
            //    AdsManager.Instance.ShowMediumBanner(GoogleMobileAds.Api.AdPosition.BottomLeft);
        }

        catch (Exception e)
        {
          //  GameAnalytics.NewErrorEvent(GAErrorSeverity.Info, "AdsManager Instance Not Found!");
        }

        Toolbox.GameplayController.HUD_Status(false);
        //Toolbox.GameManager.Add_ActiveUI(this.gameObject);
        
        AudioListener.volume = 0;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        //Toolbox.GameManager.Remove_ActiveUI(this.gameObject);
        if(!restartPressed)
            AudioListener.volume = 1;
    }

    #region ButtonListners

    public void OnPress_Home()
    {
        ////////Toolbox.ObjectiveHandler.UnloadAssetsFromMemory();
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.Back_to_mainmenu = true;
        Toolbox.GameManager.FBAnalytic_EventDesign(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "_PauseHome_Press");
        Toolbox.HUDListner.Loadingpanel.SetActive(true);
        Toolbox.GameManager.Load_MenuScene(true);
        this.gameObject.SetActive(false);
        AudioListener.volume = 1;
        //    Destroy(this.gameObject);
    }

    public void OnPress_Restart()
    {
        //////Toolbox.ObjectiveHandler.UnloadAssetsFromMemory();
        restartPressed = true;
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.FBAnalytic_EventDesign(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "_Pause_Restart");
        Toolbox.HUDListner.Loadingpanel.SetActive(true);
        Toolbox.GameManager.Load_GameScene(true, Toolbox.DB.Prefs.Get_LastSelectedGameModeSceneIndex(),1f);
        Toolbox.GameManager.Call_ad_after_restart = true;
        this.gameObject.SetActive(false);
        AudioListener.volume = 1;
        //    Destroy(this.gameObject);
    }

    public void OnPress_Resume()
    {
        //////Toolbox.ObjectiveHandler.UnloadAssetsFromMemory();
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameplayController.HUD_Status(true);
        Toolbox.GameManager.FBAnalytic_EventDesign(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "_Pause_resume");
        Toolbox.HUDListner.ShowBanner();
        this.gameObject.SetActive(false);
        AudioListener.volume = 1;
        // Destroy(this.gameObject);
    }

    #endregion
}
