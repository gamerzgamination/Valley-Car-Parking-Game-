//using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
//using GameAnalyticsSDK;

public class GunSelectionListner : MonoBehaviour
{
    //public RawImage Bg;
    //public Sprite Gunselectionbgsprite;
    //public GameObject Mainmenubg;
    public Button next;
    public Button prev;
    public Button unlockVehicle;
    public Button play;
    public Button Back;
    public GameObject lockPanel;
    public GameObject unlockBtn;
    public GameObject unlockAllCarsBtn;
    //public GameObject TryWeapon;
 //   public GameObject unlockPanel;
    //public Text vehicleName;
    public Text vehicleCost;
    public Image[] attributeFillImg;
    //public GameObject[] BarHover;

    public Transform vehicleSpawnPosition;
    private GameObject spawnedVehicleObj;
    private int curVehicleIndex = 2;

    //[HideInInspector]
    private GameObject vehiclesPrefabs;
    //[HideInInspector]
    private GunSelection_Gun vehiclesData;
  //  public ConsoliAdsBannerView consoliAdsBannerView = new ConsoliAdsBannerView();

    private void OnEnable()
    {
       
        curVehicleIndex = Toolbox.DB.Prefs.LastSelectedVehicle;
        //Toolbox.GameManager.Add_ActiveUI(this.gameObject);
        FetchVehiclesDataFromResources();
        ShowVehicle(curVehicleIndex);
        Toolbox.UIManager.UpdateTxts();
        vehicleSpawnPosition.gameObject.SetActive(true);
        if (Toolbox.DB.Prefs.AreAllGunsUnlocked())
            UnlockAllCarsBtn(false);
        else
            UnlockAllCarsBtn(true);




    }
    private void Start()
    {
       // Invoke("ShowMegaOffers",1f);
    }

    private void ShowMegaOffers()
    {
        if (!Toolbox.DB.Prefs.AreAllGunsUnlocked() && !Toolbox.DB.Prefs.MegaOfferPurchased)
        {
            Toolbox.UIManager.MegaOffers.SetActive(true);
            //   Toolbox.GameManager.Instantiate_MegaOffer();
        }
    }

    private void OnDisable()
    {

        //Mainmenubg.SetActive(true);
       // vehicleSpawnPosition.gameObject.SetActive(false);
        CancelInvoke();
        StopAllCoroutines();
       // Bg.texture = Mainmenubgsprite.texture;
    }


    public void FetchVehiclesDataFromResources()
    {
        vehiclesData = Resources.Load<GunSelection_Gun>(Constants.folderPath_Scriptables + Constants.folderPath_Scriptables_VehicleSelection_Vehicles + curVehicleIndex);
        vehiclesPrefabs = Resources.Load<GameObject>(Constants.folderPath_Prefabs + Constants.folderPath_Prefabs_VehicleSelection_Vehicles + curVehicleIndex);
    }

    private void ShowVehicle(int _index) {

        if (spawnedVehicleObj)
            Destroy(spawnedVehicleObj);

        spawnedVehicleObj = Instantiate(vehiclesPrefabs, vehicleSpawnPosition.position, vehicleSpawnPosition.rotation, vehicleSpawnPosition);

        //foreach (GameObject g in BarHover)
        //    g.SetActive(false);
        //vehicleName.text = vehiclesData.name.ToString();
        vehicleCost.text = vehiclesData.cost.ToString();

        attributeFillImg[0].fillAmount = vehiclesData.speed;
        attributeFillImg[2].fillAmount = vehiclesData.handling;
        attributeFillImg[2].fillAmount = vehiclesData.Aceleration;
        // BarHover[_index].SetActive(true);
        Toolbox.DB.Prefs.LastSelectedVehicle = _index;

        SetButtonState(Toolbox.DB.Prefs.VehiclesUnlocked[_index]);
    }

    private void SetButtonState(bool _gunUnlocked) {
        
        // Just for Weapon Try
        //if(Toolbox.DB.Prefs.Tryweapon)
        //    TryWeapon.gameObject.SetActive(false);
        //else
        //TryWeapon.gameObject.SetActive(!_gunUnlocked);


        play.gameObject.SetActive(_gunUnlocked);
       // if Directly showing shop then always keep this button off
        if (Toolbox.GameManager.GodirectshopfromMenu)
            play.gameObject.SetActive(false);
        // if(Toolbox.GameManager.GoDirectGamePlayAfterCompleteDirectShop1)
        //    play.gameObject.SetActive(true);

        unlockVehicle.gameObject.SetActive(!_gunUnlocked);

        lockPanel.SetActive(!_gunUnlocked);
        unlockBtn.SetActive(!_gunUnlocked);
    //    unlockPanel.SetActive(_vehicleUnlocked);

        vehicleCost.gameObject.SetActive(!_gunUnlocked);        
    }

    public void UnlockAllCarsBtn(bool _val) {

        unlockAllCarsBtn.SetActive(_val);
    }

    #region ButtonListners

