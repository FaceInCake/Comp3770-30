using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class gun : MonoBehaviour
{
    public float range;
    public float damage;
    public int maxAmmo;
    public int currentAmmo;
    private Camera playerCamera;

    private void Start() {
        playerCamera = transform.parent.GetComponentInChildren<Camera>();
        currentAmmo = maxAmmo;
    // Update is called once per frame
    }

    public void OnFire()
    {
        // Must have a linked camera and some bullets
        if (!playerCamera
        || currentAmmo<=0) return;

        currentAmmo--;
        RaycastHit hit;
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            // We got a hit, go up the Parents trying to find an Alive component
            Transform target = hit.transform;
            Alive entity = target.GetComponentInParent<Alive>();
            if (entity) {
                entity.dealDamage(damage);
            }
        }

    }
}
