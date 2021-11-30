using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alive : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float currentHealth;


    public delegate void Died(GameObject entity);
    public static event Died OnDeath;

    static void entityHasDiedCallback(GameObject entity)
    {
        //Object.Destroy(entity);
        if (OnDeath != null)
        {
            OnDeath(entity);
        }
    }


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void dealDamage(float damage)
    {
        if (damage < 0) return; // prevents healing through this function

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if(gameObject != GameObject.Find("Player"))
            {
                //GameEvents.current.KilledEnemies();
            }
            entityHasDiedCallback(gameObject);
        }
    }

    public void heal(float amount)
    {
        if (amount < 0) return; // prevents inflicting damage through this function

        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void setMaxHealth(float max)
    {
        if (max <= 0) return;

        maxHealth = max;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void setHealth(float health)
    {
        currentHealth = health;

        if (currentHealth < 0)
        {
            entityHasDiedCallback(gameObject);
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

}
