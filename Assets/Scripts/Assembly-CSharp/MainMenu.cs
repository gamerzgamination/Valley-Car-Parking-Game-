using System;
using System.Collections.Generic;
using System.Diagnostics;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public static MainMenu menu;

	public static int currentlevel = 1;

	public static int driveside = 1;

	public static int car_control = 1;

	public static int current_mode = 1;

	public static int currnt_car = 1;

	public GameObject menu_panel;

	public GameObject quit_panel;

	public GameObject color_panel;

	public GameObject setting_panel;

	public GameObject levels;

	public GameObject level_panel;

	public GameObject loading;

	public GameObject left_side;

	public GameObject right_side;

	public GameObject l_r_btns;

	public GameObject steerint_btns;

	public GameObject cans;

	public GameObject mode_panel;

	public static bool issound = true;

	public GameObject level_panel2;

	public Slider sound_slider;

	public Slider music_slider;

	public string analyticsVersionString;

	public static bool iscomplete;

	public GameObject levels2;

	public GameObject share_panel;

	public GameObject mode_3_panel_notification;

	public GameObject mode_4_panel_notification;

	public GameObject mode_5_panel_notification;

	public GameObject free_coins_panel;

	public Button mode2;

	public Button share_butn;

	public Button mode3;

	public Button mode3_btn_check;

	public Button mode4;

	public Button mode4_btn_check;

	public Button mode5;

	public Button mode5_btn_check;

	public GameObject pain_panel;

	public GameObject rim_panel;

	public GameObject stickers_panel;

	public GameObject level_panel3;

	public GameObject levels3;

	public GameObject share_complete;

	public GameObject only_complete;

	public GameObject only_share;

	public GameObject levels4;

	public GameObject level_panel4;

	public GameObject my_canvas;

	public GameObject levels5;

	public GameObject level_panel5;

	public GameObject about_us_panel;

	public GameObject video_not_available;

	public AudioSource btn_click;

	public Canvas main_cans;

	public Sprite unlock_image_level;

	public Sprite glow_image;

	public Sprite normal_image;

	public DateTime startTime;

	public DateTime endTime;

	private TimeSpan mainTIme = TimeSpan.FromSeconds(2.0);

	public static bool istwitter;

	public static bool isfacebook;

	public static bool ishare;

	public static bool isyoutube;

	public Button twitter_btn;

	public Button fb_btn;

	public Button yt_btn;

	public Button yt_btn_2;

	public Button fb_btn2;

	public Button share_btn;

	public Button free_btn;

	public Button right_arrow;

	public GameObject Main_car;

	public Animator top_bar;

	public GameObject discount_panel;

	public GameObject special_pack_panel;

	public GameObject pop_panel_levels;

	public GameObject special_butn;

	public GameObject unlock_all_modes;

	public string[] modeTxt;

	public Text Txt;

	public GameObject[] levels_butns_inapp;

	private void Start()
	{
		drive_side_check();
		car_control_check();
		menu = this;
		for (int i = 0; i <= PlayerPrefs.GetInt("level1"); i++)
		{
			levels.transform.GetChild(i).GetComponent<Button>().interactable = true;
			levels.transform.GetChild(i).GetChild(1).GetComponent<Image>()
				.enabled = false;
		}
		if (PlayerPrefs.GetInt("20wala") == 1)
		{
			mode2.interactable = true;
			share_butn.gameObject.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("10wala") == 1)
		{
			mode3.interactable = true;
			mode3_btn_check.gameObject.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("10wala4") == 1)
		{
			mode4.interactable = true;
			mode4_btn_check.gameObject.SetActive(value: false);
		}
		if (PlayerPrefs.GetInt("10wala5") == 1)
		{
			mode5.interactable = true;
			mode5_btn_check.gameObject.SetActive(value: false);
		}
		for (int j = 0; j <= PlayerPrefs.GetInt("level2"); j++)
		{
			levels2.transform.GetChild(j).GetComponent<Button>().interactable = true;
			levels2.transform.GetChild(j).GetChild(1).GetComponent<Image>()
				.enabled = false;
		}
		for (int k = 0; k <= PlayerPrefs.GetInt("level3"); k++)
		{
			levels3.transform.GetChild(k).GetComponent<Button>().interactable = true;
			levels3.transform.GetChild(k).GetChild(1).GetComponent<Image>()
				.enabled = false;
		}
		for (int l = 0; l <= PlayerPrefs.GetInt("level4"); l++)
		{
			levels4.transform.GetChild(l).GetComponent<Button>().interactable = true;
			levels4.transform.GetChild(l).GetChild(1).GetComponent<Image>()
				.enabled = false;
		}
		for (int m = 0; m <= PlayerPrefs.GetInt("level5"); m++)
		{
			levels5.transform.GetChild(m).GetComponent<Button>().interactable = true;
			levels5.transform.GetChild(m).GetChild(1).GetComponent<Image>()
				.enabled = false;
		}
		if (PlayerPrefs.GetInt("mode2") == 1 && PlayerPrefs.GetInt("onetime") == 0)
		{
			PlayerPrefs.SetInt("onetime", 1);
			menu_panel.SetActive(value: false);
			current_mode = 2;
			Main_car.SetActive(value: false);
			main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
		}
		if (PlayerPrefs.GetInt("mode3") == 1 && PlayerPrefs.GetInt("onetime3") == 0)
		{
			PlayerPrefs.SetInt("onetime3", 1);
			menu_panel.SetActive(value: false);
			current_mode = 3;
			Main_car.SetActive(value: false);
			main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
		}
		if (PlayerPrefs.GetInt("mode4") == 1 && PlayerPrefs.GetInt("onetime4") == 0)
		{
			PlayerPrefs.SetInt("onetime4", 1);
			menu_panel.SetActive(value: false);
			current_mode = 4;
			Main_car.SetActive(value: false);
			main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
		}
		if (PlayerPrefs.GetInt("mode5") == 1 && PlayerPrefs.GetInt("onetime5") == 0)
		{
			PlayerPrefs.SetInt("onetime5", 1);
			menu_panel.SetActive(value: false);
			current_mode = 5;
			Main_car.SetActive(value: false);
			main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
		}
		if (PlayerPrefs.GetInt("ms") == 0)
		{
			PlayerPrefs.SetInt("ms", 1);
			PlayerPrefs.SetFloat("sound", 0.25f);
			PlayerPrefs.SetFloat("music", 0.25f);
			sound_slider.value = PlayerPrefs.GetFloat("sound");
			music_slider.value = PlayerPrefs.GetFloat("music");
		}
		else
		{
			sound_slider.value = PlayerPrefs.GetFloat("sound");
			music_slider.value = PlayerPrefs.GetFloat("music");
			cans.GetComponent<AudioSource>().volume = music_slider.value;
		}
		cans.GetComponent<AudioSource>().volume = music_slider.value;
		if (PlayerPrefs.GetInt("showroom") == 1)
		{
			PlayerPrefs.SetInt("showroom", 0);
			menu_panel.SetActive(value: false);
			color_panel.SetActive(value: true);
			main_cans.renderMode = RenderMode.ScreenSpaceCamera;
			Main_car.SetActive(value: true);
			top_bar.SetTrigger("in");
		}
		else if (PlayerPrefs.GetInt("selection") == 1)
		{
			PlayerPrefs.SetInt("selection", 0);
			if (current_mode == 1)
			{
				level_panel.SetActive(value: true);
				menu_panel.SetActive(value: false);
				Invoke("delay_ad_icon", 0.01f);
			}
			else if (current_mode == 2)
			{
				level_panel2.SetActive(value: true);
				menu_panel.SetActive(value: false);
				Invoke("delay_ad_icon", 0.01f);
			}
			else if (current_mode == 3)
			{
				level_panel3.SetActive(value: true);
				menu_panel.SetActive(value: false);
				Invoke("delay_ad_icon", 0.01f);
			}
			else if (current_mode == 4)
			{
				level_panel4.SetActive(value: true);
				menu_panel.SetActive(value: false);
				Invoke("delay_ad_icon", 0.01f);
			}
			else if (current_mode == 5)
			{
				level_panel5.SetActive(value: true);
				menu_panel.SetActive(value: false);
				Invoke("delay_ad_icon", 0.01f);
			}
		}
		if (PlayerPrefs.GetInt("TwitterClicked") == 0)
		{
			twitter_btn.interactable = true;
		}
		else
		{
			twitter_btn.interactable = false;
		}
		if (PlayerPrefs.GetInt("FacebookClicked") == 0)
		{
			fb_btn.interactable = true;
			fb_btn2.interactable = true;
		}
		else
		{
			fb_btn.interactable = false;
			fb_btn2.interactable = false;
		}
		if (PlayerPrefs.GetInt("YOUTUBEClicked") == 0)
		{
			yt_btn.interactable = true;
			yt_btn_2.interactable = true;
		}
		else
		{
			yt_btn_2.interactable = false;
			yt_btn.interactable = false;
		}
		if (PlayerPrefs.GetInt("SHARECLICEKD") == 0)
		{
			share_btn.interactable = true;
		}
		else
		{
			share_btn.interactable = false;
		}
		if (PlayerPrefs.GetInt("specialpack") == 0)
		{
			PlayerPrefs.SetInt("specialpack", 1);
		}
		else if (menu_panel.activeSelf)
		{
			Invoke("open_pack", 0.2f);
		}
		if (level_panel.activeSelf || level_panel2.activeSelf || level_panel3.activeSelf || level_panel4.activeSelf || level_panel5.activeSelf)
		{
			int @int = PlayerPrefs.GetInt("level" + current_mode);
			if (@int % 6 == 0 && @int > 0)
			{
				if (PlayerPrefs.GetInt("level1") < 19)
				{
					discount_panel.SetActive(value: true);
					Txt.text = modeTxt[current_mode - 1];
					levels_butns_inapp[current_mode - 1].SetActive(value: true);
				}
				else if (PlayerPrefs.GetInt("level2") < 9)
				{
					discount_panel.SetActive(value: true);
					Txt.text = modeTxt[current_mode - 1];
					levels_butns_inapp[current_mode - 1].SetActive(value: true);
				}
				else if (PlayerPrefs.GetInt("level3") < 9)
				{
					discount_panel.SetActive(value: true);
					Txt.text = modeTxt[current_mode - 1];
					levels_butns_inapp[current_mode - 1].SetActive(value: true);
				}
				else if (PlayerPrefs.GetInt("level4") < 9)
				{
					discount_panel.SetActive(value: true);
					Txt.text = modeTxt[current_mode - 1];
					levels_butns_inapp[current_mode - 1].SetActive(value: true);
				}
				else if (PlayerPrefs.GetInt("level5") < 9)
				{
					discount_panel.SetActive(value: true);
					Txt.text = modeTxt[current_mode - 1];
					levels_butns_inapp[current_mode - 1].SetActive(value: true);
				}
			}
		}
	//	_ads_manager.Instance._show_admob_Banner_new(AdPosition.Top, AdSize.Banner, _req: true);
//_ads_manager.Instance.hideBanner();
		if (PlayerPrefs.GetInt("level1") >= 10)
		{
			try
			{
				right_arrow.onClick.Invoke();
			}
			catch (Exception)
			{
			}
		}
	}

	private void delay_ad_icon()
	{
		//ADS.ads.ad_icon_levels1();
	}

	private void open_pack()
	{
		special_pack_panel.SetActive(value: true);
		main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
		PlayerPrefs.SetInt("specialpack", 0);
	}

	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if (menu_panel.activeSelf && !quit_panel.activeSelf && !special_pack_panel.activeSelf)
			{
				Buttons("quit");
			}
			else if (quit_panel.activeSelf && menu_panel.activeSelf)
			{
				Buttons("no");
			}
			else if (level_panel.activeSelf || color_panel.activeSelf || setting_panel.activeSelf || mode_panel.activeSelf || level_panel2.activeSelf || share_panel.activeSelf || level_panel3.activeSelf || mode_3_panel_notification.activeSelf || level_panel4.activeSelf || mode_4_panel_notification.activeSelf || mode_5_panel_notification.activeSelf || about_us_panel.activeSelf || free_coins_panel.activeSelf || discount_panel.activeSelf || special_pack_panel.activeSelf || pop_panel_levels.activeSelf || unlock_all_modes.activeSelf)
			{
				Buttons("back");
			}
			else if (!loading.activeSelf)
			{
			}
		}
	}

	public void sound()
	{
		if (cans.GetComponent<AudioSource>().volume != music_slider.value)
		{
			cans.GetComponent<AudioSource>().volume = music_slider.value;
			PlayerPrefs.SetFloat("music", music_slider.value);
		}
	}

	public void level_selection(int levels)
	{
		currentlevel = levels;
		btn_sound();
		loading.SetActive(value: true);
		Invoke("level_loading", 5f);
		//_ads_manager.Instance._show_admob_Banner_new(AdPosition.Top, AdSize.Banner);
		if (current_mode == 1)
		{
			Analytics.CustomEvent("level_start" + analyticsVersionString, new Dictionary<string, object> { { "level_index", currentlevel } });
		}
		else if (current_mode == 2)
		{
			Analytics.CustomEvent("level_start" + analyticsVersionString, new Dictionary<string, object> { { "level_index1", currentlevel } });
		}
		else if (current_mode == 3)
		{
			Analytics.CustomEvent("level_start" + analyticsVersionString, new Dictionary<string, object> { { "level_index2", currentlevel } });
		}
		else if (current_mode == 4)
		{
			Analytics.CustomEvent("level_start" + analyticsVersionString, new Dictionary<string, object> { { "level_index3", currentlevel } });
		}
		else if (current_mode == 5)
		{
			Analytics.CustomEvent("level_start" + analyticsVersionString, new Dictionary<string, object> { { "level_index4", currentlevel } });
		}
	}

	public void level_loading()
	{
		//_ads_manager.Instance.DestroyBanner();
		if (current_mode == 1)
		{
			SceneManager.LoadScene(3);
		}
		else if (current_mode == 2)
		{
			SceneManager.LoadScene(4);
		}
		else if (current_mode == 3)
		{
			SceneManager.LoadScene(5);
		}
		else if (current_mode == 4)
		{
			SceneManager.LoadScene(6);
		}
		else if (current_mode == 5)
		{
			SceneManager.LoadScene(7);
		}
	}

	public void share_mode()
	{
		if (PlayerPrefs.GetInt("shareit") == 1)
		{
			mode2.interactable = true;
			share_butn.gameObject.SetActive(value: false);
			PlayerPrefs.SetInt("sharedone", 1);
		}
		PlayerPrefs.SetInt("shared_pressed", 1);
		share_text_check();
	}

	public void mode_3_check()
	{
		if (PlayerPrefs.GetInt("10wala") == 1)
		{
			mode3.interactable = true;
			mode3_btn_check.gameObject.SetActive(value: false);
		}
		else
		{
			mode_3_panel_notification.SetActive(value: true);
		}
		btn_sound();
	}

	public void mode4_check()
	{
		if (PlayerPrefs.GetInt("10wala4") == 1)
		{
			mode4.interactable = true;
			mode4_btn_check.gameObject.SetActive(value: false);
		}
		else
		{
			mode_4_panel_notification.SetActive(value: true);
		}
		btn_sound();
	}

	public void mode5_check()
	{
		if (PlayerPrefs.GetInt("10wala5") == 1)
		{
			mode5.interactable = true;
			mode5_btn_check.gameObject.SetActive(value: false);
		}
		else
		{
			mode_5_panel_notification.SetActive(value: true);
		}
		btn_sound();
	}

	public void share_check()
	{
		share_panel.SetActive(value: true);
		share_text_check();
		btn_sound();
	}

	public void share_text_check()
	{
		if (PlayerPrefs.GetInt("20wala") == 0)
		{
			share_complete.SetActive(value: false);
			only_complete.SetActive(value: true);
		}
	}

	public void drive_side(int dside)
	{
		driveside = dside;
		Invoke("drive_side_check", 0f);
		btn_sound();
	}

	public void car_control_side(int cside)
	{
		car_control = cside;
		Invoke("car_control_check", 0f);
		btn_sound();
	}

	public void drive_side_check()
	{
		if (driveside == 2)
		{
			left_side.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 0.5f);
			right_side.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 1f);
		}
		else if (driveside == 1)
		{
			left_side.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 1f);
			right_side.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 0.5f);
		}
	}

	public void car_control_check()
	{
		if (car_control == 1)
		{
			l_r_btns.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 0.5f);
			steerint_btns.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 1f);
		}
		else if (car_control == 2)
		{
			l_r_btns.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 1f);
			steerint_btns.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 0.5f);
		}
	}

	public void Buttons(string btns)
	{
		switch (btns)
		{
		case "play":
			mode_panel.SetActive(value: true);
			menu_panel.SetActive(value: false);
			break;
		case "custom":
			color_panel.SetActive(value: true);
			menu_panel.SetActive(value: false);
			break;
		case "setting":
			setting_panel.SetActive(value: true);
			menu_panel.SetActive(value: false);
		//	ADS.ads.ad_icon_settings();
			//_ads_manager.Instance._show_admob_Banner_new(AdPosition.Top, AdSize.Banner);
			break;
		case "no":
			quit_panel.SetActive(value: false);
			if (menu_panel.activeSelf)
			{
			}
			main_cans.renderMode = RenderMode.ScreenSpaceCamera;
			break;
		case "yes":
			Process.GetCurrentProcess().Kill();
			break;
		case "quit":
			quit_panel.SetActive(value: true);
			main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
		//	ADS.ads.ad_icon();
			break;
		case "back":
			if (level_panel.activeSelf && !discount_panel.activeSelf && !pop_panel_levels.activeSelf)
			{
				level_panel.SetActive(value: false);
				mode_panel.SetActive(value: true);
			//	ADS.ads.ad_icon_modes();
			}
			else if (color_panel.activeSelf && !Modifications.modify.paint_panel.activeSelf && !Modifications.modify.decal_panel.activeSelf && !Modifications.modify.rim_panel.activeSelf)
			{
				color_panel.SetActive(value: false);
				pain_panel.SetActive(value: true);
				stickers_panel.SetActive(value: false);
				rim_panel.SetActive(value: false);
				menu_panel.SetActive(value: true);
				Modifications.modify.buy_panel_of();
				Modifications.modify.rewarded_panel_of();
				Modifications.modify.ColorFunction();
				Modifications.modify.rimfunction();
				Modifications.modify.stickerfunction();
				Main_car.SetActive(value: false);
				top_bar.SetTrigger("out");
			}
			else if (setting_panel.activeSelf)
			{
				setting_panel.SetActive(value: false);
				menu_panel.SetActive(value: true);
			//	_ads_manager.Instance.hideBanner();
			}
			else if (mode_panel.activeSelf && !share_panel.activeSelf && !mode_3_panel_notification.activeSelf && !mode_4_panel_notification.activeSelf && !mode_5_panel_notification.activeSelf && !unlock_all_modes.activeSelf)
			{
				color_panel.SetActive(value: true);
				mode_panel.SetActive(value: false);
				main_cans.renderMode = RenderMode.ScreenSpaceCamera;
				pain_panel.SetActive(value: true);
				stickers_panel.SetActive(value: false);
				rim_panel.SetActive(value: false);
				Main_car.SetActive(value: true);
			}
			else if (level_panel2.activeSelf && !discount_panel.activeSelf && !pop_panel_levels.activeSelf)
			{
				level_panel2.SetActive(value: false);
				mode_panel.SetActive(value: true);
			//	ADS.ads.ad_icon_modes();
			}
			else if (share_panel.activeSelf)
			{
				share_panel.SetActive(value: false);
			}
			else if (level_panel3.activeSelf && !discount_panel.activeSelf && !pop_panel_levels.activeSelf)
			{
				level_panel3.SetActive(value: false);
				mode_panel.SetActive(value: true);
			//	ADS.ads.ad_icon_modes();
			}
			else if (mode_3_panel_notification.activeSelf)
			{
				mode_3_panel_notification.SetActive(value: false);
			}
			else if (level_panel4.activeSelf && !discount_panel.activeSelf && !pop_panel_levels.activeSelf)
			{
				level_panel4.SetActive(value: false);
				mode_panel.SetActive(value: true);
			//	ADS.ads.ad_icon_modes();
			}
			else if (mode_4_panel_notification.activeSelf)
			{
				mode_4_panel_notification.SetActive(value: false);
			}
			else if (level_panel5.activeSelf && !discount_panel.activeSelf && !pop_panel_levels.activeSelf)
			{
				level_panel5.SetActive(value: false);
				mode_panel.SetActive(value: true);
			//	ADS.ads.ad_icon_modes();
			}
			else if (mode_5_panel_notification.activeSelf)
			{
				mode_5_panel_notification.SetActive(value: false);
			}
			else if (about_us_panel.activeSelf)
			{
				menu_panel.SetActive(value: true);
				about_us_panel.SetActive(value: false);
			}
			else if (free_coins_panel.activeSelf)
			{
				free_coins_panel.SetActive(value: false);
				menu_panel.SetActive(value: true);
			}
			else if (discount_panel.activeSelf && !pop_panel_levels.activeSelf)
			{
				discount_panel.SetActive(value: false);
			}
			else if (special_pack_panel.activeSelf)
			{
				special_pack_panel.SetActive(value: false);
				main_cans.renderMode = RenderMode.ScreenSpaceCamera;
			}
			else if (Modifications.modify.paint_panel.activeSelf)
			{
				Modifications.modify.paint_panel.SetActive(value: false);
				menu.main_cans.renderMode = RenderMode.ScreenSpaceCamera;
			}
			else if (Modifications.modify.rim_panel.activeSelf)
			{
				Modifications.modify.rim_panel.SetActive(value: false);
				menu.main_cans.renderMode = RenderMode.ScreenSpaceCamera;
			}
			else if (Modifications.modify.decal_panel.activeSelf)
			{
				Modifications.modify.decal_panel.SetActive(value: false);
				menu.main_cans.renderMode = RenderMode.ScreenSpaceCamera;
			}
			else if (pop_panel_levels.activeSelf)
			{
				pop_panel_levels.SetActive(value: false);
			}
			else if (unlock_all_modes.activeSelf)
			{
				unlock_all_modes.SetActive(value: false);
			}
			break;
		case "policy":
			Application.OpenURL("http://www.theknightzpvt.com/home/privacypolicy");
			break;
		case "color":
			pain_panel.SetActive(value: true);
			rim_panel.SetActive(value: false);
			stickers_panel.SetActive(value: false);
			Modifications.modify.buy_panel_of();
			Modifications.modify.rewarded_panel_of();
			break;
		case "rims":
			pain_panel.SetActive(value: false);
			rim_panel.SetActive(value: true);
			stickers_panel.SetActive(value: false);
			Modifications.modify.buy_panel_of();
			Modifications.modify.rewarded_panel_of();
			break;
		case "stickers":
			stickers_panel.SetActive(value: true);
			pain_panel.SetActive(value: false);
			rim_panel.SetActive(value: false);
			Modifications.modify.buy_panel_of();
			Modifications.modify.rewarded_panel_of();
			break;
		case "more":
			Application.OpenURL("https://play.google.com/store/apps/developer?id=The+Knights+Pvt+Ltd");
			break;
		case "rates":
			PlayerPrefs.SetInt("rates", 1);
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.volcano.modrn.car.parking.d");
			break;
		case "about":
			about_us_panel.SetActive(value: true);
			menu_panel.SetActive(value: false);
			break;
		case "youtube":
			isyoutube = true;
			isfacebook = false;
			istwitter = false;
			ishare = false;
			fb_btn2.interactable = false;
			twitter_btn.interactable = false;
			share_btn.interactable = false;
			free_btn.interactable = false;
			Application.OpenURL("https://www.youtube.com/channel/UCNq75e3JuMJSPd5ZLlMZzSg/videos");
			MonoBehaviour.print(isyoutube);
			break;
		case "facebook":
			isfacebook = true;
			istwitter = false;
			ishare = false;
			twitter_btn.interactable = false;
			share_btn.interactable = false;
			free_btn.interactable = false;
			yt_btn_2.interactable = false;
			Application.OpenURL("https://www.facebook.com/Modern-Car-Drive-Parking-3d-555996114919096/?modal=admin_todo_tour&_rdc=1&_rdr");
			break;
		case "twitter":
			isfacebook = false;
			istwitter = true;
			ishare = false;
			fb_btn2.interactable = false;
			share_btn.interactable = false;
			free_btn.interactable = false;
			yt_btn_2.interactable = false;
			Application.OpenURL("https://twitter.com/knights_pvt");
			break;
		case "share":
			isfacebook = false;
			istwitter = false;
			ishare = true;
			twitter_btn.interactable = false;
			fb_btn2.interactable = false;
			free_btn.interactable = false;
			yt_btn_2.interactable = false;
			break;
		case "free":
			free_coins_panel.SetActive(value: true);
			menu_panel.SetActive(value: false);
			break;
		case "rcoins":
			try
			{
				twitter_btn.interactable = false;
				fb_btn2.interactable = false;
				share_btn.interactable = false;
				yt_btn.interactable = false;
				yt_btn_2.interactable = false;
				isfacebook = false;
				istwitter = false;
				ishare = false;
				isyoutube = false;
				//_ads_manager.last_reward = rewarde_type.rewarded_coins;
				//_ads_manager.Instance._Show_unity_rewarded_Ad();
			}
			catch (Exception)
			{
			}
			break;
		case "discountoffer":
			if (PlayerPrefs.GetInt("level1") < 19 && level_panel.activeSelf)
			{
				discount_panel.SetActive(value: true);
				Txt.text = modeTxt[current_mode - 1];
				levels_butns_inapp[current_mode - 1].SetActive(value: true);
			}
			if (PlayerPrefs.GetInt("level2") < 9 && level_panel2.activeSelf)
			{
				discount_panel.SetActive(value: true);
				Txt.text = modeTxt[current_mode - 1];
				levels_butns_inapp[current_mode - 1].SetActive(value: true);
			}
			if (PlayerPrefs.GetInt("level3") < 9 && level_panel3.activeSelf)
			{
				discount_panel.SetActive(value: true);
				Txt.text = modeTxt[current_mode - 1];
				levels_butns_inapp[current_mode - 1].SetActive(value: true);
			}
			if (PlayerPrefs.GetInt("level4") < 9 && level_panel4.activeSelf)
			{
				discount_panel.SetActive(value: true);
				Txt.text = modeTxt[current_mode - 1];
				levels_butns_inapp[current_mode - 1].SetActive(value: true);
			}
			if (PlayerPrefs.GetInt("level5") < 9 && level_panel5.activeSelf)
			{
				discount_panel.SetActive(value: true);
				Txt.text = modeTxt[current_mode - 1];
				levels_butns_inapp[current_mode - 1].SetActive(value: true);
			}
			break;
		case "special":
			special_pack_panel.SetActive(value: true);
			main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
			break;
		case "directshowoorm":
			color_panel.SetActive(value: true);
			pop_panel_levels.SetActive(value: false);
			level_panel.SetActive(value: false);
			level_panel2.SetActive(value: false);
			level_panel3.SetActive(value: false);
			level_panel4.SetActive(value: false);
			level_panel5.SetActive(value: false);
			discount_panel.SetActive(value: false);
			Main_car.SetActive(value: true);
			main_cans.renderMode = RenderMode.ScreenSpaceCamera;
			break;
		case "allmodes":
			if (PlayerPrefs.GetInt("20wala") != 1 || PlayerPrefs.GetInt("10wala") != 1 || PlayerPrefs.GetInt("10wala4") != 1 || PlayerPrefs.GetInt("10wala5") != 1)
			{
				unlock_all_modes.SetActive(value: true);
			}
			break;
		}
		btn_sound();
	}

	public void mode_selection(int mode)
	{
		current_mode = mode;
		on_mode();
		btn_sound();
	}

	public void on_mode()
	{
		btn_sound();
		if (current_mode == 1)
		{
			level_panel.SetActive(value: true);
			level_panel2.SetActive(value: false);
			mode_panel.SetActive(value: false);
		//	ADS.ads.ad_icon_levels1();
		}
		else if (current_mode == 2)
		{
			if (PlayerPrefs.GetInt("20wala") == 1)
			{
				level_panel2.SetActive(value: true);
				level_panel.SetActive(value: false);
				mode_panel.SetActive(value: false);
				col.rate_one_time = true;
			//	ADS.ads.ad_icon_levels1();
			}
			else
			{
				share_check();
				current_mode = 1;
			}
		}
		else if (current_mode == 3)
		{
			if (PlayerPrefs.GetInt("10wala") == 1)
			{
				level_panel3.SetActive(value: true);
				mode_panel.SetActive(value: false);
				level_panel2.SetActive(value: false);
				level_panel.SetActive(value: false);
				col.rate_one_time = true;
			//	ADS.ads.ad_icon_levels1();
			}
			else
			{
				mode_3_check();
				current_mode = 1;
			}
		}
		else if (current_mode == 4)
		{
			if (PlayerPrefs.GetInt("10wala4") == 1)
			{
				level_panel4.SetActive(value: true);
				mode_panel.SetActive(value: false);
				level_panel2.SetActive(value: false);
				level_panel.SetActive(value: false);
				col.rate_one_time = true;
			//	ADS.ads.ad_icon_levels1();
			}
			else
			{
				mode4_check();
				current_mode = 1;
			}
		}
		else if (current_mode == 5)
		{
			if (PlayerPrefs.GetInt("10wala5") == 1)
			{
				level_panel5.SetActive(value: true);
				level_panel4.SetActive(value: false);
				mode_panel.SetActive(value: false);
				level_panel2.SetActive(value: false);
				level_panel.SetActive(value: false);
				col.rate_one_time = true;
			//	ADS.ads.ad_icon_levels1();
			}
			else
			{
				mode5_check();
				current_mode = 1;
			}
		}
		if (!level_panel.activeSelf && !level_panel2.activeSelf && !level_panel3.activeSelf && !level_panel4.activeSelf && !level_panel5.activeSelf)
		{
			return;
		}
		int @int = PlayerPrefs.GetInt("level" + current_mode);
		if (@int % 6 == 0 && @int > 0)
		{
			if (PlayerPrefs.GetInt("level1") < 19)
			{
				discount_panel.SetActive(value: true);
				Txt.text = modeTxt[current_mode - 1];
				levels_butns_inapp[current_mode - 1].SetActive(value: true);
			}
			else if (PlayerPrefs.GetInt("level2") < 9)
			{
				discount_panel.SetActive(value: true);
				Txt.text = modeTxt[current_mode - 1];
				levels_butns_inapp[current_mode - 1].SetActive(value: true);
			}
			else if (PlayerPrefs.GetInt("level3") < 9)
			{
				discount_panel.SetActive(value: true);
				Txt.text = modeTxt[current_mode - 1];
				levels_butns_inapp[current_mode - 1].SetActive(value: true);
			}
			else if (PlayerPrefs.GetInt("level4") < 9)
			{
				discount_panel.SetActive(value: true);
				Txt.text = modeTxt[current_mode - 1];
				levels_butns_inapp[current_mode - 1].SetActive(value: true);
			}
			else if (PlayerPrefs.GetInt("level5") < 9)
			{
				discount_panel.SetActive(value: true);
				Txt.text = modeTxt[current_mode - 1];
				levels_butns_inapp[current_mode - 1].SetActive(value: true);
			}
		}
	}

	public void btn_sound()
	{
		if (sound_slider.value > 0f)
		{
			btn_click.Play();
		}
	}

	public void sound_slider_bar()
	{
		btn_click.volume = sound_slider.value;
		PlayerPrefs.SetFloat("sound", sound_slider.value);
	}

	public void garage_open()
	{
		menu_panel.SetActive(value: false);
		color_panel.SetActive(value: true);
		Main_car.SetActive(value: true);
		main_cans.renderMode = RenderMode.ScreenSpaceCamera;
		btn_sound();
	}

	public void garage_close()
	{
		mode_panel.SetActive(value: true);
		color_panel.SetActive(value: false);
		Main_car.SetActive(value: false);
		main_cans.renderMode = RenderMode.ScreenSpaceOverlay;
		Modifications.modify.ColorFunction();
		Modifications.modify.rimfunction();
		Modifications.modify.stickerfunction();
		btn_sound();
	//	ADS.ads.ad_icon_modes();
	}

	public void buton_select(Button sbtn)
	{
		sbtn.transform.GetChild(0).GetComponent<Text>().color = Color.yellow;
		sbtn.GetComponent<Image>().sprite = glow_image;
	}

	public void buton_deselect(Button dbtn)
	{
		dbtn.transform.GetChild(0).GetComponent<Text>().color = Color.white;
		dbtn.GetComponent<Image>().sprite = normal_image;
	}

	public void InitilizeTwitterTimer()
	{
		startTime = DateTime.Now;
	}

	public void FinalTwitterTimer()
	{
		endTime = DateTime.Now;
		TimeSpan timeSpan = endTime - startTime;
		if (timeSpan > mainTIme)
		{
			PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 100);
			Modifications.modify.coin_text.text = PlayerPrefs.GetInt("coins").ToString();
			if (istwitter)
			{
				PlayerPrefs.SetInt("TwitterClicked", 1);
				twitter_btn.interactable = false;
				istwitter = false;
				free_btn.interactable = true;
				if (PlayerPrefs.GetInt("SHARECLICEKD") == 0)
				{
					share_btn.interactable = true;
				}
				if (PlayerPrefs.GetInt("FacebookClicked") == 0)
				{
					fb_btn2.interactable = true;
				}
				if (PlayerPrefs.GetInt("YOUTUBEClicked") == 0)
				{
					yt_btn.interactable = true;
					yt_btn_2.interactable = true;
				}
			}
			else if (isfacebook)
			{
				PlayerPrefs.SetInt("FacebookClicked", 1);
				fb_btn.interactable = false;
				fb_btn2.interactable = false;
				isfacebook = false;
				free_btn.interactable = true;
				if (PlayerPrefs.GetInt("TwitterClicked") == 0)
				{
					twitter_btn.interactable = true;
				}
				if (PlayerPrefs.GetInt("SHARECLICEKD") == 0)
				{
					share_btn.interactable = true;
				}
				if (PlayerPrefs.GetInt("YOUTUBEClicked") == 0)
				{
					yt_btn.interactable = true;
					yt_btn_2.interactable = true;
				}
			}
			else if (ishare)
			{
				PlayerPrefs.SetInt("SHARECLICEKD", 1);
				share_btn.interactable = false;
				ishare = false;
				free_btn.interactable = true;
				if (PlayerPrefs.GetInt("TwitterClicked") == 0)
				{
					twitter_btn.interactable = true;
				}
				if (PlayerPrefs.GetInt("FacebookClicked") == 0)
				{
					fb_btn2.interactable = true;
				}
				if (PlayerPrefs.GetInt("YOUTUBEClicked") == 0)
				{
					yt_btn.interactable = true;
					yt_btn_2.interactable = true;
				}
			}
			else if (isyoutube)
			{
				MonoBehaviour.print(isyoutube);
				PlayerPrefs.SetInt("YOUTUBEClicked", 1);
				yt_btn.interactable = false;
				yt_btn_2.interactable = false;
				isyoutube = false;
				free_btn.interactable = true;
				if (PlayerPrefs.GetInt("TwitterClicked") == 0)
				{
					twitter_btn.interactable = true;
				}
				if (PlayerPrefs.GetInt("FacebookClicked") == 0)
				{
					fb_btn2.interactable = true;
				}
				if (PlayerPrefs.GetInt("SHARECLICEKD") == 0)
				{
					share_btn.interactable = true;
				}
			}
		}
		else
		{
			isfacebook = false;
			istwitter = false;
			ishare = false;
		}
	}

	public void in_app_func(string app_code)
	{
		switch (app_code)
		{
		case "removeads_1000_coins":
			if (PlayerPrefs.GetInt("RemoveAds") != 1)
			{
				//_ads_manager.Instance.inAppDeals(10);
			}
			break;
		case "removeads_modern_mode":
			if (PlayerPrefs.GetInt("RemoveAds") != 1 || PlayerPrefs.GetInt("20wala") != 1)
			{
				//_ads_manager.Instance.inAppDeals(11);
			}
			break;
		case "unlock_all_modes":
			//_ads_manager.Instance.inAppDeals(6);
			break;
		case "unlock_all_classic_levels":
			//_ads_manager.Instance.inAppDeals(1);
			break;
		case "unlock_all_modern_levels":
			//_ads_manager.Instance.inAppDeals(2);
			break;
		case "unlock_all_mission_levels":
			//_ads_manager.Instance.inAppDeals(3);
			break;
		case "unlock_all_snow_levels":
			//_ads_manager.Instance.inAppDeals(5);
			break;
		case "unlock_all_time_levels":
			//_ads_manager.Instance.inAppDeals(4);
			break;
		case "unlock_modern_mode":
			if (PlayerPrefs.GetInt("20wala") != 1)
			{
				//_ads_manager.Instance.inAppDeals(12);
			}
			break;
		case "unlock_mission_mode":
			if (PlayerPrefs.GetInt("10wala") != 1)
			{
				//_ads_manager.Instance.inAppDeals(13);
			}
			break;
		case "unlock_time_attack":
			if (PlayerPrefs.GetInt("10wala4") != 1)
			{
				//_ads_manager.Instance.inAppDeals(14);
			}
			break;
		case "unlock_snow":
			if (PlayerPrefs.GetInt("10wala5") != 1)
			{
				//_ads_manager.Instance.inAppDeals(15);
			}
			break;
		case "unlock_decals":
			if (PlayerPrefs.GetInt("sold9") != 9 || PlayerPrefs.GetInt("sold4") != 4 || PlayerPrefs.GetInt("sold10") != 10 || PlayerPrefs.GetInt("sold5") != 5)
			{
				//_ads_manager.Instance.inAppDeals(8);
			}
			break;
		case "unlock_paints":
			if (PlayerPrefs.GetInt("sold2") != 2 || PlayerPrefs.GetInt("sold1") != 1 || PlayerPrefs.GetInt("sold6") != 6 || PlayerPrefs.GetInt("sold7") != 7 || PlayerPrefs.GetInt("sold24") != 24 || PlayerPrefs.GetInt("sold15") != 15 || PlayerPrefs.GetInt("sold16") != 16 || PlayerPrefs.GetInt("sold23") != 23 || PlayerPrefs.GetInt("sold17") != 17 || PlayerPrefs.GetInt("sold18") != 18 || PlayerPrefs.GetInt("sold22") != 22 || PlayerPrefs.GetInt("sold19") != 19 || PlayerPrefs.GetInt("sold20") != 20 || PlayerPrefs.GetInt("sold6") != 21)
			{
				//_ads_manager.Instance.inAppDeals(7);
			}
			break;
		case "unlock_rims":
			if (PlayerPrefs.GetInt("sold8") != 8 || PlayerPrefs.GetInt("sold3") != 3)
			{
				//_ads_manager.Instance.inAppDeals(9);
			}
			break;
		case "removeads":
			if (PlayerPrefs.GetInt("RemoveAds") == 0)
			{
				//_ads_manager.Instance.inAppDeals(0);
			}
			break;
		case "unlock_all_mode_special":
			if (PlayerPrefs.GetInt("20wala") != 1 || PlayerPrefs.GetInt("10wala") != 1 || PlayerPrefs.GetInt("10wala4") != 1 || PlayerPrefs.GetInt("10wala5") != 1)
			{
				//_ads_manager.Instance.inAppDeals(16);
			}
			break;
		}
	}
}
