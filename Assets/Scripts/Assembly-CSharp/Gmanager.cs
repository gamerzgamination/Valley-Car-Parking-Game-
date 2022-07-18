using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gmanager : MonoBehaviour
{
	public static Gmanager gm;

	public Material[] levels_materials;

	public Material[] for_mode_5;

	public GameObject ground_material;

	public GameObject ground_material2;

	public GameObject ground_material3;

	public GameObject gas_button;

	public GameObject rcc_camera;

	public GameObject rcc_R_hud;

	public GameObject steering;

	public GameObject hand_brake;

	public GameObject gear_btn;

	public GameObject left_butn;

	public GameObject right_btn;

	public GameObject player;

	public GameObject scroll_bar;

	[HideInInspector]
	public int gear = 1;

	[HideInInspector]
	public int map = 1;

	[HideInInspector]
	public int head_light_counter = 1;

	[HideInInspector]
	public int brak_light_counter;

	public bool iscomplete;

	public bool forcam;

	public bool ishome;

	public bool islevel2;

	public GameObject[] levels_camera;

	public Slider camera_slider;

	public GameObject map_btn;

	public GameObject ins_level;

	public GameObject cans;

	public GameObject pause_panel;

	public GameObject level_complete;

	public GameObject level_fail;

	public GameObject loading_panel;

	public GameObject levels;

	public GameObject blast_smoke;

	public GameObject pause_btn;

	public GameObject tutorial_panel;

	public Rigidbody rb;

	public GameObject extra_panel;

	public GameObject head_lights_cars;

	public GameObject revers_wala;

	public GameObject head_light_button;

	public GameObject brake_light;

	public GameObject revers_light;

	public AudioSource blast_sound;

	public AudioSource traffice_sound;

	public static int failcounter;

	public static int pausecounter;

	public static int completecounter;

	public static float time_limit;

	public string analyticsVersionString;

	public Scrollbar gearscrollbar;

	public GameObject gearhandle;

	public GameObject blac_spot_D;

	public GameObject blac_spot_R;

	public Sprite D_image;

	public Sprite R_image;

	public Sprite gas_r_image;

	public Sprite gear_d_right_image;

	public Sprite gear_r_right_image;

	public Sprite gas_L_image;

	public Sprite gear_d_left_image;

	public Sprite gear_r_left_image;

	public GameObject levels2;

	public GameObject next_panel;

	public GameObject levels_text;

	public GameObject[] mode2_levels_camera;

	public Animator[] mode2_animtor_hurdles;

	public GameObject mode2_level5_hurdles;

	public GameObject meter_image;

	public GameObject switch_mode;

	public GameObject skip_btn;

	public GameObject static_game_ad;

	public GameObject skip_btn2;

	public GameObject[] mode2_arrows;

	public Text level_number;

	public Text levels_times;

	public GameObject levels3;

	public GameObject levels4;

	public GameObject rate_us_panel;

	public GameObject mode3_switch_panel;

	public GameObject share_complete;

	public GameObject only_complete;

	public GameObject reset_btn;

	public GameObject video_availablity_check;

	public GameObject mode4_switch_panel;

	public GameObject levels5;

	public GameObject mode5_switch_panel;

	public GameObject[] mode3_levels_cameras;

	public GameObject[] mode4_levels_camera;

	public GameObject[] mode5_levels_camera;

	public Color night_mode_clor;

	public Color day__mode_color;

	public Color sky_color;

	public Light dir_light;

	public Light dark_light;

	public bool istime_stop;

	public float[] time_;

	public GameObject left_reverse_wala;

	public GameObject right_reverse_wala;

	public int t;

	public GameObject setting_panel;

	public GameObject left_side;

	public GameObject right_side;

	public GameObject l_r_btns;

	public GameObject steerint_btns;

	public GameObject setting_btn;

	public Slider music_game_play;

	public Slider sound_slider;

	public static int new_drive_side;

	public static int new_car_control;

	public Text level_reward_text;

	public Text total_coins;

	public bool istouched;

	public AudioSource car_unlock_key_sound;

	public AudioSource btn_click;

	public AudioSource gear_sound;

	public AudioSource reverse_beep;

	public GameObject ground_4;

	public GameObject pop_up_panel;

	public GameObject camera_tutorial_panel;

	public RectTransform gas_right;

	public Sprite brake_on_image;

	public Sprite brake_of_image;

	public Image left_right_inner_hole;

	public Image steering_inner_hole;

	public Image right_side_inner_hole;

	public Image left_side_inner_hole;

	public Image for_press;

	public GameObject[] animted_butns;

	public GameObject animted_butns_panel;

	public GameObject tempx;

	public GameObject no_ad_pop_up;

	public GameObject revive_panel;

	public Animator timer_barrier;

	public Image clock_for_barrier;

	public Material faded_material;

	public MeshRenderer container_faded;

	public GameObject[] coins_for_levels;

	public Animator level9_hurdl1;

	public Animator level9_hurdl2;

	public Animator level_15_hurdl;

	private static bool isskip;

	private void OnEnable()
	{
		t = MainMenu.currentlevel - 1;
		if (MainMenu.current_mode == 1)
		{
			if (MainMenu.currentlevel == 2)
			{
				if (PlayerPrefs.GetInt("le2") == 0)
				{
					pause_btn.SetActive(value: false);
					map_btn.SetActive(value: false);
					setting_btn.SetActive(value: false);
				}
				else
				{
					pause_btn.SetActive(value: true);
					map_btn.SetActive(value: true);
					setting_btn.SetActive(value: true);
					levels_text.SetActive(value: true);
				}
			}
			else if (MainMenu.currentlevel == 7)
			{
				if (PlayerPrefs.GetInt("map7") == 0)
				{
					pause_btn.SetActive(value: false);
					setting_btn.SetActive(value: false);
					levels_text.SetActive(value: false);
				}
				else
				{
					pause_btn.SetActive(value: true);
					setting_btn.SetActive(value: true);
					levels_text.SetActive(value: true);
				}
				map_btn.SetActive(value: true);
			}
			else if (MainMenu.currentlevel == 5)
			{
				pause_btn.SetActive(value: true);
				map_btn.SetActive(value: true);
				setting_btn.SetActive(value: true);
				levels_text.SetActive(value: true);
			}
			else
			{
				pause_btn.SetActive(value: true);
				map_btn.SetActive(value: true);
				setting_btn.SetActive(value: true);
				levels_text.SetActive(value: true);
			}
			level_number.text = MainMenu.currentlevel.ToString();
			levels.transform.GetChild(t).gameObject.SetActive(value: true);
			if (!Checkpoints.isrewarded_revival)
			{
				player.transform.position = levels.transform.GetChild(t).GetChild(0).transform.position;
				player.transform.rotation = levels.transform.GetChild(t).GetChild(0).transform.rotation;
			}
			else
			{
				player.transform.position = Checkpoints.chk.lasposssss;
				player.transform.rotation = Checkpoints.chk.laspoRotation;
				Checkpoints.isrewarded_revival = false;
			}
			try
			{
				if (levels_materials.Length > t && t > -1)
				{
					ground_material.GetComponent<MeshRenderer>().material = levels_materials[t];
				}
			}
			catch (Exception)
			{
			}
		}
		else if (MainMenu.current_mode == 2)
		{
			pause_btn.SetActive(value: true);
			map_btn.SetActive(value: true);
			setting_btn.SetActive(value: true);
			levels_text.SetActive(value: true);
			level_number.text = MainMenu.currentlevel.ToString();
			levels2.transform.GetChild(t).gameObject.SetActive(value: true);
			if (!Checkpoints.isrewarded_revival)
			{
				player.transform.position = levels2.transform.GetChild(t).GetChild(0).transform.position;
				player.transform.rotation = levels2.transform.GetChild(t).GetChild(0).transform.rotation;
			}
			else
			{
				player.transform.position = Checkpoints.chk.lasposssss;
				player.transform.rotation = Checkpoints.chk.laspoRotation;
				Checkpoints.isrewarded_revival = false;
			}
			try
			{
				if (levels_materials.Length > t && t > -1)
				{
					ground_material.GetComponent<MeshRenderer>().material = levels_materials[22];
				}
			}
			catch (Exception)
			{
			}
		}
		else if (MainMenu.current_mode == 3)
		{
			pause_btn.SetActive(value: true);
			levels_text.SetActive(value: true);
			setting_btn.SetActive(value: true);
			level_number.text = MainMenu.currentlevel.ToString();
			map_btn.SetActive(value: true);
			levels3.transform.GetChild(t).gameObject.SetActive(value: true);
			if (!Checkpoints.isrewarded_revival)
			{
				player.transform.position = levels3.transform.GetChild(t).GetChild(0).transform.position;
				player.transform.rotation = levels3.transform.GetChild(t).GetChild(0).transform.rotation;
			}
			else
			{
				player.transform.position = Checkpoints.chk.lasposssss;
				player.transform.rotation = Checkpoints.chk.laspoRotation;
				Checkpoints.isrewarded_revival = false;
			}
			try
			{
				if (levels_materials.Length > t && t > -1)
				{
					ground_material.GetComponent<MeshRenderer>().material = levels_materials[22];
				}
				if (MainMenu.currentlevel % 2 == 1 && MainMenu.current_mode == 3)
				{
					RenderSettings.ambientLight = night_mode_clor;
					dir_light.color = night_mode_clor;
				}
			}
			catch (Exception)
			{
			}
		}
		else if (MainMenu.current_mode == 4)
		{
			levels_times.transform.parent.gameObject.SetActive(value: true);
			if (time_.Length > t && t > -1)
			{
				time_limit = time_[t];
			}
			levels_times.text = time_limit.ToString();
			pause_btn.SetActive(value: true);
			map_btn.SetActive(value: true);
			setting_btn.SetActive(value: true);
			level_number.text = MainMenu.currentlevel.ToString();
			levels_text.SetActive(value: true);
			levels4.transform.GetChild(t).gameObject.SetActive(value: true);
			if (!Checkpoints.isrewarded_revival)
			{
				player.transform.position = levels4.transform.GetChild(t).GetChild(0).transform.position;
				player.transform.rotation = levels4.transform.GetChild(t).GetChild(0).transform.rotation;
			}
			else
			{
				player.transform.position = Checkpoints.chk.lasposssss;
				player.transform.rotation = Checkpoints.chk.laspoRotation;
				Checkpoints.isrewarded_revival = false;
			}
			try
			{
				if (levels_materials.Length > t && t > -1)
				{
					ground_material2.GetComponent<MeshRenderer>().material = levels_materials[23];
				}
			}
			catch (Exception)
			{
			}
			if (MainMenu.currentlevel % 2 == 1 && MainMenu.current_mode == 4)
			{
				dir_light.gameObject.SetActive(value: false);
				dark_light.gameObject.SetActive(value: true);
				ground_material2.SetActive(value: true);
				ground_material.SetActive(value: false);
				RenderSettings.ambientSkyColor = sky_color;
				head_light_button.SetActive(value: true);
				head_lights_cars.SetActive(value: true);
			}
			else
			{
				RenderSettings.ambientSkyColor = Color.white;
				ground_material2.SetActive(value: false);
				ground_material.SetActive(value: true);
			}
		}
		else
		{
			if (MainMenu.current_mode != 5)
			{
				return;
			}
			pause_btn.SetActive(value: true);
			map_btn.SetActive(value: true);
			setting_btn.SetActive(value: true);
			levels5.transform.GetChild(t).gameObject.SetActive(value: true);
			level_number.text = MainMenu.currentlevel.ToString();
			levels_text.SetActive(value: true);
			if (!Checkpoints.isrewarded_revival)
			{
				player.transform.position = levels5.transform.GetChild(t).GetChild(0).transform.position;
				player.transform.rotation = levels5.transform.GetChild(t).GetChild(0).transform.rotation;
			}
			else
			{
				player.transform.position = Checkpoints.chk.lasposssss;
				player.transform.rotation = Checkpoints.chk.laspoRotation;
				Checkpoints.isrewarded_revival = false;
			}
			if (MainMenu.currentlevel % 2 == 0)
			{
				RenderSettings.ambientSkyColor = Color.white;
				ground_material3.SetActive(value: false);
				ground_material.SetActive(value: true);
				try
				{
					if (for_mode_5.Length > t && t > -1)
					{
						ground_material.GetComponent<MeshRenderer>().material = for_mode_5[t];
					}
				}
				catch (Exception)
				{
				}
			}
			else
			{
				if (MainMenu.currentlevel % 2 != 1)
				{
					return;
				}
				try
				{
					if (for_mode_5.Length > t && t > -1)
					{
						ground_material.GetComponent<MeshRenderer>().material = for_mode_5[t];
					}
				}
				catch (Exception)
				{
				}
				dir_light.gameObject.SetActive(value: false);
				dark_light.gameObject.SetActive(value: true);
				ground_material3.SetActive(value: true);
				ground_material.SetActive(value: false);
				RenderSettings.ambientSkyColor = sky_color;
				head_light_button.SetActive(value: true);
				head_lights_cars.SetActive(value: true);
			}
		}
	}

	private void Awake()
	{
		gm = this;
		ishome = false;
		islevel2 = false;
		istime_stop = false;
		Invoke("key_sounds", 0.35f);
		if (MainMenu.car_control == 1)
		{
			RCC_Settings.instance.useSteeringWheelForSteering = true;
			new_car_control = MainMenu.car_control;
			Invoke("car_control_check_again", 0.1f);
		}
		else
		{
			RCC_Settings.instance.useSteeringWheelForSteering = false;
			new_car_control = MainMenu.car_control;
			Invoke("car_control_check_again", 0.1f);
		}
		if (MainMenu.driveside == 1)
		{
			new_drive_side = MainMenu.driveside;
			drive_side_check_again();
		}
		else if (MainMenu.driveside == 2)
		{
			new_drive_side = MainMenu.driveside;
			drive_side_check_again();
		}
		if (MainMenu.current_mode == 1)
		{
			if (MainMenu.currentlevel == 2 && PlayerPrefs.GetInt("le2") == 0)
			{
				revers_wala.SetActive(value: true);
				if (MainMenu.driveside == 2)
				{
					left_reverse_wala.SetActive(value: true);
				}
				else
				{
					right_reverse_wala.SetActive(value: true);
				}
				hand_brake.GetComponent<Image>().enabled = false;
				steering.GetComponent<Image>().enabled = false;
				camera_slider.gameObject.SetActive(value: false);
				left_butn.GetComponent<Button>().image.enabled = false;
				right_btn.GetComponent<Button>().image.enabled = false;
				gas_button.GetComponent<Button>().image.enabled = false;
			}
			if (MainMenu.currentlevel == 7 && MainMenu.current_mode == 1 && PlayerPrefs.GetInt("map7") == 0)
			{
				tutorial_panel.SetActive(value: true);
				hand_brake.GetComponent<Image>().enabled = false;
				steering.GetComponent<Image>().enabled = false;
				gear_btn.SetActive(value: false);
				left_butn.GetComponent<Button>().image.enabled = false;
				right_btn.GetComponent<Button>().image.enabled = false;
				gas_button.GetComponent<Button>().image.enabled = false;
				camera_slider.gameObject.SetActive(value: false);
				for_press.gameObject.SetActive(value: true);
				levels_text.SetActive(value: false);
				map_on();
			}
			if (MainMenu.currentlevel == 3)
			{
				camera_slider.value = 58f;
			}
		}
		if ((MainMenu.currentlevel == 9 || MainMenu.currentlevel == 13 || MainMenu.currentlevel == 18 || MainMenu.currentlevel == 15 || MainMenu.currentlevel == 19) && MainMenu.current_mode == 1)
		{
			reset_btn.SetActive(value: true);
		}
		//_ads_manager.Instance._show_admob_Banner_new(AdPosition.TopLeft, AdSize.MediumRectangle, _req: true);
		//_ads_manager.Instance.hideBanner();
	}

	private void Start()
	{
		cans.GetComponent<AudioSource>().volume = MainMenu.menu.music_slider.value;
		music_game_play.value = MainMenu.menu.music_slider.value;
		sound_slider.value = MainMenu.menu.sound_slider.value;
		if ((MainMenu.current_mode == 2 || MainMenu.current_mode == 3) && music_game_play.value > 0f)
		{
			traffice_sound.Play();
		}
		if (MainMenu.current_mode == 1)
		{
			int lv_index = 0;
			if (MainMenu.currentlevel <= 5)
			{
				lv_index = MainMenu.currentlevel - 1;
			}
			else if (MainMenu.currentlevel == 9)
			{
				lv_index = 5;
			}
			else if (MainMenu.currentlevel == 17)
			{
				lv_index = 6;
			}
			else if (MainMenu.currentlevel == 18)
			{
				lv_index = 7;
			}
			else if (MainMenu.currentlevel == 13)
			{
				lv_index = 8;
			}
			else if (MainMenu.currentlevel == 14)
			{
				lv_index = 9;
			}
			else if (MainMenu.currentlevel == 15)
			{
				lv_index = 10;
			}
			else if (MainMenu.currentlevel == 7)
			{
				lv_index = 11;
			}
			else if (MainMenu.currentlevel == 10)
			{
				lv_index = 12;
			}
			else if (MainMenu.currentlevel == 12)
			{
				lv_index = 13;
			}
			else if (MainMenu.currentlevel == 19)
			{
				lv_index = 14;
			}
			open_coins(lv_index);
		}
		player.GetComponent<RCC_CarControllerV3>().maxEngineSoundVolume = sound_slider.value;
		reverse_beep.volume = sound_slider.value;
		gear_sound.volume = sound_slider.value;
		btn_click.volume = sound_slider.value;
	}

	public void all_on_level2()
	{
		PlayerPrefs.SetInt("le2", 1);
		revers_wala.SetActive(value: false);
		hand_brake.GetComponent<Image>().enabled = true;
		steering.GetComponent<Image>().enabled = true;
		pause_btn.SetActive(value: true);
		map_btn.SetActive(value: true);
		setting_btn.SetActive(value: true);
		levels_text.SetActive(value: true);
		if (MainMenu.car_control != 1 && MainMenu.car_control == 2)
		{
			left_butn.GetComponent<Button>().image.enabled = true;
			right_btn.GetComponent<Button>().image.enabled = true;
		}
		gas_button.GetComponent<Button>().image.enabled = true;
	}

	private void Update()
	{
		if (MainMenu.current_mode == 4)
		{
			if (time_limit > 0f)
			{
				if (!istime_stop)
				{
					int num = Mathf.FloorToInt(time_limit / 60f);
					int num2 = Mathf.FloorToInt(time_limit % 60f);
					levels_times.text = num.ToString("00") + ":" + num2.ToString("00");
					time_limit -= Time.deltaTime;
				}
			}
			else
			{
				fail_levl();
				col.cl.stop();
			}
		}
		if (!Input.GetKeyUp(KeyCode.Escape))
		{
			return;
		}
		if (!pause_panel.activeSelf && !tutorial_panel.activeSelf && !level_complete.activeSelf && !level_fail.activeSelf && !extra_panel.activeSelf && !ins_level.activeSelf && !revers_wala.activeSelf && !next_panel.activeSelf && !switch_mode.activeSelf && !mode3_switch_panel.activeSelf && !mode4_switch_panel.activeSelf && !mode5_switch_panel.activeSelf && !setting_panel.activeSelf && Desi_rcc.desi.main_2.alpha == 1f && !rate_us_panel.activeSelf && !revive_panel.activeSelf && !pop_up_panel.activeSelf && !animted_butns_panel.activeSelf && !no_ad_pop_up.activeSelf && !static_game_ad.activeSelf)
		{
			buttons_func("pause");
		}
		else if (pause_panel.activeSelf && !no_ad_pop_up.activeSelf && !animted_butns_panel.activeSelf)
		{
			buttons_func("resume");
		}
		else if (level_complete.activeSelf)
		{
			buttons_func("homee");
		}
		else if (level_fail.activeSelf)
		{
			buttons_func("homee");
		}
		else if (!loading_panel.activeSelf && !revers_wala.activeSelf && !extra_panel.activeSelf && !next_panel.activeSelf && !switch_mode.activeSelf)
		{
			if (static_game_ad.activeSelf)
			{
				ad_game_close();
			}
			else if (switch_mode.activeSelf)
			{
				share_panel_close();
			}
			else if (mode3_switch_panel.activeSelf)
			{
				mode3_switch_of();
			}
			else if (mode4_switch_panel.activeSelf)
			{
				mode4_switch_of();
			}
			else if (mode5_switch_panel.activeSelf)
			{
				mode5_switch_of();
			}
			else if (setting_panel.activeSelf)
			{
				buttons_func("setting_close");
			}
			else if (pop_up_panel.activeSelf)
			{
				buttons_func("popclose");
			}
			else if (rate_us_panel.activeSelf)
			{
				buttons_func("later");
			}
			else if (animted_butns_panel.activeSelf && !no_ad_pop_up.activeSelf)
			{
				buttons_func("resume");
				animted_butns_panel.SetActive(value: false);
				tempx.SetActive(value: false);
			}
			else if (no_ad_pop_up.activeSelf)
			{
				no_ad_pop_up.SetActive(value: false);
			}
		}
	}

	public void zoom_in()
	{
	}

	public void mode3_switch_on()
	{
		mode3_switch_panel.SetActive(value: true);
	}

	public void mode4_switch_on()
	{
		mode4_switch_panel.SetActive(value: true);
	}

	public void mode5_switch_on()
	{
		mode5_switch_panel.SetActive(value: true);
	}

	public void next_mode_3()
	{
		PlayerPrefs.SetInt("mode3", 1);
		ad_game_open();
	}

	public void next_mode_4()
	{
		PlayerPrefs.SetInt("mode4", 1);
		ad_game_open();
	}

	public void next_mode_5()
	{
		PlayerPrefs.SetInt("mode5", 1);
		ad_game_open();
	}

	public void mode3_switch_of()
	{
		PlayerPrefs.SetInt("mode3", 1);
		SceneManager.LoadScene(2);
		PlayerPrefs.SetInt("showroom", 0);
		PlayerPrefs.SetInt("selection", 1);
	}

	public void mode4_switch_of()
	{
		PlayerPrefs.SetInt("mode4", 1);
		SceneManager.LoadScene(2);
		PlayerPrefs.SetInt("showroom", 0);
		PlayerPrefs.SetInt("selection", 1);
	}

	public void mode5_switch_of()
	{
		PlayerPrefs.SetInt("mode5", 1);
		SceneManager.LoadScene(2);
		PlayerPrefs.SetInt("showroom", 0);
		PlayerPrefs.SetInt("selection", 1);
	}

	public void gear_value()
	{
		if (gearscrollbar.value == 0f)
		{
			gearscrollbar.value = 1f;
			gearhandle.GetComponent<Image>().overrideSprite = R_image;
			blac_spot_D.SetActive(value: false);
			blac_spot_R.SetActive(value: true);
			if (MainMenu.currentlevel == 2 && !islevel2)
			{
				all_on_level2();
				islevel2 = true;
			}
			revers_light.SetActive(value: true);
			if (sound_slider.value > 0f)
			{
				gear_sound.Play();
				reverse_beep.Play();
			}
		}
		else
		{
			gearscrollbar.value = 0f;
			gearhandle.GetComponent<Image>().overrideSprite = D_image;
			blac_spot_D.SetActive(value: true);
			blac_spot_R.SetActive(value: false);
			revers_light.SetActive(value: false);
			if (sound_slider.value > 0f)
			{
				gear_sound.Play();
				reverse_beep.Stop();
			}
		}
	}

	public void map_check()
	{
		map++;
		if (map % 2 == 0)
		{
			if (MainMenu.current_mode == 1)
			{
				try
				{
					if (levels_camera.Length > t && t > -1)
					{
						levels_camera[t].SetActive(value: true);
						rcc_camera.SetActive(value: false);
						rcc_R_hud.SetActive(value: false);
						player.GetComponent<Rigidbody>().isKinematic = true;
					}
				}
				catch (Exception)
				{
				}
			}
			else if (MainMenu.current_mode == 2)
			{
				try
				{
					if (mode2_levels_camera.Length > t && t > -1)
					{
						mode2_levels_camera[t].SetActive(value: true);
						rcc_camera.SetActive(value: false);
						rcc_R_hud.SetActive(value: false);
						player.GetComponent<Rigidbody>().isKinematic = true;
					}
				}
				catch (Exception)
				{
				}
			}
			else if (MainMenu.current_mode == 3)
			{
				try
				{
					if (mode3_levels_cameras.Length > t && t > -1)
					{
						mode3_levels_cameras[t].SetActive(value: true);
						rcc_camera.SetActive(value: false);
						rcc_R_hud.SetActive(value: false);
						player.GetComponent<Rigidbody>().isKinematic = true;
					}
				}
				catch (Exception)
				{
				}
			}
			else if (MainMenu.current_mode == 4)
			{
				try
				{
					if (mode4_levels_camera.Length > t && t > -1)
					{
						mode4_levels_camera[t].SetActive(value: true);
						rcc_camera.SetActive(value: false);
						rcc_R_hud.SetActive(value: false);
						player.GetComponent<Rigidbody>().isKinematic = true;
					}
				}
				catch (Exception)
				{
				}
			}
			else if (MainMenu.current_mode == 5)
			{
				try
				{
					if (mode5_levels_camera.Length > t && t > -1)
					{
						mode5_levels_camera[t].SetActive(value: true);
						rcc_camera.SetActive(value: false);
						rcc_R_hud.SetActive(value: false);
						player.GetComponent<Rigidbody>().isKinematic = true;
					}
				}
				catch (Exception)
				{
				}
			}
			Time.timeScale = 0f;
		}
		else
		{
			if (map % 2 != 1)
			{
				return;
			}
			Time.timeScale = 1f;
			if (MainMenu.current_mode == 1)
			{
				try
				{
					if (levels_camera.Length > t && t > -1)
					{
						levels_camera[t].SetActive(value: false);
						rcc_R_hud.SetActive(value: true);
						rcc_camera.SetActive(value: true);
						player.GetComponent<Rigidbody>().isKinematic = false;
					}
				}
				catch (Exception)
				{
				}
			}
			else if (MainMenu.current_mode == 2)
			{
				try
				{
					if (mode2_levels_camera.Length > t && t > -1)
					{
						mode2_levels_camera[t].SetActive(value: false);
						rcc_R_hud.SetActive(value: true);
						rcc_camera.SetActive(value: true);
						player.GetComponent<Rigidbody>().isKinematic = false;
					}
				}
				catch (Exception)
				{
				}
			}
			else if (MainMenu.current_mode == 3)
			{
				try
				{
					if (mode3_levels_cameras.Length > t && t > -1)
					{
						mode3_levels_cameras[t].SetActive(value: false);
						rcc_R_hud.SetActive(value: true);
						rcc_camera.SetActive(value: true);
						player.GetComponent<Rigidbody>().isKinematic = false;
					}
				}
				catch (Exception)
				{
				}
			}
			else if (MainMenu.current_mode == 4)
			{
				try
				{
					if (mode4_levels_camera.Length > t && t > -1)
					{
						mode4_levels_camera[t].SetActive(value: false);
						rcc_R_hud.SetActive(value: true);
						rcc_camera.SetActive(value: true);
						player.GetComponent<Rigidbody>().isKinematic = false;
					}
				}
				catch (Exception)
				{
				}
			}
			else
			{
				if (MainMenu.current_mode != 5)
				{
					return;
				}
				try
				{
					if (mode5_levels_camera.Length > t && t > -1)
					{
						mode5_levels_camera[t].SetActive(value: false);
						rcc_R_hud.SetActive(value: true);
						rcc_camera.SetActive(value: true);
						player.GetComponent<Rigidbody>().isKinematic = false;
					}
				}
				catch (Exception)
				{
				}
			}
		}
	}

	public void buttons_func(string nam)
	{
		switch (nam)
		{
		case "pause":
			player.GetComponent<Rigidbody>().isKinematic = true;
			if (!pause_panel.activeSelf && !tutorial_panel.activeSelf)
			{
				pause_panel.SetActive(value: true);
				tempx = animted_butns[UnityEngine.Random.Range(0, 6)];
				tempx.SetActive(value: true);
				animted_butns_panel.SetActive(value: true);
				//ADS.ads.pause_ad_banner();
				//_ads_manager.Instance._show_admob_Banner_new(AdPosition.TopLeft, AdSize.MediumRectangle);
			}
			if (pausecounter < 1)
			{
				pausecounter++;
			}
			else
			{
				pausads();
				pausecounter = 0;
			}
			pause_sound();
			Time.timeScale = 0f;
			break;
		case "resume":
			Time.timeScale = 1f;
			if (tempx != null)
			{
				tempx.SetActive(value: false);
			}
			animted_butns_panel.SetActive(value: false);
			no_ad_pop_up.SetActive(value: false);
			pause_panel.SetActive(value: false);
			player.GetComponent<Rigidbody>().isKinematic = false;
			resume_sound();
		//	_ads_manager.Instance.hideBanner();
			break;
		case "restart":
			Time.timeScale = 1f;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			//_ads_manager.Instance.hideBanner();
			if (MainMenu.current_mode == 1 && MainMenu.currentlevel == 4)
			{
				Analytics.CustomEvent("level_restart", new Dictionary<string, object> { { "restart", 4 } });
			}
			break;
		case "next":
			Time.timeScale = 1f;
			failcounter = 0;
			pausecounter = 0;
			isskip = false;
			if (MainMenu.current_mode == 1)
			{
				if (MainMenu.currentlevel < 20)
				{
					MainMenu.currentlevel++;
					SceneManager.LoadScene(3);
					//_ads_manager.Instance.hideBanner();
				}
				else
				{
					if (PlayerPrefs.GetInt("sharedone") == 0)
					{
						share_panel_open();
						PlayerPrefs.SetInt("sharedone", 1);
						level_complete.SetActive(value: false);
						animted_butns_panel.SetActive(value: false);
						tempx.SetActive(value: false);
						PlayerPrefs.SetInt("showroom", 0);
						PlayerPrefs.SetInt("selection", 1);
					}
					else
					{
						animted_butns_panel.SetActive(value: false);
						tempx.SetActive(value: false);
						SceneManager.LoadScene(2);
						PlayerPrefs.SetInt("onetime", 0);
						PlayerPrefs.SetInt("showroom", 0);
						PlayerPrefs.SetInt("selection", 1);
					}
					//_ads_manager.Instance.hideBanner();
				}
			}
			else if (MainMenu.current_mode == 2)
			{
				if (MainMenu.currentlevel < 10)
				{
					MainMenu.currentlevel++;
					SceneManager.LoadScene(4);
					//_ads_manager.Instance.hideBanner();
				}
				else
				{
					if (PlayerPrefs.GetInt("mode3") == 0)
					{
						mode3_switch_on();
						level_complete.SetActive(value: false);
						animted_butns_panel.SetActive(value: false);
						tempx.SetActive(value: false);
						PlayerPrefs.SetInt("showroom", 0);
						PlayerPrefs.SetInt("selection", 1);
					}
					else
					{
						animted_butns_panel.SetActive(value: false);
						tempx.SetActive(value: false);
						PlayerPrefs.SetInt("onetime3", 0);
						SceneManager.LoadScene(2);
						PlayerPrefs.SetInt("showroom", 0);
						PlayerPrefs.SetInt("selection", 1);
					}
					//_ads_manager.Instance.hideBanner();
				}
			}
			else if (MainMenu.current_mode == 3)
			{
				if (MainMenu.currentlevel < 10)
				{
					MainMenu.currentlevel++;
					SceneManager.LoadScene(5);
					//_ads_manager.Instance.hideBanner();
				}
				else
				{
					if (PlayerPrefs.GetInt("mode4") == 0)
					{
						mode4_switch_on();
						level_complete.SetActive(value: false);
						animted_butns_panel.SetActive(value: false);
						tempx.SetActive(value: false);
						PlayerPrefs.SetInt("showroom", 0);
						PlayerPrefs.SetInt("selection", 1);
					}
					else
					{
						animted_butns_panel.SetActive(value: false);
						tempx.SetActive(value: false);
						PlayerPrefs.SetInt("onetime4", 0);
						SceneManager.LoadScene(2);
						PlayerPrefs.SetInt("showroom", 0);
						PlayerPrefs.SetInt("selection", 1);
					}
					//_ads_manager.Instance.hideBanner();
				}
			}
			else if (MainMenu.current_mode == 4)
			{
				if (MainMenu.currentlevel < 10)
				{
					MainMenu.currentlevel++;
					SceneManager.LoadScene(6);
					//_ads_manager.Instance.hideBanner();
				}
				else
				{
					if (PlayerPrefs.GetInt("mode5") == 0)
					{
						mode5_switch_on();
						level_complete.SetActive(value: false);
						animted_butns_panel.SetActive(value: false);
						tempx.SetActive(value: false);
						PlayerPrefs.SetInt("showroom", 0);
						PlayerPrefs.SetInt("selection", 1);
					}
					else
					{
						PlayerPrefs.SetInt("onetime5", 0);
						animted_butns_panel.SetActive(value: false);
						tempx.SetActive(value: false);
						SceneManager.LoadScene(2);
						PlayerPrefs.SetInt("showroom", 0);
						PlayerPrefs.SetInt("selection", 1);
					}
					//_ads_manager.Instance.hideBanner();
				}
			}
			else if (MainMenu.current_mode == 5)
			{
				if (MainMenu.currentlevel < 10)
				{
					MainMenu.currentlevel++;
					SceneManager.LoadScene(7);
					//_ads_manager.Instance.hideBanner();
				}
				else
				{
					ad_game_open();
					level_complete.SetActive(value: false);
					animted_butns_panel.SetActive(value: false);
					tempx.SetActive(value: false);
					//_ads_manager.Instance.hideBanner();
				}
			}
			if (MainMenu.current_mode == 1)
			{
				Analytics.CustomEvent("level_start" + analyticsVersionString, new Dictionary<string, object> { 
				{
					"level_index",
					MainMenu.currentlevel
				} });
			}
			else if (MainMenu.current_mode == 2)
			{
				Analytics.CustomEvent("level_start" + analyticsVersionString, new Dictionary<string, object> { 
				{
					"level_index1",
					MainMenu.currentlevel
				} });
			}
			else if (MainMenu.current_mode == 3)
			{
				Analytics.CustomEvent("level_start" + analyticsVersionString, new Dictionary<string, object> { 
				{
					"level_index2",
					MainMenu.currentlevel
				} });
			}
			else if (MainMenu.current_mode == 4)
			{
				Analytics.CustomEvent("level_start" + analyticsVersionString, new Dictionary<string, object> { 
				{
					"level_index3",
					MainMenu.currentlevel
				} });
			}
			else if (MainMenu.current_mode == 5)
			{
				Analytics.CustomEvent("level_start" + analyticsVersionString, new Dictionary<string, object> { 
				{
					"level_index4",
					MainMenu.currentlevel
				} });
			}
			//_ads_manager.Instance.hideBanner();
			break;
		case "home":
			Time.timeScale = 1f;
			MainMenu.iscomplete = false;
			ishome = true;
			failcounter = 0;
			pausecounter = 0;
			isskip = false;
			SceneManager.LoadScene(1);
			//_ads_manager.Instance.DestroyBanner();
			break;
		case "switch":
			Time.timeScale = 1f;
			PlayerPrefs.SetInt("shareit", 1);
			PlayerPrefs.SetInt("mode2", 1);
			PlayerPrefs.SetInt("sharedone", 1);
			SceneManager.LoadScene(2);
			Analytics.CustomEvent("mode" + analyticsVersionString, new Dictionary<string, object> { { "switch", 1 } });
			break;
		case "cancel":
			Time.timeScale = 1f;
			switch_mode.SetActive(value: false);
			comp_level();
			Analytics.CustomEvent("mode" + analyticsVersionString, new Dictionary<string, object> { { "switch", 2 } });
			break;
		case "now":
			PlayerPrefs.SetInt("rates", 1);
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.volcano.modrn.car.parking.d");
			rate_us_panel.SetActive(value: false);
			comp_level();
			break;
		case "later":
			PlayerPrefs.SetInt("rates", 0);
			rate_us_panel.SetActive(value: false);
			comp_level();
			break;
		case "setting":
			setting_panel.SetActive(value: true);
			player.GetComponent<Rigidbody>().isKinematic = true;
			break;
		case "setting_close":
			setting_panel.SetActive(value: false);
			player.GetComponent<Rigidbody>().isKinematic = false;
			break;
		case "garage":
			PlayerPrefs.SetInt("showroom", 1);
			PlayerPrefs.SetInt("selection", 0);
			Time.timeScale = 1f;
			//_ads_manager.Instance.DestroyBanner();
			SceneManager.LoadScene(2);
			break;
		case "levelselection":
			Time.timeScale = 1f;
			PlayerPrefs.SetInt("showroom", 0);
			PlayerPrefs.SetInt("selection", 1);
			//_ads_manager.Instance.DestroyBanner();
			SceneManager.LoadScene(2);
			break;
		case "popclose":
			gm.comp_level();
			pop_up_panel.SetActive(value: false);
			break;
		case "startnow":
			camera_tutorial_panel.SetActive(value: false);
			PlayerPrefs.SetInt("cameradone", 1);
			break;
		case "feedbacx":
			PlayerPrefs.SetInt("feedback", 1);
			PlayerPrefs.SetInt("rates", 1);
			rate_us_panel.SetActive(value: false);
			comp_level();
			break;
		}
		btn_sound();
	}

	public void resume_sound()
	{
		if (cans.GetComponent<AudioSource>().volume > 0f && cans.GetComponent<AudioSource>().enabled)
		{
			cans.GetComponent<AudioSource>().volume = MainMenu.menu.music_slider.value;
			if (MainMenu.current_mode == 2 || MainMenu.current_mode == 3)
			{
				traffice_sound.Play();
			}
			player.transform.GetChild(3).gameObject.SetActive(value: true);
			reverse_beep.UnPause();
		}
	}

	public void pause_sound()
	{
		if (cans.GetComponent<AudioSource>().volume > 0f && cans.GetComponent<AudioSource>().enabled)
		{
			cans.GetComponent<AudioSource>().volume = 0.0001f;
			if (MainMenu.current_mode == 2 || MainMenu.current_mode == 3)
			{
				traffice_sound.Pause();
			}
			player.transform.GetChild(3).gameObject.SetActive(value: false);
			if (reverse_beep.isPlaying)
			{
				reverse_beep.Pause();
			}
		}
	}

	public void comp_level()
	{
		if (!level_complete.activeSelf)
		{
			forcam = true;
			rcc_R_hud.SetActive(value: false);
			map_btn.SetActive(value: false);
			reverse_beep.Stop();
			col.cl.orbit_camera.GetComponent<ob>().autoRotateOn = false;
			player.transform.GetChild(3).gameObject.SetActive(value: false);
			PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 50);
			total_coins.text = PlayerPrefs.GetInt("coins").ToString();
			Invoke("comp2", 0.25f);
			if (MainMenu.current_mode == 1)
			{
				if (MainMenu.currentlevel >= PlayerPrefs.GetInt("level1"))
				{
					PlayerPrefs.SetInt("level1", MainMenu.currentlevel);
				}
			}
			else if (MainMenu.current_mode == 2)
			{
				if (MainMenu.currentlevel >= PlayerPrefs.GetInt("level2"))
				{
					PlayerPrefs.SetInt("level2", MainMenu.currentlevel);
				}
			}
			else if (MainMenu.current_mode == 3)
			{
				if (MainMenu.currentlevel >= PlayerPrefs.GetInt("level3"))
				{
					PlayerPrefs.SetInt("level3", MainMenu.currentlevel);
				}
			}
			else if (MainMenu.current_mode == 4)
			{
				if (MainMenu.currentlevel >= PlayerPrefs.GetInt("level4"))
				{
					PlayerPrefs.SetInt("level4", MainMenu.currentlevel);
				}
			}
			else if (MainMenu.current_mode == 5 && MainMenu.currentlevel >= PlayerPrefs.GetInt("level5"))
			{
				PlayerPrefs.SetInt("level5", MainMenu.currentlevel);
			}
		}
		cans.GetComponent<AudioSource>().enabled = false;
	}

	public void comp2()
	{
		forcam = false;
		extra_panel.SetActive(value: true);
		extra_panel.transform.GetChild(0).GetChild(0).GetChild(UnityEngine.Random.Range(0, 5))
			.gameObject.SetActive(value: true);
		if (cans.GetComponent<AudioSource>().volume > 0f)
		{
			extra_panel.GetComponent<AudioSource>().enabled = true;
		}
		Invoke("comp3", 1f);
	}

	public void comp3()
	{
		extra_panel.SetActive(value: false);
		if (!level_complete.activeSelf)
		{
			tempx = animted_butns[UnityEngine.Random.Range(0, 6)];
			tempx.SetActive(value: true);
			animted_butns_panel.SetActive(value: true);
			level_complete.SetActive(value: true);
			//ADS.ads.complete_ad_banner();
			if (((MainMenu.current_mode == 1 && MainMenu.currentlevel == 4) || (MainMenu.current_mode == 2 && (MainMenu.currentlevel == 1 || MainMenu.currentlevel == 9)) || (MainMenu.current_mode == 3 && MainMenu.currentlevel == 7) || (MainMenu.current_mode == 4 && MainMenu.currentlevel == 6) || (MainMenu.current_mode == 5 && (MainMenu.currentlevel == 1 || MainMenu.currentlevel == 8))) && PlayerPrefs.GetInt("rates") == 1 && PlayerPrefs.GetInt("feedback") == 1)
			{
				PlayerPrefs.SetInt("rates", 0);
			}
			else
			{
				compads();
			}
		}
	}

	private void Inovessss()
	{
		rb.isKinematic = false;
	}

	public void fail_levl()
	{
		if ((MainMenu.current_mode == 1 && (MainMenu.currentlevel == 9 || MainMenu.currentlevel == 15 || MainMenu.currentlevel == 16 || MainMenu.currentlevel == 20)) || (MainMenu.current_mode == 2 && MainMenu.currentlevel == 9) || (MainMenu.current_mode == 3 && (MainMenu.currentlevel == 2 || MainMenu.currentlevel == 8)) || (MainMenu.current_mode == 4 && (MainMenu.currentlevel == 2 || MainMenu.currentlevel == 5 || MainMenu.currentlevel == 8)) || (MainMenu.current_mode == 5 && (MainMenu.currentlevel == 2 || MainMenu.currentlevel == 6)))
		{
			if (failcounter == 1)
			{
				revive_panel.SetActive(value: true);
			}
			else
			{
				if (!level_fail.activeSelf)
				{
					Invoke("fail_2", 0.75f);
					rcc_R_hud.SetActive(value: false);
					map_btn.SetActive(value: false);
				}
				if (MainMenu.current_mode == 1 && MainMenu.currentlevel <= 10 && PlayerPrefs.GetInt("level_fail" + MainMenu.currentlevel, 0) == 0)
				{
					Analytics.CustomEvent("level_fail" + analyticsVersionString, new Dictionary<string, object> { 
					{
						"fail",
						MainMenu.currentlevel
					} });
					PlayerPrefs.SetInt("level_fail" + MainMenu.currentlevel, 1);
				}
				cans.GetComponent<AudioSource>().enabled = false;
				traffice_sound.Stop();
			}
		}
		else
		{
			if (!level_fail.activeSelf)
			{
				Invoke("fail_2", 0.75f);
				rcc_R_hud.SetActive(value: false);
				map_btn.SetActive(value: false);
			}
			if (MainMenu.current_mode == 1 && MainMenu.currentlevel <= 10 && PlayerPrefs.GetInt("level_fail" + MainMenu.currentlevel, 0) == 0)
			{
				Analytics.CustomEvent("level_fail" + analyticsVersionString, new Dictionary<string, object> { 
				{
					"fail",
					MainMenu.currentlevel
				} });
				PlayerPrefs.SetInt("level_fail" + MainMenu.currentlevel, 1);
			}
		}
		cans.GetComponent<AudioSource>().enabled = false;
		player.transform.GetChild(3).gameObject.SetActive(value: false);
		traffice_sound.Stop();
		reverse_beep.Stop();
	}

	public void new_fail()
	{
		if (!level_fail.activeSelf)
		{
			Invoke("fail_2", 0.75f);
			rcc_R_hud.SetActive(value: false);
			map_btn.SetActive(value: false);
		}
		if (MainMenu.current_mode == 1 && MainMenu.currentlevel <= 10 && PlayerPrefs.GetInt("level_fail" + MainMenu.currentlevel, 0) == 0)
		{
			Analytics.CustomEvent("level_fail" + analyticsVersionString, new Dictionary<string, object> { 
			{
				"fail",
				MainMenu.currentlevel
			} });
			PlayerPrefs.SetInt("level_fail" + MainMenu.currentlevel, 1);
		}
		cans.GetComponent<AudioSource>().enabled = false;
		traffice_sound.Stop();
	}

	public void fail_2()
	{
		if (!level_fail.activeSelf)
		{
			level_fail.SetActive(value: true);
			//ADS.ads.fail_ad_banner();
			if (failcounter == 1)
			{
				if (MainMenu.current_mode == 1 && (MainMenu.currentlevel == 9 || MainMenu.currentlevel == 18))
				{
					if (PlayerPrefs.GetInt("20wala") == 1 && PlayerPrefs.GetInt("10wala") == 1 && PlayerPrefs.GetInt("10wala4") == 1 && PlayerPrefs.GetInt("10wala5") == 1)
					{
					}
				}
				else
				{
					tempx = animted_butns[UnityEngine.Random.Range(0, 6)];
					tempx.SetActive(value: true);
					animted_butns_panel.SetActive(value: true);
				}
			}
			else if (failcounter == 0)
			{
				tempx = animted_butns[UnityEngine.Random.Range(0, 5)];
				tempx.SetActive(value: true);
				animted_butns_panel.SetActive(value: true);
			}
			else if (failcounter == 2)
			{
				if (MainMenu.current_mode == 1 && (MainMenu.currentlevel == 7 || MainMenu.currentlevel == 15))
				{
					if (PlayerPrefs.GetInt("RemoveAds") != 0)
					{
					}
				}
				else
				{
					tempx = animted_butns[UnityEngine.Random.Range(0, 6)];
					tempx.SetActive(value: true);
					animted_butns_panel.SetActive(value: true);
				}
			}
			skip_show();
			if (failcounter < 2)
			{
				failcounter++;
			}
			else if (MainMenu.currentlevel > 1)
			{
				failads();
				failcounter = 0;
			}
		}
		//_ads_manager.Instance._show_admob_Banner_new(AdPosition.TopLeft, AdSize.MediumRectangle);
	}

	public void skip_show()
	{
		if (failcounter != 2)
		{
			return;
		}
		if (MainMenu.current_mode == 1)
		{
			if (MainMenu.currentlevel == 4 || MainMenu.currentlevel == 9 || MainMenu.currentlevel == 13 || MainMenu.currentlevel == 15 || MainMenu.currentlevel == 16 || MainMenu.currentlevel == 18)
			{
				skip_btn.SetActive(value: true);
			}
		}
		else if (MainMenu.current_mode == 2)
		{
			if (MainMenu.currentlevel == 8 || MainMenu.currentlevel == 9)
			{
				skip_btn.SetActive(value: true);
			}
		}
		else if (MainMenu.current_mode == 4 && MainMenu.currentlevel < 10)
		{
			skip_btn.SetActive(value: true);
		}
	}

	public void failads()
	{
		//_ads_manager.Instance.Fail_ad();
	}

	public void compads()
	{
		if (MainMenu.current_mode == 5 && MainMenu.currentlevel == 10)
		{
			ad_game_open();
		}
		else
		{
			if (MainMenu.current_mode == 1)
			{
				if (MainMenu.currentlevel == 3 || MainMenu.currentlevel == 16)
				{
					no_ad_pop_up.SetActive(value: true);
					no_ad_pop_up.transform.GetChild(2).gameObject.SetActive(value: true);
					Invoke("inapp_banner_hide", 0.15f);
				}
				else if (MainMenu.currentlevel == 7 || MainMenu.currentlevel == 12)
				{
					if (PlayerPrefs.GetInt("level1") != 19 && PlayerPrefs.GetInt("level1") != 20)
					{
						no_ad_pop_up.SetActive(value: true);
						no_ad_pop_up.transform.GetChild(7).gameObject.SetActive(value: true);
						Invoke("inapp_banner_hide", 0.15f);
					}
				}
				else if (MainMenu.currentlevel > 1 && MainMenu.current_mode >= 1)
				{
					Invoke("comp_banner", 0.1f);
				}
			}
			else if (MainMenu.current_mode == 2)
			{
				if (MainMenu.currentlevel == 4)
				{
					no_ad_pop_up.SetActive(value: true);
					no_ad_pop_up.transform.GetChild(2).gameObject.SetActive(value: true);
					Invoke("inapp_banner_hide", 0.15f);
				}
				else if (MainMenu.currentlevel == 1)
				{
					if (PlayerPrefs.GetInt("level2") != 9 && PlayerPrefs.GetInt("level2") != 10)
					{
						no_ad_pop_up.SetActive(value: true);
						no_ad_pop_up.transform.GetChild(7).gameObject.SetActive(value: true);
						Invoke("inapp_banner_hide", 0.15f);
					}
				}
				else
				{
					Invoke("comp_banner", 0.1f);
				}
			}
			else if (MainMenu.current_mode == 3)
			{
				if (MainMenu.currentlevel == 5)
				{
					no_ad_pop_up.SetActive(value: true);
					no_ad_pop_up.transform.GetChild(2).gameObject.SetActive(value: true);
					Invoke("inapp_banner_hide", 0.15f);
				}
				else if (MainMenu.currentlevel == 3)
				{
					if (PlayerPrefs.GetInt("level3") != 9 && PlayerPrefs.GetInt("level3") != 10)
					{
						no_ad_pop_up.SetActive(value: true);
						no_ad_pop_up.transform.GetChild(7).gameObject.SetActive(value: true);
						Invoke("inapp_banner_hide", 0.15f);
					}
				}
				else
				{
					Invoke("comp_banner", 0.1f);
				}
			}
			else if (MainMenu.current_mode == 4)
			{
				if (MainMenu.currentlevel == 4)
				{
					no_ad_pop_up.SetActive(value: true);
					no_ad_pop_up.transform.GetChild(2).gameObject.SetActive(value: true);
					Invoke("inapp_banner_hide", 0.15f);
				}
				else if (MainMenu.currentlevel == 2)
				{
					if (PlayerPrefs.GetInt("level4") != 9 && PlayerPrefs.GetInt("level4") != 10)
					{
						no_ad_pop_up.SetActive(value: true);
						no_ad_pop_up.transform.GetChild(7).gameObject.SetActive(value: true);
						Invoke("inapp_banner_hide", 0.15f);
					}
				}
				else
				{
					Invoke("comp_banner", 0.1f);
				}
			}
			else if (MainMenu.current_mode == 5)
			{
				if (PlayerPrefs.GetInt("level5") != 9 && PlayerPrefs.GetInt("level5") != 10)
				{
					no_ad_pop_up.SetActive(value: true);
					no_ad_pop_up.transform.GetChild(7).gameObject.SetActive(value: true);
					Invoke("inapp_banner_hide", 0.15f);
				}
				else
				{
					Invoke("comp_banner", 0.1f);
				}
			}
			else if (MainMenu.currentlevel > 1 && MainMenu.current_mode >= 1)
			{
				Invoke("comp_banner", 0.1f);
			}
			if (MainMenu.current_mode == 1 && (MainMenu.currentlevel == 10 || MainMenu.currentlevel == 19))
			{
				Invoke("no_ad_pop", 0.5f);
			}
		}
		//_ads_manager.Instance._show_admob_Banner_new(AdPosition.TopLeft, AdSize.MediumRectangle);
	}

	public void comp_banner()
	{
		//_ads_manager.Instance.Comp_ad();
	}

	public void no_ad_pop()
	{
		if (PlayerPrefs.GetInt("RemoveAds") == 0)
		{
			inapp_banner_hide();
			no_ad_pop_up.SetActive(value: true);
			no_ad_pop_up.transform.GetChild(1).gameObject.SetActive(value: true);
		}
	}

	public void pausads()
	{
		//_ads_manager.Instance.Pause_ad();
	}

	public void map_on()
	{
		player.GetComponent<Rigidbody>().isKinematic = true;
		tutorial_panel.gameObject.transform.GetChild(3).gameObject.SetActive(value: true);
		tutorial_panel.GetComponent<Image>().enabled = true;
	}

	public void map_of()
	{
		if (MainMenu.currentlevel == 7 && MainMenu.current_mode == 1)
		{
			PlayerPrefs.SetInt("map7", 1);
			player.GetComponent<Rigidbody>().isKinematic = false;
			tutorial_panel.gameObject.transform.GetChild(3).gameObject.SetActive(value: false);
			tutorial_panel.GetComponent<Image>().enabled = false;
			tutorial_panel.SetActive(value: false);
			hand_brake.GetComponent<Image>().enabled = true;
			steering.GetComponent<Image>().enabled = true;
			pause_btn.SetActive(value: true);
			gear_btn.SetActive(value: true);
			setting_btn.SetActive(value: true);
			for_press.gameObject.SetActive(value: false);
			levels_text.SetActive(value: true);
			if (MainMenu.car_control != 1 && MainMenu.car_control == 2)
			{
				left_butn.GetComponent<Button>().image.enabled = true;
				right_btn.GetComponent<Button>().image.enabled = true;
			}
			gas_button.GetComponent<Button>().image.enabled = true;
		}
	}

	public void Head_lights_on_of()
	{
		head_light_counter++;
		if (head_light_counter % 2 == 0)
		{
			head_lights_cars.SetActive(value: false);
		}
		else if (head_light_counter % 2 == 1)
		{
			head_lights_cars.SetActive(value: true);
		}
	}

	public void brake_on()
	{
		brake_light.SetActive(value: true);
	}

	public void brake_of()
	{
		brake_light.SetActive(value: false);
	}

	public void zoom_on()
	{
		player.GetComponent<Rigidbody>().isKinematic = true;
		camera_slider.gameObject.SetActive(value: true);
		tutorial_panel.gameObject.transform.GetChild(7).gameObject.SetActive(value: true);
		tutorial_panel.GetComponent<Image>().enabled = true;
		PlayerPrefs.SetInt("zoom5", 1);
	}

	public void zoom_of()
	{
		if (MainMenu.currentlevel == 5 && MainMenu.current_mode == 1)
		{
			player.GetComponent<Rigidbody>().isKinematic = false;
			tutorial_panel.gameObject.transform.GetChild(7).gameObject.SetActive(value: false);
			tutorial_panel.GetComponent<Image>().enabled = false;
			tutorial_panel.SetActive(value: false);
			map_btn.SetActive(value: true);
			hand_brake.GetComponent<Image>().enabled = true;
			steering.GetComponent<Image>().enabled = true;
			pause_btn.SetActive(value: true);
			gear_btn.SetActive(value: true);
			setting_btn.SetActive(value: true);
			if (MainMenu.car_control != 1 && MainMenu.car_control == 2)
			{
				left_butn.GetComponent<Button>().image.enabled = true;
				right_btn.GetComponent<Button>().image.enabled = true;
			}
			gas_button.GetComponent<Button>().image.enabled = true;
		}
	}

	public void rates()
	{
		rate_us_panel.SetActive(value: true);
		btn_sound();
	}

	public void rewarded_skip_now()
	{
		try
		{
			inapp_banner_hide();
		//	_ads_manager.last_reward = rewarde_type.rewarded_skip;
		//	_ads_manager.Instance._Show_unity_rewarded_Ad();
		}
		catch (Exception)
		{
		}
		Analytics.CustomEvent("rewarded_skip", new Dictionary<string, object> { { "rewarded_skip", 1 } });
	}

	public void level_skip()
	{
		isskip = false;
		Time.timeScale = 1f;
		btn_sound();
		if (MainMenu.current_mode == 1)
		{
			if (MainMenu.currentlevel == 4 || MainMenu.currentlevel == 9 || MainMenu.currentlevel == 13 || MainMenu.currentlevel == 15 || MainMenu.currentlevel == 16 || MainMenu.currentlevel == 18)
			{
				MainMenu.currentlevel++;
				//_ads_manager.Instance.hideBanner();
				SceneManager.LoadScene(3);
				if (MainMenu.current_mode == 1)
				{
					Analytics.CustomEvent("level_start" + analyticsVersionString, new Dictionary<string, object> { 
					{
						"level_index",
						MainMenu.currentlevel
					} });
				}
			}
		}
		else if (MainMenu.current_mode == 2)
		{
			if (MainMenu.currentlevel == 8 || MainMenu.currentlevel == 9)
			{
				MainMenu.currentlevel++;
				//_ads_manager.Instance.hideBanner();
				SceneManager.LoadScene(4);
				if (MainMenu.current_mode == 2)
				{
					Analytics.CustomEvent("level_start" + analyticsVersionString, new Dictionary<string, object> { 
					{
						"level_index1",
						MainMenu.currentlevel
					} });
				}
			}
		}
		else if (MainMenu.current_mode == 4)
		{
			failcounter = 0;
			MainMenu.currentlevel++;
			//_ads_manager.Instance.hideBanner();
			SceneManager.LoadScene(6);
			if (MainMenu.current_mode == 4)
			{
				Analytics.CustomEvent("level_start" + analyticsVersionString, new Dictionary<string, object> { 
				{
					"level_index3",
					MainMenu.currentlevel
				} });
			}
		}
		if (MainMenu.current_mode == 1)
		{
			Analytics.CustomEvent("level_complete" + analyticsVersionString, new Dictionary<string, object> { 
			{
				"level_index",
				MainMenu.currentlevel - 1
			} });
		}
		else if (MainMenu.current_mode == 2)
		{
			Analytics.CustomEvent("level_complete" + analyticsVersionString, new Dictionary<string, object> { 
			{
				"level_index1",
				MainMenu.currentlevel - 1
			} });
		}
		else if (MainMenu.current_mode == 4)
		{
			Analytics.CustomEvent("level_complete" + analyticsVersionString, new Dictionary<string, object> { 
			{
				"level_index3",
				MainMenu.currentlevel - 1
			} });
		}
		if (MainMenu.current_mode == 1)
		{
			Analytics.CustomEvent("skip", new Dictionary<string, object> { 
			{
				"skip",
				MainMenu.currentlevel
			} });
		}
		else if (MainMenu.current_mode == 2)
		{
			Analytics.CustomEvent("skip", new Dictionary<string, object> { 
			{
				"skip1",
				MainMenu.currentlevel
			} });
		}
		else if (MainMenu.current_mode == 4)
		{
			Analytics.CustomEvent("skip", new Dictionary<string, object> { 
			{
				"skip4",
				MainMenu.currentlevel
			} });
		}
	}

	public void ad_game_link()
	{
		ad_game_close();
	}

	public void ad_game_open()
	{
		static_game_ad.SetActive(value: true);
		//ADS.ads.static_ad_full();
	}

	public void ad_game_close()
	{
		SceneManager.LoadScene(2);
	}

	public void share_panel_open()
	{
		switch_mode.SetActive(value: true);
		if (PlayerPrefs.GetInt("shared_pressed") == 0)
		{
			only_complete.SetActive(value: true);
		}
		else
		{
			only_complete.SetActive(value: true);
		}
	}

	public void share_panel_close()
	{
		Time.timeScale = 1f;
		PlayerPrefs.SetInt("showroom", 0);
		PlayerPrefs.SetInt("selection", 1);
		if (PlayerPrefs.GetInt("shared_pressed") == 1)
		{
			PlayerPrefs.SetInt("mode2", 1);
			PlayerPrefs.SetInt("sharedone", 1);
		}
		//_ads_manager.Instance.DestroyBanner();
		SceneManager.LoadScene(2);
	}

	public void next_new()
	{
		PlayerPrefs.SetInt("shareit", 1);
		PlayerPrefs.SetInt("mode2", 1);
		PlayerPrefs.SetInt("sharedone", 1);
		PlayerPrefs.SetInt("showroom", 0);
		PlayerPrefs.SetInt("selection", 1);
		//_ads_manager.Instance.DestroyBanner();
		SceneManager.LoadScene(2);
		Analytics.CustomEvent("mode" + analyticsVersionString, new Dictionary<string, object> { { "switch", 1 } });
	}

	public void Reset_car()
	{
		Checkpoints.chk.Reward();
		Analytics.CustomEvent("car_reset", new Dictionary<string, object> { 
		{
			"reset",
			MainMenu.currentlevel
		} });
	}

	public void Reset_car_reward()
	{
		try
		{
		//	_ads_manager.last_reward = rewarde_type.revive_player;
		//	_ads_manager.Instance._Show_unity_rewarded_Ad();
		}
		catch (Exception)
		{
		}
	}

	public void rewarde_to_unlock_mode2()
	{
		//_ads_manager.last_reward = rewarde_type.mode2_unlock;
		//_ads_manager.Instance._Show_unity_rewarded_Ad();
		Analytics.CustomEvent("rewarded_mode", new Dictionary<string, object> { { "rewarded_mode", 1 } });
	}

	public void result_mode2_rewarded_press()
	{
		PlayerPrefs.SetInt("shareit", 1);
		PlayerPrefs.SetInt("mode2", 1);
		PlayerPrefs.SetInt("sharedone", 1);
		SceneManager.LoadScene(2);
	}

	public void sound_check_again()
	{
		cans.GetComponent<AudioSource>().volume = music_game_play.value;
		traffice_sound.volume = music_game_play.value;
		PlayerPrefs.SetFloat("music", music_game_play.value);
	}

	public void sound_btn_check()
	{
		btn_click.volume = sound_slider.value;
		PlayerPrefs.SetFloat("sound", sound_slider.value);
		player.GetComponent<RCC_CarControllerV3>().maxEngineSoundVolume = sound_slider.value;
		reverse_beep.volume = sound_slider.value;
		gear_sound.volume = sound_slider.value;
	}

	public void drive_side_again(int dside)
	{
		new_drive_side = dside;
		drive_side_check_again();
		MainMenu.driveside = new_drive_side;
		btn_sound();
		RCC_UISteeringWheelController.ins.SteeringWheelInit();
	}

	private void drive_side_check_again()
	{
		if (new_drive_side == 2)
		{
			left_side.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 0.5f);
			right_side.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 1f);
			left_side_inner_hole.enabled = false;
			right_side_inner_hole.enabled = true;
			gas_button.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.1385756f, 0.01749125f);
			gas_button.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.2327943f, 0.3737413f);
			hand_brake.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.02015221f, 0.02920006f);
			hand_brake.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.1157773f, 0.2429501f);
			steering.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.72f, 0.043f);
			steering.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.961f, 0.46f);
			gear_btn.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.0026f, 0.45f);
			gear_btn.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.06f, 0.72f);
			left_butn.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.693f, 0.058f);
			left_butn.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.815f, 0.254f);
			right_btn.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.85f, 0.058f);
			right_btn.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.97f, 0.25f);
		}
		else if (new_drive_side == 1)
		{
			left_side.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 1f);
			right_side.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 0.5f);
			left_side_inner_hole.enabled = true;
			right_side_inner_hole.enabled = false;
			gas_button.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.87f, 0.006f);
			gas_button.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.98f, 0.31f);
			steering.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.006f, 0.04f);
			steering.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.28f, 0.47f);
			hand_brake.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.73f, 0.006f);
			hand_brake.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.84f, 0.25f);
			gear_btn.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.92f, 0.45f);
			gear_btn.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.99f, 0.72f);
			left_butn.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.035f, 0.063f);
			left_butn.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.17f, 0.3f);
			right_btn.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.19f, 0.06f);
			right_btn.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.33f, 0.3f);
		}
	}

	public void car_control_side_again(int cside)
	{
		new_car_control = cside;
		MainMenu.car_control = new_car_control;
		Invoke("car_control_check_again", 0.15f);
		btn_sound();
	}

	private void car_control_check_again()
	{
		if (new_car_control == 1)
		{
			l_r_btns.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 0.5f);
			steerint_btns.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 1f);
			steering_inner_hole.enabled = true;
			left_right_inner_hole.enabled = false;
			RCC_Settings.instance.useSteeringWheelForSteering = true;
			left_butn.SetActive(value: false);
			right_btn.SetActive(value: false);
			if (MainMenu.currentlevel == 2 && MainMenu.current_mode == 1)
			{
				if (PlayerPrefs.GetInt("le2") != 0)
				{
				}
			}
			else if (MainMenu.currentlevel == 7 && MainMenu.current_mode == 1 && PlayerPrefs.GetInt("map7") != 0)
			{
			}
		}
		else
		{
			if (new_car_control != 2)
			{
				return;
			}
			l_r_btns.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 1f);
			steerint_btns.GetComponent<Image>().color = new Color(left_side.GetComponent<Image>().color.r, left_side.GetComponent<Image>().color.g, left_side.GetComponent<Image>().color.b, 0.5f);
			RCC_Settings.instance.useSteeringWheelForSteering = false;
			steering_inner_hole.enabled = false;
			left_right_inner_hole.enabled = true;
			if (PlayerPrefs.GetInt("le2") == 0)
			{
				left_butn.SetActive(value: false);
				right_btn.SetActive(value: false);
				return;
			}
			left_butn.SetActive(value: true);
			right_btn.SetActive(value: true);
			if (!left_butn.GetComponent<Button>().image.enabled && !right_btn.GetComponent<Button>().image.enabled)
			{
				left_butn.GetComponent<Button>().image.enabled = true;
				right_btn.GetComponent<Button>().image.enabled = true;
			}
		}
	}

	public void on_down()
	{
		istouched = true;
	}

	public void on_up()
	{
		istouched = false;
	}

	private void key_sounds()
	{
		if (MainMenu.menu.music_slider.value > 0f)
		{
			car_unlock_key_sound.Play();
		}
	}

	public void btn_sound()
	{
		if (sound_slider.value > 0f)
		{
			btn_click.Play();
		}
	}

	public void double_the_coins()
	{
		try
		{
			inapp_banner_hide();
		//	_ads_manager.last_reward = rewarde_type.doublethecoins;
		//	_ads_manager.Instance._Show_unity_rewarded_Ad();
		}
		catch (Exception)
		{
		}
	}

	public void camera_trainning()
	{
		camera_tutorial_panel.SetActive(value: true);
	}

	public void in_app_func(string app_code)
	{
		switch (app_code)
		{
		case "unlock_all_modes":
			if (PlayerPrefs.GetInt("20wala") != 1 || PlayerPrefs.GetInt("10wala") != 1 || PlayerPrefs.GetInt("10wala4") != 1 || PlayerPrefs.GetInt("10wala5") != 1)
			{
			//	_ads_manager.Instance.inAppDeals(6);
			}
			break;
		case "unlock_modern_mode":
			if (PlayerPrefs.GetInt("20wala") != 1)
			{
			//	_ads_manager.Instance.inAppDeals(12);
			}
			break;
		case "unlock_mission_mode":
			if (PlayerPrefs.GetInt("10wala") != 1)
			{
			//	_ads_manager.Instance.inAppDeals(13);
			}
			break;
		case "unlock_time_attack":
			if (PlayerPrefs.GetInt("10wala4") != 1)
			{
			//	_ads_manager.Instance.inAppDeals(14);
			}
			break;
		case "unlock_snow":
			if (PlayerPrefs.GetInt("10wala5") != 1)
			{
			//	_ads_manager.Instance.inAppDeals(15);
			}
			break;
		case "removeads":
			if (PlayerPrefs.GetInt("RemoveAds") == 0)
			{
			//	_ads_manager.Instance.inAppDeals(0);
			}
			break;
		case "unlock_all_modes_levels":
			if (MainMenu.current_mode == 1)
			{
				if (PlayerPrefs.GetInt("level1") != 20)
				{
				//	_ads_manager.Instance.inAppDeals(1);
				}
			}
			else if (MainMenu.current_mode == 2)
			{
				if (PlayerPrefs.GetInt("level2") != 10)
				{
				//	_ads_manager.Instance.inAppDeals(2);
				}
			}
			else if (MainMenu.current_mode == 3)
			{
				if (PlayerPrefs.GetInt("level3") != 10)
				{
				//	_ads_manager.Instance.inAppDeals(3);
				}
			}
			else if (MainMenu.current_mode == 4)
			{
				if (PlayerPrefs.GetInt("level4") != 10)
				{
				//	_ads_manager.Instance.inAppDeals(4);
				}
			}
			else if (MainMenu.current_mode == 5 && PlayerPrefs.GetInt("level5") != 10)
			{
			//	_ads_manager.Instance.inAppDeals(5);
			}
			break;
		}
	}

	public void timer_barrier_cube_1()
	{
		clock_for_barrier.gameObject.SetActive(value: true);
	}

	public void timer_barrier_cube_2()
	{
		timer_barrier.enabled = true;
		clock_for_barrier.gameObject.SetActive(value: false);
	}

	public void open_coins(int lv_index)
	{
		if (PlayerPrefs.GetInt("star1" + MainMenu.currentlevel) == 0)
		{
			coins_for_levels[lv_index].SetActive(value: true);
		}
	}

	public void _level_start_18()
	{
		player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
	}

	public void _level_end_18()
	{
		player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
	}

	public void level_9_start_animations()
	{
		if (MainMenu.current_mode == 1 && MainMenu.currentlevel == 9)
		{
			level9_hurdl1.enabled = true;
			level9_hurdl2.enabled = true;
		}
		else if (MainMenu.current_mode == 1 && MainMenu.currentlevel == 15)
		{
			level_15_hurdl.enabled = true;
		}
	}

	public void _level_start_15()
	{
		player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
	}

	public void _level_end_15()
	{
		player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
	}

	public void _to_stop_car()
	{
		player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
	}

	public void _to_move_car()
	{
		player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
	}

	public void inapp_banner_hide()
	{
	//	_ads_manager.Instance.hideBanner();
	}

	public void inapp_banner_show()
	{
		//_ads_manager.Instance._show_admob_Banner_new(AdPosition.TopLeft, AdSize.MediumRectangle);
	}
}
