//using GameAnalyticsSDK;
//using GoogleMobileAds.Api;
using System;
using UnityEngine;


public class LevelFailListner : MonoBehaviour
{
    public GameObject ReviveRewardBtn;
    private void OnEnable()
    {
        ////////////////////Toolbox.ObjectiveHandler.UnloadAssetsFromMemory();
    }

    private void OnDisable()
    {
    //    Toolbox.AdsManager.Hide_BAd();
       // Toolbox.GameManager.Remove_ActiveUI(this.gameObject);
    }

    private void Start()
    {
        try
        {
            //if (AdsManager.Instance)
            //    AdsManager.Instance.ShowMediumBanner(GoogleMobileAds.Api.AdPosition.BottomLeft);

        }

        catch (Exception e)
        {
            //GameAnalytics.NewErrorEvent(GAErrorSeverity.Info, "AdsManager Instance Not Found!");
        }

        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.levelFail);
        Toolbox.GameManager.FBAnalytic_EventLevel_Fail(Toolbox.GameManager.Get_CurGameModeName(), Toolbox.DB.Prefs.LastSelectedchapter_of_gamemode, Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode());
        Toolbox.GameManager.Analytics_ProgressionEvent_Fail(Toolbox.GameManager.Get_CurGameModeName(), Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode());
     
    }

    public void OnPress_Home()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks); 
        Toolbox.GameManager.Back_to_mainmenu = true;
        Toolbox.GameManager.Analytics_DesignEvent(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "_" + "LevelFail_Home_Pressed");
        Toolbox.HUDListner.Loadingpanel.SetActive(true);
        Toolbox.GameManager.Load_MenuScene(true);
        this.gameObject.SetActive(false);
    //    Destroy(this.gameObject);
    }

    public void OnPress_Restart()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.Analytics_DesignEvent(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "_" + "LevelFail_Restart_Pressed");
        Toolbox.GameManager.Call_ad_after_restart = true;
        Toolbox.HUDListner.Loadingpanel.SetActive(true);
        Toolbox.GameManager.Load_GameScene(true, Toolbox.DB.Prefs.Get_LastSelectedGameModeSceneIndex(),3f);
        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
    }
   
}
