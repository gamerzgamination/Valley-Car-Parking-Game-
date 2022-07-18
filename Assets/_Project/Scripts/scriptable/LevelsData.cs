using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/Level")]
public class LevelsData : ScriptableObject
{
    public int time =300;
    public bool FirstTimeTutorial = false; 
    //public int lives = 3;

}