    public void OnPress_Prev()
    {
     
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        curVehicleIndex--;

        if (curVehicleIndex < 0)
            curVehicleIndex = Toolbox.DB.Prefs.VehiclesUnlocked.Length - 1;


        FetchVehiclesDataFromResources();
        ShowVehicle(curVehicleIndex);

        //Toolbox.DB.Prefs.LastSelectedVehicle = curVehicleIndex;

    }

    public void OnPress_Next()
    {
       
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        curVehicleIndex++;

        if (curVehicleIndex >= Toolbox.DB.Prefs.VehiclesUnlocked.Length)
            curVehicleIndex = 0;

        FetchVehiclesDataFromResources();
        ShowVehicle(curVehicleIndex);

        //Toolbox.DB.Prefs.LastSelectedVehicle = curVehicleIndex;
    }

    public void ShowGun(int _index)
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        curVehicleIndex = _index;
        FetchVehiclesDataFromResources();
        ShowVehicle(curVehicleIndex);
        SetButtonState(Toolbox.DB.Prefs.VehiclesUnlocked[_index]);

    }

    public void OnPress_UnlockVehicle()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);

        if (Toolbox.DB.Prefs.GoldCoins >= vehiclesData.cost)
        {
            Toolbox.DB.Prefs.GoldCoins -= vehiclesData.cost;
            Toolbox.DB.Prefs.VehiclesUnlocked[curVehicleIndex] = true;
            SetButtonState(true);
        }
        else {
            Toolbox.UIManager.LowCoinUnlockCar_Panel.SetActive(true);
            Toolbox.UIManager.LowCoinUnlockCar_Panel.GetComponent<LowCoinVehicleBuy>().CurVehicle = curVehicleIndex;
       //   Toolbox.GameManager.Instantiate_LowCoinUnlockCar(curVehicleIndex);
        }
    }

    public void OnPress_UnlockAllVehicle()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
         InAppHandler.Instance.Buy_AllVehicles();
    }


    public void OnPress_Back()
    {
        if (this.GetComponentInParent<UIManager>().DirectShowingShop)
            this.GetComponentInParent<UIManager>().DirectShowMain();
        else
            Toolbox.UIManager.Go_BackFromWeaponselection();
            this.GetComponentInParent<UIManager>().ShowPrevUI();
       //s StartCoroutine(GunSelection(0.5f,false));
        Toolbox.GameManager.Analytics_DesignEvent("GunSelection_Press_Back");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
       if ( Toolbox.GameManager.GodirectshopfromMenu )
            Toolbox.GameManager.GodirectshopfromMenu = false;
    }

    public void OnPress_Play()
    {
        Toolbox.DB.Prefs.LastSelectedVehicle = curVehicleIndex;
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.FBAnalytic_EventDesign("GunSelection_Press_Play");
        Toolbox.GameManager.Analytics_DesignEvent("GunSelection_Press_Play");
      //  Invoke("Ads",1.2f);
        this.GetComponentInParent<UIManager>().ShowNextUI();
    }
  
    public void OnPress_Store()
    {
        Toolbox.GameManager.FBAnalytic_EventDesign("GunSelection_Press_Store");
        Toolbox.GameManager.Analytics_DesignEvent("GunSelection_Press_Store");
        Toolbox.UIManager.Shop_Panel.SetActive(true);
    //    Toolbox.GameManager.InstantiateUI_Shop();
    }

    public void OnPress_3DView(bool _Val)
    {
        Toolbox.GameManager.FBAnalytic_EventDesign("OnPress_3DView");
        Toolbox.GameManager.Analytics_DesignEvent("OnPress_3DView");
     //   View3DPanel.SetActive(_Val);
     //   View2DPanel.SetActive(!_Val);
    }

    //public void TRYWeapon()
    //{
    //    //s AdsManager.Instance.ShowRewardedVideo(AdsManager.RewardType.TRYWEAPON);
    //    Toolbox.DB.Prefs.Tyrweaponindex = curVehicleIndex;
    //}

    public void Unlockweapon()
    {
        //Toolbox.DB.Prefs.Tryweapon = true;
        Toolbox.UIManager.MessagePopup.SetActive(true);
        Toolbox.UIManager.MessagePopup.GetComponent<MessageListner>().UpdateTxt("This Weapon is Unlocked Just for Try, Now you can play the Level with this weapon.", "WEAPON UNLOCK FOR TRY");
        //Toolbox.DB.Prefs.WeaponUnlock_Try(Toolbox.DB.Prefs.Tyrweaponindex, true);
        //curVehicleIndex = Toolbox.DB.Prefs.Tyrweaponindex;
        //SetButtonState(Toolbox.DB.Prefs.VehiclesUnlocked[Toolbox.DB.Prefs.Tyrweaponindex]);
    }
    private IEnumerator GunSelection(float delay,bool _Val)
    {
        yield return new WaitForSeconds(delay);    
        vehicleSpawnPosition.gameObject.SetActive(_Val);
        
        StopCoroutine(GunSelection(0f,false));
    }
   
    #endregion
}
