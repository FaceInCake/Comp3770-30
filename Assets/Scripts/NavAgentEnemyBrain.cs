using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAgentEnemyBrain : MonoBehaviour
{

    GameObject player;
    public float trackingRadius = 5.0f;

    void Start()
    {
        player = GameObject.Find("Player");
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

}
