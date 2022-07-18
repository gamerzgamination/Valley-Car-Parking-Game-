using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

#region InAppStructure
public enum InAppItemName
{
    Coins,
    Stars,
    RemoveAds,
    UnlockAllLevels,
    UnlockAllVehicles,
    UnlockAllModes,
    MegaOffer,
    SubscriptionWeekly,
    SubscriptionMonthly,
    SubscriptionQuaterly
}
[System.Serializable]
public class ConsumableInApps
{
    public string inAppName, inAppId;
    public int quantityToGive;
    //public int priceForInApp;
    public InAppItemName inAppType = InAppItemName.Coins;
}

[System.Serializable]
public class NonConsumeableInApps
{
    public string inAppName;
    public string inAppId;
    //public int priceForInApp;
    public InAppItemName inAppType = InAppItemName.RemoveAds;
}
[System.Serializable]
public class SubscriptionInApps
{
    public string inAppName;
    public string inAppId;
    //public int priceForInApp;
    public InAppItemName inAppType = InAppItemName.SubscriptionWeekly;
}
#endregion
public class InAppHandler : MonoBehaviour, IStoreListener
{

    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
    private IAppleExtensions m_AppleExtensions;
    private IGooglePlayStoreExtensions m_GoogleExtensions;


    public List<ConsumableInApps> consumeableInApps;
    public List<NonConsumeableInApps> nonConsumeableInApps;
    public List<SubscriptionInApps> subscriptionInApps;
    public static InAppHandler Instance;
    private static List<string> m_PendingProducts;
    private static List<Product> m_CompletedProducts;
    private static Product currentProduct;

    public bool PurchasingInapp;

#if UNITY_ANDROID

    //    public string WEEKLY_SUBSCRIPTION = "weeklysubscription";
    //    public string MONTHLY_SUBSCRIPTION = "monthlysubscription";
    //    public string QUATERLY_SUBSCRIPTION = "quaterlysubscription";

    //    // Google Play Store-specific product identifier subscription product.
    private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";
#endif

#if UNITY_IOS

    //public string WEEKLY_SUBSCRIPTION = "weeklysubscription";
    //public string MONTHLY_SUBSCRIPTION = "monthlysubscription";
    //public string QUATERLY_SUBSCRIPTION = "quaterlysubscription";
        // Apple App Store-specific product identifier for the subscription product.
    private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

#endif
    //#endregion
    public void OnPurchaseComplete()
    {
        if (PurchasingInapp)
        {
            Toolbox.UIManager.MessagePopup.SetActive(true);
            Toolbox.UIManager.MessagePopup.GetComponent<MessageListner>().UpdateTxt("YOUR PURCHASE HAS BEEN VERIFIED AND ITEM(S) HAS BEEN ADDED TO YOUR GAME", "PURCHASE SUCCESSFUL");
            PurchasingInapp = false;
        }

    }
    public void OnPurchaseFailed()
    {
        Toolbox.GameManager.Permanent_Log("Purchase Failed");
    }
    public void RemoveAdsPurchased()
    {
        Toolbox.GameManager.Permanent_Log("Remove Ads purchased!");
    }

