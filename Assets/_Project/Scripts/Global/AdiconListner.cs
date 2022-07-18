using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdiconListner : MonoBehaviour
{
    private int curricon = 0;
    public float DelayForNext=5f;
    public Image currIconImage;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(Iconchanger());
    }
    void OnDisable()
    {
        StopCoroutine(Iconchanger());
    }
    private IEnumerator Iconchanger()
    {
        while (true)
        {
            Toolbox.GameManager.Permanent_Log("curricon :" + curricon +"total :"+ Toolbox.AdIconHandler.GameLinks.Length);
           // currIconImage.gameObject.GetComponent<UIAnimatorCore.UIAnimator>().PlayAnimation(UIAnimatorCore.AnimSetupType.Intro);
            currIconImage.sprite = Toolbox.AdIconHandler.GameIcon[curricon];
            yield return new WaitForSeconds(DelayForNext);
            if (curricon >= Toolbox.AdIconHandler.GameLinks.Length-1)
                curricon=0;
            else
                curricon++;
        }
        
    }

    public void On_PressAdIcon()
    {
        Application.OpenURL(Toolbox.AdIconHandler.GameLinks[curricon]);
    }
}
