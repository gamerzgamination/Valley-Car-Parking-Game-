using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "ScriptableObjects/GunSelection_Gun")]
public class GunSelection_Gun : ScriptableObject
{
    public new string name = "";

    public int cost = 100;
    [Range(0, 1)]
    public float speed = 1;
    [Range(0, 1)]
    public float  handling  = 1;
    [Range(0, 1)]
    public float Aceleration = 1;
   
}

