//using GoogleMobileAds.Api;
using System;
using UnityEngine;
using UnityEngine.UI;
//using GameAnalyticsSDK;

public class MainMenuListner : MonoBehaviour
{
    public Text coinsTxt;
    //public RectTransform bannerAd;
    //public RectTransform IconAd;

    public GameObject noAdsButton;

    //public GameObject vehicleSelMenu;
    //public Image PowerBar;
    //public Image RankBar;
   // public ConsoliAdsBannerView consoliAdsBannerView = new ConsoliAdsBannerView();


    private void Awake()
    {
        ShowBannner();

    }
    public void OnEnable()
    {
        Time.timeScale = 1;
        Toolbox.UIManager.UpdateTxts();
        NoAdsButtonHandling();
    }

    private void OnDisable()
    {
      //  Toolbox.GameManager.Remove_ActiveUI(this.gameObject);
    //    Toolbox.AdsManager.Hide_BAd();
    }

    private void Start()
    {
        if (Toolbox.GameManager.Back_to_mainmenu)
        {

            try
            {
                //if (FindObjectOfType<AbstractAdsmanager>())
                //    FindObjectOfType<AbstractAdsmanager>().ShowInterstitial();
            }

            catch (Exception e)
            {
               // GameAnalytics.NewErrorEvent(GAErrorSeverity.Info, "AdsManager Instance Not Found!");
                Toolbox.GameManager.Back_to_mainmenu = false;
            }
        }
       // Invoke("MegaOffer", 1.0f); 
    }

    private void MegaOffer()
    {
        if (!Toolbox.GameManager.FirstShowMegaOffer && !Toolbox.DB.Prefs.MegaOfferPurchased)
        {
            //Toolbox.GameManager.Instantiate_MegaOffer();
           Toolbox.UIManager.MegaOffers.SetActive(true);
            Toolbox.GameManager.FirstShowMegaOffer = true;
        }
    }

    public void NoAdsButtonHandling()
    {
        if (Toolbox.DB.Prefs.NoAdsPurchased)
            noAdsButton.GetComponent<Button>().interactable = false;
    }
    public void ShowBannner() 
    {
        //if (AdsManager.Instance)
        //    AdsManager.Instance.ShowSmallBanner(GoogleMobileAds.Api.AdPosition.Top);
    }
   

    #region ButtonListners

    public void OnPress_Next()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.FBAnalytic_EventDesign("MainMenu_Press_Next");
        Toolbox.DB.Prefs.LastSelectedchapter_of_gamemode = 0;
        this.GetComponentInParent<UIManager>().ShowNextUI();
        //Toolbox.GameManager.loading_Delay(5f);
        //Invoke("Next",5.01f);
    }

    private void Next()
    {
        Toolbox.DB.Prefs.LastSelectedchapter_of_gamemode = 0;
        this.GetComponentInParent<UIManager>().ShowNextUI();
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
    }

    public void OnPress_Settings()
    {
      //Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPress);
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.FBAnalytic_EventDesign("MainMenu_Press_Settings");
        //  Toolbox.GameManager.InstantiateUI_Settings();
        Toolbox.UIManager.Settings_Panel.SetActive(true);
    }

    public void OnPress_RateUs()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.FBAnalytic_EventDesign("MainMenu_Press_RateUs");
        Application.OpenURL(Toolbox.GameManager.Get_RateUsLink());
    }

    public void OnPress_MoreGames()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.FBAnalytic_EventDesign("MainMenu_Press_MoreGames");
        Application.OpenURL(Constants.link_MoreGames);
    }

    public void OnPress_PrivacyPolicy()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Application.OpenURL(Constants.link_PrivacyPolicy);
    }

    public void OnPress_Quit()
    {
        try
        {
            //if (FindObjectOfType<AbstractAdsmanager>())
            //    FindObjectOfType<AbstractAdsmanager>().ShowInterstitial();
        }

        catch (Exception e)
        {
           // GameAnalytics.NewErrorEvent(GAErrorSeverity.Info, "AdsManager Instance Not Found!");
        }
        //Toolbox.GameManager.InstantiateUI_Quit();
        Toolbox.UIManager.Quit_Panel.SetActive(true);
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);

    }
    public void OnPress_WatchVideo()
    {
       Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        //if (AdsManager.Instance)
        //    AdsManager.Instance.ShowRewardedVideo(AdsManager.RewardType.FREEREWARD);
    }

    public void OnPress_FB()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.FBAnalytic_EventDesign("MainMenu_Press_FB");
        Application.OpenURL(Constants.link_Facebook);
    }

    public void OnPress_AdsScene() {

        Toolbox.GameManager.LoadLevel(4, false);
    }
    public void OnPress_RemoveAds()
    {
        Toolbox.GameManager.FBAnalytic_EventDesign("MainMenu_Press_RemoveAds");
        InAppHandler.Instance.Buy_NoAds();
    }

    public void OnPress_Store() {

        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.FBAnalytic_EventDesign("MainMenu_Press_Store");
        //  Toolbox.GameManager.InstantiateUI_Shop();
        Toolbox.UIManager.Shop_Panel.SetActive(true);
    }
    public void On_Press_Shop()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.FBAnalytic_EventDesign("MainMenu_Press_Shop");
        this.GetComponentInParent<UIManager>().DirectShowShop();
        Toolbox.GameManager.GodirectshopfromMenu = true;
    }
    #endregion

}
