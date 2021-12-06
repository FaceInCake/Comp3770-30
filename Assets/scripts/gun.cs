using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public float range;
    public float damage;
    public int maxAmmo;
    public int currentAmmo;
    private AudioSource gunfire;
    private Camera playerCamera;

    private void Start() {
        playerCamera = transform.parent.GetComponentInChildren<Camera>();
        currentAmmo = maxAmmo;
        gunfire = GetComponent<AudioSource>();
    }

    public void OnFire()
    {
        // Must have a linked camera and some bullets
        if (!playerCamera
        || currentAmmo<=0) return;

        currentAmmo--;
        gunfire.Play(0);
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
