using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using GoogleMobileAds.Api;

public class UIManager : MonoBehaviour
{
	public int curUiIndex = 0;
	bool vehicleSelLoaded = false;
	private   GameObject curUIObj;
	//private readonly List<GameObject> uiList = new List<GameObject>();
	//public Animator anim;
	public GameObject [] uiList;
	[SerializeField] private bool directShowingShop = false;
	//[SerializeField] private bool directShowingLevelSelection = false;
	public int shopIndex = 3;
	public int LevelselectionIndex = 2;
	public Text coinsTxt;

	// UI Menus 
	public GameObject ModeLockPopup;
	public GameObject MessagePopup;
	public GameObject LowCoinUnlockCar_Panel;
	public GameObject Quit_Panel;
	public GameObject Settings_Panel;
	public GameObject Gameplay_Loading;
	public GameObject UIDummy_Loading;
	public GameObject Shop_Panel;
	public GameObject MegaOffers;
	public GameObject PrivacyPolicy;
	public GameObject SurePop;
	public bool DirectShowingShop { get => directShowingShop; set => directShowingShop = value; }

    void Awake()
	{
		Toolbox.Soundmanager.PlayMusic_Menu();
		Toolbox.Set_Uimanager(this);
		//InitializeAllUI();
		curUiIndex = 0;
		refreshstatus();

		if (Toolbox.GameManager.DirectShowVehicleSelectionOnMenu)
        {
            ShowUI(curUiIndex);
            DirectShowShopAfterLevelComplete();
            Toolbox.GameManager.DirectShowVehicleSelectionOnMenu = false;
        }
        else
        {
            ShowUI(curUiIndex);
        }
    }

    private void Start()
    {
		//Toolbox.AdsManager.Load_RAd(0);
	}
    private void InitializeAllUI()
	{
		//for (int i = 0; i < this.transform.childCount; i++)
		//{
		//	uiList.Add(this.transform.GetChild(i).gameObject);
		//}
	}
	public void refreshstatus()
    {
		UpdateTxts();
    }
	public void UpdateTxts()
	{
		coinsTxt.text = Toolbox.DB.Prefs.GoldCoins.ToString();
	}
   
    public void ShowUI(int _index) {

		if (curUiIndex >= uiList.Length || curUiIndex < 0)
		{
			return;
		}
		else
		{
			uiList[_index].SetActive(true);

			if (curUIObj)
				curUIObj.SetActive(false);

			curUiIndex = _index;
			curUIObj = uiList[_index];
		}
	}

	public void ShowPrevUI()
	{
		curUiIndex--;

		if (curUiIndex < 0)
		{
			curUiIndex = 0;
		}
		else
		{
		//	Toolbox.GameManager.loading_Delay(3f);
			ShowUI(curUiIndex);
		}

	}

	public void ShowNextUI() {

		curUiIndex++;

		if (curUiIndex >= uiList.Length)
		{

			Toolbox.GameManager.Loading_GameScene(true, Toolbox.DB.Prefs.Get_LastSelectedGameModeSceneIndex());
			//Toolbox.GameManager.Loading_GameScene(true, Toolbox.DB.Prefs.Get_LastSelectedGameModeSceneIndex());
			//Destroy(this.gameObject);
		}
		else {
			
			ShowUI(curUiIndex);
		}
	}

	public void DirectShowShop() {

		DirectShowingShop = true;
		ShowUI(shopIndex);
	}
	public void DirectShowLevelSelection()
	{

		//directShowingLevelSelection = true;
		ShowUI(LevelselectionIndex);
	}
	public void DirectShowShopAfterLevelComplete()
	{
		DirectShowingShop = true;
		ShowUI(shopIndex);
		Go_DirectWeaponShop();
	}

	public void DirectShowMain()
	{
		Go_BackDirectWeaponShop_To_MainMenu();
		DirectShowingShop = false;
		ShowUI(0);
	}
    //For Button Sounds 
    public void Onclick(AudioClip sound)
    {
        Toolbox.Soundmanager.PlaySound(sound);
    }
	public void On_PressAds()
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=com.gamerz.bus.parker.simulation.game");
	}
	public void Go_BackFromWeaponselection()
	{
		//anim.SetBool("LevelSelection", false);
		//anim.SetBool("WeaponSelection", false);
	}
	public void Go_DirectWeaponShop()
	{ 
		//anim.SetBool("DirectShop", true);
	}
	public void Go_BackDirectWeaponShop_To_MainMenu()
	{
		//anim.SetBool("DirectShop", false);
	}
}