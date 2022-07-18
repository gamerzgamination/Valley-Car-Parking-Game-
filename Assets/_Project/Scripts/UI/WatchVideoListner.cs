//using GameAnalyticsSDK;
using System;
using UnityEngine;
using UnityEngine.UI;

public class WatchVideoListner : MonoBehaviour
{
    public Text messageTxt;
    public Text HeaderTxt;
    private void OnEnable()
    {
     //   Toolbox.GameManager.Add_ActiveUI(this.gameObject);
        Invoke("PauseState",0.8f);
    }

    private void OnDisable()
    {
      //  Toolbox.GameManager.Remove_ActiveUI(this.gameObject);
    }
    public void UpdateTxt(string _str,string str) {

        messageTxt.text = _str;
        HeaderTxt.text = str;
    }
    private void PauseState()
    {
        Time.timeScale = 0f;   
    }
    public void OnPress_Okay() {
        Time.timeScale = 1.0f;
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        this.gameObject.SetActive(false);
     //   Destroy(this.gameObject);
    }
    public void OnPress_WatchVideo()
    {
        Time.timeScale = 1.0f;
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.Analytics_DesignEvent("OnPress_WatchVideo_Press_Store");
       
        try
        {
            //if (FindObjectOfType<AdsManager>())
            //    FindObjectOfType<AdsManager>().ShowRewardedVideo(AdsManager.RewardType.HEALTHONINJECTION);
        }

        catch (Exception e)
        {
          //  GameAnalytics.NewErrorEvent(GAErrorSeverity.Info, "AdsManager Instance Not Found!");
        }
        this.gameObject.SetActive(false);
        //    Destroy(this.gameObject);
    }

}
