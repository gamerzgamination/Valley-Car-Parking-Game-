using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class col : MonoBehaviour
{
	public Material redmat;

	public Material objmat1;

	private GameObject parkk;

	private GameObject hpark;

	public static col cl;

	public Material round;

	public string analyticsVersionString;

	public int coin;

	public GameObject base_out_cube;

	public Animator[] gate_wla;

	public static bool rate_one_time;

	public Animator lifter_check;

	public Animator lifter_check_2;

	public Animator rotator;

	public Animator rotator2;

	public Animator rotator3;

	public Animator rotator4;

	public Animator rotator5;

	public Animator new_lifter_1;

	public Animator lifter_6_mode5;

	public Animator lifter_18;

	public AudioSource coin_sound;

	public AudioSource ray_alram_sound;

	public AudioSource car_hit_sound;

	public bool oneee;

	public GameObject orbit_camera;

	public Animator level_18_hurdl;

	public Animator level_15_hurdl;

	public Animator level_12_1;

	public Animator level_12_2;

	public Animator level_12_3;

	public Animator mode2_level_7;

	public Animator mode2_level_9;

	private void Start()
	{
		cl = this;
		oneee = false;
	}

	private void OnCollisionEnter(Collision park)
	{
		if (park.gameObject.CompareTag("hurdl"))
		{
			hpark = park.gameObject;
			objmat1 = hpark.GetComponent<MeshRenderer>().material;
			orignal_mat(hpark, hpark.GetComponent<MeshRenderer>().material);
			Handheld.Vibrate();
			Invoke("stop", 0.05f);
			Gmanager.gm.fail_levl();
		}
		if (park.gameObject.CompareTag("Finish") && !oneee)
		{
			oneee = true;
			orbit_camera.GetComponent<ob>().autoRotateOn = true;
			GetComponent<Rigidbody>().isKinematic = true;
			if (MainMenu.current_mode == 4)
			{
				Gmanager.gm.istime_stop = true;
			}
			if (MainMenu.currentlevel == 20)
			{
				Gmanager.gm.comp_level();
				PlayerPrefs.SetInt("20wala", 1);
				PlayerPrefs.SetInt("shareit", 1);
			}
			else if (((MainMenu.current_mode == 1 && MainMenu.currentlevel == 4) || (MainMenu.current_mode == 2 && (MainMenu.currentlevel == 9 || MainMenu.currentlevel == 1)) || (MainMenu.current_mode == 3 && MainMenu.currentlevel == 7) || (MainMenu.current_mode == 4 && MainMenu.currentlevel == 6) || (MainMenu.current_mode == 5 && (MainMenu.currentlevel == 8 || MainMenu.currentlevel == 1))) && PlayerPrefs.GetInt("rates") == 0)
			{
				Gmanager.gm.rates();
			}
			else
			{
				Gmanager.gm.comp_level();
			}
			if (MainMenu.currentlevel == 10 && MainMenu.current_mode == 4)
			{
				PlayerPrefs.SetInt("10wala5", 1);
			}
			if (MainMenu.current_mode == 1)
			{
				Analytics.CustomEvent("level_complete" + analyticsVersionString, new Dictionary<string, object> { 
				{
					"level_index",
					MainMenu.currentlevel
				} });
			}
			else if (MainMenu.current_mode == 2)
			{
				Analytics.CustomEvent("level_complete" + analyticsVersionString, new Dictionary<string, object> { 
				{
					"level_index1",
					MainMenu.currentlevel
				} });
			}
			else if (MainMenu.current_mode == 3)
			{
				Analytics.CustomEvent("level_complete" + analyticsVersionString, new Dictionary<string, object> { 
				{
					"level_index2",
					MainMenu.currentlevel
				} });
			}
			else if (MainMenu.current_mode == 4)
			{
				Analytics.CustomEvent("level_complete" + analyticsVersionString, new Dictionary<string, object> { 
				{
					"level_index3",
					MainMenu.currentlevel
				} });
			}
			else if (MainMenu.current_mode == 5)
			{
				Analytics.CustomEvent("level_complete" + analyticsVersionString, new Dictionary<string, object> { 
				{
					"level_index4",
					MainMenu.currentlevel
				} });
			}
			if ((MainMenu.current_mode == 2 || MainMenu.current_mode == 3) && Gmanager.gm.traffice_sound.isPlaying)
			{
				Gmanager.gm.traffice_sound.Stop();
			}
			if (MainMenu.current_mode == 1 && (MainMenu.currentlevel <= 5 || MainMenu.currentlevel == 9 || MainMenu.currentlevel == 17 || MainMenu.currentlevel == 18 || MainMenu.currentlevel == 13 || MainMenu.currentlevel == 14 || MainMenu.currentlevel == 15 || MainMenu.currentlevel == 7 || MainMenu.currentlevel == 10 || MainMenu.currentlevel == 12 || MainMenu.currentlevel == 19))
			{
				PlayerPrefs.SetInt("star1" + MainMenu.currentlevel, 1);
			}
		}
		if (park.gameObject.CompareTag("cars"))
		{
			hpark = park.gameObject;
			objmat1 = hpark.GetComponent<MeshRenderer>().material;
			orignal_mat(hpark, hpark.GetComponent<MeshRenderer>().material);
			Handheld.Vibrate();
			Invoke("stop", 0.05f);
			Gmanager.gm.fail_levl();
		}
		if (park.gameObject.CompareTag("base"))
		{
			parkk = park.gameObject;
			parkk.SetActive(value: false);
			ob.obi.distance = 2.5f;
			ob.obi.rotationXAxis = 50f;
		}
		if (park.gameObject.CompareTag("next"))
		{
			parkk = park.gameObject;
			parkk.SetActive(value: false);
			Gmanager.gm.next_panel.SetActive(value: true);
			Invoke("of_ho", 1.75f);
			if (MainMenu.currentlevel == 4 && MainMenu.current_mode == 2)
			{
				Gmanager.gm.mode2_arrows[0].SetActive(value: false);
				Gmanager.gm.mode2_arrows[1].SetActive(value: true);
				Gmanager.gm.mode2_animtor_hurdles[0].enabled = true;
			}
			else if (MainMenu.currentlevel == 5 && MainMenu.current_mode == 2)
			{
				Gmanager.gm.mode2_arrows[2].SetActive(value: false);
				Gmanager.gm.mode2_arrows[3].SetActive(value: true);
				Gmanager.gm.mode2_level5_hurdles.SetActive(value: false);
			}
			else if (MainMenu.currentlevel == 9 && MainMenu.current_mode == 2)
			{
				Gmanager.gm.mode2_arrows[4].SetActive(value: false);
				Gmanager.gm.mode2_arrows[5].SetActive(value: true);
			}
			else if (MainMenu.currentlevel == 10 && MainMenu.current_mode == 2)
			{
				Gmanager.gm.mode2_arrows[6].SetActive(value: false);
				Gmanager.gm.mode2_arrows[7].SetActive(value: true);
				Gmanager.gm.mode2_animtor_hurdles[5].enabled = true;
				Gmanager.gm.mode2_animtor_hurdles[6].enabled = true;
				Gmanager.gm.mode2_animtor_hurdles[7].enabled = true;
			}
			else if (MainMenu.current_mode == 3 && MainMenu.currentlevel == 5)
			{
				Gmanager.gm.mode2_arrows[22].SetActive(value: false);
				Gmanager.gm.mode2_arrows[10].SetActive(value: true);
			}
			else if (MainMenu.currentlevel == 6 && MainMenu.current_mode == 3)
			{
				if (Gmanager.gm.mode2_arrows[11].activeSelf)
				{
					Gmanager.gm.mode2_arrows[11].SetActive(value: false);
					Gmanager.gm.mode2_arrows[12].SetActive(value: true);
				}
				else if (Gmanager.gm.mode2_arrows[12].activeSelf)
				{
					Gmanager.gm.mode2_arrows[12].SetActive(value: false);
					Gmanager.gm.mode2_arrows[13].SetActive(value: true);
				}
			}
			else if (MainMenu.currentlevel == 7 && MainMenu.current_mode == 3)
			{
				Gmanager.gm.mode2_arrows[14].SetActive(value: false);
				Gmanager.gm.mode2_arrows[15].SetActive(value: true);
			}
			else if (MainMenu.currentlevel == 8 && MainMenu.current_mode == 3)
			{
				Gmanager.gm.mode2_arrows[16].SetActive(value: false);
				Gmanager.gm.mode2_arrows[17].SetActive(value: true);
				Gmanager.gm.mode2_animtor_hurdles[8].enabled = true;
			}
			else if (MainMenu.currentlevel == 9 && MainMenu.current_mode == 3)
			{
				Gmanager.gm.mode2_arrows[18].SetActive(value: false);
				Gmanager.gm.mode2_arrows[19].SetActive(value: true);
			}
			else if (MainMenu.currentlevel == 10 && MainMenu.current_mode == 3)
			{
				Gmanager.gm.mode2_arrows[20].SetActive(value: false);
				Gmanager.gm.mode2_arrows[21].SetActive(value: true);
			}
		}
		if (park.gameObject.CompareTag("Finish2") && !oneee)
		{
			oneee = true;
			orbit_camera.GetComponent<ob>().autoRotateOn = true;
			GetComponent<Rigidbody>().isKinematic = true;
			if (MainMenu.currentlevel == 10 && MainMenu.current_mode == 2)
			{
				PlayerPrefs.SetInt("10wala", 1);
			}
			if (MainMenu.currentlevel == 10 && MainMenu.current_mode == 3)
			{
				PlayerPrefs.SetInt("10wala4", 1);
			}
			if (MainMenu.currentlevel == 10 && MainMenu.current_mode == 4)
			{
				Gmanager.gm.rcc_R_hud.SetActive(value: false);
				Gmanager.gm.map_btn.SetActive(value: false);
				Gmanager.gm.level_complete.SetActive(value: true);
				if (MainMenu.currentlevel >= PlayerPrefs.GetInt("level4"))
				{
					PlayerPrefs.SetInt("level4", MainMenu.currentlevel);
				}
				Gmanager.gm.cans.GetComponent<AudioSource>().enabled = false;
			}
			else
			{
				Gmanager.gm.comp_level();
			}
			if (MainMenu.current_mode == 2)
			{
				Analytics.CustomEvent("level_complete" + analyticsVersionString, new Dictionary<string, object> { 
				{
					"level_index1",
					MainMenu.currentlevel
				} });
			}
			else if (MainMenu.current_mode == 3)
			{
				Analytics.CustomEvent("level_complete" + analyticsVersionString, new Dictionary<string, object> { 
				{
					"level_index2",
					MainMenu.currentlevel
				} });
			}
			else if (MainMenu.current_mode == 4)
			{
				Analytics.CustomEvent("level_complete" + analyticsVersionString, new Dictionary<string, object> { 
				{
					"level_index3",
					MainMenu.currentlevel
				} });
			}
			if ((MainMenu.current_mode == 2 || MainMenu.current_mode == 3) && Gmanager.gm.traffice_sound.isPlaying)
			{
				Gmanager.gm.traffice_sound.Stop();
			}
		}
		if (park.gameObject.CompareTag("round"))
		{
			if (park.gameObject.transform.parent.GetComponent<Animator>() != null)
			{
				park.gameObject.transform.parent.GetComponent<Animator>().enabled = false;
			}
			hpark = park.gameObject;
			objmat1 = hpark.GetComponent<MeshRenderer>().material;
			orignal_mat(hpark, hpark.GetComponent<MeshRenderer>().material);
			Handheld.Vibrate();
			Invoke("stop", 0.05f);
			Gmanager.gm.fail_levl();
		}
		if (park.gameObject.CompareTag("crash"))
		{
			hpark = park.gameObject;
			objmat1 = hpark.GetComponent<MeshRenderer>().material;
			orignal_mat(hpark, hpark.GetComponent<MeshRenderer>().material);
			Handheld.Vibrate();
			Invoke("stop", 0.05f);
			Gmanager.gm.fail_levl();
		}
		if (park.gameObject.CompareTag("lift"))
		{
			lifter_check = park.gameObject.transform.parent.GetComponent<Animator>();
			lifter_check.enabled = true;
			park.gameObject.SetActive(value: false);
		}
		if (park.gameObject.CompareTag("upwards"))
		{
			lifter_check = park.gameObject.transform.parent.GetComponent<Animator>();
			lifter_check.enabled = true;
			park.gameObject.SetActive(value: false);
			Gmanager.gm.player.transform.parent = park.gameObject.transform.parent;
		}
		if (park.gameObject.CompareTag("downwards"))
		{
			lifter_check = park.gameObject.transform.parent.GetComponent<Animator>();
			lifter_check.enabled = true;
			park.gameObject.SetActive(value: false);
			Gmanager.gm.player.transform.parent = park.gameObject.transform.parent;
		}
		if (park.gameObject.CompareTag("stoper"))
		{
			park.gameObject.GetComponent<Animator>().enabled = false;
			hpark = park.gameObject;
			objmat1 = hpark.GetComponent<MeshRenderer>().material;
			orignal_mat(hpark, hpark.GetComponent<MeshRenderer>().material);
			Handheld.Vibrate();
			Invoke("stop", 0.05f);
			Gmanager.gm.fail_levl();
		}
		if (park.gameObject.CompareTag("lift2"))
		{
			lifter_check = park.gameObject.transform.parent.GetComponent<Animator>();
			lifter_check.enabled = true;
			park.gameObject.SetActive(value: false);
		}
		if (park.gameObject.CompareTag("baseout"))
		{
			parkk = park.gameObject;
			parkk.SetActive(value: false);
			ob.obi.distance = 5f;
			ob.obi.rotationXAxis = 70f;
		}
		if (park.gameObject.CompareTag("gate"))
		{
			parkk = park.gameObject;
			parkk.SetActive(value: false);
			gate_wla[0].enabled = true;
		}
		if (park.gameObject.CompareTag("gate1"))
		{
			parkk = park.gameObject;
			parkk.SetActive(value: false);
			gate_wla[1].enabled = true;
		}
		if (park.gameObject.CompareTag("gate2"))
		{
			parkk = park.gameObject;
			parkk.SetActive(value: false);
			gate_wla[2].enabled = true;
		}
		if (park.gameObject.CompareTag("gate3"))
		{
			parkk = park.gameObject;
			parkk.SetActive(value: false);
			gate_wla[3].enabled = true;
		}
		if (park.gameObject.CompareTag("gate4"))
		{
			parkk = park.gameObject;
			parkk.SetActive(value: false);
			gate_wla[4].enabled = true;
		}
		if (park.gameObject.CompareTag("ray"))
		{
			Handheld.Vibrate();
			Invoke("stop", 0.05f);
			Gmanager.gm.fail_levl();
		}
		if (park.gameObject.CompareTag("rotater"))
		{
			Gmanager.gm.player.transform.parent = park.gameObject.transform.parent;
			rotator.enabled = true;
			park.gameObject.SetActive(value: false);
		}
		if (park.gameObject.CompareTag("rotater2"))
		{
			rotator2.enabled = true;
			park.gameObject.SetActive(value: false);
		}
		if (park.gameObject.CompareTag("rotater3"))
		{
			rotator3.enabled = true;
			park.gameObject.SetActive(value: false);
		}
		if (park.gameObject.CompareTag("rotator4"))
		{
			Gmanager.gm.player.transform.parent = park.gameObject.transform.parent;
			rotator4.enabled = true;
			park.gameObject.SetActive(value: false);
		}
		if (park.gameObject.CompareTag("rotator4"))
		{
			Gmanager.gm.player.transform.parent = park.gameObject.transform.parent;
			rotator4.enabled = true;
			park.gameObject.SetActive(value: false);
		}
		if (park.gameObject.CompareTag("rotator5"))
		{
			Gmanager.gm.player.transform.parent = park.gameObject.transform.parent;
			rotator5.enabled = true;
			park.gameObject.SetActive(value: false);
		}
		if (park.gameObject.CompareTag("walls"))
		{
			Invoke("stop", 0.05f);
			Gmanager.gm.fail_levl();
		}
		if (park.gameObject.CompareTag("checktime"))
		{
			park.gameObject.SetActive(value: false);
			Gmanager.gm.timer_barrier_cube_1();
		}
		if (park.gameObject.CompareTag("sidelift"))
		{
			level_18_hurdl.enabled = true;
			Gmanager.gm.player.transform.parent = park.gameObject.transform.parent;
			park.gameObject.SetActive(value: false);
		}
		if (park.gameObject.CompareTag("forward"))
		{
			level_15_hurdl.enabled = true;
			Gmanager.gm.player.transform.parent = park.gameObject.transform.parent;
			park.gameObject.SetActive(value: false);
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("coins"))
		{
			PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 5);
			other.gameObject.SetActive(value: false);
			if (MainMenu.current_mode == 3 && MainMenu.currentlevel == 5)
			{
				base_out_cube.SetActive(value: true);
				Gmanager.gm.next_panel.SetActive(value: true);
				Invoke("of_ho", 1.75f);
				Gmanager.gm.mode2_arrows[8].SetActive(value: false);
				Gmanager.gm.mode2_arrows[9].SetActive(value: true);
			}
			if (Gmanager.gm.cans.GetComponent<AudioSource>().volume > 0f)
			{
				coin_sound.Play();
			}
		}
		if (other.gameObject.CompareTag("deattach"))
		{
			Gmanager.gm.player.transform.SetParent(null);
			MonoBehaviour.print("hua");
		}
		if (other.gameObject.CompareTag("new_lifter"))
		{
			new_lifter_1.enabled = true;
		}
		if (other.gameObject.CompareTag("lift6"))
		{
			lifter_6_mode5.enabled = true;
			other.gameObject.SetActive(value: false);
		}
		if (other.gameObject.CompareTag("ep"))
		{
			Gmanager.gm.container_faded.material = Gmanager.gm.faded_material;
		}
		if (other.gameObject.CompareTag("animall"))
		{
			level_12_1.enabled = true;
			level_12_2.enabled = true;
			level_12_3.enabled = true;
		}
		if (other.gameObject.CompareTag("anim"))
		{
			if (MainMenu.currentlevel == 7 && MainMenu.current_mode == 2)
			{
				mode2_level_7.enabled = true;
			}
			else if (MainMenu.currentlevel == 9 && MainMenu.current_mode == 2)
			{
				mode2_level_9.enabled = true;
			}
		}
	}

	public void stop()
	{
		GetComponent<Rigidbody>().isKinematic = true;
	}

	public void of_ho()
	{
		Gmanager.gm.next_panel.SetActive(value: false);
	}

	private void Red(GameObject obj, Material objmat)
	{
		hpark.GetComponent<MeshRenderer>().material = objmat1;
		Invoke("Red_color", 0.15f);
		Invoke("cancel_all", 0.5f);
	}

	private void orignal_mat(GameObject obj, Material objmat)
	{
		hpark.GetComponent<MeshRenderer>().material = redmat;
		Invoke("white_color", 0.15f);
		Invoke("cancel_all", 0.7f);
		if (Gmanager.gm.music_game_play.value > 0f)
		{
			car_hit_sound.Play();
		}
	}

	private void white_color()
	{
		Red(hpark, hpark.GetComponent<MeshRenderer>().material);
	}

	private void Red_color()
	{
		orignal_mat(hpark, hpark.GetComponent<MeshRenderer>().material);
	}

	private void cancel_all()
	{
		CancelInvoke("white_color");
		CancelInvoke("Red_color");
	}

	private void rot_to_comp()
	{
		if (Gmanager.completecounter < 4)
		{
			Gmanager.completecounter++;
			Gmanager.gm.comp_level();
			return;
		}
		Gmanager.completecounter = 0;
		Gmanager.gm.pop_up_panel.SetActive(value: true);
		cl.orbit_camera.GetComponent<ob>().autoRotateOn = false;
		Gmanager.gm.rcc_R_hud.SetActive(value: false);
		Gmanager.gm.map_btn.SetActive(value: false);
		PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 50);
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
}
