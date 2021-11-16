using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alive : MonoBehaviour
{

    public float initialMaxHealth;
    private float maxHealth;
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
        if (initialMaxHealth <= 0)
        {
            entityHasDiedCallback(gameObject);
        }

        maxHealth = initialMaxHealth; // maxHealth is seperate from initialMaxHealth to privatize the maxHealth member. Otherwise currentHealth would have to be compared in Update()
        currentHealth = maxHealth;
    }

    public void dealDamage(float damage)
    {
        if (damage < 0) return; // prevents healing through this function

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
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

}
