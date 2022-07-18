using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class Modifications : MonoBehaviour
{
	public static int current_rim = 1;

	public static int current_sticker = 1;

	public static int car_color = 1;

	public static int rewarded_item = 1;

	public static int coin_item = 1;

	public static int current_steering = 1;

	public Texture[] sticker_textures;

	public Material player_body;

	public Color green;

	public Color brown;

	public Color lightblue;

	public Color yellow;

	public Color red;

	public Color pink;

	public Color menu_car_color;

	public Color white;

	public Color mix1;

	public Color mix2;

	public Color mix3;

	public Color mix4;

	public Color mix5;

	public Color mix6;

	public Color mix7;

	public Color mix8;

	public Color mix9;

	public Color mix10;

	public Color mix11;

	public Color mix12;

	public Color mix13;

	public Button[] rewarded_btns;

	public Button[] coin_btns;

	public Button start_btn;

	public Image[] orginal_btns;

	public Image[] buy_wali_image;

	public Image[] dummy_images;

	public Image[] red_images;

	public Image[] green_images;

	public static Modifications modify;

	public Text coin_text;

	public Text price_text;

	public Text V4;

	public Text V5;

	public Text V6;

	public Text V7;

	public Text R4;

	public Text R5;

	public Texture[] tyres_meshes;

	public Material[] tyres_materials;

	public GameObject buy_panel;

	public GameObject rewarded_panel;

	public GameObject video_not_avaible;

	public GameObject not_enough_coins;

	public string analyticsVersionString;

	public GameObject paint_panel;

	public GameObject decal_panel;

	public GameObject rim_panel;

	public int paint_counter;

	public int decal_counter;

	public int rim_counter;

	private void Start()
	{
		modify = this;
		Invoke("ColorFunction", 0.2f);
		Invoke("rimfunction", 0.2f);
		Invoke("stickerfunction", 0.2f);
		Invoke("purchased_items", 0.2f);
		coin_text.text = PlayerPrefs.GetInt("coins").ToString();
		if (PlayerPrefs.GetInt("coins") == 500)
		{
			PlayerPrefs.SetInt("500", 1);
		}
	}

	public void color_selction(int colorr)
	{
		car_color = colorr;
		PlayerPrefs.SetInt("color", car_color);
		ColorFunction();
		buy_panel_of();
		rewarded_panel_of();
		MainMenu.menu.btn_sound();
		Analytics.CustomEvent("paint" + analyticsVersionString, new Dictionary<string, object> { { "paint", car_color } });
		paint_counter++;
		if (PlayerPrefs.GetInt("sold6") != 6 && PlayerPrefs.GetInt("sold7") != 7 && PlayerPrefs.GetInt("sold1") != 1 && PlayerPrefs.GetInt("sold2") != 2)
		{
			if (paint_counter == 2)
			{
				paint_panel.SetActive(value: true);
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
			else if (paint_counter == 5)
			{
				paint_panel.SetActive(value: true);
				paint_counter = 0;
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
		}
	}

	public void rim_selction(int rims)
	{
		current_rim = rims;
		PlayerPrefs.SetInt("rim", current_rim);
		rimfunction();
		buy_panel_of();
		rewarded_panel_of();
		MainMenu.menu.btn_sound();
		Analytics.CustomEvent("rim" + analyticsVersionString, new Dictionary<string, object> { { "rim", current_rim } });
		rim_counter++;
		if (PlayerPrefs.GetInt("sold8") != 8 && PlayerPrefs.GetInt("sold3") != 3)
		{
			if (rim_counter == 2)
			{
				rim_panel.SetActive(value: true);
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
			else if (rim_counter == 5)
			{
				rim_panel.SetActive(value: true);
				rim_counter = 0;
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
		}
	}

	public void sticker_selction(int sticker)
	{
		current_sticker = sticker;
		PlayerPrefs.SetInt("sticker", current_sticker);
		stickerfunction();
		buy_panel_of();
		rewarded_panel_of();
		MainMenu.menu.btn_sound();
		Analytics.CustomEvent("sticker" + analyticsVersionString, new Dictionary<string, object> { { "sticker", current_sticker } });
		decal_counter++;
		if (PlayerPrefs.GetInt("sold9") != 9 && PlayerPrefs.GetInt("sold10") != 10 && PlayerPrefs.GetInt("sold4") != 4 && PlayerPrefs.GetInt("sold5") != 5)
		{
			if (decal_counter == 2)
			{
				decal_panel.SetActive(value: true);
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
			else if (decal_counter == 5)
			{
				decal_panel.SetActive(value: true);
				decal_counter = 0;
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
		}
	}

	public void ColorFunction()
	{
		if (PlayerPrefs.GetInt("color") == 1)
		{
			player_body.color = green;
		}
		else if (PlayerPrefs.GetInt("color") == 2)
		{
			player_body.color = brown;
		}
		else if (PlayerPrefs.GetInt("color") == 3 && PlayerPrefs.GetInt("sold6") == 6)
		{
			player_body.color = lightblue;
		}
		else if (PlayerPrefs.GetInt("color") == 4 && PlayerPrefs.GetInt("sold7") == 7)
		{
			player_body.color = yellow;
		}
		else if (PlayerPrefs.GetInt("color") == 5 && PlayerPrefs.GetInt("sold1") == 1)
		{
			player_body.color = red;
		}
		else if (PlayerPrefs.GetInt("color") == 6)
		{
			player_body.color = white;
		}
		else if (PlayerPrefs.GetInt("color") == 7 && PlayerPrefs.GetInt("sold2") == 2)
		{
			player_body.color = pink;
		}
		else if (PlayerPrefs.GetInt("color") == 8 && PlayerPrefs.GetInt("sold15") == 15)
		{
			player_body.color = mix1;
		}
		else if (PlayerPrefs.GetInt("color") == 9 && PlayerPrefs.GetInt("sold16") == 16)
		{
			player_body.color = mix2;
		}
		else if (PlayerPrefs.GetInt("color") == 10 && PlayerPrefs.GetInt("sold17") == 17)
		{
			player_body.color = mix3;
		}
		else if (PlayerPrefs.GetInt("color") == 11 && PlayerPrefs.GetInt("sold18") == 18)
		{
			player_body.color = mix4;
		}
		else if (PlayerPrefs.GetInt("color") == 12 && PlayerPrefs.GetInt("sold19") == 19)
		{
			player_body.color = mix5;
		}
		else if (PlayerPrefs.GetInt("color") == 13 && PlayerPrefs.GetInt("sold20") == 20)
		{
			player_body.color = mix6;
		}
		else if (PlayerPrefs.GetInt("color") == 14 && PlayerPrefs.GetInt("sold21") == 21)
		{
			player_body.color = mix7;
		}
		else if (PlayerPrefs.GetInt("color") == 15 && PlayerPrefs.GetInt("sold22") == 22)
		{
			player_body.color = mix8;
		}
		else if (PlayerPrefs.GetInt("color") == 16)
		{
			player_body.color = mix9;
		}
		else if (PlayerPrefs.GetInt("color") == 17 && PlayerPrefs.GetInt("sold23") == 23)
		{
			player_body.color = mix10;
		}
		else if (PlayerPrefs.GetInt("color") == 18 && PlayerPrefs.GetInt("sold24") == 24)
		{
			player_body.color = mix11;
		}
		else if (PlayerPrefs.GetInt("color") == 19)
		{
			player_body.color = mix12;
		}
		else if (PlayerPrefs.GetInt("color") == 20)
		{
			player_body.color = mix13;
		}
		else if (PlayerPrefs.GetInt("color") == 0)
		{
			player_body.color = menu_car_color;
		}
	}

	public void rimfunction()
	{
		if (PlayerPrefs.GetInt("rim") == 1)
		{
			tyres_materials[0].mainTexture = tyres_meshes[1];
		}
		else if (PlayerPrefs.GetInt("rim") == 2 && PlayerPrefs.GetInt("sold8") == 8)
		{
			tyres_materials[0].mainTexture = tyres_meshes[2];
		}
		else if (PlayerPrefs.GetInt("rim") == 3 && PlayerPrefs.GetInt("sold3") == 3)
		{
			tyres_materials[0].mainTexture = tyres_meshes[3];
		}
		else if (PlayerPrefs.GetInt("rim") == 4)
		{
			tyres_materials[0].mainTexture = tyres_meshes[4];
		}
		else if (PlayerPrefs.GetInt("rim") == 0)
		{
			tyres_materials[0].mainTexture = tyres_meshes[0];
		}
	}

	public void stickerfunction()
	{
		if (PlayerPrefs.GetInt("sticker") == 1)
		{
			player_body.mainTexture = sticker_textures[0];
		}
		else if (PlayerPrefs.GetInt("sticker") == 2)
		{
			player_body.mainTexture = sticker_textures[1];
		}
		else if (PlayerPrefs.GetInt("sticker") == 3)
		{
			player_body.mainTexture = sticker_textures[2];
		}
		else if (PlayerPrefs.GetInt("sticker") == 4 && PlayerPrefs.GetInt("sold9") == 9)
		{
			player_body.mainTexture = sticker_textures[3];
		}
		else if (PlayerPrefs.GetInt("sticker") == 5 && PlayerPrefs.GetInt("sold10") == 10)
		{
			player_body.mainTexture = sticker_textures[4];
		}
		else if (PlayerPrefs.GetInt("sticker") == 6 && PlayerPrefs.GetInt("sold4") == 4)
		{
			player_body.mainTexture = sticker_textures[5];
		}
		else if (PlayerPrefs.GetInt("sticker") == 7 && PlayerPrefs.GetInt("sold5") == 5)
		{
			player_body.mainTexture = sticker_textures[6];
		}
		else if (PlayerPrefs.GetInt("sticker") == 0)
		{
			player_body.mainTexture = sticker_textures[7];
		}
	}

	public void rewarded_item_no(int item_no)
	{
		rewarded_item = item_no;
		just_check_items_rewarded();
		MainMenu.menu.btn_sound();
		if (rewarded_item == 1 || rewarded_item == 2 || rewarded_item == 6 || rewarded_item == 7 || rewarded_item == 8 || rewarded_item == 10 || rewarded_item == 11)
		{
			paint_counter++;
			if (paint_counter == 2)
			{
				paint_panel.SetActive(value: true);
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
			else if (paint_counter == 5)
			{
				paint_panel.SetActive(value: true);
				paint_counter = 0;
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
		}
		else if (rewarded_item == 3)
		{
			rim_counter++;
			if (rim_counter == 2)
			{
				rim_panel.SetActive(value: true);
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
			else if (rim_counter == 5)
			{
				rim_panel.SetActive(value: true);
				rim_counter = 0;
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
		}
		else if (rewarded_item == 4 || rewarded_item == 5)
		{
			decal_counter++;
			if (decal_counter == 2)
			{
				decal_panel.SetActive(value: true);
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
			else if (decal_counter == 5)
			{
				decal_panel.SetActive(value: true);
				decal_counter = 0;
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
		}
	}

	public void coin_item_no(int coin_no)
	{
		coin_item = coin_no;
		just_check_items();
		MainMenu.menu.btn_sound();
		if (coin_item == 1 || coin_item == 2 || coin_item == 6 || coin_item == 7 || coin_item == 8 || coin_item == 9 || coin_item == 10)
		{
			paint_counter++;
			if (paint_counter == 2)
			{
				paint_panel.SetActive(value: true);
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
			else if (paint_counter == 5)
			{
				paint_panel.SetActive(value: true);
				paint_counter = 0;
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
		}
		else if (coin_item == 3)
		{
			rim_counter++;
			if (rim_counter == 2)
			{
				rim_panel.SetActive(value: true);
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
			else if (rim_counter == 5)
			{
				rim_panel.SetActive(value: true);
				rim_counter = 0;
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
		}
		else if (coin_item == 4 || coin_item == 5)
		{
			decal_counter++;
			if (decal_counter == 2)
			{
				decal_panel.SetActive(value: true);
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
			else if (decal_counter == 5)
			{
				decal_panel.SetActive(value: true);
				decal_counter = 0;
				MainMenu.menu.main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			}
		}
	}

	public void btn(string btns)
	{
		switch (btns)
		{
		case "no":
			buy_panel_of();
			rewarded_panel_of();
			break;
		case "reward":
			if (MainMenu.menu.pain_panel.activeSelf)
			{
				//_ads_manager.last_reward = rewarde_type.paints;
			}
			else if (MainMenu.menu.rim_panel.activeSelf)
			{
				//_ads_manager.last_reward = rewarde_type.rims;
			}
			else if (MainMenu.menu.stickers_panel.activeSelf)
			{
				//_ads_manager.last_reward = rewarde_type.vinyls;
			}
		//	_ads_manager.Instance._Show_unity_rewarded_Ad();
			break;
		case "coines":
			coin_purchase();
			break;
		}
		MainMenu.menu.btn_sound();
	}

	public void rewarded_result()
	{
		if (rewarded_item == 1)
		{
			rewarded_btns[0].gameObject.SetActive(value: false);
			red_images[5].sprite = green_images[0].sprite;
			PlayerPrefs.SetInt("sold1", 1);
			PlayerPrefs.SetInt("color", 5);
			rewarded_panel_of();
			Analytics.CustomEvent("paint" + analyticsVersionString, new Dictionary<string, object> { { "paint", 5 } });
		}
		else if (rewarded_item == 2)
		{
			rewarded_btns[1].gameObject.SetActive(value: false);
			red_images[6].sprite = green_images[0].sprite;
			PlayerPrefs.SetInt("sold2", 2);
			PlayerPrefs.SetInt("color", 7);
			rewarded_panel_of();
			Analytics.CustomEvent("paint" + analyticsVersionString, new Dictionary<string, object> { { "paint", 7 } });
		}
		else if (rewarded_item == 3)
		{
			rewarded_btns[2].gameObject.SetActive(value: false);
			red_images[7].sprite = green_images[0].sprite;
			PlayerPrefs.SetInt("sold3", 3);
			PlayerPrefs.SetInt("rim", 3);
			R5.enabled = true;
			rewarded_panel_of();
			Analytics.CustomEvent("rim" + analyticsVersionString, new Dictionary<string, object> { { "rim", 3 } });
		}
		else if (rewarded_item == 4)
		{
			rewarded_btns[3].gameObject.SetActive(value: false);
			red_images[8].sprite = green_images[0].sprite;
			PlayerPrefs.SetInt("sold4", 4);
			PlayerPrefs.SetInt("sticker", 6);
			V6.enabled = true;
			orginal_btns[1].color = Color.white;
			rewarded_panel_of();
			Analytics.CustomEvent("sticker" + analyticsVersionString, new Dictionary<string, object> { { "sticker", 6 } });
		}
		else if (rewarded_item == 5)
		{
			rewarded_btns[4].gameObject.SetActive(value: false);
			red_images[9].sprite = green_images[0].sprite;
			PlayerPrefs.SetInt("sold5", 5);
			PlayerPrefs.SetInt("sticker", 7);
			V7.enabled = true;
			rewarded_panel_of();
			Analytics.CustomEvent("sticker" + analyticsVersionString, new Dictionary<string, object> { { "sticker", 7 } });
		}
		else if (rewarded_item == 6)
		{
			rewarded_btns[5].gameObject.SetActive(value: false);
			red_images[10].sprite = green_images[0].sprite;
			PlayerPrefs.SetInt("sold20", 20);
			PlayerPrefs.SetInt("color", 13);
			rewarded_panel_of();
			Analytics.CustomEvent("paint" + analyticsVersionString, new Dictionary<string, object> { { "paint", 13 } });
		}
		else if (rewarded_item == 7)
		{
			rewarded_btns[6].gameObject.SetActive(value: false);
			red_images[11].sprite = green_images[0].sprite;
			PlayerPrefs.SetInt("sold21", 21);
			PlayerPrefs.SetInt("color", 14);
			rewarded_panel_of();
			Analytics.CustomEvent("paint" + analyticsVersionString, new Dictionary<string, object> { { "paint", 14 } });
		}
		else if (rewarded_item == 8)
		{
			rewarded_btns[7].gameObject.SetActive(value: false);
			red_images[12].sprite = green_images[0].sprite;
			PlayerPrefs.SetInt("sold22", 22);
			PlayerPrefs.SetInt("color", 15);
			rewarded_panel_of();
			Analytics.CustomEvent("paint" + analyticsVersionString, new Dictionary<string, object> { { "paint", 15 } });
		}
		else if (rewarded_item == 10)
		{
			rewarded_btns[8].gameObject.SetActive(value: false);
			red_images[13].sprite = green_images[0].sprite;
			PlayerPrefs.SetInt("sold23", 23);
			PlayerPrefs.SetInt("color", 17);
			rewarded_panel_of();
			Analytics.CustomEvent("paint" + analyticsVersionString, new Dictionary<string, object> { { "paint", 17 } });
		}
		else if (rewarded_item == 11)
		{
			rewarded_btns[9].gameObject.SetActive(value: false);
			red_images[14].sprite = green_images[0].sprite;
			PlayerPrefs.SetInt("sold24", 24);
			PlayerPrefs.SetInt("color", 18);
			rewarded_panel_of();
			Analytics.CustomEvent("paint" + analyticsVersionString, new Dictionary<string, object> { { "paint", 18 } });
		}
	}

	public void coin_purchase()
	{
		if (coin_item == 1)
		{
			if (PlayerPrefs.GetInt("coins") >= 100)
			{
				PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 100);
				coin_btns[0].gameObject.SetActive(value: false);
				red_images[0].sprite = green_images[0].sprite;
				PlayerPrefs.SetInt("sold6", 6);
				coin_text.text = PlayerPrefs.GetInt("coins").ToString();
				PlayerPrefs.SetInt("color", 3);
				buy_panel_of();
			}
			else
			{
				not_enough_coins.SetActive(value: true);
			}
			Analytics.CustomEvent("paint" + analyticsVersionString, new Dictionary<string, object> { { "paint", 3 } });
		}
		if (coin_item == 2)
		{
			if (PlayerPrefs.GetInt("coins") >= 200)
			{
				PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 200);
				coin_btns[1].gameObject.SetActive(value: false);
				red_images[1].sprite = green_images[0].sprite;
				PlayerPrefs.SetInt("sold7", 7);
				coin_text.text = PlayerPrefs.GetInt("coins").ToString();
				PlayerPrefs.SetInt("color", 4);
				buy_panel_of();
			}
			else
			{
				not_enough_coins.SetActive(value: true);
			}
			Analytics.CustomEvent("paint" + analyticsVersionString, new Dictionary<string, object> { { "paint", 4 } });
		}
		else if (coin_item == 3)
		{
			if (PlayerPrefs.GetInt("coins") >= 300)
			{
				PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 300);
				coin_btns[2].gameObject.SetActive(value: false);
				red_images[2].sprite = green_images[0].sprite;
				PlayerPrefs.SetInt("sold8", 8);
				coin_text.text = PlayerPrefs.GetInt("coins").ToString();
				PlayerPrefs.SetInt("rim", 2);
				R4.enabled = true;
				buy_panel_of();
			}
			else
			{
				not_enough_coins.SetActive(value: true);
			}
			Analytics.CustomEvent("rim" + analyticsVersionString, new Dictionary<string, object> { { "rim", 2 } });
		}
		else if (coin_item == 4)
		{
			if (PlayerPrefs.GetInt("coins") >= 400)
			{
				PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 400);
				coin_btns[3].gameObject.SetActive(value: false);
				red_images[3].sprite = green_images[0].sprite;
				V4.enabled = true;
				PlayerPrefs.SetInt("sold9", 9);
				PlayerPrefs.SetInt("sticker", 4);
				coin_text.text = PlayerPrefs.GetInt("coins").ToString();
				buy_panel_of();
			}
			else
			{
				not_enough_coins.SetActive(value: true);
			}
			Analytics.CustomEvent("sticker" + analyticsVersionString, new Dictionary<string, object> { { "sticker", 4 } });
		}
		else if (coin_item == 5)
		{
			if (PlayerPrefs.GetInt("coins") >= 500)
			{
				PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 500);
				coin_btns[4].gameObject.SetActive(value: false);
				red_images[4].sprite = green_images[0].sprite;
				V5.enabled = true;
				PlayerPrefs.SetInt("sold10", 10);
				PlayerPrefs.SetInt("sticker", 5);
				coin_text.text = PlayerPrefs.GetInt("coins").ToString();
				buy_panel_of();
			}
			else
			{
				not_enough_coins.SetActive(value: true);
			}
			Analytics.CustomEvent("sticker" + analyticsVersionString, new Dictionary<string, object> { { "sticker", 5 } });
		}
		else if (coin_item == 6)
		{
			if (PlayerPrefs.GetInt("coins") >= 500)
			{
				PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 500);
				coin_btns[5].gameObject.SetActive(value: false);
				red_images[15].sprite = green_images[0].sprite;
				PlayerPrefs.SetInt("sold15", 15);
				coin_text.text = PlayerPrefs.GetInt("coins").ToString();
				PlayerPrefs.SetInt("color", 8);
				buy_panel_of();
			}
			else
			{
				not_enough_coins.SetActive(value: true);
			}
			Analytics.CustomEvent("paint" + analyticsVersionString, new Dictionary<string, object> { { "paint", 8 } });
		}
		else if (coin_item == 7)
		{
			if (PlayerPrefs.GetInt("coins") >= 600)
			{
				PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 600);
				coin_btns[6].gameObject.SetActive(value: false);
				red_images[16].sprite = green_images[0].sprite;
				PlayerPrefs.SetInt("sold16", 16);
				coin_text.text = PlayerPrefs.GetInt("coins").ToString();
				PlayerPrefs.SetInt("color", 9);
				buy_panel_of();
			}
			else
			{
				not_enough_coins.SetActive(value: true);
			}
			Analytics.CustomEvent("paint" + analyticsVersionString, new Dictionary<string, object> { { "paint", 9 } });
		}
		else if (coin_item == 8)
		{
			if (PlayerPrefs.GetInt("coins") >= 700)
			{
				PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 700);
				coin_btns[7].gameObject.SetActive(value: false);
				red_images[17].sprite = green_images[0].sprite;
				PlayerPrefs.SetInt("sold17", 17);
				coin_text.text = PlayerPrefs.GetInt("coins").ToString();
				PlayerPrefs.SetInt("color", 10);
				buy_panel_of();
			}
			else
			{
				not_enough_coins.SetActive(value: true);
			}
			Analytics.CustomEvent("paint" + analyticsVersionString, new Dictionary<string, object> { { "paint", 10 } });
		}
		else if (coin_item == 9)
		{
			if (PlayerPrefs.GetInt("coins") >= 800)
			{
				PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 800);
				coin_btns[8].gameObject.SetActive(value: false);
				red_images[18].sprite = green_images[0].sprite;
				PlayerPrefs.SetInt("sold18", 18);
				coin_text.text = PlayerPrefs.GetInt("coins").ToString();
				PlayerPrefs.SetInt("color", 11);
				buy_panel_of();
			}
			else
			{
				not_enough_coins.SetActive(value: true);
			}
			Analytics.CustomEvent("paint" + analyticsVersionString, new Dictionary<string, object> { { "paint", 11 } });
		}
		else if (coin_item == 10)
		{
			if (PlayerPrefs.GetInt("coins") >= 900)
			{
				PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 900);
				coin_btns[9].gameObject.SetActive(value: false);
				red_images[19].sprite = green_images[0].sprite;
				PlayerPrefs.SetInt("sold19", 19);
				coin_text.text = PlayerPrefs.GetInt("coins").ToString();
				PlayerPrefs.SetInt("color", 12);
				buy_panel_of();
			}
			else
			{
				not_enough_coins.SetActive(value: true);
			}
			Analytics.CustomEvent("paint" + analyticsVersionString, new Dictionary<string, object> { { "paint", 12 } });
		}
	}

	public void purchased_items()
	{
		if (PlayerPrefs.GetInt("sold1") == 1)
		{
			rewarded_btns[0].gameObject.SetActive(value: false);
			red_images[5].sprite = green_images[0].sprite;
		}
		if (PlayerPrefs.GetInt("sold2") == 2)
		{
			rewarded_btns[1].gameObject.SetActive(value: false);
			red_images[6].sprite = green_images[0].sprite;
		}
		if (PlayerPrefs.GetInt("sold6") == 6)
		{
			coin_btns[0].gameObject.SetActive(value: false);
			red_images[0].sprite = green_images[0].sprite;
		}
		if (PlayerPrefs.GetInt("sold7") == 7)
		{
			coin_btns[1].gameObject.SetActive(value: false);
			red_images[1].sprite = green_images[0].sprite;
		}
		if (PlayerPrefs.GetInt("sold8") == 8)
		{
			coin_btns[2].gameObject.SetActive(value: false);
			red_images[2].sprite = green_images[0].sprite;
			R4.enabled = true;
		}
		if (PlayerPrefs.GetInt("sold3") == 3)
		{
			rewarded_btns[2].gameObject.SetActive(value: false);
			red_images[7].sprite = green_images[0].sprite;
			R5.enabled = true;
		}
		if (PlayerPrefs.GetInt("sold9") == 9)
		{
			coin_btns[3].gameObject.SetActive(value: false);
			red_images[3].sprite = green_images[0].sprite;
			V4.enabled = true;
		}
		if (PlayerPrefs.GetInt("sold10") == 10)
		{
			coin_btns[4].gameObject.SetActive(value: false);
			red_images[4].sprite = green_images[0].sprite;
			V5.enabled = true;
		}
		if (PlayerPrefs.GetInt("sold4") == 4)
		{
			rewarded_btns[3].gameObject.SetActive(value: false);
			red_images[8].sprite = green_images[0].sprite;
			V6.enabled = true;
		}
		if (PlayerPrefs.GetInt("sold5") == 5)
		{
			rewarded_btns[4].gameObject.SetActive(value: false);
			red_images[9].sprite = green_images[0].sprite;
			V7.enabled = true;
		}
		if (PlayerPrefs.GetInt("sold11") == 11)
		{
			coin_btns[5].gameObject.SetActive(value: false);
			red_images[5].sprite = green_images[0].sprite;
		}
		if (PlayerPrefs.GetInt("sold12") == 12)
		{
			coin_btns[6].gameObject.SetActive(value: false);
			red_images[6].sprite = green_images[0].sprite;
		}
		if (PlayerPrefs.GetInt("sold15") == 15)
		{
			coin_btns[5].gameObject.SetActive(value: false);
			red_images[15].sprite = green_images[0].sprite;
		}
		if (PlayerPrefs.GetInt("sold16") == 16)
		{
			coin_btns[6].gameObject.SetActive(value: false);
			red_images[16].sprite = green_images[0].sprite;
		}
		if (PlayerPrefs.GetInt("sold17") == 17)
		{
			coin_btns[7].gameObject.SetActive(value: false);
			red_images[17].sprite = green_images[0].sprite;
		}
		if (PlayerPrefs.GetInt("sold18") == 18)
		{
			coin_btns[8].gameObject.SetActive(value: false);
			red_images[18].sprite = green_images[0].sprite;
		}
		if (PlayerPrefs.GetInt("sold19") == 19)
		{
			coin_btns[9].gameObject.SetActive(value: false);
			red_images[19].sprite = green_images[0].sprite;
		}
		if (PlayerPrefs.GetInt("sold20") == 20)
		{
			rewarded_btns[5].gameObject.SetActive(value: false);
			red_images[10].sprite = green_images[0].sprite;
		}
		if (PlayerPrefs.GetInt("sold21") == 21)
		{
			rewarded_btns[6].gameObject.SetActive(value: false);
			red_images[11].sprite = green_images[0].sprite;
		}
		if (PlayerPrefs.GetInt("sold22") == 22)
		{
			rewarded_btns[7].gameObject.SetActive(value: false);
			red_images[12].sprite = green_images[0].sprite;
		}
		if (PlayerPrefs.GetInt("sold23") == 23)
		{
			rewarded_btns[8].gameObject.SetActive(value: false);
			red_images[13].sprite = green_images[0].sprite;
		}
		if (PlayerPrefs.GetInt("sold24") == 24)
		{
			rewarded_btns[9].gameObject.SetActive(value: false);
			red_images[14].sprite = green_images[0].sprite;
		}
	}

	public void just_check_items()
	{
		if (coin_item == 1)
		{
			player_body.color = lightblue;
			buy_panel_on();
			rewarded_panel_of();
		}
		else if (coin_item == 2)
		{
			player_body.color = yellow;
			buy_panel_on();
			rewarded_panel_of();
		}
		else if (coin_item == 3)
		{
			tyres_materials[0].mainTexture = tyres_meshes[2];
			buy_panel_on();
			rewarded_panel_of();
		}
		else if (coin_item == 4)
		{
			player_body.mainTexture = sticker_textures[3];
			buy_panel_on();
			rewarded_panel_of();
		}
		else if (coin_item == 5)
		{
			player_body.mainTexture = sticker_textures[4];
			buy_panel_on();
			rewarded_panel_of();
		}
		else if (coin_item == 6)
		{
			player_body.color = mix1;
			buy_panel_on();
			rewarded_panel_of();
		}
		else if (coin_item == 7)
		{
			player_body.color = mix2;
			buy_panel_on();
			rewarded_panel_of();
		}
		else if (coin_item == 8)
		{
			player_body.color = mix3;
			buy_panel_on();
			rewarded_panel_of();
		}
		else if (coin_item == 9)
		{
			player_body.color = mix4;
			buy_panel_on();
			rewarded_panel_of();
		}
		else if (coin_item == 10)
		{
			player_body.color = mix5;
			buy_panel_on();
			rewarded_panel_of();
		}
	}

	public void just_check_items_rewarded()
	{
		if (rewarded_item == 1)
		{
			player_body.color = red;
			rewarded_panel_on();
			buy_panel_of();
		}
		else if (rewarded_item == 2)
		{
			player_body.color = pink;
			rewarded_panel_on();
			buy_panel_of();
		}
		else if (rewarded_item == 3)
		{
			tyres_materials[0].mainTexture = tyres_meshes[3];
			rewarded_panel_on();
			buy_panel_of();
		}
		else if (rewarded_item == 4)
		{
			player_body.mainTexture = sticker_textures[5];
			rewarded_panel_on();
			buy_panel_of();
		}
		else if (rewarded_item == 5)
		{
			player_body.mainTexture = sticker_textures[6];
			rewarded_panel_on();
			buy_panel_of();
		}
		else if (rewarded_item == 6)
		{
			player_body.color = mix6;
			rewarded_panel_on();
			buy_panel_of();
		}
		else if (rewarded_item == 7)
		{
			player_body.color = mix7;
			rewarded_panel_on();
			buy_panel_of();
		}
		else if (rewarded_item == 8)
		{
			player_body.color = mix8;
			rewarded_panel_on();
			buy_panel_of();
		}
		else if (rewarded_item == 10)
		{
			player_body.color = mix10;
			rewarded_panel_on();
			buy_panel_of();
		}
		else if (rewarded_item == 11)
		{
			player_body.color = mix11;
			rewarded_panel_on();
			buy_panel_of();
		}
	}

	public void buy_panel_on()
	{
		buy_panel.SetActive(value: true);
		start_btn.gameObject.SetActive(value: false);
	}

	public void buy_panel_of()
	{
		buy_panel.SetActive(value: false);
		start_btn.gameObject.SetActive(value: true);
	}

	public void rewarded_panel_on()
	{
		rewarded_panel.SetActive(value: true);
		start_btn.gameObject.SetActive(value: false);
	}

	public void rewarded_panel_of()
	{
		rewarded_panel.SetActive(value: false);
		start_btn.gameObject.SetActive(value: true);
	}
}
