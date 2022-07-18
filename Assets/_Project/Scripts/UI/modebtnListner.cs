using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class modebtnListner : MonoBehaviour
{
    public GameObject lockObj;
   
    public void Lock_Status(bool _val)
    {

        lockObj.SetActive(_val);
    }
    #region ButtonListners

    public void OnPress_chapterButton(GameObject _buttonTransform)
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        this.GetComponentInParent<ModeSelectionListner>().OnPress_ModeButton(_buttonTransform);
    }

    public void OnPress_ChapterLockButton(int index)
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        if(Toolbox.DB.Prefs.GameData[Toolbox.DB.Prefs.LastSelectedGameMode].Modeunlocked)
        {
            this.GetComponentInParent<ModeSelectionListner>().OnPress_UnlockAllChapter();
        }
        else
        {
            Toolbox.UIManager.ModeLockPopup.SetActive(true);
            Toolbox.UIManager.ModeLockPopup.GetComponent<MessageListner>().UpdateTxt("This chapter is currently locked. Play atleast " + (Constants.mode2UnlockAfterLevels + 1) + " levels of current chapter to unlock the glory of this chapter", "LOCKED");
        }
        }

    public void OnPress_ChapterLock_ComingSoon()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        Toolbox.UIManager.ModeLockPopup.SetActive(true);
        Toolbox.UIManager.ModeLockPopup.GetComponent<MessageListner>().UpdateTxt("This chapter is currently locked. Coming Soon" , "LOCKED");
    }
    #endregion

}
