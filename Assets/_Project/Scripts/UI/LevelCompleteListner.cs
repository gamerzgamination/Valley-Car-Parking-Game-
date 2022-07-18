using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//using GoogleMobileAds.Api;

public class LevelCompleteListner : MonoBehaviour
{
    public GameObject panel_2Buttons;
    public GameObject panel_3Buttons;
    //public GameObject doubleRewardBtn;
    public GameObject nextButton;

    public Text ObjectiveBonusTxt;
    public Text HeadshotBonusTxt;
    public Text totalCoinsTxt;
    private bool NextmodeUnlock = false;

    int curObjBonus = 0;
    int curHSBonus = 0;
    int curtotalCoins = 0;

    int ObjectiveBonus = 0;
    int HeadshotBonus = 0;
    int totalCoins = 0;
    int totaldoublecoins;

    bool showCoinsAnim = false;

    public Button DoubleReward;
    public Text doubleRewardCoinsTxt;

    int coinsReward = 0;
    int coinIncVal = 20;
    bool coinIncremented = false;

    //private void OnEnable()
    //{

    //}

    private void OnDisable()
    {
        StopAllCoroutines();
        Time.timeScale = 1;
    }

    private void Start()
    {


        Toolbox.GameManager.FBAnalytic_EventLevel_Complete(Toolbox.GameManager.Get_CurGameModeName(), Toolbox.DB.Prefs.LastSelectedchapter_of_gamemode, Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode());
        Toolbox.GameManager.Analytics_ProgressionEvent_Complete(Toolbox.GameManager.Get_CurGameModeName(), Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode());
        UnlockNextLevel();
        HandleRewards();
        RewardPlayer();
    }


    private void HandleRewards()
    {

        ObjectiveBonus = UnityEngine.Random.Range(300, 500); //Should be changed to proper implementation or different attribute
        HeadshotBonus = UnityEngine.Random.Range(100, 300); //Should be changed to proper implementation or different attribute
        totalCoins = (ObjectiveBonus + HeadshotBonus);

        ObjectiveBonusTxt.text = "";
        HeadshotBonusTxt.text = "";
        totalCoinsTxt.text = "";
        //Toolbox.GameManager.Log("totalcoins :"+totalCoins + "ObjectiveBonus :" + ObjectiveBonus + "HeadshotBonus :"+ HeadshotBonus);
        //Toolbox.GameManager.Log("totalCoinsTxt :" + totalCoinsTxt.text.ToString());
        //doubleRewardCoinsTxt.text = Mathf.RoundToInt(totalCoins * 2).ToString();

        coinsReward = (totalCoins);
        StartCoroutine(CR_CoinsAnimation());
    }

