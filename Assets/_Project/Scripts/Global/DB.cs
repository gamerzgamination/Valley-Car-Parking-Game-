using System;
using UnityEngine;

[System.Serializable]
public class Prefs_Data
{


    [SerializeField] private bool gameAudio = true;
    [SerializeField] private bool gameSound = true;

    [SerializeField] private float soundVolume = 1.0f;
    [SerializeField] private float musicVolume = 0.4f;

    [SerializeField] private int goldCoins = 0;
    [SerializeField] private int highScore = 0;

    [SerializeField] private string playerName;
    [SerializeField] private bool fbLoggedIn = false;

    [SerializeField] private bool soundMute = false;

    [SerializeField] private bool modesautoscroller = false;
    [SerializeField] private bool firstRun = true;
    [SerializeField] private bool megaofferpanelShowed = false;
    [SerializeField] private bool appRated = false;
    [SerializeField] private bool userConsent = false;

    [SerializeField] private int lastLevelStartAnimation = 0;

    [SerializeField] private GameData[] gamedata;
    //[SerializeField] private GameMode[] gamemode;

    [SerializeField] private int lastSelectedGameMode = 0;
    [SerializeField] private int lastSelectedchapter_of_gamemode = 0;
    [SerializeField] private string lastSelectedscenename = "";

    [SerializeField] private bool mode2Unlocked = false;
    [SerializeField] private bool noAdsPurchased = false;
    [SerializeField] private bool megaOfferPurchased = false;


    [SerializeField] private int lastSelectedVehicle = 0;
    [SerializeField] private bool[] vehiclesUnlocked;


    [SerializeField] private int dailyRewardDay = 0;
    [SerializeField] private DateTime nextDailyRewardTime;
    [SerializeField] private string dailyRewardTime;
    [SerializeField] private int defaultController;

    //[SerializeField] private DateTime dailyhealthinjectiontime;
    //[SerializeField] private string healthinjectiontime;
    //[SerializeField] private DateTime airdroptime;
    //[SerializeField] private string airdropdatetime;

    //[SerializeField] private DateTime classicMode_unlockDateTime= DateTime.Now.AddDays(2);
    //[SerializeField] private float playerSensitivityRecomended;
    //[SerializeField] private bool autoshoot;
    //[SerializeField] private bool autoAiming;
    //[SerializeField] private float powerbar;
    //[SerializeField] private float rankbar;
    [SerializeField] private bool unlockalllevel;
    [SerializeField] private bool unlockallguns;
    [SerializeField] private bool unlockallchapter;
    //[SerializeField] private bool tryweapon = false;
    //[SerializeField] private int tyrweaponindex = 0;
    //[SerializeField] private bool speciallevel = true;
    //[SerializeField] private bool tutorialshowfirsttime = false;
    private int dynamicDailyRewardItemNumber1 = -1;

    [SerializeField] private int scheduledNotificationId = -1;


    public int Get_LastUnlockedLevelofCurrentGameMode()
    {
        for (int i = 0; i < GameData[LastSelectedGameMode].LevelUnlocked.Length; i++)
        {
            if (!GameData[LastSelectedGameMode].LevelUnlocked[i])
            {
                return i - 1;
            }
        }
        return GameData[LastSelectedGameMode].LevelUnlocked.Length - 1;
        //for (int i = 0; i < Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked.Length; i++)
        //{
        //    if (!Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked[i])
        //    {
        //        if (i > Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked.Length)

        //            return Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked.Length - 1;
        //        else
        //            return i - 1;
        //    }
        //}
        //return Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked.Length - 1;
    }

    public int Get_LastUnlockedLevelOfGameMode(int _mode)
    {

        for (int i = 0; i < GameData[_mode].LevelUnlocked.Length; i++)
        {
            if (!GameData[LastSelectedGameMode].LevelUnlocked[i])
            {
                return i - 1;
            }
        }
        return GameData[LastSelectedGameMode].LevelUnlocked.Length - 1;
        //for (int i = 0; i < Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked.Length; i++)
        //{
        //    if (!Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked[i])
        //    {
        //        if (i >= Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked.Length)
        //            return Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked.Length - 1;
        //        else
        //            return i - 1;
        //    }
        //}

        //return Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked.Length - 1;

    }

