using UnityEngine;
using System.Collections;

public class SoundsManager : MonoBehaviour {

	[SerializeField] private float defaultSoundSourceVolume = 1;
	[SerializeField] private float defaultMusicSourceVolume = 1;

	[Header("Audio Sources")]
	public AudioSource soundSource;
	public AudioSource musicSource;

	[Header("BG Clips")]
	public AudioClip menuBG;
	public AudioClip gameBG;

	[Header("Sound Clips")]
	public AudioClip Gearshiftingclip;
	public AudioClip RevreseBeepclip;
	public AudioClip CarLock;
	public AudioClip CarHitAlarm;
	//public AudioClip IntroAnimation;
	//public AudioClip buttonPress;
	//public AudioClip On_PressMoreGameRateus;
	//public AudioClip PrivacyPolicyPress;
	//public AudioClip weaponPress;
	//public AudioClip Quit;
	//public AudioClip okyesNo;
	//public AudioClip levelpress;
	//public AudioClip OnPressCompaignMode;
	//public AudioClip OnPresslockedbutton;
	//public AudioClip OnAnyPopupAppear;
	//public AudioClip lockedlevelpopupokclick;
	//public AudioClip settingpopup;
	//public AudioClip settingpopupOkclick;
	//public AudioClip PlayButtonMainMenuclick;
	//public AudioClip BackButtonAnySelectionclick;
	//public AudioClip PlayButtonGunselectionclick;
	//public AudioClip OnPressNextGun;
	//public AudioClip OnPressPreviousGun;
	//public AudioClip GamePLayPopup;
	public AudioClip GameUIclicks;
	public AudioClip levelComplete;
	public AudioClip levelFail;
	//public AudioClip Terroristwin;
	public AudioClip singleCoinsSound;


	private void Start()
    {

		Set_MusicStatus(Toolbox.DB.Prefs.GameAudio);
        Set_SoundStatus(Toolbox.DB.Prefs.GameSound);

		Set_SoundVolume(Toolbox.DB.Prefs.SoundVolume);
		Set_MusicVolume(Toolbox.DB.Prefs.MusicVolume);
        
        
	}

    public void Pause(){

		this.soundSource.Pause ();
		this.musicSource.Pause();
	}

	public void UnPause(){

		this.soundSource.UnPause ();
		this.musicSource.UnPause();

	}

	public void PlaySound(AudioClip _clip){

		soundSource.PlayOneShot (_clip);
	}

	public void Stopsound()
	{
		soundSource.Stop();
	}
	public void PlaySoundAfterStop(AudioClip _clip)
	{
		Stop_PlayingSound();
		soundSource.PlayOneShot(_clip);
	}

	public void Stop_PlayingSound(){
		soundSource.Stop ();
	}

	public void PlayMusic_Menu() {

		musicSource.clip = menuBG;
		musicSource.Play();
	}

	public void PlayMusic_Game()
	{
		musicSource.clip = gameBG;
		musicSource.Play();

	}

	public void Set_MusicStatus(bool _val) {

		if (_val)
		{
			musicSource.volume = defaultMusicSourceVolume;
		}
		else { 
			musicSource.volume = 0;
		}
        Toolbox.DB.Prefs.GameAudio = _val;
	}

	public void Set_SoundStatus(bool _val)
	{
		if (_val)
		{
			soundSource.volume = defaultSoundSourceVolume;
		}
		else
		{
			soundSource.volume = 0;
		}
        Toolbox.DB.Prefs.GameSound= _val;
    }

	public void Set_SoundVolume(float _val)
	{
		soundSource.volume = _val;
	}

	public void Set_MusicVolume(float _val)
	{
		musicSource.volume = _val;
	}

}
