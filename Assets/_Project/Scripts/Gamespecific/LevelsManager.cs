using System.Collections.Generic;
using UnityEngine;
public class LevelsManager : MonoBehaviour
{
    [HideInInspector]
    [SerializeField] private LevelsData SelectedLevelData;
    public List<GameObject> Levels;
    private void Start()
    {
        LevelStartHandling();
    }
    private void LevelStartHandling()
    {
        SpawnLevel();
        LevelDataHandling();
    }

    private void SpawnLevel()
    {
        Levels[Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode()].SetActive(true);

    }

    private void LevelDataHandling()
    {
        SelectedLevelData = Resources.Load<LevelsData>(Constants.folderPath_Scriptables + Constants.folderPath_Scriptables_Levels + Toolbox.DB.Prefs.LastSelectedGameMode + "/" + Toolbox.DB.Prefs.Get_LastSelectedLevelOfCurrentGameMode());
        Toolbox.GameplayController.Gametutorial = SelectedLevelData.FirstTimeTutorial;
        if (Toolbox.DB.Prefs.LastSelectedGameMode == 3)
            Toolbox.HUDListner.SetTime(SelectedLevelData.time);

    }

}
