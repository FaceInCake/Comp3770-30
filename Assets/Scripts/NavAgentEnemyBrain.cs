using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAgentEnemyBrain : MonoBehaviour
{

    GameObject player;
    Alive php; // Player health points
    AudioSource sound_atk;
    public float trackingRadius;
    public float damage;
    private float distToPlayer2; // Squared distance to player, used by private functions

    void Start()
    {
        player = GameObject.Find("Player");
        php = player.GetComponent<Alive>();
        sound_atk = gameObject.GetComponent<AudioSource>();
        InvokeRepeating("attemptToAttackPlayer", Random.Range(0f,1f), 1.1f);
    }

    int frame = 0;
    void Update()
    {
        // Must calculate before `trackPlayer()` and `attemptToAttackPlayer()`
        distToPlayer2 = (transform.position - player.transform.position).sqrMagnitude;

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
        }

    }

    void attemptToAttackPlayer () {
        if (distToPlayer2 < 4) { // Less than 2 units away
            php.dealDamage(damage);
            sound_atk.PlayOneShot(sound_atk.clip);
        }
    }

    void setupNavMeshAgentComponent()
    {
        gameObject.AddComponent(typeof(UnityEngine.AI.NavMeshAgent));
        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
    }


    void trackPlayer()
    {
        if (distToPlayer2 < (trackingRadius * trackingRadius))
        {
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(player.transform.position);
        }
        else
        {
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(gameObject.transform.position);
        }

    }

}
