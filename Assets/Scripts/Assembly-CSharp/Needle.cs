using UnityEngine;

public class Needle : MonoBehaviour
{
	public AudioSource tan;

	public void OnCollisionEnter2D(Collision2D obj)
	{
		if (obj.gameObject.CompareTag("point") && MainMenu.menu.music_slider.value > 0f)
		{
			tan.Play();
		}
	}

	public void OnCollisionStay2D(Collision2D obj)
	{
		if (obj.gameObject.CompareTag("point") && obj.transform.parent.GetComponent<Rigidbody2D>().velocity.magnitude < 1f)
		{
			obj.transform.parent.GetComponent<Rigidbody2D>().AddTorque(-0.2f);
			base.transform.localPosition = new Vector3(4.700012f, 145f);
		}
	}

	public void OnCollisionExit2D(Collision2D obj)
	{
		if (obj.gameObject.CompareTag("point"))
		{
			base.transform.localEulerAngles = Vector3.zero;
			base.transform.localPosition = new Vector3(4.700012f, 145f);
		}
	}
}
