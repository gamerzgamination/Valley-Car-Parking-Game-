//using GameAnalyticsSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyListner : MonoBehaviour
{

    private void OnEnable()
    {

        try
        {
            //if (FindObjectOfType<AbstractAdsmanager>())
            //{
            //    FindObjectOfType<AbstractAdsmanager>().ShowInterstitial();
            //}
        }

        catch (Exception e)
        {
          //   GameAnalytics.NewErrorEvent(GAErrorSeverity.Info, "AdsManager Instance Not Found!");


        }

    }

    private void OnDisable()
    {


    }
    public void OpenLink(string url)
    {
        Application.OpenURL(url);    
    }
}

