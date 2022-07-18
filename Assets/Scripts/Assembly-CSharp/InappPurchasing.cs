//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Purchasing;

//public class InappPurchasing : MonoBehaviour, IStoreListener
//{
//	private static IStoreController m_StoreController;

//	private static IExtensionProvider m_StoreExtensionProvider;

//	public static string kProductIDConsumable = "consumable";

//	public static string remove_ads = "remove_ads";

//	public static string unlock_all_levels = "unlock_all_levels";

//	public static string kProductIDSubscription = "subscription";

//	[Header("Write your desire Deal's string to be used for further references")]
//	public List<string> InappKeys;

//	private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

//	private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

//	private void Start()
//	{
//		if (m_StoreController == null)
//		{
//			InitializePurchasing();
//		}
//	}

//	public void InitializePurchasing()
//	{
//		if (!IsInitialized())
//		{
//			ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
//			configurationBuilder.AddProduct(InappKeys[0], ProductType.NonConsumable);
//			configurationBuilder.AddProduct(InappKeys[1], ProductType.NonConsumable);
//			for (int i = 2; i < InappKeys.Count; i++)
//			{
//				configurationBuilder.AddProduct(InappKeys[i], ProductType.NonConsumable);
//			}
//			UnityPurchasing.Initialize(this, configurationBuilder);
//		}
//	}

//	private bool IsInitialized()
//	{
//		return m_StoreController != null && m_StoreExtensionProvider != null;
//	}

//	public void BuyDeals(int _num)
//	{
//		BuyProductID(InappKeys[_num]);
//	}

//	private void BuyProductID(string productId)
//	{
//		if (IsInitialized())
//		{
//			Product product = m_StoreController.products.WithID(productId);
//			if (product != null && product.availableToPurchase)
//			{
//				m_StoreController.InitiatePurchase(product);
//			}
//		}
//	}

//	public void RestorePurchases()
//	{
//		if (IsInitialized() && (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer))
//		{
//			IAppleExtensions extension = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
//			extension.RestoreTransactions(delegate
//			{
//			});
//		}
//	}

//	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//	{
//		m_StoreController = controller;
//		m_StoreExtensionProvider = extensions;
//	}

//	public void OnInitializeFailed(InitializationFailureReason error)
//	{
//	}

//	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
//	{
//		PurchaseProcessingResult result = PurchaseProcessingResult.Pending;
//		for (int i = 0; i < InappKeys.Count; i++)
//		{
//			if (string.Equals(args.purchasedProduct.definition.id, InappKeys[i], StringComparison.Ordinal))
//			{
//				_ads_manager.Instance.InappSuccessArgs(InappKeys[i]);
//				if (i != 1)
//				{
//					return result = PurchaseProcessingResult.Complete;
//				}
//				result = PurchaseProcessingResult.Pending;
//			}
//		}
//		return result;
//	}

//	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//	{
//	}
//}
