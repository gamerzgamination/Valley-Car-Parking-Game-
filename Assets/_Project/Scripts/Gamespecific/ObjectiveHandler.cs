using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class ObjectiveHandler : MonoBehaviour
{
    public static ObjectiveHandler Instance;
   
   
    private int level;
    private int mode;
    private  GameObject levelprefab;

    private LevelsData selectedleveldata;
  
    public int Mode { get => mode; set => mode = value; }
    public int Level { get => level; set => level = value; }
    public LevelsData SelectedLevelData { get => selectedleveldata; set => selectedleveldata = value; }
    public GameObject Levelprefab { get => levelprefab; set => levelprefab = value; }

    public void Start()
    {
        Instance = this;
        Toolbox.Set_objectivehandler(this);
        level = Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode();
        mode = Toolbox.DB.Prefs.LastSelectedGameMode;
        init();
    }

    [HideInInspector]
    public int waveEnemyKilled;
    string statement;
    
    public void init()
    {
        LevelDataHandling();
        SpawnLevel();
    }
   
    private void SpawnLevel()
    {
        levelprefab = Resources.Load<GameObject>(Constants.folderPath_Prefabs + Constants.folderPath_Prefabs_Levels_Mode + mode + "/" + level);
        Instantiate(levelprefab, Vector3.zero, Quaternion.identity, this.transform);
    }

    private void LevelDataHandling()
    {

        SelectedLevelData = Resources.Load<LevelsData>(Constants.folderPath_Scriptables + Constants.folderPath_Scriptables_Levels + mode + "/" + level);

        //Toolbox.HUDListner.SetTime(levelData.time);
        //Toolbox.HUDListner.SetTotalLives(levelData.lives);
        //Toolbox.GameplayController.Lives = levelData.lives;
    }
    public void UnloadAssetsFromMemory()
    {
        Resources.UnloadAsset(SelectedLevelData);
    }
}