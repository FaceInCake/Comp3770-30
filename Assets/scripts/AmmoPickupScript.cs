using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupScript : MonoBehaviour
{
    private Quaternion rotSpd ;
    private BoxCollider bcol;
    private MeshRenderer mesh;
    void Start()
    {
        rotSpd = Quaternion.Euler(0, 1, 0);
        bcol = gameObject.GetComponent<BoxCollider>();
        mesh = gameObject.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (bcol.enabled)
            transform.rotation *= rotSpd; 
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            gun g = other.GetComponentInChildren<gun>();
            if (g) {
                g.currentAmmo = g.maxAmmo;
                bcol.enabled = false;
                mesh.enabled = false;
                StartCoroutine(delayedEnable());
            }
        }
    }

    IEnumerator delayedEnable () {
        yield return new WaitForSeconds(10);
        bcol.enabled = true;
        mesh.enabled = true;
    }
}
