using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAgentEnemyBrain : MonoBehaviour
{

    GameObject player;
    public float trackingRadius = 5.0f;
    public float attackRange = 1.5f;
    public float attackDamage = 20;
    public float attackFrequencyInSeconds = 1.0f;

    Alive life;

    bool dieNextFrame = false;

    void Start()
    {
        player = GameObject.Find("Player");

        life = gameObject.GetComponent<Alive>();
        Alive.OnDeath += enemyDeath;
    }

    int frame = 0;
    void Update()
    {

        if (frame >= 0)
        {
            frame++;

            if (frame > 4)
            {
                setupNavMeshAgentComponent();

                frame = -1;
            }
        }

        if (frame == -1)
        {
            trackPlayer();
            attackPlayer();
            getAttackedByPlayer();
        }

        if (dieNextFrame)
        {
            Destroy(gameObject);
        }
    }

    void setupNavMeshAgentComponent()
    {
        gameObject.AddComponent(typeof(UnityEngine.AI.NavMeshAgent));
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
    }


    void trackPlayer()
    {
        float dx = player.transform.position.x - gameObject.transform.position.x;
        float dy = player.transform.position.z - gameObject.transform.position.z;
        if ((dx * dx) + (dy * dy) < (trackingRadius * trackingRadius))
        {
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(player.transform.position);
        }
        else
        {
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(gameObject.transform.position);
        }

    }


    float getAttackedTimer = 0.0f;
    float getAttackedFrequency = 0.7f;
    float playerDamage = 35;
    float playerAttackRange = 2.0f;
    void getAttackedByPlayer()
    {

        if (getAttackedTimer < getAttackedFrequency)
        {
            getAttackedTimer += Time.deltaTime;
        } else
        {

            float dx = player.transform.position.x - gameObject.transform.position.x;
            float dy = player.transform.position.z - gameObject.transform.position.z;
            if ((dx * dx) + (dy * dy) < (playerAttackRange * playerAttackRange))
            {
                life.dealDamage(playerDamage);
                getAttackedTimer = 0.0f;
            }

        }
    }

    float attackTimer = 0.0f;
    void attackPlayer()
    {
        if (attackTimer < attackFrequencyInSeconds)
        {
            attackTimer += Time.deltaTime;
        }
        else
        {

            float dx = player.transform.position.x - gameObject.transform.position.x;
            float dy = player.transform.position.z - gameObject.transform.position.z;
            if ((dx * dx) + (dy * dy) < (attackRange * attackRange))
            {
                player.GetComponent<Alive>().dealDamage(attackDamage);
            }

            attackTimer = 0.0f;

        }

    }


    private void OnDisable()
    {
        Alive.OnDeath -= enemyDeath;
    }
    
    void enemyDeath(GameObject entity)
    {
        if (gameObject == entity)
        {
            dieNextFrame = true;
        }
    }

}
