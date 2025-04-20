using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int PlayerMaxHP;
    public int PlayerHP;
    public int PlayerMaxMana;
    public int PlayerMana;
    public float PlayerHPBarWidth;
    public float PlayerManaBarWidth;

    public Vector3 PlayerPosition;
    public SerializableDictionary<string, bool> potionsCollected;
    public SerializableDictionary<int, EnemyData> EnemyStatistics;

    public GameData()
    {
        this.PlayerMaxHP = 20;
        this.PlayerHP = 20;
        this.PlayerMaxMana = 15;
        this.PlayerMana = 15;
        this.PlayerHPBarWidth = 100;
        this.PlayerManaBarWidth = 75;

        PlayerPosition = new Vector3(-19.72f, 3.26f, -21.3f);

        potionsCollected = new SerializableDictionary<string, bool>();

        EnemyStatistics = new SerializableDictionary<int, EnemyData>();
    }
}
