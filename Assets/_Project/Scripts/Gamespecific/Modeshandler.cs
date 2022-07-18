using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modeshandler : MonoBehaviour
{

    public List<GameObject> Prefabs;
    //public Material[] skyboxes;
    private int i;
    private void Start()
    {

        if ((Toolbox.DB.Prefs.LastSelectedGameMode == 11 || Toolbox.DB.Prefs.LastSelectedGameMode == 1 || Toolbox.DB.Prefs.LastSelectedGameMode == 2 || Toolbox.DB.Prefs.LastSelectedGameMode == 3))
        {
            //fOR jUST TRAINING mODE o for those modes which haven't any Day and Night mode
            //i = Random.Range(0, 2);
            foreach (GameObject g in Prefabs)
            {
                if (g)
                    g.SetActive(false);
            }
            if (Prefabs[0])
                Prefabs[0].SetActive(true);
        }
        else 
        {
                //switch (Toolbox.ObjectiveHandler.SelectedLevelData.ModeType)
                //{
                //    case LevelsData.modetype.Random:
                //        i = Random.Range(0, 2);
                //        break;
                //    case LevelsData.modetype.Day:
                //        i = 0;
                //        break;
                //    case LevelsData.modetype.Night:
                //        i = 1;
                //        break;
                //}
                //foreach (GameObject g in Prefabs)
                //{
                //    if (g)
                //        g.SetActive(false);
                //}
                //if (Prefabs[i])
                //    Prefabs[i].SetActive(true);
        }

       
    }
}