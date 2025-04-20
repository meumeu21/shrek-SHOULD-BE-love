using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public Vector3 enemyPosition;
    public int enemyHealth;
    public bool isDead;
    public EnemyData(Vector3 pos, int health, bool isDead)
    {
        enemyPosition = pos;
        enemyHealth = health;
        this.isDead = isDead;
    }
}