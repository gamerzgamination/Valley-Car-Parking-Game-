using System;
using UnityEngine;

public class FEED : MonoBehaviour
{
	public void EmailUs()
	{
		try
		{
			string text = "kn.playaso1@gmail.com";
			string text2 = MyEscapeURL("feedback");
			string text3 = MyEscapeURL("please type your message here");
			Application.OpenURL("mailto:" + text + "?subject=" + text2 + "&body=" + text3);
		}
		catch (Exception)
		{
		}
	}

	private string MyEscapeURL(string url)
	{
		return WWW.EscapeURL(url).Replace("+", "%20");
	}
}