    IEnumerator CR_CoinsAnimation()
    {

        yield return new WaitForSeconds(1.5f);

        while (curObjBonus <= ObjectiveBonus && curObjBonus <= ObjectiveBonus - coinIncVal)
        {
            curObjBonus += coinIncVal;
            ObjectiveBonusTxt.text = curObjBonus.ToString();
            Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.singleCoinsSound);
            yield return new WaitForSeconds(0.012345f);
        }
        while (curHSBonus <= HeadshotBonus && curHSBonus <= HeadshotBonus - coinIncVal)
        {
            curHSBonus += coinIncVal;
            HeadshotBonusTxt.text = curHSBonus.ToString();
            Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.singleCoinsSound);
            yield return new WaitForSeconds(0.01234f);
        }

        totalCoins = (curObjBonus + curHSBonus);

        while (curtotalCoins <= totalCoins && curtotalCoins <= totalCoins - coinIncVal)
        {
            curtotalCoins += coinIncVal;
            totalCoinsTxt.text = curtotalCoins.ToString();
            Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.singleCoinsSound);
            yield return new WaitForSeconds(0.0123f);
        }
        totaldoublecoins = curtotalCoins * 2;
        doubleRewardCoinsTxt.text = Mathf.RoundToInt(curtotalCoins).ToString() + " x " + "2";

        //if (Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode() < (Toolbox.DB.Prefs.Get_LengthOfLevelsOfCurrentGameMode() - 1))
        //{
        //    panel_3Buttons.SetActive(true);
        //    panel_2Buttons.SetActive(false);

        //}
        //else
        //{
        //    panel_2Buttons.SetActive(true);
        //    panel_3Buttons.SetActive(false);
        //}
        // CR_ShowMegaOffer();
    }

    public void RewardPlayer()
    {

        Toolbox.DB.Prefs.GoldCoins += coinsReward;
    }

    private void UnlockNextLevel()
    {
        // Trial Period Compelete of Trial Weapon
        //if (Toolbox.DB.Prefs.Tryweapon)
        //{
        //    Toolbox.DB.Prefs.Tryweapon = false;
        //    Toolbox.DB.Prefs.WeaponUnlock_Try(Toolbox.DB.Prefs.Tyrweaponindex, false);
        //}


        //if (Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode() < Toolbox.DB.Prefs.Get_LastUnlockedLevelofCurrentGameMode())
        //    return;

        if (Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode() < (Toolbox.DB.Prefs.Get_LengthOfLevelsOfCurrentGameMode() - 1))
        {
            Toolbox.DB.Prefs.Unlock_NextLevelOfCurrentGameMode();

            if (Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode() > 0)
            {

                try
                {

                    if ((Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode() + 1) % 2 == 0)
                    {
                      
                            try
                            {
                                //if (FindObjectOfType<AbstractAdsmanager>())
                                //    FindObjectOfType<AbstractAdsmanager>().ShowInterstitial();
                            }
                            catch (Exception e)
                            {
                                //GameAnalytics.NewErrorEvent(GAErrorSeverity.Info, "AdsManager Instance Not Found!");


                            }

                       
                        //int index = Toolbox.DB.Prefs.GetLockedItemIndex(1);

                        //if (index >= 0)
                        //{
                        //    Toolbox.DB.Prefs.VehiclesUnlocked[index] = true;
                        //    Toolbox.DB.Prefs.LastSelectedVehicle = index;

                        //    Toolbox.AdsManager.Hide_BAd();
                        //    StartCoroutine(CR_ShowMsgWithDelay(index));
                        //}
                    }
                }
                catch (Exception ex)
                {
                }
            }
            // panel_2Buttons.SetActive(false);
            panel_3Buttons.SetActive(true);
        }
        else
        {    if (Toolbox.DB.Prefs.AreAllModesUnlocked())
            {
                Toolbox.HUDListner.Message.SetActive(true);
                Toolbox.HUDListner.Message.GetComponent<MessageListner>().UpdateTxt("All level has been completed ", "Congratulations");
                panel_2Buttons.SetActive(true);
            }
            else
            {
                if (Toolbox.DB.Prefs.LastSelectedGameMode < Toolbox.DB.Prefs.GameData.Length && !NextmodeUnlock)
                {
                    NextmodeUnlock = true;
                    Toolbox.DB.Prefs.LastSelectedGameMode += 1;
                    Toolbox.DB.Prefs.GameData[Toolbox.DB.Prefs.LastSelectedGameMode].Modeunlocked = true;
                    Toolbox.HUDListner.Message.SetActive(true);
                    Toolbox.HUDListner.Message.GetComponent<MessageListner>().UpdateTxt("You have unlocked No " + (Toolbox.DB.Prefs.LastSelectedGameMode + 1) + " Chapter.", "Congratulations");
                }
                panel_3Buttons.SetActive(true);
            }
        }
    }


    private void CR_ShowMegaOffer()
    {

        if ((Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode()) % 3 == 0)
        {
            Toolbox.GameManager.Permanent_Log("Modulus :" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode());
            if (!Toolbox.DB.Prefs.MegaOfferPurchased)
            {
                //AdsManager.Instance.HideBannners();
                Toolbox.HUDListner.Mega_OfferPanel.SetActive(true);
                Toolbox.GameManager.ShowMegaOfferOnComplete = true;
            }
        }
        else
        {
            //if (AdsManager.Instance)
            //    AdsManager.Instance.ShowMediumBanner(GoogleMobileAds.Api.AdPosition.BottomLeft);
        }
        //if (Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode() < (Toolbox.DB.Prefs.Get_LengthOfLevelsOfCurrentGameMode() - 1))
        //{
        //    panel_3Buttons.SetActive(true);
        //}
        //else
        //    panel_3Buttons.SetActive(false);

    }

    #region ButtonListners

    public void OnPress_Home()
    {
        ////Toolbox.ObjectiveHandler.UnloadAssetsFromMemory();
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.Back_to_mainmenu = true;
        Toolbox.GameManager.Analytics_DesignEvent(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "_" + "Cmplte_HomePress");
        Toolbox.GameManager.FBAnalytic_EventDesign(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "_" + "Cmplte_HomePress");
        Toolbox.DB.Prefs.Change_LastSelectedLevelOfCurrentGameMode(1);
        Toolbox.HUDListner.Loadingpanel.SetActive(true);
        Toolbox.GameManager.Load_MenuScene(true);
        this.gameObject.SetActive(false);
        // Destroy(this.gameObject);
    }

    public void OnPress_Next()
    {
        ////Toolbox.ObjectiveHandler.UnloadAssetsFromMemory();
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.Analytics_DesignEvent(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "_" + "LevelComplete_Next_Pressed");
        Toolbox.GameManager.FBAnalytic_EventDesign(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "_" + "LevelComplete_Next_Pressed");
        // Start next Mode Levels from Zero
        if (NextmodeUnlock)
        {
            NextmodeUnlock = false;
        }
        else
        {
            Toolbox.DB.Prefs.Change_LastSelectedLevelOfCurrentGameMode(1);
        }
        Toolbox.GameManager.Call_ad_after_restart = true;
        Toolbox.HUDListner.Loadingpanel.SetActive(true);
        Toolbox.GameManager.Load_GameScene(true, Toolbox.DB.Prefs.Get_LastSelectedGameModeSceneIndex(), 3f);
        //if ((Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode()) % 3 == 0)
        //{
        //    Toolbox.GameManager.DirectShowVehicleSelectionOnMenu = true;
        //    Toolbox.HUDListner.Loadingpanel.SetActive(true);
        //    Toolbox.GameManager.Load_MenuScene(true);
        //}
        //else
        //{
        //    Toolbox.HUDListner.Loadingpanel.SetActive(true);
        //    Toolbox.GameManager.Load_GameScene(true, Toolbox.DB.Prefs.Get_LastSelectedGameModeSceneIndex(), 3f);
        //}
        //if(Toolbox.DB.Prefs.Get_LastUnlockedLevelofCurrentGameMode() >= Constants.mode2UnlockAfterLevels && Toolbox.DB.Prefs.LastSelectedGameMode == 0 && !Toolbox.DB.Prefs.Mode2Unlocked)
        //{
        //    Toolbox.DB.Prefs.Mode2Unlocked = true;
        //    int unlocked = Toolbox.DB.Prefs.Set_ModeUnlockStatus(1);
        //    Toolbox.DB.Prefs.LastSelectedGameMode = unlocked;
        //    Toolbox.DB.Prefs.GameData[unlocked].Modeunlocked = true;
        //    int chapter = unlocked + 1;
        //}
        this.gameObject.SetActive(false);
        // Destroy(this.gameObject);
    }

    public void OnPress_Restart()
    {
        ////Toolbox.ObjectiveHandler.UnloadAssetsFromMemory();
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.Analytics_DesignEvent(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "_" + "LevelComplete_Restart_Pressed");
        Toolbox.GameManager.FBAnalytic_EventDesign(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "_" + "LevelComplete_Restart_Pressed");
        Toolbox.GameManager.Call_ad_after_restart = true;
        Toolbox.HUDListner.Loadingpanel.SetActive(true);
        if (NextmodeUnlock)
        {
            NextmodeUnlock = false;
            Toolbox.DB.Prefs.LastSelectedGameMode -= 1;
        }
        Toolbox.GameManager.Load_GameScene(true, Toolbox.DB.Prefs.Get_LastSelectedGameModeSceneIndex(), 3f);
        this.gameObject.SetActive(false);
        //   Destroy(this.gameObject);
    }

    public void OnPress_2xVideoReward()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        //AdsManager.Instance.ShowRewardedVideo(AdsManager.RewardType.LEVEL_COMPLETE_2XCOINS);
        Toolbox.GameManager.FBAnalytic_EventDesign(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "_" + "video_2x_Pressed");
        Toolbox.GameManager.Analytics_DesignEvent(Toolbox.GameManager.Get_CurGameModeName() + "_" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode().ToString() + "_" + "video_2x_Pressed");

    }
    public IEnumerator Double_CoinsAnimation()
    {

        yield return new WaitForSeconds(1.0f);

        while (curtotalCoins <= totaldoublecoins && curtotalCoins <= totaldoublecoins - coinIncVal)
        {
            curtotalCoins += coinIncVal;
            totalCoinsTxt.text = curtotalCoins.ToString();
            Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.singleCoinsSound);
            yield return new WaitForSeconds(0.0123f);
        }
        coinsReward = totaldoublecoins;
        RewardPlayer();
        DoubleReward.interactable = false;
        //StopCoroutine(Double_CoinsAnimation());
    }
    public void Add_Double_Coins()
    {
        StartCoroutine(Double_CoinsAnimation());
    }
    #endregion
}
