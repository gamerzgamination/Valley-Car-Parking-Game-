
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public Transform vehicleSpawnPoint;
    //public GameObject EndCamera;

    private void Start()
    {
        LevelStartHandling();
    }
    public void LevelStartHandling()
    {
        SpawnVehicle();
        //if (EndCamera)
        //    Toolbox.GameplayController.Endcamera = EndCamera;
    }
    private void SpawnVehicle()
    {
        //print("path :" + Constants.folderPath_Prefabs + Constants.folderPath_Prefabs_PlayerVehicles + "/" + Toolbox.DB.Prefs.LastSelectedVehicle);
        //Toolbox.GameplayController.SelectedVehiclePrefab = Resources.Load<GameObject>(Constants.folderPath_Prefabs + Constants.folderPath_Prefabs_PlayerVehicles + Toolbox.DB.Prefs.LastSelectedVehicle);
        Toolbox.GameplayController.Vehiclespawnpoint = vehicleSpawnPoint;
        
        Toolbox.GameplayController.spawnvehicle();
    }

}