    public int Get_LastSelectedLevelOfCurrentGameMode()
    {

        //  return GameData[LastSelectedGameMode].LastselectedlevelofMode;
        //if (Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LastselectedlevelofChapter > Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked.Length)
        //    return Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked.Length-1;
        //else
        //return Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LastselectedlevelofChapter;
        if (GameData[LastSelectedGameMode].LastselectedlevelofChapter > GameData[LastSelectedGameMode].LevelUnlocked.Length)
            return GameData[LastSelectedGameMode].LevelUnlocked.Length - 1;
        else
            return GameData[LastSelectedGameMode].LastselectedlevelofChapter;
    }

    public void Set_LastSelectedLevelOfCurrentGameMode(int _level)
    {
        if (GameData[LastSelectedGameMode].LastselectedlevelofChapter > GameData[LastSelectedGameMode].LevelUnlocked.Length)
            return;
        else
            GameData[LastSelectedGameMode].LastselectedlevelofChapter = _level;
    }

    public int Get_LengthOfLevelsOfCurrentGameMode()
    {
        return GameData[LastSelectedGameMode].LevelUnlocked.Length;
        // return Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked.Length;
    }

    public void Change_LastSelectedLevelOfCurrentGameMode(int _val)
    {
        //    GameData[LastSelectedGameMode].LastselectedlevelofMode += _val;
        //if (Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LastselectedlevelofChapter > Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked.Length)
        //    return;
        //else
        //Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LastselectedlevelofChapter += _val;
        if (GameData[LastSelectedGameMode].LastselectedlevelofChapter > GameData[LastSelectedGameMode].LevelUnlocked.Length)
            return;
        else
            GameData[LastSelectedGameMode].LastselectedlevelofChapter += _val;
    }


    public int Get_LastSelectedGameModeSceneIndex()
    {
        if (LastSelectedGameMode == 0 )
        {
            lastSelectedscenename = Constants.gameModeName_Mode1;
            return Constants.sceneIndex_GameMode1;
        }
        else if (LastSelectedGameMode == 1 )
        {
            lastSelectedscenename = Constants.gameModeName_Mode2;
            return Constants.sceneIndex_GameMode2;
        }
        else if (LastSelectedGameMode == 2 )
        {
            lastSelectedscenename = Constants.gameModeName_Mode3;
            return Constants.sceneIndex_GameMode3;
        }
        else if (LastSelectedGameMode == 3)
        {
            lastSelectedscenename = Constants.gameModeName_Mode4;
            return Constants.sceneIndex_GameMode4;
        }
        else if (LastSelectedGameMode == 4)
        {
            lastSelectedscenename = Constants.gameModeName_Mode5;
            return Constants.sceneIndex_GameMode5;
        }
        else
        {
            lastSelectedscenename = Constants.gameModeName_Mode1;
            return Constants.sceneIndex_trainingmode;
        }
    }

    public void Unlock_NextLevelOfCurrentGameMode()
    {

        ////if (!AreAllLevelsUnlocked())
        ////    Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked[Get_LastUnlockedLevelOfGameMode(LastSelectedGameMode)+1]= true
        if (!AreAllLevelsUnlocked())
            GameData[LastSelectedGameMode].LevelUnlocked[Get_LastUnlockedLevelOfGameMode(LastSelectedGameMode) + 1] = true;

    }

    public bool Get_LevelUnlockStatusOfCurrentGameMode(int _level)
    {
        //return Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked[_level];
        return GameData[LastSelectedGameMode].LevelUnlocked[_level];

    }

    public bool Get_ModeUnlockStatus(int mode)
    {
       
        if (mode > GameData.Length)
            return true;
        else
            return GameData[mode].Modeunlocked;
    }

