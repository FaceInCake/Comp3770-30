using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

/// <summary>
/// The GunSystem class controls the weapon info for each weapon in the game
/// This way we can have one script with several different weapon types
/// </summary>
public class GunSystem : NetworkBehaviour
{


    //Gun system class to use on different guns/ gun types


    //Gun stats
    public int damage; //damage for each shot from the weapon

    public float fireRate; //rate at which each shot is fired for rapid fire weapons in seconds

    public float spread; //bullet spread or how far away form the muzzle the bullet travels /recoil?

    public float range; //How far the weapon can shoot

    public float timeBetweenShots; //for rapid fire weapons, the time between each shot fired in seconds

    public float reloadTime; //reload time for weapon in seconds 

    public int magSize; //total magazine size for the weapon, differs from the total amount of ammo a player can carry for each weapon
    //if possible the "ammo" can be attached to the player instead of the gun
    //in that case the attached "gun" would just use the ammo attached to the player once the gun is equipped
    //The change would involve the gun having a "gun-type" option

    public int magCount; //the number of magazines allowed to carry for each weapon

    public int bulletsPerShot; //How many bullets are used up per shot

    public bool rapidFire; //Can a player hold down trigger for a continuos shot or not//semi or full automatic 

    int ammoCount; //current ammo size left per magazine
    int shotsFired; //number of bullets used from the magazine

    int maxAmmo; //the max amount of ammo the "gun/player" is allowed to carry
    //maxAmmo here would be the number of magazines allowed for the weapon multiplied by the size of each magazine

    public string WeaponName; //name of the gun 
    public bool isGunEquipped; //is the gun currently equipped to a player?

    
    public bool isInf = false; //weapons like the fist and knife should have an infinte use 
    //bool gun actions
    bool isShooting, isReloading, canShoot;

    public Camera cam;
    public RaycastHit ray;


    // Start is called before the first frame update
    void Start()
    {
        ammoCount = magSize;
        maxAmmo = magCount * magSize; //
        canShoot = true;
    }

    public int getCurAmmoCount()
    {
        return ammoCount;
    }

    public void updateCurAmmoCount(char sign, int update)
    {
        if (sign == '-')
        {
            ammoCount -= update;
        }
        else
        {
            ammoCount += update;
        }
    }

    public void setCurAmmoCount(int update)
    {
        ammoCount = update;
    }

    public int getCurShotsFired()
    {
        return shotsFired;
    }


    public void updateCurShotsFired(char sign, int update)
    {
        if (sign == '-')
        {
            shotsFired -= update;
        }
        else
        {
            shotsFired += update;
        }
    }


    public bool getReloadSatus()
    {
        return isReloading;
    }

    public void setReloadStatus(bool check)
    {
        isReloading = check;
    }

    public bool getCanShootStatus()
    {
        return canShoot;
    }

    public void setCanShootStatus(bool check)
    {
        canShoot = check;
    }

    public bool getIsShootingStatus()
    {
        return isShooting;
    }

    public void setIsShootingStatus(bool check)
    {
        isShooting = check;
    }

    public int getCurMaxAmmo()
    {
        return magCount * magSize;
    }




    // Update is called once per frame
    void Update()
    {
        if (isGunEquipped)
        {
            PlayerInput();
        }
    }

    void PlayerInput()
    {
        if(rapidFire) isShooting = Input.GetKey(KeyCode.Z);
        else isShooting = Input.GetKeyDown(KeyCode.Z);

        //if ammo count reaches 0 and the player isnt already realoding reload
        if (ammoCount == 0 && !isReloading || Input.GetKeyDown(KeyCode.Tab) && ammoCount < magSize && !isReloading) Reload();

        //shooting

        if(canShoot && isShooting && !isReloading && ammoCount > 0)
        {
            shotsFired = bulletsPerShot;
            Shoot();
        }

    }

    public int getAmmo()
    {
        return ammoCount;
    }

    void Shoot()
    {
        canShoot = false;

        //spread for shotgun/splash/burst type weapons
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 shotDirection = cam.transform.position + new Vector3(x, y, 0);


        //raycast for shot
        if(Physics.Raycast(cam.transform.position,  shotDirection , out ray, range))
        {
            Debug.Log(ray.collider.name);

            //if raycast hits another player
            if(ray.collider.CompareTag("Player"))
            {
                //if the player hit is on a different team
                //if(ray.collider.GetComponent<PlayerInfo>().onRedTeam != gameObject.GetComponent<PlayerInfo>().onRedTeam)
                //{
                //    ray.collider.GetComponent<Alive>().dealDamage(damage); 
                //}

            }

        }

        //if the weapon doesnt have infite use 
        if(!isInf)
        {
            ammoCount--;
            shotsFired--;

            if (!IsInvoking("ResetShot") && !canShoot)
            {
                Invoke("ResetShot", fireRate);
            }

            if (shotsFired > 0 && ammoCount > 0)
            {
                Invoke("Shoot", timeBetweenShots);
            }
        }

    }

    void ResetShot()
    {
        canShoot = true;
    }



    void Reload()
    {
        isReloading = true;
        Debug.Log("Realoding");
        Invoke("ReloadFinished", reloadTime);
    }

    void ReloadFinished()
    {
        ammoCount = magSize;
        maxAmmo -= magSize;
        isReloading = false;
    }


}
