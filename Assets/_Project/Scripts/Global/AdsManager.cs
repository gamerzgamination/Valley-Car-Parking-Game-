using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
//using GoogleMobileAds.Mediation;
//using GoogleMobileAds.Api.Mediation.UnityAds;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Common;
//using Facebook.Unity;

[Serializable]
public class AdmobId
{
    public string ADMOB_APP_ID;
    public string ADMOB_INTERTITIAL_AD_ID, ADMOB_VIDEO_AD_ID, ADMOB_BANNER_AD_ID, ADMOB_REWARDED_AD_ID;
}
public class AdsManager : AbstractAdsmanager
{
    public static AdsManager Instance;

    private RewardType curreward;

    public delegate void RewardUserDelegate();
    string INTERTITIAL_ID;
    private static RewardUserDelegate NotifyReward;

    public AdmobId ADMOB_ID = new AdmobId();

    [HideInInspector]
    public InterstitialAd interstitial;
    [HideInInspector]
    public InterstitialAd videoAd;
    [HideInInspector]
    public BannerView SmallBanner;
    [HideInInspector]
    public BannerView MediumBanner;
    [HideInInspector]
    public RewardedAd rewardBasedVideo;

    bool isRadPlaying;
    bool isSmallBannerLoaded = false;
    bool isMediumBannerLoaded = false;
    bool isAdmobInitialized = false;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        //Instance = this;
        DontDestroyOnLoad(gameObject);

    }


    // Start is called before the first frame update
    void Start()
    {


        Toolbox.GameManager.Log("GG >> Admob:Initializ");
       // Init();
    }

    public void Init()
    {

        isAdmobInitialized = false;
        Toolbox.GameManager.Log("GG >> Admob:Initializing...");

        MobileAds.Initialize((initStatus) =>
        {

            isAdmobInitialized = true;
            Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;
                switch (status.InitializationState)
                {
                    case AdapterState.NotReady:
                        // The adapter initialization did not complete.
                        Toolbox.GameManager.Log("GG >> Adapter: " + status.Description + " not ready.Name=" + className);
                        //      Debug.Log("Adapert is :: "+(AdmobGAEvents.AdaptersNotInitialized) + className);
                        Toolbox.GameManager.Analytics_DesignEvent("Admob Adaptpors NotInitialized");
                        break;
                    case AdapterState.Ready:
                        // The adapter was successfully initialized.
                        Debug.Log("GG >> Adapter: " + className + " is initialized.");
#if UNITY_ANDROID
                        MediationAdapterConsent(className);
#endif
                        //    Debug.Log("Adapert is :: " + (AdmobGAEvents.AdaptersNotInitialized) + className);
                        // Toolbox.GameManager.Log("GG >> Admob Adaptpors Initialized");
                        break;
                }

            }
            Toolbox.GameManager.Log("GG >> Admob:CreateAdsObjects");
            CreateAdsObjects(ADMOB_ID.ADMOB_INTERTITIAL_AD_ID, ADMOB_ID.ADMOB_VIDEO_AD_ID);
            BindAdsEvent();
            // LoadVideo();
            LoadInterstitial();
            LoadRewardedVideo();
            LoadSmallBanner();
            LoadMediumBanner();
            Toolbox.GameManager.Log("GG >> Admob:Initialized");
            //if (!Toolbox.DB.Prefs.UserConsent)
            //    Toolbox.GameManager.PrivacyPolicy.SetActive(true);
            //else
            //    Toolbox.GameManager.Load_MenuScene(false, 10f);
        });


    }

    /// <summary>
    /// Send User Consent in Open Bidding Adapters Consent
    /// </summary>
    void MediationAdapterConsent(string AdapterClassname)
    {

        if (AdapterClassname.Contains("Unity"))
        {
            //UnityAds Consent
            // UnityAds.SetGDPRConsentMetaData(true);
            Toolbox.GameManager.Log("GG >> UnityAds consent is send");
            //   Toolbox.GameManager.Analytics_DesignEvent("Admob:Consent:UnityAds");
        }
        else if (AdapterClassname.Contains("Chartboost"))
        {
            Toolbox.GameManager.Log("GG >> Chartboost consent is send");
            //   Toolbox.GameManager.Analytics_DesignEvent("Admob:Consent:Chartboost");
        }
    }

    /// <summary>
    /// Create Ads objects.
    /// </summary>
    private void CreateAdsObjects(string intertitialAdId, string videoAdId)
    {

        INTERTITIAL_ID = intertitialAdId;

        this.interstitial = new InterstitialAd(intertitialAdId);
        this.videoAd = new InterstitialAd(videoAdId);
        this.rewardBasedVideo = new RewardedAd(ADMOB_ID.ADMOB_REWARDED_AD_ID); //RewardBasedVideoAd.Instance;
        this.SmallBanner = new BannerView(ADMOB_ID.ADMOB_BANNER_AD_ID, AdSize.Banner, AdPosition.Center);
        this.MediumBanner = new BannerView(ADMOB_ID.ADMOB_BANNER_AD_ID, AdSize.MediumRectangle, AdPosition.TopLeft);
        //        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
    }

    #region Ads Events Bind
    private void BindSmallBannerEvents()
    {

        // INTERSTITIAL EVENTS...//

        this.SmallBanner.OnAdLoaded += SmallBanner_HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.SmallBanner.OnAdFailedToLoad += SmallBanner_HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.SmallBanner.OnAdOpening += SmallBanner_HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.SmallBanner.OnAdClosed += SmallBanner_HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        //this.SmallBanner.OnAdLeavingApplication += SmallBanner_HandleOnAdLeavingApplication;
    }

    private void BindMediumBannerEvents()
    {

        // INTERSTITIAL EVENTS...//

        this.MediumBanner.OnAdLoaded += MediumBanner_HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.MediumBanner.OnAdFailedToLoad += MediumBanner_HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.MediumBanner.OnAdOpening += MediumBanner_HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.SmallBanner.OnAdClosed += MediumBanner_HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        //this.MediumBanner.OnAdLeavingApplication += MediumBanner_HandleOnAdLeavingApplication;
    }

    private void BindIntertitialEvents()
    {

        // INTERSTITIAL EVENTS...//

        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        // this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
    }

    private void BindVideoEvents()
    {
        // VIDEO AD EVENTS...//

        this.videoAd.OnAdLoaded += Video_HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.videoAd.OnAdFailedToLoad += Video_HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.videoAd.OnAdOpening += Video_HandleOnAdOpened;
        // Called when the ad is closed.
        this.videoAd.OnAdClosed += Video_HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        //this.videoAd.OnAdLeavingApplication += Video_HandleOnAdLeavingApplication;
    }

    private void BindRewardedEvents()
    {
        //.....REWARDED ADS EVENTS.......//
        //// Get singleton reward based video ad reference.
        //this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        //rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;

        rewardBasedVideo.OnAdFailedToShow += HandleRewardedAdFailedToShow;


        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnUserEarnedReward += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        //rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
    }

    /// <summary>
    /// Bind Ads events to receive Ads Data.
    /// </summary>
    /// 
    private void BindAdsEvent()
    {
        Toolbox.GameManager.Log("GG >> Admob:BindAdsEvent");
        BindIntertitialEvents();
        BindVideoEvents();
        BindRewardedEvents();
        BindSmallBannerEvents();
        BindMediumBannerEvents();
    }

    #endregion
    #region Load Ads
    public void LoadSmallBanner()
    {
        Toolbox.GameManager.Log("GG >> Admob:LoadSmallBanner");

        if (IsSmallBannerReady() || BanStatus == AdLoadingStatus.Loading || NoAdsPurchased() || !Toolbox.GameManager.IsNetworkAvailable() /*|| Constants.adsRemoteConfigStatus == "0"*/)
        {
            Toolbox.GameManager.Log("GG >> IsSmallBannerallreadyavailable");
            return;
        }
        Toolbox.GameManager.Log("GG >> Admob:smallBanner:LoadRequest");

        //AdmobGA_Helper.GA_Log(AdmobGAEvents.LoadSmallBanner);
        this.SmallBanner = new BannerView(ADMOB_ID.ADMOB_BANNER_AD_ID, AdSize.Banner, AdPosition.Top);
        BindSmallBannerEvents();
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.SmallBanner.LoadAd(request);
        this.SmallBanner.Hide();
        BanStatus = AdLoadingStatus.Loading;
    }

    public void LoadMediumBanner()
    {
        Toolbox.GameManager.Log("GG >> Admob:LoadMediumBanner");

        if (IsMediumBannerReady() || MedBanStatus == AdLoadingStatus.Loading || NoAdsPurchased() || !Toolbox.GameManager.IsNetworkAvailable() /*|| Constants.adsRemoteConfigStatus == "0"*/)
        {
            Toolbox.GameManager.Log("GG >> IsMediumBannerallreadyavailable");
            return;
        }
        Toolbox.GameManager.Log("GG >> Admob:mediumBanner:LoadRequest");
        this.MediumBanner = new BannerView(ADMOB_ID.ADMOB_BANNER_AD_ID, AdSize.MediumRectangle, AdPosition.TopLeft);
        BindMediumBannerEvents();
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        this.MediumBanner.LoadAd(request);
        this.MediumBanner.Hide();
        MedBanStatus = AdLoadingStatus.Loading;
    }

    /// <summary>
    /// Load Interstitial Ad
    /// </summary>
    public void LoadInterstitial()
    {


        if (IsInterstitialAdReady() || IadStatus == AdLoadingStatus.Loading || NoAdsPurchased() || !Toolbox.GameManager.IsNetworkAvailable() /*|| Constants.adsRemoteConfigStatus == "0"*/)
        {
            Debug.Log("GG >> IsInterstitialAdallreadyavailable");
            return;
        }

        Debug.Log("GG >> Admob:iad:LoadRequest");
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
        IadStatus = AdLoadingStatus.Loading;
    }

    /// <summary>
    /// Load Video Ad
    /// </summary>
    public void LoadVideo()
    {

        if (IsVideoAdReady() || NoAdsPurchased() || !Toolbox.GameManager.IsNetworkAvailable() /*|| Constants.adsRemoteConfigStatus == "0"*/)
        {
            Toolbox.GameManager.Log("GG >> IsVideoAdallreadyavailable");
            return;
        }
        Toolbox.GameManager.Log("GG >> Admob:vad:LoadRequest");
        //AdmobGA_Helper.GA_Log(AdmobGAEvents.LoadVideoAd);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.videoAd.LoadAd(request);

    }

    /// <summary>
    /// Load Rewarded Ad
    /// </summary>
    public void LoadRewardedVideo()
    {

        if (IsRewardedAdReady() || RadStatus == AdLoadingStatus.Loading || isRadPlaying || NoAdsPurchased() || !Toolbox.GameManager.IsNetworkAvailable() /*|| Constants.adsRemoteConfigStatus == "0"*/)
        {
            Toolbox.GameManager.Log("GG >> IsVideoAdallreadyavailable");
            return;
        }
        Toolbox.GameManager.Log("GG >> Admob:rad:LoadRequest");
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request);
        RadStatus = AdLoadingStatus.Loading;
    }
    public override bool IsSmallBannerReady()
    {
        return isSmallBannerLoaded;
    }

    public override bool IsMediumBannerReady()
    {
        return isMediumBannerLoaded;
    }
    /// <summary>
    /// Check is iAd already loaded
    /// </summary>
    public override bool IsInterstitialAdReady()
    {
        return this.interstitial.IsLoaded();
    }

    /// <summary>
    /// Check is vAd already loaded
    /// </summary>
    public override bool IsVideoAdReady()
    {
        return this.videoAd.IsLoaded();
    }

    /// <summary>
    /// Check is rAd already loaded
    /// </summary>
    public override bool IsRewardedAdReady()
    {
        if (this.rewardBasedVideo != null)
            return this.rewardBasedVideo.IsLoaded();

        //Toolbox.GameManager.Log("Error: Rewarded Video Instance is not assigned");
        return false;
    }


    #endregion

    #region Show Ads
    //public void HideSmallBannerEvent()
    //{
    //    if(this.SmallBanner!= null)
    //        this.SmallBanner.Hide();
    //}
    //public void HideMediumBannerEvent()
    //{
    //    if(this.MediumBanner != null)
    //        this.MediumBanner.Hide();
    //}
    public override void HideBannners()
    {
        if (this.MediumBanner != null)
        {
            this.MediumBanner.Hide();
        }
        if (this.SmallBanner != null)
        {
            this.SmallBanner.Hide();
        }

        Toolbox.GameManager.Log("GG >> Banner Hide");
    }

    public override void ShowSmallBanner(AdPosition position)
    {

        if (!isAdmobInitialized || NoAdsPurchased() || !Toolbox.GameManager.IsNetworkAvailable() /*|| Constants.adsRemoteConfigStatus != "1"*/)
        {
            Toolbox.GameManager.Log("GG >> " + " isAdmobInitialized:" + isAdmobInitialized + "NoAdsPurchased " + NoAdsPurchased() + " IsNetworkAvailable " + Toolbox.GameManager.IsNetworkAvailable());
            return;
        }
        Toolbox.GameManager.Log("GG >> Admob:smallBanner:ShowCall");
        // this.SmallBanner.Hide();
        if (SmallBanner != null && BanStatus == AdLoadingStatus.Loaded)
        {
            HideBannners();
            SmallBanner.Show();
            SmallBanner.SetPosition(position);
            Toolbox.GameManager.Log("GG >> Admob:smallBanner:Displayed");
        }
        else
            Toolbox.GameManager.Log("GG >> Admob:smallBanner:Not Loaded(null)");


    }
    public override void ShowMediumBanner(AdPosition position)
    {

        if (!isAdmobInitialized || NoAdsPurchased() || !Toolbox.GameManager.IsNetworkAvailable()/*|| Constants.adsRemoteConfigStatus != "1"*/)
        {
            Toolbox.GameManager.Log("GG >> " + " isAdmobInitialized:" + isAdmobInitialized + "NoAdsPurchased " + NoAdsPurchased() + " IsNetworkAvailable " + Toolbox.GameManager.IsNetworkAvailable());
            return;
        }
        Toolbox.GameManager.Log("GG >> Admob:mediumBanner:ShowCall");

        if (this.MediumBanner != null && MedBanStatus == AdLoadingStatus.Loaded)
        {
            HideBannners();
            this.MediumBanner.Show();
            this.MediumBanner.SetPosition(position);
            Toolbox.GameManager.Log("GG >> Admob:ShowMediumBanner:Displayed");
        }
        else
            Toolbox.GameManager.Log("GG >> Admob:ShowMediumBanner:Not Loaded(Null)");
        //  this.MediumBanner.Hide();
    }

    /// <summary>
    /// Show Interstitial Ad
    /// </summary>
    public override void ShowInterstitial()
    {


        if (!isAdmobInitialized || NoAdsPurchased() || !Toolbox.GameManager.IsNetworkAvailable()  /*|| Constants.adsRemoteConfigStatus != "1"*/)
        {
            Toolbox.GameManager.Log("GG >> " + " isAdmobInitialized:" + isAdmobInitialized + "NoAdsPurchased " + NoAdsPurchased() + " IsNetworkAvailable " + Toolbox.GameManager.IsNetworkAvailable());
            return;
        }

        Toolbox.GameManager.Log("GG >> Admob:iad:ShowCall");
        if (this.interstitial != null)
        {
            if (this.interstitial.IsLoaded() && IadStatus == AdLoadingStatus.Loaded)
            {
                Toolbox.GameManager.Log("GG >> Admob:iad:WillDisplay");
                this.interstitial.Show();
            }
            else
            {
                Toolbox.GameManager.Log("GG >> Admob:iad:NotLoaded");
                LoadInterstitial();
            }
        }
    }


    private IEnumerator ShowInterstitialWithDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        this.interstitial.Show();
    }
    /// <summary>
    /// Show Video Ad
    /// </summary>
    public void ShowVideo(bool showLoadingScreen)
    {

        if (!isAdmobInitialized || NoAdsPurchased() || !Toolbox.GameManager.IsNetworkAvailable() /*|| Constants.adsRemoteConfigStatus != "1"*/)
        {
            Toolbox.GameManager.Log("GG >> " + " isAdmobInitialized:" + isAdmobInitialized + "NoAdsPurchased " + NoAdsPurchased() + " IsNetworkAvailable " + Toolbox.GameManager.IsNetworkAvailable());
            return;
        }
        Toolbox.GameManager.Log("GG >> Admob:vad:ShowCall");
        if (this.videoAd != null)
        {
            if (this.videoAd.IsLoaded())
            {

                Toolbox.GameManager.Log("GG >> Admob:vad:WillDisplay");
                StartCoroutine(ShowVideoWithDelay());
            }
            else
            {
                LoadVideo();
            }
        }
    }

    private IEnumerator ShowVideoWithDelay()
    {
        yield return new WaitForSecondsRealtime(1f);
        this.videoAd.Show();
    }
    /// <summary>
    /// Show Rewarded Ad
    /// </summary>

    public override void ShowRewardedVideo(RewardType reward)
    {

        if (this.rewardBasedVideo == null || !isAdmobInitialized || NoAdsPurchased() || !Toolbox.GameManager.IsNetworkAvailable())
        {
            Toolbox.GameManager.Log("GG >> " + " isAdmobInitialized:" + isAdmobInitialized + "NoAdsPurchased " + NoAdsPurchased() + " IsNetworkAvailable " + Toolbox.GameManager.IsNetworkAvailable());
            return;
        }
        Toolbox.GameManager.Log("GG >> Admob:rad:ShowCall");
        if (this.rewardBasedVideo.IsLoaded() && RadStatus == AdLoadingStatus.Loaded)
        {
            Toolbox.GameManager.Log("GG >> Admob:rad:WillDisplay");

            curreward = reward;
            this.rewardBasedVideo.Show();
        }
        else
        {
            Time.timeScale = 1;
            LoadRewardedVideo();
        }

    }

    public void ShowRewardedButtonEvent()
    {
        ShowRewardedVideo(curreward);
    }

    private void GiveRewardAfterRewardedAd()
    {
        Toolbox.GameManager.Log("Congratulations! Reward has been given");
    }
    #endregion

    #region Intertitial Add Handler


    //******Intertitial Ad Handlers**********//
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            IadStatus = AdLoadingStatus.Loaded;
            Toolbox.GameManager.Log("GG >> Admob:iad:Loaded");
        });
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            IadStatus = AdLoadingStatus.NoInventory;
            Toolbox.GameManager.Log("GG >> Admob:iad:NoInventory ");
        });
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            Toolbox.GameManager.Log("GG >> Admob:iad:Displayed");
            IadStatus = AdLoadingStatus.NotLoaded;
        });
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        // this.interstitial.Destroy();
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            this.interstitial = new InterstitialAd(INTERTITIAL_ID);
            BindIntertitialEvents();
            Toolbox.GameManager.Log("GG >> Admob:iad:Closed");
            LoadInterstitial();
            IadStatus = AdLoadingStatus.NotLoaded;
        });
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        Debug.Log("GG >> Admob:iad:Clicked");
    }

    #endregion
    #region Small Banner Add Handler


    //******Intertitial Ad Handlers**********//
    public void SmallBanner_HandleOnAdLoaded(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            BanStatus = AdLoadingStatus.Loaded;
            Toolbox.GameManager.Log("GG >> Admob:smallBanner:Loaded");
            isSmallBannerLoaded = true;
        });

    }

    public void SmallBanner_HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            IadStatus = AdLoadingStatus.NoInventory;
            Toolbox.GameManager.Log("GG >> Admob:smallBanner:NoInventory :: ");
            isSmallBannerLoaded = false;
        });
    }

    public void SmallBanner_HandleOnAdOpened(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            IadStatus = AdLoadingStatus.NotLoaded;
            Toolbox.GameManager.Log("GG >> Admob:smallBanner:Displayed");
            isSmallBannerLoaded = false;
        });
    }
    public void SmallBanner_HandleOnAdClosed(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            IadStatus = AdLoadingStatus.NotLoaded;
            Toolbox.GameManager.Log("GG >> Admob:smallBanner:Closed");
            isSmallBannerLoaded = false;
        });
    }
    public void SmallBanner_HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        Toolbox.GameManager.Log("GG >> Admob:smallBanner:Clicked");
    }

    #endregion

    #region Medium Banner Add Handler


    //******Intertitial Ad Handlers**********//
    public void MediumBanner_HandleOnAdLoaded(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            MedBanStatus = AdLoadingStatus.Loaded;
            Toolbox.GameManager.Log("GG >> Admob:mediumBanner:Loaded");
            isMediumBannerLoaded = true;
        });
    }

    public void MediumBanner_HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            MedBanStatus = AdLoadingStatus.NoInventory;
            Toolbox.GameManager.Log("GG >> Admob:mediumBanner:NoInventory :: ");
            isMediumBannerLoaded = false;
        });
    }

    public void MediumBanner_HandleOnAdOpened(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            MedBanStatus = AdLoadingStatus.NotLoaded;
            isMediumBannerLoaded = false;
            Toolbox.GameManager.Log("GG >> Admob:mediumBanner:Displayed");
        });
    }

    public void MediumBanner_HandleOnAdClosed(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            MedBanStatus = AdLoadingStatus.NotLoaded;
            Toolbox.GameManager.Log("GG >> Admob:mediumBanner:Closed");
            isMediumBannerLoaded = false;
        });
    }

    public void MediumBanner_HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        Toolbox.GameManager.Log("GG >> Admob:mediumBanner:Clicked");
    }

    #endregion

    #region Video Ad Handlers
    //******Video Ad Handlers**********//
    public void Video_HandleOnAdLoaded(object sender, EventArgs args)
    {
        Toolbox.GameManager.Log("GG >> Admob:vad:Loaded");
        // Toolbox.GameManager.Analytics_DesignEvent("AdmobGAEvents VideoAdLoaded");
    }

    public void Video_HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Toolbox.GameManager.Log("GG >> Admob:vad:NoInventory :: ");
    }

    public void Video_HandleOnAdOpened(object sender, EventArgs args)
    {
        Toolbox.GameManager.Log("GG >> Admob:vad:Displayed");
    }

    public void Video_HandleOnAdClosed(object sender, EventArgs args)
    {
        Toolbox.GameManager.Log("GG >> Admob:vad:Closed");
        this.videoAd.Destroy();
        this.videoAd = new InterstitialAd(ADMOB_ID.ADMOB_VIDEO_AD_ID);
        BindVideoEvents();
        // LoadVideo();
    }

    public void Video_HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        Toolbox.GameManager.Log("GG >> Admob:vad:Clicked");
    }

    #endregion

    #region Rewarded Ad Handlers
    //***** Rewarded Events *****//
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            Toolbox.GameManager.Log("GG >> Admob:rad:Loaded");
            RadStatus = AdLoadingStatus.Loaded;
        });

    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            RadStatus = AdLoadingStatus.NoInventory;
            Toolbox.GameManager.Log("GG >> Admob:rad:NoInventory");
            MonoBehaviour.print(
          "HandleRewardedAdFailedToLoad event received with message: "
                           + args.LoadAdError);
        });

    }
    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            Toolbox.GameManager.Log("GG >> Admob:rad:Displayed");
            RadStatus = AdLoadingStatus.NotLoaded;
        });
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            Toolbox.GameManager.Log("GG >> Admob:rad:RewardedAdFailedToShow");
            RadStatus = AdLoadingStatus.NotLoaded;
        });


    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {

        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            Toolbox.GameManager.Log("GG >> Admob:rad:Started");
            RadStatus = AdLoadingStatus.NotLoaded;
        });

    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            Toolbox.GameManager.Log("GG >> Admob:rad:Closed");
            RadStatus = AdLoadingStatus.NotLoaded;
            if (curreward == RewardType.REVIVEREWARD && !Toolbox.GameplayController.IsRevived)
            {
                Time.timeScale = 1f;
                if (FindObjectOfType<GameplayController>())
                    FindObjectOfType<GameplayController>().LevelFailHandling();
            }

            this.rewardBasedVideo = new RewardedAd(ADMOB_ID.ADMOB_REWARDED_AD_ID);
            BindRewardedEvents();
            isRadPlaying = false;
            LoadRewardedVideo();
        });

    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {

        Toolbox.GameManager.Log("GG >> give reward to user after watching rAd");
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            Toolbox.GameManager.Log("GG >> give reward to user after watching rAd");
            RadStatus = AdLoadingStatus.NotLoaded;
            Time.timeScale = 1;
            HandleUserReward(curreward);
        });
        //NotifyReward();
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {

        //Debug.Log("GT >> On Ad Clicked");
        //  AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedAdClicked);
        // Toolbox.GameManager.Analytics_DesignEvent("AdmobGAEvents RewardedAdClicked");
    }

    void OnAdClosed()
    {
        isRadPlaying = false;
        LoadRewardedVideo();
    }

    void OnInterstitialClosed()
    {
        //LoadInterstitial();
    }

    void OnVideoClosed()
    {
        LoadVideo();
        //if (LoadingScreenForAds.Instance)
        //{
        //    LoadingScreenForAds.Instance.HideFakeLoading();
        //}
    }
    #endregion

    // Handle reward 
    private void HandleUserReward(RewardType curreward)
    {

        switch (curreward)
        {

            case RewardType.STORE_COINS:

                Toolbox.GameManager.Log("GG >> STORE_COINS has been rewarded");
                break;

            case RewardType.REVIVEREWARD:
                ReviveController.Set_PlayerHealth();
                Toolbox.GameManager.Log("GG >> RevivePlayer has been rewarded");
                break;

            case RewardType.FREEREWARD:

                Toolbox.GameManager.Freecoins_Onwatchvieo();
                Toolbox.GameManager.Log("GG >> FreeCoins has been rewarded");
                break;

            case RewardType.ADD_LEVEL_TIME:
                Toolbox.GameManager.Log("GG >> ADD_LEVEL_TIME has been rewarded");
                break;
            case RewardType.HEALTHONINJECTION:
                //Toolbox.HUDListner.Set_PlayerHealth();
                Toolbox.GameManager.Log("GG >> HEALTHONINJECTION has been rewarded");
                break;
            case RewardType.UNLOCK_NEXT_Level:
                Toolbox.DB.Prefs.Unlock_NextLevelOfCurrentGameMode();
                if (SceneManager.GetActiveScene().buildIndex == Constants.sceneIndex_Menu)
                {
                    FindObjectOfType<LevelSelectionListner>().RefreshView();
                }
                break;
            case RewardType.LEVEL_COMPLETE_2XCOINS:
                //if (FindObjectOfType<LevelCompleteListner>())
                //{
                //    FindObjectOfType<LevelCompleteListner>().Add_Double_Coins();
                //}
                //if (FindObjectOfType<SpecialMission_CompleteListener>())
                //{
                //    FindObjectOfType<SpecialMission_CompleteListener>().Add_Double_Coins();
                //}
                break;
            case RewardType.TRYWEAPON:

                if (FindObjectOfType<GunSelectionListner>())
                {
                    FindObjectOfType<GunSelectionListner>().Unlockweapon();

                }
                break;
            case RewardType.CLAIM_NEXT_DAY_DAILYREWARD:

                //if (FindObjectOfType<DailyRewardListner>())
                //    FindObjectOfType<DailyRewardListner>().OnPress_Claim();
                Toolbox.UIManager.UpdateTxts();

                break;

        }
    }

    public bool NoAdsPurchased()
    {

        if (PlayerPrefs.GetInt("NoAdsPurchased") == 1)
        {
            return true;
        }
        else
            return false;

    }

}
