using UnityEngine;

public class LowCoinVehicleBuy : MonoBehaviour
{
    private int curVehicle = 0;

    public int CurVehicle { get => curVehicle; set => curVehicle = value; }

    #region ButtonListners

    public void OnPress_Close()
    {
        Toolbox.Soundmanager.PlaySound(Toolbox.Soundmanager.GameUIclicks);
        this.gameObject.SetActive(false);
       // Destroy(this.gameObject);
    }

    //public void OnPress_Unlock ()
    //{
    //    if (curVehicle < 3)
    //    {
    //       InAppHandler.Instance.Buy_Coins20000();
    //    }
    //    else if (curVehicle < 6)
    //    {
    //        InAppHandler.Instance.Buy_Coins30000();
    //    }
    //    else { 
    //    //    Toolbox.InAppHandler.Buy_Coins40000();
    //    }

    //    OnPress_Close();
    //}

    public void OnPress_UnlockAll()
    {
        //InAppHandler.Instance.Buy_AllGuns();
        Toolbox.UIManager.Shop_Panel.SetActive(true);
        //Toolbox.GameManager.InstantiateUI_Shop();
        OnPress_Close();
    }

    #endregion
}
