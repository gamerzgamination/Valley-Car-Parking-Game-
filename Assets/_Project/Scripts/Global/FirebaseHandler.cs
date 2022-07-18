using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using Firebase;
//using Firebase.Analytics;
using UnityEngine.Networking;
public class FirebaseHandler : MonoBehaviour
{

	public static FirebaseHandler instance;
	private bool NetworkService;
	private void Awake()
	{
		instance = this;
	}
	void Start()
	{

		//if (Application.internetReachability != NetworkReachability.NotReachable)
		//{
		//	Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
		//	{
		//		var dependencyStatus = task.Result;
		//		if (dependencyStatus == Firebase.DependencyStatus.Available)
		//		{
		//			// Create and hold a reference to your FirebaseApp,
		//			// where app is a Firebase.FirebaseApp property of your application class.
		//			Firebase.FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

		//			// Set a flag here to indicate whether Firebase is ready to use by your app.
		//			FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
		//			//	Debug.LogError(" resolve all Firebase dependencies");

		//		}
		//		else
		//		{
		//			UnityEngine.Debug.LogError(System.String.Format(
		//			  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
		//			// Firebase Unity SDK is not safe to use here.
		//		}
		//	});

		//}
		//if (CheckInternet())
		//	return;
	}
	public bool CheckInternet()
	{
		StartCoroutine(CheckInternetConnection(isConnected =>
		{

			NetworkService = isConnected;

		}));
		return NetworkService;
	}

	IEnumerator CheckInternetConnection(Action<bool> action)
	{
		UnityWebRequest request = new UnityWebRequest("http://google.com");
		yield return request.SendWebRequest();
		action(!request.isNetworkError && !request.isHttpError && request.responseCode == 200);


	}


}
