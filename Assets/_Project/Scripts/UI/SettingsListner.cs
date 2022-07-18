using UnityEngine;
using UnityEngine.UI;

public class SettingsListner : MonoBehaviour
{
    public Slider soundSlider;
    public Slider musicSlider;
    
    
    public Text versionTxt;

    private void OnEnable()
    {
       // Toolbox.GameManager.Add_ActiveUI(this.gameObject);
    }

    private void OnDisable()
    {
       // Toolbox.GameManager.Remove_ActiveUI(this.gameObject);
    }

    private void Start()
    {
        soundSlider.value = Toolbox.Soundmanager.soundSource.volume;
        musicSlider.value = Toolbox.Soundmanager.musicSource.volume;
      

        versionTxt.text = "V" + Application.version;
    }
    

   
    #region ButtonListners

    public void OnPress_Music(bool _val)
    {
        Toolbox.Soundmanager.Set_MusicStatus(_val);
    }
    public void OnPress_Sound(bool _val)
    {
        Toolbox.Soundmanager.Set_SoundStatus(_val);
    }

    public void OnSoundSliderValueChange(float _val)
    {

        Toolbox.Soundmanager.Set_SoundVolume(_val);

        Toolbox.DB.Prefs.SoundVolume = _val;
    }

    public void OnMusicSliderValueChange(float _val)
    {
        Toolbox.Soundmanager.Set_MusicVolume(_val);

        Toolbox.DB.Prefs.MusicVolume = _val;
    }
   

   
    #region GFX
    public void OnPress_Low()
    {
        Toolbox.GameManager.FBAnalytic_EventDesign("OnPress_Low");
        Toolbox.GameManager.Analytics_DesignEvent("OnPress_Low");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        QualitySettings.SetQualityLevel(0,true);
      
    //    Destroy(this.gameObject);
    }
    public void OnPress_Medium()
    {
        Toolbox.GameManager.FBAnalytic_EventDesign("OnPress_Medium");
        Toolbox.GameManager.Analytics_DesignEvent("OnPress_Medium");

        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        QualitySettings.SetQualityLevel(1,true);
       
   //    Destroy(this.gameObject);
    }
    public void OnPress_High()
    {
        Toolbox.GameManager.FBAnalytic_EventDesign("OnPress_High");
        Toolbox.GameManager.Analytics_DesignEvent("OnPress_High");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        QualitySettings.SetQualityLevel(2,true);
      
    //    Destroy(this.gameObject);
    }

    #endregion
    public void OnPress_Close()
    {
        //Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.buttonPress);
        Toolbox.GameManager.FBAnalytic_EventDesign("Settings_Press_Close");
        Toolbox.GameManager.Analytics_DesignEvent("Settings_Press_Close");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        this.gameObject.SetActive(false);
    //    Destroy(this.gameObject);
    }
    public void OnPress_PrivacyPolicy()
    {
        Toolbox.GameManager.FBAnalytic_EventDesign("Settings_Press_PrivacyPolicy");
        Toolbox.GameManager.Analytics_DesignEvent("Settings_Press_PrivacyPolicy");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Application.OpenURL(Constants.link_PrivacyPolicy);
    }

    public void OnPress_RestorePurchase()
    {
        Toolbox.GameManager.FBAnalytic_EventDesign("Settings_Press_RestorePurchase");
        Toolbox.GameManager.Analytics_DesignEvent("Settings_Press_RestorePurchase");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
    //    InAppHandler.Instance.RestorePurchases();
    }

    public void OnPress_Withdraw()
    {
        Toolbox.GameManager.FBAnalytic_EventDesign("Settings_Press_Withdraw");
        Toolbox.GameManager.Analytics_DesignEvent("Settings_Press_Withdraw");
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        this.gameObject.SetActive(false);
        Toolbox.UIManager.SurePop.SetActive(true);
    //    Toolbox.GameManager.Instantiate_Sure();
    }

    #endregion
}
