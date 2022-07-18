//using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitListner : MonoBehaviour
{
   // private ConsoliAdsBannerView bannerview;

    private void OnEnable()
    {
       
       // Toolbox.GameManager.Add_ActiveUI(this.gameObject);
    }

    private void OnDisable()
    {
       // Toolbox.GameManager.Remove_ActiveUI(this.gameObject);
    }
    #region Button Listner

    public void OnPress_Yes()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.FBAnalytic_EventDesign("Quit_Press_Yes");
        //    Destroy(this.gameObject);
        this.gameObject.SetActive(false);
        Application.Quit();
      //  AdsManager.Instance.ShowInterstitialAd();
    }
    public void OnPress_No()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.FBAnalytic_EventDesign("Quit_Press_No");
     //   Destroy(this.gameObject);
        this.gameObject.SetActive(false);

    }


    #endregion
}
