using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;


public enum AdLoadingStatus
{
    NotLoaded,
    Loading,
    Loaded,
    NoInventory
}
public enum RewardType
{
    STORE_COINS = 0,
    UNLOCK_NEXT_DAY = 1,
    UNLOCK_NEXT_Level = 1,
    FREEREWARD = 2,
    ADD_LEVEL_TIME = 3,
    LEVEL_COMPLETE_2XCOINS = 4,
    HEALTHONINJECTION = 5,
    REVIVEREWARD = 6,
    TRYWEAPON = 7,
    CLAIM_NEXT_DAY_DAILYREWARD = 8

}
public abstract class AbstractAdsmanager : MonoBehaviour
{
   
    public static RewardType givereward = RewardType.STORE_COINS;
    public static AdLoadingStatus IadStatus = AdLoadingStatus.NotLoaded;
    public static AdLoadingStatus RadStatus = AdLoadingStatus.NotLoaded;
    public static AdLoadingStatus BanStatus = AdLoadingStatus.NotLoaded;
    public static AdLoadingStatus MedBanStatus = AdLoadingStatus.NotLoaded;
    //public abstract void ShowRewardedVideo(Constants.RewardType);

    public abstract void ShowRewardedVideo(RewardType givereward);
    public abstract void ShowInterstitial();
    public abstract void ShowMediumBanner(AdPosition pos);
    public abstract void ShowSmallBanner(AdPosition pos);
    public abstract void HideBannners();

    public abstract bool IsSmallBannerReady();
    public abstract bool IsMediumBannerReady();
    public abstract bool IsInterstitialAdReady();
    public abstract bool IsVideoAdReady();
    public abstract bool IsRewardedAdReady();
    //public abstract void IsRewardedAdReady();

}
