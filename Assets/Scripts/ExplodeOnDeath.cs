using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnDeath : MonoBehaviour
{
    private AudioSource boom;
    private Rigidbody [] rigids;
    void Start()
    {
        rigids = GetComponentsInChildren<Rigidbody>();
        Alive.OnDeath += objDies;
        boom = GetComponent<AudioSource>();
    }

    void objDies (GameObject obj) {
        if (obj == gameObject) {
            // Make our parts with rigid bodies start flying
            foreach (Rigidbody rb in rigids)
                rb.isKinematic = false;
            // Unhook from event
            Alive.OnDeath -= objDies;
            // Disable muzzle flash, incase it's on when it dies
            transform.Find("Head/MuzzleFlash").gameObject.SetActive(false);
            // Turn off the turret
            gameObject.GetComponent<TurretBrain>().enabled = false;
            // Play the epic sound
            boom.Play(0);
            // Clean up the mess in 5 seconds
            Object.Destroy(gameObject, 5);
        }
    }

}
