using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/Enemy")]
public class EnemyData : ScriptableObject
{
    public float EnemyHealth;
    public float enemyBulletDamage;
    public float MaxDistanceEnemyspawn;
    public float MinDistanceEnemyspawn;
}
