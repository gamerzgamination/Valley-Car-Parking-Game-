using UnityEngine;

[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(SoundsManager))]
[RequireComponent(typeof(DB))]
//[RequireComponent(typeof(AdsManager))]

public class Toolbox : MonoBehaviour {
    private static GameManager gameManager;
    private static SoundsManager soundManager;
    private static DB db;
   // private static AdsManager adsManager;
    private static ObjectiveHandler objectivehandler;
    private static UIManager uimanager;
    //private static InAppHandler inAppHandler;
    private static AdIconHandler adIconHandler;
    private static GameplayController gameplayController;
    private static HUDListner hudListner ;
    public static GameManager GameManager {
        get { return gameManager; }
    }

    public static SoundsManager Soundmanager {
        get { return soundManager; }
    }
    
    public static DB DB
    {
        get { return db; }
    }

  
    //public static AdsManager AdsManager
    //{
    //    get { return adsManager; }
    //}
    public static ObjectiveHandler ObjectiveHandler
    {
        get { return objectivehandler; }
    }
    public static UIManager UIManager
    {
        get { return uimanager; }
    }
    
    //public static InAppHandler InAppHandler
    //{
    //    get { return inAppHandler; }
    //}
    public static AdIconHandler AdIconHandler
    {
        get { return adIconHandler; }
    }
    public static GameplayController GameplayController
    {
        get { return gameplayController; }
    }

    public static HUDListner HUDListner
    {
        get { return hudListner; }
    }
    
  
    void Awake()
    {
        gameManager = GetComponent<GameManager>();
        soundManager = GetComponent<SoundsManager>();
        db = GetComponent<DB>();
       // adsManager = GetComponent<AdsManager>();
       // inAppHandler = GetComponent<InAppHandler>();
        adIconHandler = GetComponent<AdIconHandler>();
        DontDestroyOnLoad(gameObject);
    }

    public static void Set_GameplayScript (GameplayController _game) {
        gameplayController = _game;
    }

    public static void Set_HUD(HUDListner _hud)
    {
        hudListner = _hud;
    }
    public static void Set_objectivehandler(ObjectiveHandler obj)
    {
        objectivehandler = obj;
    }
    public static void Set_Uimanager(UIManager obj)
    {
        uimanager  = obj;
    }
   
    
    
}