    public int Set_ModeUnlockStatus(int mode)
    {
        //for (int i = 0; i < Gamemode[LastSelectedGameMode].Gamedata.Length; i++)
        //{
        //    //Debug.Log("i :" + i);
        //    if (!Gamemode[LastSelectedGameMode].Gamedata[i].Modeunlocked)
        //    {
        //        mode--;

        //        if (mode == 0)
        //        {

        //            return i;
        //        }
        //    }
        //}

        ////Less items than value are locked
        //return -1;
        for (int i = 0; i < GameData.Length; i++)
        {
            //Debug.Log("i :" + i);
            if (!GameData[i].Modeunlocked)
            {
                mode--;

                if (mode == 0)
                {

                    return i;
                }
            }
        }

        //Less items than value are locked
        return -1;
    }

    public int Get_LevelStarsOfCurrentGameMode(int _level)
    {
        return GameData[LastSelectedGameMode].Levelstar[_level];
        //return Gamemode[lastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].Levelstar[_level];
    }

    //wiil return the _val number locked item from the store. e.g 1 will return the first locked
    public int GetLockedItemIndex(int _val)
    {

        for (int i = 0; i < vehiclesUnlocked.Length; i++)
        {
            //  Debug.Log("i :"+i); 
            if (!vehiclesUnlocked[i])
            {

                _val--;

                if (_val == 0)
                {

                    return i;
                }
            }
        }

        //Less items than value are locked
        return -1;
    }
    public void WeaponUnlock_Try(int weapon, bool _val)
    {
        vehiclesUnlocked[weapon] = _val;
    }

    public bool AreAllLevelsUnlocked()
    {
        //for (int i = 0; i < Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked.Length; i++)
        //{

        //    if (!Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked[i])
        //        return false;

        //}
        //return true;
        for (int i = 0; i < GameData[LastSelectedGameMode].LevelUnlocked.Length; i++)
        {

            if (!GameData[LastSelectedGameMode].LevelUnlocked[i])
                return false;

        }
        return true;
    }
    public void AllLevelslocked()
    {
        //for (int i = 0; i < Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked.Length; i++)
        //{
        //    if (i == 0)
        //    {
        //        Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked[i] = true;
        //        Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LastselectedlevelofChapter = i;
        //    }
        //    else
        //        Gamemode[LastSelectedGameMode].Gamedata[lastSelectedchapter_of_gamemode].LevelUnlocked[i] = false;
        //}
        for (int i = 0; i < GameData[LastSelectedGameMode].LevelUnlocked.Length; i++)
        {
            if (i == 0)
            {
                GameData[LastSelectedGameMode].LevelUnlocked[i] = true;
                GameData[LastSelectedGameMode].LastselectedlevelofChapter = i;
            }
            else
                GameData[LastSelectedGameMode].LevelUnlocked[i] = false;
        }
    }
    public bool AreAllGunsUnlocked()
    {
        for (int i = 0; i < vehiclesUnlocked.Length; i++)
        {
            if (!vehiclesUnlocked[i])
            {

                //Debug.LogError("All vehicles --NOT-- unlocked");
                return false;
            }

        }
        //Debug.LogError("All vehicles are unlocked");
        return true;
    }

