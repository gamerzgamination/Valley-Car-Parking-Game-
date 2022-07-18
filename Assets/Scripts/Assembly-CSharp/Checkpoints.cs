using UnityEngine;

public class Checkpoints : MonoBehaviour
{
	public static Checkpoints chk;

	[SerializeField]
	public int index;

	public Transform laspos;

	public Vector3 lasposssss;

	public Quaternion laspoRotation;

	public static bool isrewarded_revival;

	private void Start()
	{
		chk = this;
	}

	private void OnTriggerEnter(Collider obj)
	{
		if (obj.gameObject.tag == "checkpoint")
		{
			index = 1;
			lasposssss = obj.transform.position;
			laspoRotation = obj.transform.rotation;
			laspos = obj.transform;
			if (MainMenu.currentlevel == 13 && (bool)col.cl.lifter_check)
			{
				col.cl.lifter_check.enabled = false;
			}
		}
	}

	public void Reward()
	{
		if (index <= 0)
		{
			return;
		}
		if (MainMenu.currentlevel == 13 && MainMenu.current_mode == 1)
		{
			if (!col.cl.lifter_check.enabled)
			{
				Gmanager.gm.player.transform.position = laspos.position;
				Gmanager.gm.player.transform.rotation = laspos.rotation;
			}
		}
		else if (MainMenu.currentlevel == 15 && MainMenu.current_mode == 1)
		{
			if (!col.cl.lifter_check_2.enabled)
			{
				Gmanager.gm.player.transform.position = laspos.position;
				Gmanager.gm.player.transform.rotation = laspos.rotation;
			}
		}
		else if (MainMenu.currentlevel == 18 && MainMenu.current_mode == 1)
		{
			if (!col.cl.lifter_18.enabled)
			{
				Gmanager.gm.player.transform.position = laspos.position;
				Gmanager.gm.player.transform.rotation = laspos.rotation;
			}
		}
		else
		{
			Gmanager.gm.player.transform.position = laspos.position;
			Gmanager.gm.player.transform.rotation = laspos.rotation;
		}
	}

	public void Reward_ad()
	{
		if (index > 0)
		{
			isrewarded_revival = true;
			Gmanager.gm.revive_panel.SetActive(value: false);
			Application.LoadLevel(Application.loadedLevel);
			return;
		}
		Gmanager.gm.revive_panel.SetActive(value: false);
		Application.LoadLevel(Application.loadedLevel);
		if (MainMenu.current_mode == 1)
		{
			Gmanager.gm.player.transform.position = Gmanager.gm.levels.transform.GetChild(MainMenu.currentlevel - 1).GetChild(0).transform.position;
			Gmanager.gm.player.transform.rotation = Gmanager.gm.levels.transform.GetChild(MainMenu.currentlevel - 1).GetChild(0).transform.rotation;
		}
		else if (MainMenu.current_mode == 2)
		{
			Gmanager.gm.player.transform.position = Gmanager.gm.levels2.transform.GetChild(MainMenu.currentlevel - 1).GetChild(0).transform.position;
			Gmanager.gm.player.transform.rotation = Gmanager.gm.levels2.transform.GetChild(MainMenu.currentlevel - 1).GetChild(0).transform.rotation;
		}
		else if (MainMenu.current_mode == 3)
		{
			Gmanager.gm.player.transform.position = Gmanager.gm.levels3.transform.GetChild(MainMenu.currentlevel - 1).GetChild(0).transform.position;
			Gmanager.gm.player.transform.rotation = Gmanager.gm.levels3.transform.GetChild(MainMenu.currentlevel - 1).GetChild(0).transform.rotation;
		}
		else if (MainMenu.current_mode == 4)
		{
			Gmanager.gm.player.transform.position = Gmanager.gm.levels4.transform.GetChild(MainMenu.currentlevel - 1).GetChild(0).transform.position;
			Gmanager.gm.player.transform.rotation = Gmanager.gm.levels4.transform.GetChild(MainMenu.currentlevel - 1).GetChild(0).transform.rotation;
		}
		else if (MainMenu.current_mode == 5)
		{
			Gmanager.gm.player.transform.position = Gmanager.gm.levels5.transform.GetChild(MainMenu.currentlevel - 1).GetChild(0).transform.position;
			Gmanager.gm.player.transform.rotation = Gmanager.gm.levels5.transform.GetChild(MainMenu.currentlevel - 1).GetChild(0).transform.rotation;
		}
	}
}
