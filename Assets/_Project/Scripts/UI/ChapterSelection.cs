using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChapterSelection : MonoBehaviour
{
    //public Text dailyRewardTimeTxt;
    //public Text ClassicModeOpenTimeTxt;
   // public GameObject Bg;
    public Transform content;
    public GameObject UnlockallBtn;



    private void OnEnable()
    {

        // Toolbox.GameManager.Add_ActiveUI(this.gameObject);

        // This only forCompaign Mode Which have the Further chpater and More Then One
        if (Toolbox.DB.Prefs.LastSelectedGameMode == 0)
        {
            ModeUnlockCheck();
            InitchapterButtonsState();
            CheckStatus_UnlockallChapter();
        }
    //    Bg.SetActive(true);
    }

    private void OnDisable()
    {
        //Toolbox.GameManager.Remove_ActiveUI(this.gameObject);
      //  Bg.SetActive(false);
        //StopCoroutine(CR_TimeHandling());
    }
    private void InitchapterButtonsState()
    {
        for (int i = 0; i < content.childCount-2; i++)
        {
            //chapterbtnListner chapterListner = content.GetChild(i).GetComponent<chapterbtnListner>();
            bool lvlUnlocked = Toolbox.DB.Prefs.Get_ModeUnlockStatus(i);
          //  chapterListner.Lock_Status(!lvlUnlocked);
        }
    }

    public void OnPress_chapterButton(GameObject _buttonObj)
    {
        this.GetComponentInParent<UIManager>().DirectShowingShop = false;
        Toolbox.GameManager.Analytics_DesignEvent("ChapSelection_Press_Play");
      //  Toolbox.UIManager.Go_Levelselection();
        for (int i = 0; i < content.childCount; i++)
        {
            if (_buttonObj == content.GetChild(i).gameObject)
            {
                //Toolbox.DB.Prefs.LastSelectedGameMode = i;
                Toolbox.DB.Prefs.LastSelectedchapter_of_gamemode = i;
                this.GetComponentInParent<UIManager>().ShowNextUI();
                Toolbox.GameManager.Permanent_Log("LastSelectedGameMode :" + Toolbox.DB.Prefs.LastSelectedGameMode);
                return;
            }
        }
      
    }
    private void ModeUnlockCheck()
    {
        if (Toolbox.DB.Prefs.Mode2Unlocked /*|| Toolbox.DB.Prefs.LastSelectedGameMode >0*/)
            return;
        if (Toolbox.DB.Prefs.Get_LastUnlockedLevelofCurrentGameMode() >= Constants.mode2UnlockAfterLevels && Toolbox.DB.Prefs.LastSelectedchapter_of_gamemode < 1)
        {
           
            int unlocked = Toolbox.DB.Prefs.Set_ModeUnlockStatus(1);

          
            Toolbox.DB.Prefs.GameData[unlocked].Modeunlocked = true;
            
            Toolbox.DB.Prefs.Mode2Unlocked = true;
            Toolbox.DB.Prefs.LastSelectedchapter_of_gamemode = unlocked;
            int chapter = unlocked + 1;
            //ClassicModeOpenTimeTxt.gameObject.SetActive(false);
            Toolbox.UIManager.MessagePopup.SetActive(true);
            Toolbox.UIManager.MessagePopup.GetComponent<MessageListner>().UpdateTxt("You have unlocked No " + chapter + " Chapter.", "Congratulations");
            // Toolbox.GameManager.Instantiate_Message("You have unlocked No " + chapter + " Chapter.", "Congratulations");
        }
    }

    public void CheckStatus_UnlockallChapter()
    {
        if (Toolbox.DB.Prefs.Unlockallchapter)
        {
            UnlockallBtn.SetActive(false);
            InitchapterButtonsState();
        }
        else
        {
            UnlockallBtn.SetActive(true);
        }
        Toolbox.GameManager.Permanent_Log("CheckStatus_UnlockallChapter");
    }
    #region ButtonListners


    public void OnPress_DailyReward()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
 
        Toolbox.GameManager.Analytics_DesignEvent("ModeSelection_Press_DailyReward");
        Toolbox.GameManager.FBAnalytic_EventDesign("ModeSelection_Press_DailyReward");

        //Toolbox.GameManager.Instantiate_DailyReward();
    }
    public void OnPress_Play()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        this.GetComponentInParent<UIManager>().DirectShowingShop = false;
        Toolbox.GameManager.Analytics_DesignEvent("ChapSelection_Press_Play");
        Toolbox.GameManager.FBAnalytic_EventDesign("ChapSelection_Press_Play");
        //Toolbox.UIManager.Go_Levelselection();
        this.GetComponentInParent<UIManager>().ShowNextUI();
    }
    public void OnPress_Back()
    {
        Toolbox.GameManager.Analytics_DesignEvent("ChapSelection_OnPress_Back");
        Toolbox.GameManager.FBAnalytic_EventDesign("ChapSelection_OnPress_Back");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
       // Toolbox.UIManager.Go_Back_From_Chapselection();
        this.GetComponentInParent<UIManager>().ShowPrevUI();
    }
    public void OnPress_UnlockAllChapter()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
      //  InAppHandler.Instance.Buy_AllChapters();
    }

    #endregion

}
