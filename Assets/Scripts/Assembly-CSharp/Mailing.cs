using System;
using UnityEngine;

public class Mailing : MonoBehaviour
{
	public void EmailUs()
	{
		try
		{
			string text = "kn.playaso1@gmail.com";
			string text2 = MyEscapeURL("GET FREE COINS CODE");
			string text3 = MyEscapeURL("TYPE YOUR NAME TO GET FREE CODE");
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
