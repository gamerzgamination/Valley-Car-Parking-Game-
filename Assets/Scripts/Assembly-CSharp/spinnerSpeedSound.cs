using UnityEngine;

public class spinnerSpeedSound : MonoBehaviour
{
	public AudioSource WheelAudioSource;

	public Rigidbody2D WheelRB2;

	public void ButtonClick()
	{
		WheelRB2.AddTorque(200f);
		WheelAudioSource.enabled = true;
	}

	private void Update()
	{
		if (WheelRB2.angularVelocity < 15f)
		{
			WheelAudioSource.enabled = false;
			return;
		}
		WheelAudioSource.enabled = true;
		WheelAudioSource.pitch = Mathf.Clamp(WheelRB2.angularVelocity / 200f, 0.6f, 2f);
		MonoBehaviour.print(WheelRB2.angularVelocity + " , " + WheelAudioSource.pitch);
	}
}
