using UnityEngine;

public class check_engine : MonoBehaviour
{
	public AudioSource enigne_sound;

	public void btn_sound()
	{
		if (MainMenu.menu.sound_slider.value > 0f)
		{
			enigne_sound.Play();
		}
	}
}
