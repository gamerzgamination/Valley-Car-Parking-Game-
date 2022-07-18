using System;
using UnityEngine;
using UnityEngine.UI;

public class StoreListner : MonoBehaviour
{
    public Text coinsTxt;
    //public Image PowerBar;
    //public Image RankBar;
    public GameObject allVehiclesUnlockButton;

    private void OnEnable()
    {
      //  Toolbox.GameManager.Add_ActiveUI(this.gameObject);
    }

    private void OnDisable()
    {
       // Toolbox.GameManager.Remove_ActiveUI(this.gameObject);

        if (FindObjectOfType<MainMenuListner>())
            FindObjectOfType<MainMenuListner>().ShowBannner();
    }
    private void Start()
    {
        UpdateTxts();
        UnlockAllCarsButtonHandling();
    }
    private void Update()
    {
        UpdateTxts();
    }

   
    public void UnlockAllCarsButtonHandling()
    {
        if (Toolbox.DB.Prefs.GetLockedItemIndex(1) == -1)
            allVehiclesUnlockButton.SetActive(false);
    }

    public void UpdateTxts()
    {
        coinsTxt.text = Toolbox.DB.Prefs.GoldCoins.ToString();
    }

    #region ButtonListners

    public void OnPress_Close() {

        Toolbox.GameManager.FBAnalytic_EventDesign("Store_Press_Close");
        Toolbox.GameManager.Analytics_DesignEvent("Store_Press_Close");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        this.gameObject.SetActive(false);
        //    Destroy(this.gameObject);
    }

    public void OnPress_Pack1()
    {
        Toolbox.GameManager.FBAnalytic_EventDesign("Store_Press_Pack1");
        Toolbox.GameManager.Analytics_DesignEvent("Store_Press_Pack1");

        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
    //    InAppHandler.Instance.Buy_Coins5000();
    }

    public void OnPress_Pack2()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.FBAnalytic_EventDesign("Store_Press_Pack2");
        Toolbox.GameManager.Analytics_DesignEvent("Store_Press_Pack2");

    //    InAppHandler.Instance.Buy_Coins15000();
    }

    public void OnPress_Pack3()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.Analytics_DesignEvent("Store_Press_Pack3");
        Toolbox.GameManager.FBAnalytic_EventDesign("Store_Press_Pack3");
      //  InAppHandler.Instance.Buy_Coins20000();
    }

    public void OnPress_Pack4()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.Analytics_DesignEvent("Store_Press_Pack4");

        Toolbox.GameManager.FBAnalytic_EventDesign("Store_Press_Pack4");
     //   InAppHandler.Instance.Buy_Coins30000();
    }

    public void OnPress_Pack5()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.GameManager.Analytics_DesignEvent("Store_Press_Pack4");

        Toolbox.GameManager.FBAnalytic_EventDesign("Store_Press_Pack4");
    //    InAppHandler.Instance.Buy_Coins40000();
    }

    public void OnPress_Removeads()
    {
        Toolbox.GameManager.Analytics_DesignEvent("Store_Removeads");

        Toolbox.GameManager.FBAnalytic_EventDesign("Store_Removeads");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
         InAppHandler.Instance.Buy_NoAds();
    }
    public void OnPress_UnlockAllChapters()
    {
        Toolbox.GameManager.Analytics_DesignEvent("Store_UnlockAllChapters");

        Toolbox.GameManager.FBAnalytic_EventDesign("Store_UnlockAllChapters");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
    //    InAppHandler.Instance.Buy_AllChapters();
    }
    public void OnPress_UnlockAllLevels()
    {
        Toolbox.GameManager.Analytics_DesignEvent("Store_UnlockAllLevels");

        Toolbox.GameManager.FBAnalytic_EventDesign("Store_UnlockAllLevels");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        InAppHandler.Instance.Buy_AllLevels();
    }
    public void OnPress_UnlockAllGuns()
    {
        Toolbox.GameManager.Analytics_DesignEvent("Store_UnlockAllGuns");
        Toolbox.GameManager.FBAnalytic_EventDesign("Store_UnlockAllGuns");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
     //   InAppHandler.Instance.Buy_AllGuns();

    }
    public void OnPress_UnlockEveryThing()
    {
        Toolbox.GameManager.Analytics_DesignEvent("Store_UnlockEveryThing");
        Toolbox.GameManager.FBAnalytic_EventDesign("Store_UnlockEveryThing");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
    //    InAppHandler.Instance.Buy_MegaOffer();
    }
    public void OnPress_RestorePurchase()
    {
        Toolbox.GameManager.Analytics_DesignEvent("Store_RestorePurchase");
        Toolbox.GameManager.FBAnalytic_EventDesign("Store_RestorePurchase");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
    //   InAppHandler.Instance.RestorePurchases();
    }
    #endregion
}
