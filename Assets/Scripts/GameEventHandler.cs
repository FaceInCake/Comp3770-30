using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandler : MonoBehaviour
{

    //med kit pick ups
    public int healthStat;
    //enemies killed 
    public int deadEnemies;
    //damage taken by the player
    public float damage;

    public float time;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        GameEvents.current.onHealthPickUp += addHealthStat;
        GameEvents.current.enemiesKilled += addPlayerDamageStat;
        GameEvents.current.playerDamage += addEnemyDamageStat;
        GameEvents.current.timeTaken += addTime;

    }

    private void addHealthStat()
    {
        healthStat++;
    }

    private void addPlayerDamageStat()
    {
        deadEnemies++;
    }

    private void addEnemyDamageStat(float damageTaken)
    {
        damage += damageTaken;
    }

    private void addTime(float timeTaken)
    {
        time += timeTaken;
    }



}