    void Awake()
    {
        Instance = this;

        //Unlock Everything just for testing 
        //  PlayerPrefs.SetInt("NoAdsPurchased", 1);
        //Toolbox.DB.Prefs.UnlockAllGuns();
        //Toolbox.DB.Prefs.UnlockAllLevels();
        //Toolbox.DB.Prefs.AllChapters_of_Mode_Unlock();

    }
    void Start()
    {
        //Instance = this;
        DontDestroyOnLoad(this.gameObject);
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            if (!Toolbox.GameManager.IsNetworkAvailable())
                return;
            else
                InitializePurchasing();
        }
        Debug.Log("InitializePurchasing");
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            return;
        }
        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        m_PendingProducts = new List<string>();
        m_CompletedProducts = new List<Product>();
        UnRegister();
        foreach (var consumable in consumeableInApps)
        {
            builder.AddProduct(consumable.inAppId, ProductType.Consumable);
        }

        foreach (var nonConsumable in nonConsumeableInApps)
        {

            builder.AddProduct(nonConsumable.inAppId, ProductType.NonConsumable);
            //Toast.current.Show("Marked consume");
        }

        foreach (var subscription in subscriptionInApps)
        {
            builder.AddProduct(subscription.inAppId, ProductType.Subscription);
        }

        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Toolbox.GameManager.Permanent_Log("OnInitialized: PASS");
        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
        m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();
        m_GoogleExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();
        Dictionary<string, string> dict = m_AppleExtensions.GetIntroductoryPriceDictionary();

        foreach (Product item in controller.products.all)
        {
            Debug.Log("GI >>" + item.definition.type);
            if (item.definition.type == ProductType.Subscription)
            {
                if (item.receipt == null)
                {
                    Debug.Log("GI >>" + "Expried or Cancel or NotBought");
                    // Toolbox.DB.Prefs.onUnSubscribed();when subscription cancel this will be call
                }
            }

            if (item.receipt != null)
            {
                string intro_json = (dict == null || !dict.ContainsKey(item.definition.storeSpecificId)) ? null : dict[item.definition.storeSpecificId];

                if (item.definition.type == ProductType.Subscription)
                {
                    SubscriptionManager p = new SubscriptionManager(item, intro_json);
                    SubscriptionInfo info = p.getSubscriptionInfo();
                    Debug.Log("GI >> SubInfo: " + info.getProductId().ToString());
                    Debug.Log("GI >> isSubscribed: " + info.isSubscribed().ToString());
                    Debug.Log("GI >> isFreeTrial: " + info.getExpireDate().ToString());
                    Debug.Log("GI >> ISCancel:" + info.isCancelled().ToString());
                    if (info.isCancelled() == Result.True || info.isExpired() == Result.True)
                    {
                        Debug.Log("GI >>Sub is cancelled");
                        // Toolbox.DB.Prefs.onUnSubscribed();;when subscription cancel this will be call

                    }
                    if (info.isExpired() == Result.True)
                    {
                        Debug.Log("GI >>Sub is isExpired");
                        //   Toolbox.DB.Prefs.onUnSubscribed();;when subscription cancel this will be call

                    }

                }
            }


        }

    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);

    }


    //public void Buy_Coins5000()
    //{
    //    Toolbox.GameManager.Log("Coins 1000 Pressed");
    //    if (Application.internetReachability == NetworkReachability.NotReachable)
    //    {
    //        //    Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().InternetNotAvailabe();
    //        return;

    //    }
    //    if (!IsInitialized())
    //    {
    //        //    Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().NotInitilizedPurchase("Not initialized");
    //        return;
    //    }
    //    //   BuyProductID(consumeableInApps[0].inAppId);
    //    PurchasingInapp = true;
    //    PurchaseItem(consumeableInApps[0].inAppId, null);
    //}

    //public void Buy_Coins15000()
    //{
    //    Toolbox.GameManager.Log("Coins 2000 Pressed");

    //    if (!Toolbox.GameManager.IsNetworkAvailable())
    //    {
    //        //    Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().InternetNotAvailabe();
    //        return;
    //    }
    //    if (!IsInitialized())
    //    {
    //        //    Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().NotInitilizedPurchase("Not initialized");
    //        return;
    //    }
    //    // BuyProductID(consumeableInApps[1].inAppId);
    //    PurchasingInapp = true;
    //    PurchaseItem(consumeableInApps[1].inAppId, null);

    //}
    //public void Buy_Coins20000()
    //{
    //    Toolbox.GameManager.Log("Coins 5000 Pressed");
    //    if (!Toolbox.GameManager.IsNetworkAvailable())
    //    {
    //        //   Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().InternetNotAvailabe();
    //        return;
    //    }
    //    if (!IsInitialized())
    //    {
    //        //    Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().NotInitilizedPurchase("Not initialized");
    //        return;
    //    }
    //    // BuyProductID(consumeableInApps[2].inAppId);
    //    PurchasingInapp = true;
    //    PurchaseItem(consumeableInApps[2].inAppId, null);
    //}

    //public void Buy_Coins30000()
    //{
    //    Toolbox.GameManager.Log("Coins 30000 Pressed");
    //    if (!Toolbox.GameManager.IsNetworkAvailable())
    //    {
    //        //    Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().InternetNotAvailabe();
    //        return;
    //    }
    //    if (!IsInitialized())
    //    {
    //        //    Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().NotInitilizedPurchase("Not initialized");
    //        return;
    //    }
    //    //BuyProductID(consumeableInApps[3].inAppId);
    //    PurchasingInapp = true;
    //    PurchaseItem(consumeableInApps[3].inAppId, null);
    //}
    //public void Buy_Coins40000()
    //{
    //    Toolbox.GameManager.Log("Coins 400000 Pressed");
    //    if (!Toolbox.GameManager.IsNetworkAvailable())
    //    {
    //        //    Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().InternetNotAvailabe();
    //        return;
    //    }
    //    if (!IsInitialized())
    //    {
    //        //    Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().NotInitilizedPurchase("Not initialized");
    //        return;
    //    }
    //    //BuyProductID(consumeableInApps[4].inAppId);
    //    PurchasingInapp = true;
    //    PurchaseItem(consumeableInApps[4].inAppId, null);

    //}

    public void Buy_AllVehicles()
    {
        Toolbox.GameManager.Log("Buy all vehicles");
        if (!Toolbox.GameManager.IsNetworkAvailable())
        {
            //    Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().InternetNotAvailabe();
            return;
        }
        if (!IsInitialized())
        {
            //    Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().NotInitilizedPurchase("Not initialized");
            return;
        }
        //BuyProductID(nonConsumeableInApps[2].inAppId);
        PurchasingInapp = true;
        PurchaseItem(nonConsumeableInApps[3].inAppId, null);

    }
    public void Buy_AllModes()
    {
        Toolbox.GameManager.Log("Buy all Modes");
        if (!Toolbox.GameManager.IsNetworkAvailable())
        {
            //   Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().InternetNotAvailabe();
            return;
        }
        if (!IsInitialized())
        {
            //  Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().NotInitilizedPurchase("Not initialized");
            return;
        }
        //BuyProductID(nonConsumeableInApps[1].inAppId);
        PurchasingInapp = true;
        PurchaseItem(nonConsumeableInApps[1].inAppId, null);

    }
    public void Buy_AllLevels()
    {
        Toolbox.GameManager.Log("Buy all Levels ");
        if (!Toolbox.GameManager.IsNetworkAvailable())
        {
            //   Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().InternetNotAvailabe();
            return;
        }
        if (!IsInitialized())
        {
            //  Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().NotInitilizedPurchase("Not initialized");
            return;
        }
        //BuyProductID(nonConsumeableInApps[2].inAppId);
        PurchasingInapp = true;
        PurchaseItem(nonConsumeableInApps[2].inAppId, null);

    }

    public void Buy_NoAds()
    {
        Toolbox.GameManager.Log("No Ads Pressed");
        if (!Toolbox.GameManager.IsNetworkAvailable())
        {
            //  Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().InternetNotAvailabe();
            return;
        }
        if (!IsInitialized())
        {
            // Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().NotInitilizedPurchase("Not initialized");
            return;
        }
        // BuyProductID(nonConsumeableInApps[0].inAppId);
        PurchasingInapp = true;
        PurchaseItem(nonConsumeableInApps[0].inAppId, null);

    }

    public void Buy_MegaOffer()
    {
        Toolbox.GameManager.Log("Mega Offer Pressed");
        if (!Toolbox.GameManager.IsNetworkAvailable())
        {
            //  Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().InternetNotAvailabe();
            return;
        }
        if (!IsInitialized())
        {
            // Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().NotInitilizedPurchase("Not initialized");
            return;
        }
        //BuyProductID(nonConsumeableInApps[3].inAppId);
        PurchasingInapp = true;
        PurchaseItem(nonConsumeableInApps[4].inAppId, null);

    }

    //edit by uzair
    public void OnPurchaseDeferred(Product product)
    {

        Debug.Log("GI >>Deferred product " + product.definition.id.ToString());
    }

    public interface InAppPurchasesCallBacks
    {
        bool PurchaseSuccessful(string sku);
        void PurchaseFailed(string sku);

    }

    private static InAppPurchasesCallBacks callbackObj;
    public static void Register(InAppPurchasesCallBacks pCallbackObj)
    {
        callbackObj = pCallbackObj;
    }
    public static void UnRegister()
    {
        callbackObj = null;
    }

    public void PurchaseItem(string sku, MonoBehaviour context)
    {
        Debug.Log("purchase item::" + sku);
        Register(context as InAppPurchasesCallBacks);
        int index = m_PendingProducts.IndexOf(sku);
        Debug.Log("Index Value" + index);

        if (index == -1)
        {
            BuyProductID(sku);
        }
        else
        {
            ShowPendingPurchase();
        }

    }

    public static void ShowPendingPurchase()
    {
        //PendingDialog.SetActive(true);
    }

    private bool ValidatePurchase(PurchaseEventArgs e)
    {
        bool validPurchase = true; // Presume valid for platforms with no R.V.

        //MA::validation check disabled for now
        // Unity IAP's validation logic is only included on these platforms.
        //#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
        //        // Prepare the validator with the secrets we prepared in the Editor
        //        // obfuscation window.
        //        var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
        //            AppleTangle.Data(), Application.bundleIdentifier);

        //        try
        //        {
        //            // On Google Play, result has a single product ID.
        //            // On Apple stores, receipts contain multiple products.
        //            var result = validator.Validate(e.purchasedProduct.receipt);
        //            // For informational purposes, we list the receipt(s)
        //            Debug.Log("Receipt is valid. Contents:");
        //            foreach (IPurchaseReceipt productReceipt in result)
        //            {
        //                Debug.Log(productReceipt.productID);
        //                Debug.Log(productReceipt.purchaseDate);
        //                Debug.Log(productReceipt.transactionID);
        //            }
        //        }
        //        catch (IAPSecurityException)
        //        {
        //            Debug.Log("Invalid receipt, not unlocking content");
        //            validPurchase = false;
        //        }
        //#endif

        return validPurchase;
    }


    public void BuyProductID(string productId)
    {

        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchas   eFailed 
                // asynchronously.

                //    Toolbox.GameManager.PurchaseLoading();
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            //  Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().NotInitilizedPurchase("Not initialized");
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        ConsumableInApps consumableSucceededInApp = consumeableInApps.Find(x => x.inAppId == args.purchasedProduct.definition.id);
        bool isValid = ValidatePurchase(args);
        currentProduct = args.purchasedProduct;
        if (isValid)
        {
            bool isPending = m_GoogleExtensions.IsPurchasedProductDeferred(currentProduct);
            if (isPending)
            {
                int index = m_PendingProducts.IndexOf(currentProduct.definition.id);
                if (index == -1)
                {
                    Debug.Log("Adding to pending products");
                    m_PendingProducts.Add(currentProduct.definition.id);
                }

                UnRegister();
                return PurchaseProcessingResult.Pending;
            }

            else
            {

                if (consumableSucceededInApp != null)
                {
                    if (consumableSucceededInApp.inAppType == InAppItemName.Coins)
                    {
                        Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                        // Toast.current.Show(consumableSucceededInApp.quantityToGive + " Coins have been added successfully.");

                        // Add Coins in Preferences here
                        //Example:
                        Toolbox.DB.Prefs.GoldCoins += consumableSucceededInApp.quantityToGive;
                        //Toolbox.UIManager.UpdateTxts();
                        OnPurchaseComplete();
                    }
                    else if (consumableSucceededInApp.inAppType == InAppItemName.Stars)
                    {
                        Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                        // Toast.current.Show(consumableSucceededInApp.quantityToGive + " Stars have been added successfully.");

                        // Add Stars in Preferences here
                        //Example:
                        /*
                         * PreferenceManager.SetCurrentBalance(PreferenceManager.currentBalance + consumableSucceededInApp.quantityToGive);
                         */
                    }

                }

                // Or ... a non-consumable product has been purchased by this user.
                else
                {
                    NonConsumeableInApps nonConsumableSucceededInapp = nonConsumeableInApps.Find(x => x.inAppId == args.purchasedProduct.definition.id);
                    if (nonConsumableSucceededInapp != null)
                    {
                        if (nonConsumableSucceededInapp.inAppType == InAppItemName.RemoveAds)
                        {
                            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                            //The non-consumable item has been successfully purchased, set Preferences here.
                            PlayerPrefs.SetInt("NoAdsPurchased",1); 
                            Toolbox.DB.Prefs.NoAdsPurchased = true;
                            PlayerPrefs.SetInt("RemoveAds", 1);
                            AdsManager.Instance.HideBannners();
                            if (FindObjectOfType<MainMenuListner>())
                                FindObjectOfType<MainMenuListner>().NoAdsButtonHandling();
                            OnPurchaseComplete();
                        }
                        else if (nonConsumableSucceededInapp.inAppType == InAppItemName.UnlockAllLevels)
                        {
                            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                            // Toast.current.Show("All Levels has been purchased successfully!");
                            Toolbox.DB.Prefs.UnlockAllLevels();
                            Toolbox.DB.Prefs.AllChapters_of_Mode_Unlock();
                            if (FindObjectOfType<LevelSelectionListner>())
                                FindObjectOfType<LevelSelectionListner>().CheckStatus_UnlockallLevels();
                            OnPurchaseComplete();

                        }
                        else if (nonConsumableSucceededInapp.inAppType == InAppItemName.UnlockAllModes)
                        {
                            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                            Toolbox.DB.Prefs.AllChapters_of_Mode_Unlock();
                            if (FindObjectOfType<ModeSelectionListner>())
                                FindObjectOfType<ModeSelectionListner>().InitchapterButtonsState();
                            OnPurchaseComplete();

                        }
                        else if (nonConsumableSucceededInapp.inAppType == InAppItemName.UnlockAllVehicles)
                        {
                            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                            Toolbox.DB.Prefs.UnlockAllGuns();

                            if (FindObjectOfType<StoreListner>())
                                FindObjectOfType<StoreListner>().UnlockAllCarsButtonHandling();

                            if (FindObjectOfType<GunSelectionListner>())
                                FindObjectOfType<GunSelectionListner>().UnlockAllCarsBtn(false);
                            OnPurchaseComplete();


                        }
                        else if (nonConsumableSucceededInapp.inAppType == InAppItemName.MegaOffer)
                        {
                            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                            //   Toast.current.Show("Mega Offer has been purchased successfully!");
                            //if (FindObjectOfType<AbstractAdsmanager>())
                            //    FindObjectOfType<AbstractAdsmanager>().HideBannners();
                            Toolbox.DB.Prefs.MegaOfferPurchased = true;
                            PlayerPrefs.SetInt("RemoveAds", 1);
                            Toolbox.DB.Prefs.NoAdsPurchased = true;
                            Toolbox.DB.Prefs.UnlockAllGuns();
                            Toolbox.DB.Prefs.UnlockAllLevels();
                            Toolbox.DB.Prefs.AllChapters_of_Mode_Unlock();
                            if (FindObjectOfType<MainMenuListner>())
                                FindObjectOfType<MainMenuListner>().NoAdsButtonHandling();
                            if (FindObjectOfType<StoreListner>())
                                FindObjectOfType<StoreListner>().UnlockAllCarsButtonHandling();
                            if (FindObjectOfType<GunSelectionListner>())
                                FindObjectOfType<GunSelectionListner>().UnlockAllCarsBtn(false);

                            OnPurchaseComplete();

                        }


                    }

                    // Or ... a subscription product has been purchased by this user.
                    else
                    {
                        SubscriptionInApps subscriptionSucceededInApp = subscriptionInApps.Find(x => x.inAppId == args.purchasedProduct.definition.id);
                        if (subscriptionSucceededInApp != null)
                        {
                            if (subscriptionSucceededInApp.inAppType == InAppItemName.SubscriptionWeekly)
                            {
                                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                                //Toast.current.Show("Congratulations! Weekly Subscription has been purchased successfully.");
                                // TODO: The subscription item has been successfully purchased, grant this to the player.
                            }
                            // Or ... an unknown product has been purchased by this user. Fill in additional products here....
                            else if (subscriptionSucceededInApp.inAppType == InAppItemName.SubscriptionMonthly)
                            {
                                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                                //Toast.current.Show("Congratulations! Monthly Subscription has been purchased successfully.");
                                // TODO: The subscription item has been successfully purchased, grant this to the player.
                            }
                            else if (subscriptionSucceededInApp.inAppType == InAppItemName.SubscriptionQuaterly)
                            {
                                Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
                                // Toast.current.Show("Congratulations! Quaterly Subscription has been purchased successfully.");
                                // TODO: The subscription item has been successfully purchased, grant this to the player.
                            }
                            else
                            {
                                Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
                            }
                        }
                    }

                }

                Debug.Log("Removing from pending products");
                m_PendingProducts.Remove(currentProduct.definition.id);
                if (callbackObj != null)
                {
                    Debug.Log("Call back is not null");
                    callbackObj.PurchaseSuccessful(currentProduct.definition.id);
                    UnRegister();
                    return PurchaseProcessingResult.Complete;

                }
                else
                {
                    Debug.Log("Call back is null so adding in completed products");
                    m_StoreController.ConfirmPendingPurchase(currentProduct);
                    m_CompletedProducts.Add(currentProduct);
                    Debug.Log("Completed products count+++" + m_CompletedProducts.Count);
                    UnRegister();
                    return PurchaseProcessingResult.Complete;
                }
            }
            // A consumable product has been purchased by this user.

            // Return a flag indicating whether this product has completely been received, or if the application needs 
            // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
            // saving purchased products to the cloud, and when that save is delayed. 

        }
        else
        {
            UnRegister();
            return PurchaseProcessingResult.Complete;
        }

    }
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {

        //   Toolbox.GameManager.PurchaseLaoding.GetComponent<PurchaseLoading>().FailedPurchase();
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) =>
            {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

}
