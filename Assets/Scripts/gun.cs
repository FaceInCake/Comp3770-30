using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class gun : MonoBehaviour
{
    public float range = 100f;
    public float damage = 10f;
    public int currentAmmo = 10;
    public int magazineSize = 36;


    public Camera playerCamera;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFire()
    {
        Debug.Log("Shots fired!!");
        RaycastHit hit;

        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Alive entity = hit.transform.GetComponent<Alive>();

            if (currentAmmo > 0 && entity != null)
            {
                entity.dealDamage(damage);
                
            }
        }

        if(currentAmmo == 0)
        {
            Debug.Log("No ammo!!!");
        }


        currentAmmo--;


    }
}
