using System.Collections.Generic;
using UnityEngine;

public static class ReviveController
{
    private static Vector3[] LastPos;
    private static List<GameObject> SpawnedEnemy = new List<GameObject>();
    private static GameObject hassam;


    public static void Set_PlayerHealth()
    {
      //  Camera.main.GetComponent<CameraControl>().playerObj.GetComponent<FPSPlayer>().hitPoints = Toolbox.ObjectiveHandler.SelectedLevelData.PlayerHealth;
        //Camera.main.GetComponent<CameraControl>().playerObj.GetComponent<FPSPlayer>().HealPlayer(0);
        Toolbox.GameplayController.RevivePlayer();
        Time.timeScale = 1.0f;
    }

    public static void Revive_PlayerOnCoins()
    {
      
            Toolbox.DB.Prefs.GoldCoins -= Constants.reviveCoinsCost;
            //Camera.main.GetComponent<CameraControl>().playerObj.GetComponent<FPSPlayer>().hitPoints = Toolbox.ObjectiveHandler.SelectedLevelData.PlayerHealth;
            //Camera.main.GetComponent<CameraControl>().playerObj.GetComponent<FPSPlayer>().HealPlayer(0);
            Toolbox.GameplayController.RevivePlayer();
    }
}
