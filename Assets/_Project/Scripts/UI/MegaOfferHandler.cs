//using GameAnalyticsSDK;
//using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class MegaOfferHandler : MonoBehaviour
{
   // public ConsoliAdsBannerView consoliAdsBannerView = new ConsoliAdsBannerView();

    private void OnEnable()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
    }
    public void OnPress_Close() {
        if (Toolbox.GameManager.ShowMegaOfferOnComplete)
        {
            try
            {
                //if(AdsManager.Instance)
                //AdsManager.Instance.ShowMediumBanner(GoogleMobileAds.Api.AdPosition.BottomLeft);
            }

            catch (Exception e)
            {
             //   GameAnalytics.NewErrorEvent(GAErrorSeverity.Info, "AdsManager Instance Not Found!");
            }
            Toolbox.GameManager.ShowMegaOfferOnComplete = false;
        }
        // Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }

    public void OnPress_GotIt() {
       // InAppHandler.Instance.Buy_MegaOffer();
        OnPress_Close();
    }
}
