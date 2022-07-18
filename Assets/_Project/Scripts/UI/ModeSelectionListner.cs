using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Coffee.UIEffects;

public class ModeSelectionListner : MonoBehaviour
{
    public GameObject content;
    public List<GameObject> ModesItem;
    public List<GameObject> EnableItem;
    public List<GameObject> Hover;
    //public Text coinsTxt;

    private void OnEnable()
    {
       
        //Toolbox.GameManager.Add_ActiveUI(this.gameObject);
        foreach (GameObject h in Hover)
            h.SetActive(false);
        ModesItem[Toolbox.DB.Prefs.LastSelectedGameMode].transform.GetChild(1).transform.GetComponent<UIShiny>().enabled = true;
        Hover[Toolbox.DB.Prefs.LastSelectedGameMode].SetActive(true);
        //if (!Toolbox.DB.Prefs.Modesautoscroller)
        //{
        //    LeanTween.moveX(content.GetComponent<RectTransform>(), -4755.472f, 1f).setDelay(1f).setOnComplete(scrollerback);
        //    //LeanTween.moveX(content.GetComponent<RectTransform>(), 0f, 1f).setDelay(5f);
        //    Toolbox.DB.Prefs.Modesautoscroller = true;
        //}
        InitchapterButtonsState();
       // UpdateTxts();
    }

    private void scrollerback()
    {
        LeanTween.moveX(content.GetComponent<RectTransform>(), 0f, 1f).setDelay(1f);
    }

    //public void UpdateTxts()
    //{
    //    coinsTxt.text = Toolbox.DB.Prefs.GoldCoins.ToString();
    //}
    private void OnDisable()
    {
        CancelInvoke();
    }
    public void InitchapterButtonsState()
    {
        for (int i = 0; i < Toolbox.DB.Prefs.GameData.Length/*content.transform.childCount*/; i++)
        {
            modebtnListner modebtnListner = content.transform.GetChild(i).GetComponent<modebtnListner>();
            bool lvlUnlocked = Toolbox.DB.Prefs.Get_ModeUnlockStatus(i);
             modebtnListner.Lock_Status(!lvlUnlocked);
        }
    }



    //private void Mode2UnlockCheck() {

    //    if (Toolbox.DB.Prefs.Mode2Unlocked)
    //        return;

    //    if (!Toolbox.DB.Prefs.Mode2Unlocked && Toolbox.DB.Prefs.Get_LastUnlockedLevelOfGameMode(0) >= Constants.mode2UnlockAfterLevels) {
    //        Toolbox.DB.Prefs.Mode2Unlocked = true;
    //        ClassicModeOpenTimeTxt.gameObject.SetActive(false);
    //        Toolbox.GameManager.Instantiate_Message("You have unlocked " + Constants.gameModeName_Mode2 + " Mode.", "Congratulations");
    //    }
    //}

    //public void DailyRewardTxtHandling() {

    //    if (DateTime.Now >= Toolbox.DB.Prefs.NextDailyRewardTime)
    //    {
    //        dailyRewardTimeTxt.text = "Ready";
    //    }

    //    else
    //    {
    //        StartCoroutine(CR_TimeHandling());
    //    }
    //}

    //IEnumerator CR_TimeHandling()
    //{
    //    while (true)
    //    {
    //        dailyRewardTimeTxt.text = Get_DailyRewardTimeString();
    //        yield return new WaitForSeconds(1);
    //    }
    //}

    //string Get_DailyRewardTimeString() {

    //    TimeSpan diff = Toolbox.DB.Prefs.NextDailyRewardTime - DateTime.Now;
    //    int hours = diff.Hours;
    //    hours += (diff.Days * 24);
    //    return string.Format("{0}H {1}M {2}S", hours, diff.Minutes, diff.Seconds);
    //}

