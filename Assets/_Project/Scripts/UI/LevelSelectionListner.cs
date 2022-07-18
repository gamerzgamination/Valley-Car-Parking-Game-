//using GameAnalyticsSDK;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
//using GoogleMobileAds.Api;
public class LevelSelectionListner : MonoBehaviour
{
   // public GameObject Bg;
    public Transform content;
    // public Text coinsTxt;
   
    public GameObject Mainmenubg;
    
    public GameObject PlayButon;
    public GameObject UnlockallBtn;
   
    private int tileWidth = 230;
    private int tileSpacing = 40;
    //public Text coinsTxt;

    private void OnEnable()
    {
      //  Toolbox.GameManager.Add_ActiveUI(this.gameObject);
        RefreshView();
  
        content.localPosition = new Vector3(
            -(Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode() * tileWidth)
            - (Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode() * tileSpacing), 0, 0);

       // Bg.SetActive(true);
    }

    private void OnDisable()
    {
       // Toolbox.GameManager.Remove_ActiveUI(this.gameObject);
      //  Bg.SetActive(false);

    }

    public void RefreshView() {

        InitLevelButtonsState();
        Toolbox.UIManager.UpdateTxts();
        CheckStatus_UnlockallLevels();
     
    }

    

    public void CheckStatus_UnlockallLevels()
    {
        if (Toolbox.DB.Prefs.Unlockalllevel)
        {
            UnlockallBtn.SetActive(false);
            InitLevelButtonsState();
        }
        else
        {
            UnlockallBtn.SetActive(true);
        }

    }


    private void InitLevelButtonsState() {

        for (int i = 0; i < content.childCount; i++)
        {
            content.GetChild(i).gameObject.SetActive(false);
        }

        bool watchVideoBtnEnabled = false;
        Toolbox.GameManager.Permanent_Log("InitLevelButtonsState :"+ Toolbox.DB.Prefs.LastSelectedGameMode);
        for (int i = 0; i < /*content.childCount*/Toolbox.DB.Prefs.GameData[Toolbox.DB.Prefs.LastSelectedGameMode].LevelUnlocked.Length; i++)
        {
            content.GetChild(i).gameObject.SetActive(true);
            LevelButtonListner btnListner = content.GetChild(i).GetComponent<LevelButtonListner>();

            bool lvlUnlocked = Toolbox.DB.Prefs.Get_LevelUnlockStatusOfCurrentGameMode(i);
            btnListner.Set_LevleNameTxt((i+1).ToString());
            if (lvlUnlocked)
            {
                btnListner.Lock_Status(!lvlUnlocked);
                btnListner.buttonObj.SetActive(true);
            }
            else
            {
                btnListner.Lock_Status(!lvlUnlocked);
                btnListner.buttonObj.SetActive(false);
            }
            //btnListner.Stars_Status(lvlUnlocked, Toolbox.DB.Prefs.Get_LevelStarsOfCurrentGameMode(i));

            // Just for checkingLevel status played or not
            //if (i > 0)
            //{
            //    //   content.GetChild(i - 1).GetComponent<LevelButtonListner>().check_LevelState(lvlUnlocked);
            //    content.GetChild(i - 1).GetComponent<LevelButtonListner>().Set_LevelstatusTxt("CLEARED");

            //}

            //Watch video Btn for Unlock Next Level
            //if (!watchVideoBtnEnabled && !lvlUnlocked)
            //{
            //    btnListner.WatchVideoUnlock_Status(true);
            //    watchVideoBtnEnabled = true;
            //}
            //else
            //    btnListner.WatchVideoUnlock_Status(false);

            //hightlight last selected level
            if (i == Toolbox.DB.Prefs.Get_LastUnlockedLevelofCurrentGameMode())
            {
              //  btnListner.buttonObj.SetActive(false);
               // btnListner.Set_NewLevelstatus(true);

            }
            else
            {
                //btnListner.Set_NewLevelstatus(false);
            }

        }
        PlayButon.SetActive(true);
    }
    
    #region ButtonListners

    public void OnPress_LevelButton(GameObject _buttonObj) 
    {

//        Toolbox.DB.Prefs.Set_LastSelectedLevelOfCurrentGameMode(Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode());

        if (Toolbox.GameManager.Godirectlevelselectionfrommode)
            Toolbox.GameManager.Loading_GameScene(true, Toolbox.DB.Prefs.Get_LastSelectedGameModeSceneIndex());
        else
            this.GetComponentInParent<UIManager>().ShowNextUI();
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
        Mainmenubg.SetActive(false);
        for (int i = 0; i < content.childCount; i++)
            {
               if (_buttonObj == content.GetChild(i).gameObject) 
               {
                  Toolbox.DB.Prefs.Set_LastSelectedLevelOfCurrentGameMode(i);
                  PlayButon.SetActive(true);
                  return;
               }
            }
    }
    public void OnPress_Play()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        if (Toolbox.GameManager.Godirectlevelselectionfrommode)
            Toolbox.GameManager.Loading_GameScene(true, Toolbox.DB.Prefs.Get_LastSelectedGameModeSceneIndex());
        else
        this.GetComponentInParent<UIManager>().ShowNextUI();
        Mainmenubg.SetActive(false);
        Toolbox.GameManager.Permanent_Log("LastSelectedLevelOfCurrentGameMode :" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode());
        Toolbox.GameManager.Permanent_Log("LastSelectedGameMode :" + Toolbox.DB.Prefs.LastSelectedGameMode);
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
    public void OnPress_Back() {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        if (Toolbox.GameManager.Godirectlevelselectionfrommode)
        {
            this.GetComponentInParent<UIManager>().ShowUI(1);
            Toolbox.GameManager.Godirectlevelselectionfrommode = false;
        }
        else
            this.GetComponentInParent<UIManager>().ShowPrevUI();
    }

    public void UnlockNextLevel_WatchVideo()
    {

        try
        {
            //if (FindObjectOfType<AdsManager>())
            //    FindObjectOfType<AdsManager>().ShowRewardedVideo(AdsManager.RewardType.UNLOCK_NEXT_Level);
        }

        catch (Exception e)
        {
            //GameAnalytics.NewErrorEvent(GAErrorSeverity.Info, "AdsManager Instance Not Found!");
        }
    }
    public void OnPress_Shop()
    {
        Toolbox.UIManager.Shop_Panel.SetActive(true);
    //    Toolbox.GameManager.InstantiateUI_Shop();
    }

    public void OnPress_UnlockAllLevel()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        InAppHandler.Instance.Buy_AllLevels();
    }
    #endregion
}
