//using BugsnagUnity;
//using Firebase.Analytics;
//using GameAnalyticsSDK;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour {

	[HideInInspector] public AsyncOperation async = null;
    [SerializeField] private bool shouldbegaInitialize = false;
    [SerializeField] private int levelsPlayed = 0;
    [SerializeField] private bool showMegaOfferOnComplete = false;
    [SerializeField] private bool showMegaOffer = false;
    [SerializeField] private bool godirectshopfromMenu= false;
    [SerializeField] private bool godirectlevelselectionfrommode = false;


    [SerializeField] private bool firstShowMegaOffer = false;
    [SerializeField] private bool reviveplayer = false;

    // These fields just related for ads 
    [SerializeField] private bool back_to_mainmenu = false;
    [SerializeField] private bool call_ad_after_restart = false;
    [SerializeField] private bool call_ad_before_gameplay = false;

    private bool directShowVehicleSelectionOnMenu = false;
    // UI Panels 
    //public GameObject PausePanel;
    //public GameObject FailPanel;
    //public GameObject CompletePanel;
    //public GameObject RevivePanel;
    //public GameObject WatchVideoPanel;
    //public GameObject ModeLockPopup;
    public GameObject MessagePopup;
    public GameObject PurchaseLaoding;
    public GameObject PrivacyPolicy;
    //public GameObject LowCoinUnlockCar_Panel;
    //public GameObject UnlockCarMsg_Panel;
    //public GameObject DailyReward_Panel;
    //public GameObject RateUs_Panel;
    //public GameObject Quit_Panel;
    //public GameObject Settings_Panel;
    //public GameObject Gameplay_Loading;
    //public GameObject UIDummy_Loading;
    //public GameObject UI_Loading;
    //public GameObject Shop_Panel;
    //public GameObject MegaOffers;


    public List<GameObject> activeUiObjects;

    public bool DirectShowVehicleSelectionOnMenu { get => directShowVehicleSelectionOnMenu; set => directShowVehicleSelectionOnMenu = value; }
    public int LevelsPlayed { get => levelsPlayed; set => levelsPlayed = value; }
    public bool ShowMegaOffer { get => showMegaOffer; set => showMegaOffer = value; }
    public bool FirstShowMegaOffer { get => firstShowMegaOffer; set => firstShowMegaOffer = value; }
    public bool Reviveplayer { get => reviveplayer; set => reviveplayer = value; }
    public bool ShouldBeGaInitialize { get => shouldbegaInitialize; set => shouldbegaInitialize = value; }
    public bool Back_to_mainmenu { get => back_to_mainmenu; set => back_to_mainmenu = value; }
    public bool ShowMegaOfferOnComplete { get => showMegaOfferOnComplete; set => showMegaOfferOnComplete = value; }
    public bool GodirectshopfromMenu { get => godirectshopfromMenu; set => godirectshopfromMenu = value; }
    public bool Call_ad_after_restart { get => call_ad_after_restart; set => call_ad_after_restart = value; }
    public bool Call_ad_before_gameplay { get => call_ad_before_gameplay; set => call_ad_before_gameplay = value; }
    public bool Godirectlevelselectionfrommode { get => godirectlevelselectionfrommode; set => godirectlevelselectionfrommode = value; }

    private void Start()
    {
        //activeUiObjects = new List<GameObject>();
        //if (ShouldBeGaInitialize) //If you want to use GA 
        //    GameAnalytics.Initialize();
        Toolbox.Soundmanager.PlayMusic_Menu();

        if (!Toolbox.DB.Prefs.UserConsent)
            PrivacyPolicy.SetActive(true);
        else
        Load_MenuScene(false, 8f);
      
    }

    //void Update()
    //{
    //    // Debug.LogError("Testing Error");

    //    //GameAnalytics.NewErrorEvent(GAErrorSeverity.Critical, "Test error Critical Error");
    //    //Test.SetActive(true);
    //    Bugsnag.Notify(new System.InvalidOperationException("Test error"));
    //}

    #region Common_Methods

    //public void Add_ActiveUI(GameObject _obj) {

    //    activeUiObjects.Add(_obj);
    //}

    //public void Remove_ActiveUI(GameObject _obj)
    //{
    //    activeUiObjects.Remove(_obj);
    //}

    public void LoadLevel(int _index, bool _showLoading)
    {
        StartCoroutine(CR_LoadScene(_index, _showLoading, 0f));
    }

    public void Load_MenuScene(bool _showLoading)
    {
        StartCoroutine(CR_LoadScene(Constants.sceneIndex_Menu, _showLoading, 3));
    }
    public void PP_Load_MenuScene(bool _showLoading)
    {
        StartCoroutine(CR_LoadScene(Constants.sceneIndex_Menu, _showLoading, 0));
    }
    public void Load_MenuScene(bool _showLoading,float _delay)
    {
        StartCoroutine(CR_LoadScene(Constants.sceneIndex_Menu, _showLoading, _delay));
    }
    public void loading_Delay(float delay)
    {
        StartCoroutine(Dummyloading(delay));
    }
    private IEnumerator Dummyloading(float delay)
    {
        Toolbox.UIManager.UIDummy_Loading.SetActive(true);
        yield return new WaitForSeconds(delay);
        Toolbox.UIManager.UIDummy_Loading.SetActive(false);
    }
    public void Load_GameScene(bool _showLoading, int _gameSceneIndex,float delay)
    {
        StartCoroutine(CR_LoadScene(_gameSceneIndex, _showLoading, delay));
    }
    public void Loading_GameScene(bool _showLoading, int _gameSceneIndex)
    {
        CR_LoadingScene(_gameSceneIndex, _showLoading);
    }
    private void CR_LoadingScene(int _sceneIndex, bool _showLoading)
    {
        //GameObject loading = null;
        //if (_showLoading)
        //{
        //    loading = InstantiateGameplay_Loading();  
        //}
        //Permanent_Log("Loading Scene");
        //   Toolbox.DB.Save_Binary_Prefs();
        
        if (_showLoading)
        {
            Toolbox.UIManager.Gameplay_Loading.SetActive(true);
        }
        Permanent_Log("Loading Scene");
    }
    private IEnumerator CR_LoadScene(int _sceneIndex, bool _showLoading, float duration)
    {
        //GameObject loading = null;
        //if (_showLoading)
        //{
        //    loading = InstantiateUI_Loading();
        //    yield return new WaitForSeconds(duration);
        //}
        //else
        //{
        //    yield return new WaitForSeconds(duration);
        //}
        yield return new WaitForSeconds(duration);

        Permanent_Log("Loading Scene");
        SceneManager.LoadScene(_sceneIndex);
        Toolbox.DB.Save_Json_Prefs();
     
    }


    public bool IsNetworkAvailable()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            return false;
        else
            return true;
    }

    public string Get_CurGameModeName() {

        if (Toolbox.DB.Prefs.LastSelectedGameMode == Constants.gameModeIndex_Mode1)
            return "Campaign";
        else if(Toolbox.DB.Prefs.LastSelectedGameMode == Constants.gameModeIndex_Mode2)
            return "CarbineMode";
        else if (Toolbox.DB.Prefs.LastSelectedGameMode == Constants.gameModeIndex_Mode3)
            return "SMGMode";
        else if (Toolbox.DB.Prefs.LastSelectedGameMode == Constants.gameModeIndex_Mode4)
            return "PistolMode";
        else if (Toolbox.DB.Prefs.LastSelectedGameMode == Constants.gameModeIndex_Mode5)
            return "MachineMode";
        else if (Toolbox.DB.Prefs.LastSelectedGameMode == Constants.gameModeIndex_Mode6)
            return "ShotMode";
        else if (Toolbox.DB.Prefs.LastSelectedGameMode == Constants.gameModeIndex_Mode7)
            return "SniperMode";
        else if (Toolbox.DB.Prefs.LastSelectedGameMode == Constants.gameModeIndex_Mode8)
            return "VectorMode";
        else if (Toolbox.DB.Prefs.LastSelectedGameMode == Constants.gameModeIndex_Mode9)
            return "ThumperMode";
        else if (Toolbox.DB.Prefs.LastSelectedGameMode == Constants.gameModeIndex_Mode10)
            return "GernadesMode";
        else if (Toolbox.DB.Prefs.LastSelectedGameMode == Constants.gameModeIndex_Mode11)
            return "MeleeMode";
        //else if (Toolbox.DB.Prefs.LastSelectedGameMode == Constants.gameModeIndex_Mode12)
        //    return "TrainingMode";
        //else if (Toolbox.DB.Prefs.LastSelectedGameMode == Constants.gameModeIndex_Mode13)
        //    return Constants.gameModeName_Mode12;
        //else if (Toolbox.DB.Prefs.LastSelectedGameMode == Constants.gameModeIndex_Mode14)
        //    return Constants.gameModeName_Mode13;
        else
            return "TrainingMode";

    }

    //Its a high cost function. Change if experience any performance issues in meneus
    public string Get_RateUsLink() {

        return Constants.link_StoreInitial + Application.identifier;
    }

    #endregion

    #region UI_Instantiation

    public void InstantiateUI_Main()
	{
		Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_Main), Vector3.zero, Quaternion.identity);
	}

	public void InstantiateUI_HUD()
	{
		Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_HUD), Vector3.zero, Quaternion.identity);
	}
    //public GameObject InstantiateGameplay_Loading()
    //{
    //    return (GameObject)Instantiate(Gameplay_Loading, Vector3.zero, Quaternion.identity);
    //    //   return (GameObject)Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiGameplay_Loading), Vector3.zero, Quaternion.identity);
    //}
 //   public GameObject InstantiateUI_Loading()
	//{
 //       return (GameObject)Instantiate(UI_Loading, Vector3.zero, Quaternion.identity);
 //       //    return (GameObject) Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_Loading), Vector3.zero, Quaternion.identity);
 //   }
    //public GameObject InstantiateUI_DummyLoading()
    //{
    //    return (GameObject)Instantiate(UIDummy_Loading, Vector3.zero, Quaternion.identity);
    //    //   return (GameObject)Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_DummmyLoading), Vector3.zero, Quaternion.identity);
    //}

 //   public void InstantiateUI_Pause()
	//{
 //        Instantiate(PausePanel, Vector3.zero, Quaternion.identity);
 //       //Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_Pause), Vector3.zero, Quaternion.identity);
 //   }

 //   public void InstantiateUI_Settings()
 //   {
 //       Instantiate(Settings_Panel, Vector3.zero, Quaternion.identity);
 //       //  Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_Settings), Vector3.zero, Quaternion.identity);
 //   }

    public void InstantiateUI_ModeSelection()
    {
        Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_ModeSelection), Vector3.zero, Quaternion.identity);
    }

    public void InstantiateUI_RampMissionLevelSelection()
    {
        Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_RampMissionLevelSelection), Vector3.zero, Quaternion.identity);
    }

    public void InstantiateUI_VehicleSelection()
    {
        Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_VehicleSelection), Vector3.zero, Quaternion.identity);
    }

 //   public void InstantiateUI_Quit()
 //   {
 //       Instantiate(Quit_Panel, Vector3.zero, Quaternion.identity);
 //       //   Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_Quit), Vector3.zero, Quaternion.identity);
 //   }

 //   public void InstantiateUI_Shop()
 //   {
 //       Instantiate(Shop_Panel, Vector3.zero, Quaternion.identity);
 //       //    Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_Store), Vector3.zero, Quaternion.identity);
 //   }
 //   public void InstantiateUI_RateUs()
 //   {
 //       Instantiate(RateUs_Panel, Vector3.zero, Quaternion.identity);
 //       //    Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_RateUs), Vector3.zero, Quaternion.identity);
 //   }
 //   public void InstantiateUI_LevelComplete()
	//{
 //       Instantiate(CompletePanel, Vector3.zero, Quaternion.identity);
 //       //	Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_LevelComplete), Vector3.zero, Quaternion.identity);
 //   }
 //   public void InstantiateUI_LevelFail()
	//{
 //       Instantiate(FailPanel, Vector3.zero, Quaternion.identity);
 //       //	Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_LevelFail), Vector3.zero, Quaternion.identity);
 //   }
 //   public void InstantiateUI_Revive()
 //   {
 //       Instantiate(RevivePanel, Vector3.zero, Quaternion.identity);
 //       //  Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_Revive), Vector3.zero, Quaternion.identity);
 //   }
  
    //public void Instantiate_DailyReward()
    //{
    //    Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_DailyReward), Vector3.zero, Quaternion.identity);
    //}
    //public void Instantiate_Sure()
    //{
    //    Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_Sure), Vector3.zero, Quaternion.identity);
    //}
    public void Instantiate_Message(string _str,string _heading)
    {
        GameObject obj = (GameObject)Instantiate(MessagePopup, Vector3.zero, Quaternion.identity);
        //   GameObject obj = (GameObject) Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_Message), Vector3.zero, Quaternion.identity);
        obj.GetComponent<MessageListner>().UpdateTxt(_str, _heading);
    }
    //public void Instantiate_SufficientMessage(string _str, string _heading)
    //{
    //    GameObject obj = (GameObject)Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_SufficientMessage), Vector3.zero, Quaternion.identity);
    //    obj.GetComponent<MessageListner>().UpdateTxt(_str, _heading);
    //}
    //public void Instantiate_ModeLockedMessage(string _str, string _heading)
    //{
    //    GameObject obj = (GameObject)Instantiate(ModeLockPopup, Vector3.zero, Quaternion.identity);
    //    //   GameObject obj = (GameObject)Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_ModeLockedMessage), Vector3.zero, Quaternion.identity);
    //    obj.GetComponent<MessageListner>().UpdateTxt(_str, _heading);
    //}

    //public void Instantiate_UnlockCarMsg(int _val)
    //{
    //    GameObject obj = (GameObject)Instantiate(UnlockCarMsg_Panel, Vector3.zero, Quaternion.identity);
    //    //   GameObject obj = (GameObject)Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_UnlockCarMsg), Vector3.zero, Quaternion.identity);
    //    obj.GetComponent<UnlockMsgListner>().EnableProduct(_val);
    //}
    //public void Instantiate_WatchVideoMsg(string _str, string _heading)
    //{
    //    GameObject obj = (GameObject)Instantiate(WatchVideoPanel, Vector3.zero, Quaternion.identity);
    //    //    GameObject obj = (GameObject)Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_WatchVideo), Vector3.zero, Quaternion.identity);
    //    obj.GetComponent<WatchVideoListner>().UpdateTxt(_str, _heading);
    //}
    public void Instantiate_CoinEffect()
    {
        Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiCoinEffect), Vector3.zero, Quaternion.identity);
        //Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.coinEffect);
    }

    //public void Instantiate_MegaOffer()
    //{
    //    Instantiate(Toolbox.GameManager.MegaOffers, Vector3.zero, Quaternion.identity) ;
    //    print("Instantiate_MegaOffer");
    //    //  Instantiate(Resources.Load(Constants.folderPath_UI + Constants.inAppId_megaOffer), Vector3.zero, Quaternion.identity);
    //}

    //public void Instantiate_LowCoinUnlockCar(int _val)
    //{
    //    GameObject obj = (GameObject)Instantiate(LowCoinUnlockCar_Panel, Vector3.zero, Quaternion.identity);
    //    //  GameObject obj = (GameObject)Instantiate(Resources.Load(Constants.folderPath_UI + Constants.uiName_LowCoinVehicleBuy), Vector3.zero, Quaternion.identity);
    //    obj.GetComponent<LowCoinVehicleBuy>().CurVehicle = _val;
    //}

    #endregion

    #region LOGS

    public void Log(string str)
    {
       Debug.Log("<color=green> GG -> </color>" + str);
    }

    public void Permanent_Log(string str)
    {
      Debug.Log("<color=Yellow>P-LOG -> </color>" + str);
    }

    public void Log(string str, string col)
    {
        //Debug.Log("<color=" + col + ">-> </color>" + str);
    }

    public void Log_ImplementationError(string str)
    {
      // Debug.Log("<color=red> Error -> </color>" + str);
    }

    public void Log_Analytic(string str)
    {
     //   Debug.Log("Analytic | " + str);
    }
    #endregion

    #region Analytics

    public void Analytics_ProgressionEvent_Start(string world,int chapter, int level)
    {
        Permanent_Log("START_" + "Mode_" + world + "Level_" + level + "_Start");
       
    //    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, world, "Chapter "+chapter + "Level " + level + "_Start");
    }

    public void Analytics_ProgressionEvent_Fail(string world, int level)
    {
        Permanent_Log("FAIL_" + "Mode_" + world + "Level_" + level + "_Fail");
       
   //    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, world, "Level " + level + "_Fail");
    }

    public void Analytics_ProgressionEvent_Complete(string world, int level)
    {
        Permanent_Log("COMPLETE_" + "Mode_" + world + "Level_" + level + "_Complete");
        
     //   GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, world, "Level " + level + "_Complete");
    }

    //Separate with ':' for multiple event Ids
    public void Analytics_DesignEvent(string _event)
    {
        Permanent_Log("DESIGN_" + _event);
      
    //    GameAnalytics.NewDesignEvent(_event);

    }

    #endregion

    #region Firebase-Analytics

    public void FBAnalytic_EventLevel_Started(string Mode, int chapter, int level)
    {
        Mode = Mode.Replace(" ", "_");

        try
        {
            //FirebaseAnalytics.LogEvent("lvl_start_" + Mode+ "_C_" + chapter + "_Lvl" + level);
            Log_Analytic("Level_started_" + "Mode_" + Mode + "_C_"+chapter + "_" + "Lvl_" + level);
        }
        catch (Exception e)
        {
            Log_Analytic("Error in Analytics:" + e.ToString());
        }
    }

    public void FBAnalytic_EventLevel_Complete(string Mode,int chapter, int level)
    {
        Mode = Mode.Replace(" ", "_");

        try
        {
          // FirebaseAnalytics.LogEvent("lvl_complete_" + Mode+ "_C_" + chapter + "_" + level);
            Log_Analytic("Lvl_complete_" + "Mode_" + Mode + "_C_" + chapter + "_" + "Level_" + level);
        }
        catch (Exception e)
        {
            Log_Analytic("Error in Analytics: " + e.ToString());
        }
    }

    public void FBAnalytic_EventLevel_Fail(string Mode, int chapter, int level)
    {
        Mode = Mode.Replace(" ", "_");
        try
        {
           // FirebaseAnalytics.LogEvent("lvl_fail_" + Mode+ "_C_" + chapter + "_" + level);
            Log_Analytic("Lvl_Fail_" + "Mode_" + Mode + "_C_" + chapter + "_" + "Level_" + level);
        }
        catch (Exception e)
        {
            Log_Analytic("Error in Analytics: " + e.ToString());
        }
    }

    public void FBAnalytic_EventDesign(string eventName)
    {
        eventName = eventName.Replace(" ", "_");
        try
        {
           // FirebaseAnalytics.LogEvent(eventName);
            Log_Analytic(eventName);
        }
        catch (Exception e)
        {
            Log_Analytic("Error in Analytics: " + e.ToString());
        }
    }

    public void NotificationsOpen(string eventname)
    {
        //try
        //{
        //    FirebaseAnalytics.LogEvent("Open_" + eventname);
        //    Permanent_Log("AnalyticsNotification: " + eventname);
        //}
        //catch (Exception e)
        //{
        //    Permanent_Log("Analytics_Design: Error in Analytics: " + e.ToString());
        //}
    }
    #endregion

    #region Rewards of Watch-Videos
    public void Freecoins_Onwatchvieo()
    {
        Toolbox.DB.Prefs.GoldCoins += 100;
        Toolbox.UIManager.UpdateTxts();
    }
    #endregion

    //private void OnApplicationFocus(bool focus)
    //{
    //    if (focus)
    //    {
    //        Debug.Log("GG >> Application comes back of focus");
    //    }

    //    if (!focus)
    //    {
    //        Debug.Log("GG >> Application out of focus");
    //      //  Toolbox.DB.Save_Json_Prefs();
    //    }
    //}

    public void ShowMessage(string str1,string str2)
    {
        MessagePopup.SetActive(true);
        Toolbox.GameManager.MessagePopup.GetComponent<MessageListner>().UpdateTxt(str1, str2);
    }
    public void PurchaseLoading()
    {
        if (Toolbox.GameManager.ShowMegaOfferOnComplete)
        {
            return;
        }
        PurchaseLaoding.SetActive(true);
        PurchaseLaoding.GetComponent<PurchaseLoading>().Title.text = "purchase processing . . . ".ToString();
    }
    public void AdsLoading()
    {
        PurchaseLaoding.GetComponent<PurchaseLoading>().AdLoading();
    }
    public bool NetworkAvailability()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
            return false;
        else
            return true;
    }
    public bool RemoveadsPurchased()
    {
        if (Toolbox.DB.Prefs.NoAdsPurchased == true)
            return true;
        else
            return false;
    }

    //private void OnApplicationFocus(bool focus)
    //{
    //    Toolbox.GameManager.Permanent_Log("OnApplicationFocus");
    //    Toolbox.DB.Save_Json_Prefs();
    //}
    void OnApplicationQuit()
    {
        //Toolbox.GameManager.Permanent_Log("OnApplicationQuit");
          Toolbox.DB.Save_Json_Prefs();
    }
    
}