    public void UnlockAllGuns()
    {

        for (int i = 0; i < vehiclesUnlocked.Length; i++)
        {
            if (!vehiclesUnlocked[i])
            {
                vehiclesUnlocked[i] = true;
            }
        }
        unlockallguns = true;
    }
    public void AllChapters_of_Mode_Unlock()
    {
        for (int i = 0; i < GameData.Length; i++)
        {
            GameData[i].Modeunlocked = true;
        }
        unlockallchapter = true;
        //for (int i = 0; i < Gamemode[lastSelectedGameMode].Gamedata.Length - 2; i++)
        //{
        //    Gamemode[lastSelectedGameMode].Gamedata[i].Modeunlocked = true;
        //}
        //unlockallchapter = true;
    }
    public bool AreAllModesUnlocked()
    {
        for (int i = 0; i < GameData.Length; i++)
        {
            if (!GameData[i].Modeunlocked)
            {

                //Debug.LogError("All vehicles --NOT-- unlocked");
                return false;
            }

        }
        //Debug.LogError("All vehicles are unlocked");
        return true;
    }
    //public void AllModeUnlock()
    //{
    //    //for (int i = 0; i < GameData.Length-2; i++)
    //    //{
    //    //    GameData[i].Modeunlocked = true;
    //    //}
    //    //unlockallchapter = true;
    //    for (int i = 0; i < Gamemode[lastSelectedGameMode].Gamedata.Length - 2; i++)
    //    {
    //        Gamemode[lastSelectedGameMode].Gamedata[i].Modeunlocked = true;
    //    }
    //    unlockallchapter = true;
    //}
    public void UnlockAllLevels()
    {


        //for (int m = 0; m < Gamemode.Length; m++)
        //{
        //    for (int i = 0; i < Gamemode[m].Gamedata.Length; i++)
        //    {
        //        for (int l = 0; l < Gamemode[m].Gamedata[i].LevelUnlocked.Length; l++)
        //        {
        //            if (!Gamemode[m].Gamedata[i].LevelUnlocked[l])
        //            {
        //                Gamemode[m].Gamedata[i].LevelUnlocked[l] = true;
        //            }
        //        }
        //    }
        //}
        //unlockalllevel = true;
        //Mode2Unlocked = true;

        for (int m = 0; m < GameData.Length; m++)
        {
            for (int l = 0; l < GameData[m].LevelUnlocked.Length; l++)
            {
                if (!GameData[m].LevelUnlocked[l])
                {
                    GameData[m].LevelUnlocked[l] = true;
                }
            }
        }
        unlockalllevel = true;
        Mode2Unlocked = true;
    }

