using UnityEngine;
using UnityEngine.UI;

public class LevelButtonListner : MonoBehaviour
{

    public Text levelNoTxt;
    //public Text levelStatusTxt;
    //public Text levelNewnoTxt;
    //public Text levelNewStatusTxt;
    public GameObject buttonObj;
    //public GameObject watchVideoUnlockBtn;
    public GameObject lockObj;
    //public GameObject PlayedState;
    //public GameObject NewLevel;
  //public GameObject[] stars;

    //public void Stars_Status(bool _val, int _enabledStars)
    //{
    //    starsParent.SetActive(_val);

    //    if (_val) {

    //        for (int i = 0; i < _enabledStars; i++)
    //        {
    //            stars[i].SetActive(true);
    //        }
    //    }
    //}

    public void Lock_Status(bool _val) {

        lockObj.SetActive(_val);
    }

    //public void check_LevelState(bool _Val)
    //{
    //    PlayedState.SetActive(_Val);
    //}
    public void Set_LevleNameTxt(string _val)
    {
        levelNoTxt.text = _val;
        //levelNewnoTxt.text = _val;
    }
    //public void Set_LevelstatusTxt(string _val)
    //{
    //    levelStatusTxt.text = _val;
       
    //}

    public void check_OutlineStatus(bool _val)
    {
      //  Outline.SetActive(_val);
    }

    //public void Set_NewLevelstatus(bool _Val)
    //{
    //    NewLevel.SetActive(_Val);
    //}

    //public void WatchVideoUnlock_Status(bool _val) {

    //    watchVideoUnlockBtn.SetActive(_val);
    //}

    #region ButtonListners

    public void OnPress_LevelButton(GameObject _buttonTransform)
    {
        //Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.G);
        this.GetComponentInParent<LevelSelectionListner>().OnPress_LevelButton(_buttonTransform);

    }

    public void OnPress_LevelLockButton(GameObject _buttonTransform)
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.UIManager.ModeLockPopup.SetActive(true);
        Toolbox.UIManager.ModeLockPopup.GetComponent<MessageListner>().UpdateTxt("Level is locked.", "LOCKED");
     //  Toolbox.GameManager.Instantiate_ModeLockedMessage("Level is locked.", "LOCKED");
    }

    #endregion

}
