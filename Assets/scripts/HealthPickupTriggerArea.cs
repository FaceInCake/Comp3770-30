using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupTriggerArea : MonoBehaviour
{
    HealthPickupBrain healthPickupBrain;

    void Start()
    {
        healthPickupBrain = transform.parent.gameObject.GetComponent<HealthPickupBrain>();
    }


    void OnTriggerEnter(Collider c)
    {
        healthPickupBrain.handleTriggerEnter(c);
    }

}
