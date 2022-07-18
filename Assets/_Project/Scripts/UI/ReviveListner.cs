//using GameAnalyticsSDK;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ReviveListner : MonoBehaviour
{
    public Image timeRing;
    public Button videoBtn;
    public Text coinsTxt;
    public Text EnoughCoinTxt;

    float ringEndSpeed = 0.2f;
    float time = 100;

    private void OnEnable()
    {
        if (Toolbox.GameplayController.IsRevived )
        {
            OnPress_Close();
        }
        Time.timeScale = 0.03f;
      //  Toolbox.GameManager.Add_ActiveUI(this.gameObject);
        coinsTxt.text = Constants.reviveCoinsCost.ToString();
    }

    private void OnDisable()
    {
      //  Toolbox.GameManager.Remove_ActiveUI(this.gameObject);
    }

    private void Update()
    {
        time -= ringEndSpeed;

        timeRing.fillAmount = time / 100;
     //   print("time :"+time);
        if (time <= 0)
            OnPress_Close();
    }

    public void OnPress_Revive() {

    //    Time.timeScale = 1.0f;
        Toolbox.GameManager.Analytics_DesignEvent("_Revived");
       
        this.gameObject.SetActive(false);
        try
        {
            //if (FindObjectOfType<AdsManager>())
            //{
            //    //if (FindObjectOfType<AdsManager>().rewardBasedVideo.IsLoaded() && FindObjectOfType<AdsManager>().IsRewardedAdReady() && !GRS_AdIDs.Ads_Purchase && GRS_AdIDs.Check_FullScreen)
            //    //    FindObjectOfType<AdsManager>().ShowRewardedVideo(AdsManager.RewardType.REVIVEREWARD);
            //    //else
            //    //    OnPress_Close();
            //}
        }
        catch (Exception e)
        {
          //  GameAnalytics.NewErrorEvent(GAErrorSeverity.Info, "AdsManager Instance Not Found!");
        }
    }


    public void OnPress_UseCoins()
    {
        Time.timeScale = 1.0f;
        if (Toolbox.DB.Prefs.GoldCoins >= Constants.reviveCoinsCost)
        {
            Toolbox.GameManager.FBAnalytic_EventDesign("_Revived OnPress_UseCoins");
            Toolbox.GameManager.Analytics_DesignEvent("_Revived OnPress_UseCoins");
            ReviveController.Revive_PlayerOnCoins();

        }
        else
        {
            EnoughCoinTxt.text = ("No enough Coins").ToString();
        }
        this.gameObject.SetActive(false);
        //  Destroy(this.gameObject);
    }

    public void OnPress_Close()
    {
        Time.timeScale = 1.0f;
        Toolbox.HUDListner.FailPanel.SetActive(true);    
        this.gameObject.SetActive(false);
    }
    
}
