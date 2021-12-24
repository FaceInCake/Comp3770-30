using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onHealthPickUp;
    public void HealthPickUp()
    {
        if(onHealthPickUp != null)
        {
            onHealthPickUp();
        }
    }

    public event Action<float> playerDamage;
    public void TakeDamage(float damage)
    {
        if(playerDamage != null)
        {
            playerDamage(damage);
        }
    }

    public event Action enemiesKilled;
    public void KilledEnemies()
    {
        if(enemiesKilled != null)
        {
            enemiesKilled();
        }
    }

    public event Action<float> timeTaken;
    public void saveTime(float time)
    {
        if(timeTaken != null )
        {
            timeTaken(time);
        }
    }
}