    //public void ModeRewardTxtHandling()
    //{
    //    if (!Toolbox.DB.Prefs.Mode2Unlocked && DateTime.Now >= Toolbox.DB.Prefs.ClassicMode_UnlockDateTime)
    //    {
    //        Toolbox.DB.Prefs.Mode2Unlocked = true;
    //        ClassicModeOpenTimeTxt.gameObject.SetActive(false);
    //        Toolbox.GameManager.Instantiate_Message("You have unlocked After 48 Hours" + Constants.gameModeName_Mode2 + " Mode.", "Congratulations");
    //        StopCoroutine("Mode_TimeHandling");
    //    }
    //    else if (Toolbox.DB.Prefs.Mode2Unlocked) {

    //        ClassicModeOpenTimeTxt.gameObject.SetActive(false);
    //        StopCoroutine("Mode_TimeHandling");

    //    }
    //    else
    //    {
    //        StartCoroutine(Mode_TimeHandling());
    //    }
    //}
    //IEnumerator Mode_TimeHandling()
    //{
    //    while (true)
    //    {
    //        ClassicModeOpenTimeTxt.text = Get_ClassicModeTimeString();
    //        yield return new WaitForSeconds(1);
    //    }
    //}
    //string Get_ClassicModeTimeString()
    //{

    //    TimeSpan diff = Toolbox.DB.Prefs.ClassicMode_UnlockDateTime - DateTime.Now;
    //    int hours = diff.Hours;
    //    hours += (diff.Days * 24);
    //    return string.Format("{0}H {1}M {2}S", hours, diff.Minutes, diff.Seconds);
    //}


