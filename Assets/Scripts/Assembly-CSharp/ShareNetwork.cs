using System;
using UnityEngine;

public class ShareNetwork : MonoBehaviour
{
	public string link = string.Empty;

	private string subject = string.Empty;

	public void shareText()
	{
		try
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent");
			androidJavaObject.Call<AndroidJavaObject>("setAction", new object[1] { androidJavaClass.GetStatic<string>("ACTION_SEND") });
			androidJavaObject.Call<AndroidJavaObject>("setType", new object[1] { "text/plain" });
			androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[2]
			{
				androidJavaClass.GetStatic<string>("EXTRA_TEXT"),
				link
			});
			AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass2.GetStatic<AndroidJavaObject>("currentActivity");
			@static.Call("startActivity", androidJavaObject);
		}
		catch (Exception)
		{
		}
	}
}
