using UnityEngine;

public class Device_Size : MonoBehaviour
{
	public static float ScreenHeightInInches;

	public static float ScreenWidthInInches;

	private void Start()
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("android.util.DisplayMetrics");
		AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.util.DisplayMetrics");
		AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject androidJavaObject2 = @static.Call<AndroidJavaObject>("getWindowManager", new object[0]);
		AndroidJavaObject androidJavaObject3 = androidJavaObject2.Call<AndroidJavaObject>("getDefaultDisplay", new object[0]);
		androidJavaObject3.Call("getMetrics", androidJavaObject);
		float num = androidJavaObject.Get<float>("density");
		int num2 = androidJavaObject.Get<int>("densityDpi");
		int num3 = androidJavaObject.Get<int>("heightPixels");
		int num4 = androidJavaObject.Get<int>("widthPixels");
		float num5 = androidJavaObject.Get<float>("scaledDensity");
		float num6 = androidJavaObject.Get<float>("xdpi");
		float num7 = androidJavaObject.Get<float>("ydpi");
		float num8 = (float)num4 / num6;
		float num9 = (float)num3 / num7;
		float num10 = num8 * 2.54f;
		float num11 = num9 * 2.54f;
		ScreenHeightInInches = num11 * 49.5f;
		ScreenWidthInInches = num10 * 27.2f;
	}
}