    #region ButtonListners
    public void OnPress_ModeButton(GameObject _buttonObj)
    {
        Toolbox.GameManager.Analytics_DesignEvent("ChapSelection_Press_Play");
        for (int i = 0; i < Toolbox.DB.Prefs.GameData.Length/* content.transform.childCount*/; i++)
        {
            if (_buttonObj == content.transform.GetChild(i).gameObject)
            {
                Toolbox.DB.Prefs.LastSelectedGameMode = i;
                this.GetComponentInParent<UIManager>().ShowNextUI();
                Toolbox.GameManager.Permanent_Log("LastSelectedGameMode :" + Toolbox.DB.Prefs.LastSelectedGameMode);
                return;
            }
        }

    }
    public void OnPress_Mode1(int mode)
    {

        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        this.GetComponentInParent<UIManager>().DirectShowingShop = false;

        foreach (GameObject h in Hover)
            h.SetActive(false);
        Hover[mode].SetActive(true);
        for (int u = 0; u < ModesItem.Count; u++)
        {
            ModesItem[u].transform.GetChild(0).transform.GetComponent<UIShiny>().enabled = false;
        }
        ModesItem[mode].transform.GetChild(0).transform.GetComponent<UIShiny>().enabled = true;
        this.GetComponentInParent<UIManager>().ShowNextUI();

        switch (mode)
        {
            case 0:
                Toolbox.DB.Prefs.LastSelectedGameMode = mode;
                Toolbox.GameManager.FBAnalytic_EventDesign("Press_campaignMode");
                break;

            case 1:
                Toolbox.DB.Prefs.LastSelectedGameMode = mode;
                Toolbox.DB.Prefs.LastSelectedchapter_of_gamemode = 0;
                Toolbox.GameManager.FBAnalytic_EventDesign("Press_AssualtRiffleMode");
                Toolbox.GameManager.Godirectlevelselectionfrommode = true;
                break;
            case 2:
                Toolbox.DB.Prefs.LastSelectedGameMode = mode;
                Toolbox.DB.Prefs.LastSelectedchapter_of_gamemode = 0;
                Toolbox.GameManager.FBAnalytic_EventDesign("Press_SMGGunMode");
                Toolbox.GameManager.Godirectlevelselectionfrommode = true;
                break;
            case 3:
                Toolbox.DB.Prefs.LastSelectedGameMode = mode;
                Toolbox.DB.Prefs.LastSelectedchapter_of_gamemode = 0;
                Toolbox.GameManager.FBAnalytic_EventDesign("Press_PistolGunMode");
                Toolbox.GameManager.Godirectlevelselectionfrommode = true;
                break;
        }
       

    }
    private void Delay_Due_to_Ad()
    {
       // Toolbox.UIManager.Go_Chapterselection();
        this.GetComponentInParent<UIManager>().ShowNextUI();
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
    }
    private void LevelSelection_Menu ()
    {
     //   Toolbox.UIManager.Go_Levelselection();
        this.GetComponentInParent<UIManager>().DirectShowLevelSelection();
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
    }
    public void OnPress_Mode2()
    {
            Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
            this.GetComponentInParent<UIManager>().DirectShowingShop = false;
            Toolbox.GameManager.FBAnalytic_EventDesign("ModeSelection_Press_Sniper");
            Toolbox.UIManager.ModeLockPopup.SetActive(true);
            Toolbox.UIManager.ModeLockPopup.GetComponent<MessageListner>().UpdateTxt("This mode will be available soon with all the amazing features.", "LOCKED");
          // Toolbox.GameManager.Instantiate_ModeLockedMessage("This mode will be available soon with all the amazing features.", "LOCKED");
            Invoke("Popupsound", 0.3f);
    }
    public void OnPress_Mode3()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        this.GetComponentInParent<UIManager>().DirectShowingShop = false;
        Toolbox.GameManager.FBAnalytic_EventDesign("ModeSelection_Press_Multiplayer");
        Toolbox.UIManager.ModeLockPopup.SetActive(true);
        Toolbox.UIManager.ModeLockPopup.GetComponent<MessageListner>().UpdateTxt("This mode will be available soon with all the amazing features.", "LOCKED");
        //Toolbox.GameManager.Instantiate_ModeLockedMessage("This mode will be available soon with all the amazing features.", "LOCKED");
        Invoke("Popupsound", 0.3f);
    }
    public void OnPress_Mode4()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        this.GetComponentInParent<UIManager>().DirectShowingShop = false;
        Toolbox.GameManager.FBAnalytic_EventDesign("ModeSelection_Press_Zoombie");
        Toolbox.UIManager.ModeLockPopup.SetActive(true);
        Toolbox.UIManager.ModeLockPopup.GetComponent<MessageListner>().UpdateTxt("This mode will be available soon with all the amazing features.", "LOCKED");
      //Toolbox.GameManager.Instantiate_ModeLockedMessage("This mode will be available soon with all the amazing features.", "LOCKED");
        Invoke("Popupsound",0.3f);
    }
    public void OnPress_StartMission()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.FBAnalytic_EventDesign("ModeSelection_Press_Shop");
        switch (Toolbox.DB.Prefs.LastSelectedGameMode)
        {
            case 0:
                Toolbox.GameManager.Godirectlevelselectionfrommode = false;
                Toolbox.GameManager.loading_Delay(5f);
                Invoke("Delay_Due_to_Ad", 5f);
                //if (Toolbox.DB.Prefs.Speciallevel)
                //{
                //    Toolbox.GameManager.Loading_GameScene(true, Toolbox.DB.Prefs.Get_LastSelectedGameModeSceneIndex());
                //}
                //else
                //{
                //    Toolbox.GameManager.loading_Delay(5f);
                //    Invoke("Delay_Due_to_Ad", 5f);
                //}
                break;

            case 1:
                Toolbox.GameManager.loading_Delay(5f);
                Toolbox.GameManager.Godirectlevelselectionfrommode = true;
                Invoke("LevelSelection_Menu", 5.01f);
                break;
            case 2:
                Toolbox.GameManager.loading_Delay(5f);
                Toolbox.GameManager.Godirectlevelselectionfrommode = true;
                Invoke("LevelSelection_Menu", 5.01f);
                break;
            case 3:
                Toolbox.GameManager.loading_Delay(5f);
                Toolbox.GameManager.Godirectlevelselectionfrommode = true;
                Invoke("LevelSelection_Menu", 5.01f);
                break;
            case 4:
                Toolbox.GameManager.loading_Delay(5f);
                Toolbox.GameManager.Godirectlevelselectionfrommode = true;
                Invoke("LevelSelection_Menu", 5.01f);
                break;
            case 5:
                Toolbox.GameManager.loading_Delay(5f);
                Toolbox.GameManager.Godirectlevelselectionfrommode = true;
                Invoke("LevelSelection_Menu", 5.01f);
                break;
            case 6:
                Toolbox.GameManager.loading_Delay(5f);
                Toolbox.GameManager.Godirectlevelselectionfrommode = true;
                Invoke("LevelSelection_Menu", 5.01f);
                break;
            case 7:
                Toolbox.GameManager.loading_Delay(5f);
                Toolbox.GameManager.Godirectlevelselectionfrommode = true;
                Invoke("LevelSelection_Menu", 5.01f);
                break;
            case 8:
                Toolbox.GameManager.loading_Delay(5f);
                Toolbox.GameManager.Godirectlevelselectionfrommode = true;
                Invoke("LevelSelection_Menu", 5.01f);
                break;
            case 9:
                Toolbox.GameManager.loading_Delay(5f);
                Toolbox.GameManager.Godirectlevelselectionfrommode = true;
                Invoke("LevelSelection_Menu", 5.01f);
                break;
            case 10:
                Toolbox.GameManager.loading_Delay(5f);
                Toolbox.GameManager.Godirectlevelselectionfrommode = true;
                Invoke("LevelSelection_Menu", 5.01f);
                break;
            case 11:
                Toolbox.GameManager.Loading_GameScene(true, Toolbox.DB.Prefs.Get_LastSelectedGameModeSceneIndex());
                break;
            case 12:
                Toolbox.GameManager.loading_Delay(5f);
                Toolbox.GameManager.Godirectlevelselectionfrommode = true;
                Invoke("LevelSelection_Menu", 5.01f);
                break;
        }
    }
    public void OnPress_Back()
    {
        Toolbox.GameManager.Analytics_DesignEvent("ModeSelection_OnPress_Back");
        Toolbox.GameManager.FBAnalytic_EventDesign("ModeSelection_OnPress_Back");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        this.GetComponentInParent<UIManager>().ShowPrevUI();
    }
    public void OnPress_UnlockAllChapter()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
         InAppHandler.Instance.Buy_AllModes();
    }
    //public void OnPress_DailyReward()
    //{
    //    Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.OnPresslockedbutton); ;
    //    Toolbox.GameManager.FBAnalytic_EventDesign("ModeSelection_Press_DailyReward");
    //    Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.OnAnyPopupAppear);
    //}
    //public void OnPress_Shop()
    //{
    //    Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.weaponPress);
    //    Toolbox.UIManager.Go_DirectWeaponShop();
    //    Toolbox.GameManager.FBAnalytic_EventDesign("ModeSelection_Press_Shop");
    //    this.GetComponentInParent<UIManager>().DirectShowShop();
    //    Toolbox.GameManager.GodirectshopfromMenu = true;
    //}
    public void OnPress_ModeLockButton()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.UIManager.ModeLockPopup.SetActive(true);
        Toolbox.UIManager.ModeLockPopup.GetComponent<MessageListner>().UpdateTxt("This Mode is currently locked. Coming Soon!", "LOCKED");
        // Toolbox.GameManager.Instantiate_ModeLockedMessage("This chapter is currently locked. Play atleast "+(Constants.mode2UnlockAfterLevels + 1)+ " levels of current chapter to unlock the glory of this chapter", "LOCKED");
    }
    private void Popupsound()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
    }
    
    #endregion

}
