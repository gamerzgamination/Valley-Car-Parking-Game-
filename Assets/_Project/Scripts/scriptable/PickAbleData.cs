using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickAbleData", menuName = "ScriptableObjects/PickAbleData")]
public class PickAbleData : ScriptableObject
{
    public bool Ammo;
    public bool Healthpack;
    public bool PickGun;
    public List<int> PickGunItem;
    public List<int> HealthItem;
    public List<int> GunAmmoItem;
}
