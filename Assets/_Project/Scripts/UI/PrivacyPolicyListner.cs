using UnityEngine;

public class PrivacyPolicyListner : MonoBehaviour
{

    private void Start()
    {
      //  DontDestroyOnLoad(this.gameObject);
    }

    private void OnDisable()
    {
        //Toolbox.GameManager.Remove_ActiveUI(this.gameObject);
    }
    #region ButtonListner

    public void Close() {

        if (!Toolbox.DB.Prefs.UserConsent)
            Toolbox.GameManager.Load_MenuScene(false,8);
        else
            Toolbox.GameManager.Load_MenuScene(false, 0);
        Toolbox.DB.Prefs.UserConsent = true;
        Toolbox.GameManager.Analytics_DesignEvent("PrivacyPolicy_Press_Close");
        this.gameObject.SetActive(false);
    }

    public void OnPress_PrivacyLink()
    {
        Toolbox.GameManager.Analytics_DesignEvent("PrivacyPolicy_Press_PrivacyLink");
        Application.OpenURL(Constants.link_PrivacyPolicy);
    }

    public void OnPress_Yes() {

        Toolbox.GameManager.Analytics_DesignEvent("PrivacyPolicy_Press_Yes");
        Close();
    }

    public void OnPress_No()
    {
        Toolbox.GameManager.Analytics_DesignEvent("PrivacyPolicy_Press_No");
        Toolbox.DB.Prefs.UserConsent = false;
        Close();
    }

    #endregion
}