    public bool GameAudio { get => gameAudio; set => gameAudio = value; }
    public bool GameSound { get => gameSound; set => gameSound = value; }
    public int GoldCoins
    {
        get => goldCoins; set
        {

            goldCoins = value;
            //Toolbox.GameManager.UpdateCoinsTxtHandling();
        }
    }
    public int HighScore { get => highScore; set => highScore = value; }
    public bool FirstRun { get => firstRun; set => firstRun = value; }
    public bool AppRated { get => appRated; set => appRated = value; }
    public string PlayerName { get => playerName; set => playerName = value; }
    public bool FbLoggedIn { get => fbLoggedIn; set => fbLoggedIn = value; }
    public bool SoundMute { get => soundMute; set => soundMute = value; }
    public GameData[] GameData { get => gamedata; set => gamedata = value; }
    public int LastSelectedVehicle { get => lastSelectedVehicle; set => lastSelectedVehicle = value; }
    public bool[] VehiclesUnlocked { get => vehiclesUnlocked; set => vehiclesUnlocked = value; }
    public int LastSelectedGameMode { get => lastSelectedGameMode; set => lastSelectedGameMode = value; }
    public DateTime NextDailyRewardTime { get => nextDailyRewardTime; set => nextDailyRewardTime = value; }
    //public DateTime ClassicMode_UnlockDateTime { get => classicMode_unlockDateTime; set => classicMode_unlockDateTime = value; }
    public int DailyRewardDay { get => dailyRewardDay; set => dailyRewardDay = value; }
    public bool NoAdsPurchased { get => noAdsPurchased; set => noAdsPurchased = value; }
    public float SoundVolume { get => soundVolume; set => soundVolume = value; }
    public float MusicVolume { get => musicVolume; set => musicVolume = value; }
    public int DynamicDailyRewardItemNumber1 { get => dynamicDailyRewardItemNumber1; set => dynamicDailyRewardItemNumber1 = value; }
    public bool UserConsent { get => userConsent; set => userConsent = value; }
    public bool Mode2Unlocked { get => mode2Unlocked; set => mode2Unlocked = value; }
    public int ScheduledNotificationId { get => scheduledNotificationId; set => scheduledNotificationId = value; }
    public int LastLevelStartAnimation { get => lastLevelStartAnimation; set => lastLevelStartAnimation = value; }
    public bool MegaOfferPurchased { get => megaOfferPurchased; set => megaOfferPurchased = value; }
    //public float PlayerSensitivityRecomended { get => playerSensitivityRecomended; set => playerSensitivityRecomended = value; }
    //public bool Autoshoot { get => autoshoot; set => autoshoot = value; }
    //public float Powerbar { get => powerbar; set => powerbar = value; }
    //public float Rankbar { get => rankbar; set => rankbar = value; }
    public bool Unlockalllevel { get => unlockalllevel; set => unlockalllevel = value; }
    public bool Unlockallguns { get => unlockallguns; set => unlockallguns = value; }
    public string LastSelectedscenename { get => lastSelectedscenename; set => lastSelectedscenename = value; }
    public bool Unlockallchapter { get => unlockallchapter; set => unlockallchapter = value; }
    //public DateTime Dailyhealthinjectiontime { get => dailyhealthinjectiontime; set => dailyhealthinjectiontime = value; }
    //public DateTime Airdroptime { get => airdroptime; set => airdroptime = value; }
    //public GameMode[] Gamemode { get => gamemode; set => gamemode = value; }
    public int LastSelectedchapter_of_gamemode { get => lastSelectedchapter_of_gamemode; set => lastSelectedchapter_of_gamemode = value; }
    public bool Modesautoscroller { get => modesautoscroller; set => modesautoscroller = value; }
    //public bool Tryweapon { get => tryweapon; set => tryweapon = value; }
    //public int Tyrweaponindex { get => tyrweaponindex; set => tyrweaponindex = value; }
    //public bool AutoAiming { get => autoAiming; set => autoAiming = value; }
    //public bool Speciallevel { get => speciallevel; set => speciallevel = value; }
    //public string Healthinjectiontime { get => healthinjectiontime; set => healthinjectiontime = value; }
    //public string Airdropdatetime { get => airdropdatetime; set => airdropdatetime = value; }
    public string DailyRewardTime { get => dailyRewardTime; set => dailyRewardTime = value; }
    public int DefaultController { get => defaultController; set => defaultController = value; }
}

[System.Serializable]
public class GameData
{
    public string Name;
    [SerializeField] private bool[] levelUnlocked;
    [SerializeField] private int[] levelstar;
    [SerializeField] private int lastselectedlevelofchapter;
    [SerializeField] private bool modeunlocked;
    //[SerializeField] private bool specialMissionhave = false;
    public bool[] LevelUnlocked { get => levelUnlocked; set => levelUnlocked = value; }
    public int[] Levelstar { get => levelstar; set => levelstar = value; }
    public int LastselectedlevelofChapter { get => lastselectedlevelofchapter; set => lastselectedlevelofchapter = value; }
    public bool Modeunlocked { get => modeunlocked; set => modeunlocked = value; }
    // public bool SpecialMissionhave { get => specialMissionhave; set => specialMissionhave = value; }

    public int GetlastUnlockedLevel()
    {
        for (int i = 0; i < levelUnlocked.Length; i++)
        {
            if (!levelUnlocked[i])
            {
                return i - 1;
            }
        }
        return levelUnlocked.Length - 1;
    }

}
[System.Serializable]
//public class GameMode
//{
//    public string Name;
//    [SerializeField]private GameData[] gamedata;
//    public GameData[] Gamedata { get => gamedata; set => gamedata = value; }
//}


public class DB : MonoBehaviour
{

    [SerializeField] private Prefs_Data prefs;

    public Prefs_Data Prefs { get => prefs; set => prefs = value; }

    private void Awake()
    {
        // Load_Binary_Prefs();

        Initialize_Prefs();
    }


