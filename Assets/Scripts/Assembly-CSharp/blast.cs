using UnityEngine;

public class blast : MonoBehaviour
{
	public static blast bls;

	private bool oneblast;

	public GameObject temp;

	private void Start()
	{
		bls = this;
		oneblast = false;
	}

	private void OnTriggerEnter(Collider mine)
	{
		if (mine.gameObject.tag == "bomb" && !oneblast)
		{
			oneblast = true;
			if (Gmanager.gm.cans.GetComponent<AudioSource>().volume > 0f)
			{
				Gmanager.gm.blast_sound.Play();
			}
			Handheld.Vibrate();
			Gmanager.gm.player.GetComponent<Rigidbody>().AddForce(Vector3.up * 200000f);
			Invoke("stop", 0.1f);
			temp = Object.Instantiate(Gmanager.gm.blast_smoke, new Vector3(mine.gameObject.transform.position.x, mine.gameObject.transform.position.y, mine.gameObject.transform.position.z), Quaternion.identity);
			Gmanager.gm.fail_levl();
		}
	}

	public void stop()
	{
		Gmanager.gm.player.GetComponent<Rigidbody>().isKinematic = true;
		Object.Destroy(temp);
	}
}
