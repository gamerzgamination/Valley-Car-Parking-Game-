using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class Spinner : MonoBehaviour
{
	public GameObject fortune_wheel;

	public GameObject needel;

	public GameObject skull_btn;

	public GameObject paint_btn;

	public GameObject coins_btn;

	public GameObject rim_btn;

	public GameObject extra_spin;

	public GameObject feed_more_panel;

	public GameObject ad_check_panel;

	public GameObject hand_tutorial;

	public GameObject hook;

	public GameObject bar;

	public Text no_of_spins;

	public Button spin_wheel_btn;

	private int counter = -1;

	private float last_value;

	public string analyticsVersionString = string.Empty;

	public static Spinner spiner;

	public GameObject[] particles_effects;

	public AudioSource[] particles_sounds;

	public AudioSource spin_sound;

	public AudioClip end_part;

	private void Awake()
	{
		if (PlayerPrefs.GetInt("handon") == 0)
		{
			hand_tutorial.SetActive(value: true);
		}
	}

	private void Start()
	{
		spiner = this;
	}

	private void Update()
	{
		if (fortune_wheel.GetComponent<Rigidbody2D>().angularVelocity > -2f)
		{
			if (counter == 0)
			{
				counter = 1;
				needl_check();
			}
		}
		else
		{
			counter = 0;
		}
	}

	public void start_spin()
	{
		PlayerPrefs.SetInt("spiner", PlayerPrefs.GetInt("spiner") - 1);
		fortune_wheel.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-1000, -750));
		StartCoroutine("spinner_drag");
		fortune_wheel.GetComponent<Image>().color = Color.white;
		hook.GetComponent<Image>().color = Color.white;
		spin_wheel_btn.interactable = false;
		Analytics.CustomEvent("spinner" + analyticsVersionString, new Dictionary<string, object> { { "spinner", 1 } });
		PlayerPrefs.SetInt("handon", 1);
		hand_tutorial.SetActive(value: false);
	}

	public void late_check()
	{
		counter = 0;
	}

	private IEnumerator spinner_drag()
	{
		fortune_wheel.GetComponent<Rigidbody2D>().angularDrag = 0.2f;
		yield return new WaitForSeconds(1.5f);
		float random_value = Random.Range(0.005f, 0.015f);
		for (int i = 0; i <= 100; i++)
		{
			yield return new WaitForSeconds(0.2f);
			fortune_wheel.GetComponent<Rigidbody2D>().angularDrag += random_value;
		}
	}

	public void needl_check()
	{
		last_value = fortune_wheel.transform.localEulerAngles.z;
		fortune_wheel.GetComponent<Rigidbody2D>().angularVelocity = 0f;
		if (last_value < 0f)
		{
			last_value += 360f;
		}
		if ((last_value >= 0f && last_value <= 28.2f) || (last_value >= -31.21f && last_value <= 0f) || (last_value > 328.61f && last_value <= 360f))
		{
			coins_btn.SetActive(value: true);
			particles_effects[0].SetActive(value: true);
			if (MainMenu.menu.music_slider.value > 0f)
			{
				particles_sounds[0].Play();
			}
			spin_sound.Stop();
		}
		else if (last_value > 28.2f && last_value <= 88.25f)
		{
			paint_btn.SetActive(value: true);
			particles_effects[1].SetActive(value: true);
			if (MainMenu.menu.music_slider.value > 0f)
			{
				particles_sounds[1].Play();
			}
			spin_sound.Stop();
		}
		else if (last_value > 88.25f && last_value <= 148f)
		{
			spin_sound.Stop();
		}
		else if (last_value > 148f && last_value <= 209.4f)
		{
			extra_spin.SetActive(value: true);
			bar.SetActive(value: false);
			particles_effects[2].SetActive(value: true);
			if (MainMenu.menu.music_slider.value > 0f)
			{
				particles_sounds[2].Play();
			}
			spin_sound.Stop();
		}
		else if (last_value > 209.4f && last_value <= 268.91f)
		{
			skull_btn.SetActive(value: true);
			particles_effects[4].SetActive(value: true);
			spin_sound.Stop();
		}
		else if (last_value > 268.91f && last_value <= 328.61f)
		{
			rim_btn.SetActive(value: true);
			particles_effects[3].SetActive(value: true);
			if (MainMenu.menu.music_slider.value > 0f)
			{
				particles_sounds[3].Play();
			}
			spin_sound.Stop();
		}
	}

	public void btns(string names)
	{
		if (names == "skull")
		{
			PlayerPrefs.SetInt("sold4", 4);
			PlayerPrefs.SetInt("sticker", 6);
			skull_btn.SetActive(value: false);
			particles_effects[4].SetActive(value: false);
			MainMenu.menu.color_panel.SetActive(value: true);
			MainMenu.menu.stickers_panel.SetActive(value: true);
			MainMenu.menu.my_canvas.GetComponent<Canvas>().worldCamera = null;
			Modifications.modify.rimfunction();
			Modifications.modify.stickerfunction();
			Modifications.modify.ColorFunction();
			Modifications.modify.purchased_items();
			if (MainMenu.menu.music_slider.value > 0f)
			{
				MainMenu.menu.my_canvas.GetComponent<AudioSource>().Play();
			}
		}
		else if (names == "paint")
		{
			PlayerPrefs.SetInt("sold2", 2);
			PlayerPrefs.SetInt("color", 7);
			paint_btn.SetActive(value: false);
			particles_effects[1].SetActive(value: false);
			particles_sounds[1].Stop();
			MainMenu.menu.color_panel.SetActive(value: true);
			MainMenu.menu.pain_panel.SetActive(value: true);
			MainMenu.menu.my_canvas.GetComponent<Canvas>().worldCamera = null;
			Modifications.modify.rimfunction();
			Modifications.modify.stickerfunction();
			Modifications.modify.ColorFunction();
			Modifications.modify.purchased_items();
			if (MainMenu.menu.music_slider.value > 0f)
			{
				MainMenu.menu.my_canvas.GetComponent<AudioSource>().Play();
			}
		}
		switch (names)
		{
		case "coins":
			coins_btn.SetActive(value: false);
			PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 50);
			Modifications.modify.coin_text.text = PlayerPrefs.GetInt("coins").ToString();
			particles_effects[0].SetActive(value: false);
			particles_sounds[0].Stop();
			MainMenu.menu.menu_panel.SetActive(value: true);
			MainMenu.menu.my_canvas.GetComponent<Canvas>().worldCamera = null;
			Modifications.modify.rimfunction();
			Modifications.modify.stickerfunction();
			Modifications.modify.ColorFunction();
			Modifications.modify.purchased_items();
			if (MainMenu.menu.music_slider.value > 0f)
			{
				MainMenu.menu.my_canvas.GetComponent<AudioSource>().Play();
			}
			break;
		case "rim":
			PlayerPrefs.SetInt("sold3", 3);
			PlayerPrefs.SetInt("rim", 3);
			rim_btn.SetActive(value: false);
			particles_effects[3].SetActive(value: false);
			MainMenu.menu.color_panel.SetActive(value: true);
			MainMenu.menu.rim_panel.SetActive(value: true);
			MainMenu.menu.my_canvas.GetComponent<Canvas>().worldCamera = null;
			Modifications.modify.rimfunction();
			Modifications.modify.stickerfunction();
			Modifications.modify.ColorFunction();
			Modifications.modify.purchased_items();
			if (MainMenu.menu.music_slider.value > 0f)
			{
				MainMenu.menu.my_canvas.GetComponent<AudioSource>().Play();
			}
			break;
		case "oneplus":
			spin_wheel_btn.interactable = true;
			extra_spin.SetActive(value: false);
			particles_effects[2].SetActive(value: false);
			spin_wheel_btn.interactable = true;
			break;
		case "feedmore":
			feed_more_panel.SetActive(value: true);
			break;
		case "cancel":
			feed_more_panel.SetActive(value: false);
			break;
		}
	}

	public void spiner_result()
	{
		spin_wheel_btn.interactable = true;
		bar.SetActive(value: false);
	}

	public void back_all()
	{
		particles_effects[0].SetActive(value: false);
		particles_effects[1].SetActive(value: false);
		particles_effects[2].SetActive(value: false);
		particles_effects[3].SetActive(value: false);
		particles_effects[4].SetActive(value: false);
		particles_sounds[0].Stop();
		particles_sounds[1].Stop();
		particles_sounds[2].Stop();
		particles_sounds[3].Stop();
	}
}