    #region Save-LOAD
    private void Initialize_Prefs()
    {
        string jsonString = JsonUtility.ToJson(prefs);
        string str = PlayerPrefs.GetString("Prefs");

        try
        {
            if (PlayerPrefs.GetInt("FirstRun67") == 0)
            {
                PlayerPrefs.SetInt("FirstRun67", 1);
                Save_Json_Prefs();
            }
            if (!PlayerPrefs.HasKey("Prefs") && Prefs.FirstRun)
            {
                Prefs.FirstRun = false;
                //Debug.Log("jsonStringWasEmpty");
                Save_Json_Prefs();
            }
            else
            {
                //Debug.Log("jsonStringWasNotEmpty:");
                Load_Json_Prefs();
            }
        }
        catch (Exception E)
        {

        }

    }
    public void Save_Json_Prefs()
    {
        try
        {
            //Debug.Log("Data Saved");
            //prefs.Healthinjectiontime = prefs.Dailyhealthinjectiontime.ToString();
            //prefs.DailyRewardTime = prefs.NextDailyRewardTime.ToString();
            //prefs.Airdropdatetime = prefs.Airdroptime.ToString();
            string jsonString = JsonUtility.ToJson(prefs);
            PlayerPrefs.SetString("Prefs", jsonString);
            //print(" Save_Json_Prefs :"+ PlayerPrefs.GetString("Prefs"));
            // Load_Json_Prefs();
        }
        catch (Exception E)
        {

        }
    }

