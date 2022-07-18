//using GameAnalyticsSDK;
//using GoogleMobileAds.Api;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDListner : MonoBehaviour
{
    /// <summary>
    /// UI menus
    /// </summary>
    public GameObject PausePanel;
    public GameObject FailPanel;
    public GameObject CompletePanel;
    //public GameObject SpecialMission_CompletePanel;
    public GameObject RevivePanel;
    public GameObject WatchVideoPanel;
    public GameObject RateUs_Panel;
    public GameObject Mega_OfferPanel;
    public GameObject Loadingpanel;
    public GameObject Message;
    public GameObject Tutorial;
    public GameObject CinematicEffect;
    /// <summary>
    /// Other Text 
    /// </summary>
    public GameObject Missioncompletetext;
    public GameObject MissionFailtext;
    public GameObject ObjectiveClear;
    public GameObject PlayerHudCanvas;
    public Button pauseBtn;
    //public Button skipStartCinematicBtn;
    public GameObject playerControlsPanel;
    public GameObject PlayerGadgets;
    public CanvasGroup canvasGroup;
    //public GameObject map;
    public Text timeTxt;
    private int time;
    // Gear Items 
    public bool istouched;
    public Scrollbar gearscrollbar;
    public GameObject gearhandle;
    public GameObject blac_spot_D;
    public GameObject blac_spot_R;
    public Sprite D_image;
    public Sprite R_image;




    private void OnEnable()
    {
        Toolbox.Set_HUD(this);
    }
    private void Start()
    {
        Invoke("OnPress_OkTutorial", 0.5f);
    }
    //private void OnDisable()
    //{
    //}

    private void Awake()
    {
        Toolbox.Set_HUD(this);

        //if (Toolbox.GameManager.Call_ad_after_restart || Toolbox.GameManager.Call_ad_before_gameplay)
        //{
        //    Toolbox.GameManager.Call_ad_after_restart = false;
        //    Toolbox.GameManager.Call_ad_before_gameplay = false;
        //    try
        //    {
        //        if (FindObjectOfType<AbstractAdsmanager>())
        //            FindObjectOfType<AbstractAdsmanager>().ShowInterstitial();
        //    }
        //    catch (Exception e)
        //    {
        //        //GameAnalytics.NewErrorEvent(GAErrorSeverity.Info, "AdsManager Instance Not Found!");


        //    }

        //}

        ShowBanner();
        Set_PlayerControls(false);

    }


    public void ShowBanner()
    {

        try
        {
            //if (AdsManager.Instance)
            //    AdsManager.Instance.ShowSmallBanner(GoogleMobileAds.Api.AdPosition.Top);
        }

        catch (Exception e)
        {
            //GameAnalytics.NewErrorEvent(GAErrorSeverity.Info, "AdsManager Instance Not Found!");

        }

    }

    public void SetTime(int _val)
    {
        time = _val;
        int min = time / 60;
        timeTxt.text = string.Format("{0:D2}:{1:D2}", min, time - (min * 60));
        timeTxt.transform.parent.gameObject.SetActive(true);
        StartCoroutine(CR_TimeHandling());
    }
    IEnumerator CR_TimeHandling()
    {

        int min = 0;
        while (true)
        {
            yield return new WaitForSeconds(1);

            if (Time.timeScale == 1)
            {
                time--;
                min = time / 60;
                timeTxt.text = string.Format("{0:D2}:{1:D2}", min, time - (min * 60));
                // print("Time :"+ Get_Time());
                if (time < 0)
                {
                    StopCoroutine(CR_TimeHandling());
                    Toolbox.GameplayController.LevelFailHandling();
                }

            }

        }
    }
    public void SetLives(int _val)
    {
        //   livesTxt.text = _val.ToString();
    }

    public void SetTotalLives(int _val)
    {
        //     totalLivesTxt.text = _val.ToString();
    }

    public float Get_Time()
    {

        return time;
    }




    public void Set_PlayerControls(bool _val)
    {
        if (playerControlsPanel)
            playerControlsPanel.SetActive(_val);
    }

    //public void Set_Mapstatus(bool _val)
    //{
    //    if (map)
    //        map.SetActive(_val);
    //}
    //public void SetStatus_SkipAnimationButton(bool _val)
    //{
    //    if (skipStartCinematicBtn.gameObject)
    //        skipStartCinematicBtn.gameObject.SetActive(_val);
    //}
    public void Set_CinematicEffectstatus(bool _val)
    {
        if (CinematicEffect)
            CinematicEffect.SetActive(_val);
    }
    public void Set_PlayerStatus(bool _val)
    {
        if (PlayerHudCanvas)
            PlayerHudCanvas.SetActive(_val);
    }


    public void set_statusEnemyCounter()
    {
        //print("countkills :" + Toolbox.ObjectiveHandler.countKills + "Total Enemy :" + Toolbox.ObjectiveHandler.SelectedLevelData.totalEnemy);
        //if (Toolbox.DB.Prefs.Speciallevel)
        //    EnemyCounter.text = SpecialMission.Instance.countKills + "/" + SpecialMission.Instance.Selectedleveldata.totalEnemy;
        //else
        //    EnemyCounter.text = Toolbox.ObjectiveHandler.countKills + "/" + Toolbox.ObjectiveHandler.SelectedLevelData.totalEnemy;

    }



    #region ButtonListners

    public void OnPress_Pause()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.Analytics_DesignEvent(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "Pause_sPress");
        Toolbox.GameManager.FBAnalytic_EventDesign(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "Pause_Press");
        PausePanel.SetActive(true);
        // Toolbox.GameManager.InstantiateUI_Pause();
    }
    public void OnPress_Fail()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.Analytics_DesignEvent(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "Pause_sPress");
        Toolbox.GameManager.FBAnalytic_EventDesign(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "Pause_Press");
        FailPanel.SetActive(true);
        // Toolbox.GameManager.InstantiateUI_Pause();
    }
    public void OnPress_Complete()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.Analytics_DesignEvent(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "Pause_sPress");
        Toolbox.GameManager.FBAnalytic_EventDesign(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "Pause_Press");
        CompletePanel.SetActive(true);
        // Toolbox.GameManager.InstantiateUI_Pause();
    }
    public void OnPress_Injection()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Time.timeScale = 0.03f;
        WatchVideoPanel.SetActive(true);
        WatchVideoPanel.GetComponent<WatchVideoListner>().UpdateTxt("To Get The Health on Watch Video", "Watch Video");
        // Toolbox.GameManager.Instantiate_WatchVideoMsg("To Get The Health on Watch Video","Watch Video");
    }


    public void OnPress_OkTutorial()
    {
        //print("OnPress_OkTutorial");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        //Toolbox.ObjectiveHandler.player.levelLoadFadeObj.GetComponent<LevelLoadFade>().FadeAndLoadLevel(Color.black, 1.5f, true);
        //Set_Mapstatus(true);
        Set_PlayerControls(true);
        Set_PlayerStatus(true);

        //fOR jUST TRAINING mODE
        if (Toolbox.DB.Prefs.LastSelectedGameMode != 11)
        {
            //if (Toolbox.DB.Prefs.Speciallevel)
            //{

            //    //Toolbox.SpecialMission.startSpawning();
            //    pauseBtn.gameObject.SetActive(false);
            //}
            //else
            //{
            //   // Toolbox.ObjectiveHandler.startSpawning();
            //    pauseBtn.gameObject.SetActive(true);
            //}

        }
        // SetStatus_SkipAnimationButton(false);
    }

    public void SkipSAnimations()
    {

        OnPress_OkTutorial();
    }

    public void handleplayerhud(bool _Val)
    {
        PlayerHudCanvas.gameObject.SetActive(_Val);
    }

    public void on_down()
    {
        istouched = true;
    }

    public void on_up()
    {
        istouched = false;
    }
    public void gear_value()
    {
        if (gearscrollbar.value == 0f)
        {
            gearscrollbar.value = 1f;
            gearhandle.GetComponent<Image>().overrideSprite = R_image;
            blac_spot_D.SetActive(value: false);
            blac_spot_R.SetActive(value: true);
            //if (MainMenu.currentlevel == 2 && !islevel2)
            //{
            //    all_on_level2();
            //    islevel2 = true;
            //}
            //revers_light.SetActive(value: true);
            Toolbox.GameplayController.GearShiftingsound.Play();
            Toolbox.GameplayController.ReverseSound.Play();
        }
        else
        {
            gearscrollbar.value = 0f;
            gearhandle.GetComponent<Image>().overrideSprite = D_image;
            blac_spot_D.SetActive(value: true);
            blac_spot_R.SetActive(value: false);
            // revers_light.SetActive(value: false);
            Toolbox.GameplayController.GearShiftingsound.Play();
            Toolbox.GameplayController.ReverseSound.Stop();
        }
    }

    #endregion

    #region FadeInout
    public IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0;
        Toolbox.GameManager.Permanent_Log("FadeInout");
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetValue;
    }
    #endregion
}
