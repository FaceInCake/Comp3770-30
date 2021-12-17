using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPadTrigger : MonoBehaviour
{
    public float speedBoost;
    public float boostTime;
    
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            c.transform.gameObject.GetComponent<MovePlayer>().applySpeedModifier(speedBoost, boostTime);
        }
    }

}