    private void Load_Json_Prefs()
    {
        try
        {

            string jsonString = PlayerPrefs.GetString("Prefs");
            prefs = JsonUtility.FromJson<Prefs_Data>(jsonString);
            //prefs.Dailyhealthinjectiontime = Convert.ToDateTime(prefs.Healthinjectiontime);
            //prefs.NextDailyRewardTime = Convert.ToDateTime(prefs.DailyRewardTime);
            //prefs.Airdroptime = Convert.ToDateTime(Prefs.Airdropdatetime);
            //print(" Save_Json_Prefs :" + PlayerPrefs.GetString("Prefs"));
            //Prefs_Data loadedprefs = JsonUtility.FromJson<Prefs_Data>(jsonString);
            //if (IsDefaultFileStructureDifferentFromSavedOne(loadedprefs))
            //{
            //    HandleChanges(loadedprefs);
            //}
            //else
            //{
            //    prefs = loadedprefs;
            //}
        }
        catch (Exception E)
        {

        }
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Save_Json_Prefs();
        }

    }
    //public void Save_Binary_Prefs()
    //{


    //    string path = GetFilePath();

    //    BinaryFormatter formatter = new BinaryFormatter();

    //    FileStream file = new FileStream(path, FileMode.Create);

    //    formatter.Serialize(file, prefs);

    //    file.Close();
    //}

    //private void Load_Binary_Prefs()
    //{


    //    string path = GetFilePath();
    //    if (File.Exists(path))
    //    {
    //        BinaryFormatter formatter = new BinaryFormatter();

    //        FileStream file = new FileStream(path, FileMode.Open);

    //        Prefs_Data loadedPrefs = formatter.Deserialize(file) as Prefs_Data;

    //        file.Close();

    //        if (IsDefaultFileStructureDifferentFromSavedOne(loadedPrefs))
    //        {
    //            HandleChanges(loadedPrefs);
    //        }
    //        else
    //        {
    //            prefs = loadedPrefs;
    //        }
    //    }
    //    else
    //    {
    //        Save_Binary_Prefs();
    //    }
    //}

    private void HandleChanges(Prefs_Data loadedPrefs)
    {
        //GameMode[] mode = prefs.Gamemode;
        //// print("Current GameData Length :" + mode.Length + "loaded GameData Length: " + loadedPrefs.Gamemode.Length);
        //// print("Current GameData LEVELLength :" + mode[10].Gamedata[0].LevelUnlocked.Length + "loaded GameData LEVELLength: " + loadedPrefs.Gamemode[10].Gamedata[0].LevelUnlocked.Length);

        //for (int i = 0; i < loadedPrefs.Gamemode.Length; i++)
        //{
        //    for (int j = 0; j < loadedPrefs.Gamemode[i].Gamedata.Length; j++)
        //    {

        //        mode[i] = loadedPrefs.Gamemode[j];
        //        mode[i].Gamedata[j] = loadedPrefs.Gamemode[i].Gamedata[j];
        //        for (int k = 0; k < loadedPrefs.Gamemode[i].Gamedata[j].LevelUnlocked.Length; j++)
        //        {
        //            mode[i].Gamedata[j].LevelUnlocked[k] = loadedPrefs.Gamemode[i].Gamedata[j].LevelUnlocked[k];
        //            mode[i].Gamedata[j].Levelstar[k] = loadedPrefs.Gamemode[i].Gamedata[j].Levelstar[k];
        //        }
        //        //  data[i].Levelstar[j] = loadedPrefs.GameData[i].Levelstar[j];
        //        //data[i].LevelUnlocked[j] = loadedPrefs.GameData[i].LevelUnlocked[j];
        //        //data[i].Levelstar[j] = loadedPrefs.GameData[i].Levelstar[j];
        //    }
        //}
        //// prefs = loadedPrefs;
        //// prefs.Gamemode = data;

        GameData[] mode = prefs.GameData;
        // print("Current GameData Length :" + mode.Length + "loaded GameData Length: " + loadedPrefs.Gamemode.Length);
        // print("Current GameData LEVELLength :" + mode[10].Gamedata[0].LevelUnlocked.Length + "loaded GameData LEVELLength: " + loadedPrefs.Gamemode[10].Gamedata[0].LevelUnlocked.Length);

        
            for (int j = 0; j < loadedPrefs.GameData.Length; j++)
            {

                //mode[i] = loadedPrefs.GameData[j];
                for (int k = 0; k < loadedPrefs.GameData[j].LevelUnlocked.Length; j++)
                {
                    mode[j].LevelUnlocked[k] = loadedPrefs.GameData[j].LevelUnlocked[k];
                    mode[j].Levelstar[k] = loadedPrefs.GameData[j].Levelstar[k];
                }
                
            }
     

    }

    public bool IsDefaultFileStructureDifferentFromSavedOne(Prefs_Data _loadedPrefs)
    {

            if (prefs.GameData.Length != _loadedPrefs.GameData.Length)
            {
                //if (Toolbox.GameManager)
                // Toolbox.GameManager.Log("No Of Modes Different in File Structure");
                return true;
            }


            else
            {
                //if (Toolbox.GameManager)
                //  Toolbox.GameManager.Log("Same Modes File Structure :"+ prefs.Gamemode.Length);
                for (int m = 0; m < prefs.GameData.Length; m++)
                {
                    if (prefs.GameData.Length != _loadedPrefs.GameData.Length)
                    {
                        //if (Toolbox.GameManager)
                        //    Toolbox.GameManager.Log("Different File Structure Compare Btw No of Chapters");
                        return true;
                    }
                    else
                    {
                        // if (Toolbox.GameManager)
                        //   Toolbox.GameManager.Log("Same Chapters File Structure");
                        //return false;
                    }
                    if (prefs.GameData[m].LevelUnlocked.Length != _loadedPrefs.GameData[m].LevelUnlocked.Length
                            || prefs.GameData[m].Levelstar.Length != _loadedPrefs.GameData[m].Levelstar.Length)
                    {
                        return true;
                    }
                    else
                    {
                        //if (Toolbox.GameManager)
                        //    Toolbox.GameManager.Log("Same Levels and stars File Structure");
                    }
                }
            }
            return false;
        }

    //string GetFilePath()
    //{
    //    return Application.persistentDataPath + "/" + Constants.prefsFileName;
    //}

    #endregion



}
