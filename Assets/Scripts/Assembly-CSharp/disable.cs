using UnityEngine;
using UnityEngine.UI;

public class disable : MonoBehaviour
{
	public Button[] inapp_butns;

	private void Start()
	{
		Invoke("all_off", 0.1f);
	}

	public void all_off()
	{
		if (PlayerPrefs.GetInt("20wala") == 1 && PlayerPrefs.GetInt("10wala") == 1 && PlayerPrefs.GetInt("10wala4") == 1 && PlayerPrefs.GetInt("10wala5") == 1)
		{
			inapp_butns[0].interactable = false;
			inapp_butns[11].interactable = false;
		}
		if (PlayerPrefs.GetInt("level1") == 19 || PlayerPrefs.GetInt("level1") == 20)
		{
			inapp_butns[1].interactable = false;
		}
		if (PlayerPrefs.GetInt("level2") == 9 || PlayerPrefs.GetInt("level2") == 10)
		{
			inapp_butns[2].interactable = false;
		}
		if (PlayerPrefs.GetInt("level3") == 9 || PlayerPrefs.GetInt("level3") == 10)
		{
			inapp_butns[3].interactable = false;
		}
		if (PlayerPrefs.GetInt("level4") == 9 || PlayerPrefs.GetInt("level4") == 10)
		{
			inapp_butns[4].interactable = false;
		}
		if (PlayerPrefs.GetInt("level5") == 9 || PlayerPrefs.GetInt("level5") == 10)
		{
			inapp_butns[5].interactable = false;
		}
		if (PlayerPrefs.GetInt("RemoveAds") == 1)
		{
			inapp_butns[6].interactable = false;
		}
		if (PlayerPrefs.GetInt("sold9") == 9 && PlayerPrefs.GetInt("sold10") == 10 && PlayerPrefs.GetInt("sold4") == 4 && PlayerPrefs.GetInt("sold5") == 5)
		{
			inapp_butns[7].transform.parent.gameObject.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("sold8") == 8 && PlayerPrefs.GetInt("sold3") == 3)
		{
			inapp_butns[8].transform.parent.gameObject.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("sold6") == 6 && PlayerPrefs.GetInt("sold7") == 7 && PlayerPrefs.GetInt("sold1") == 1 && PlayerPrefs.GetInt("sold2") == 2)
		{
			inapp_butns[9].transform.parent.gameObject.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("RemoveAds") == 1 && PlayerPrefs.GetInt("20wala") == 1)
		{
			inapp_butns[10].interactable = false;
		}
	}
}
