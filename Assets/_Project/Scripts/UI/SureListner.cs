using UnityEngine;

public class SureListner : MonoBehaviour
{
    private void OnEnable()
    {
     //   Toolbox.GameManager.Add_ActiveUI(this.gameObject);
    }

    private void OnDisable()
    {
       // Toolbox.GameManager.Remove_ActiveUI(this.gameObject);
    }
    public void OnPress_Yes (){

        Toolbox.GameManager.Analytics_DesignEvent("Sure_Press_Yes");
        //Toolbox.GameManager.InstantiateUI_PrivacyPolicy();
        Toolbox.UIManager.PrivacyPolicy.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OnPress_No()
    {
        Toolbox.GameManager.Analytics_DesignEvent("Sure_Press_No");
        this.gameObject.SetActive(false);
    }
}
