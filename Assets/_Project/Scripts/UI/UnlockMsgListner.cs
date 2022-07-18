//using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class UnlockMsgListner : MonoBehaviour
{
    public Image img;
    public Sprite [] productImg;

    private void OnEnable()
    {
      //  Toolbox.GameManager.Add_ActiveUI(this.gameObject);
    }

    private void OnDisable()
    {
       // Toolbox.GameManager.Remove_ActiveUI(this.gameObject);
    }
    public void EnableProduct(int _val)
    {

        img.sprite = productImg[_val];

    }

    public void OnPress_Okay()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
    //    Toolbox.AdsManager.Show_BAd(AdSize.Banner,AdPosition.TopLeft);
        Destroy(this.gameObject);
    }

}
