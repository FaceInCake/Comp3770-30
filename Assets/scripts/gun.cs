using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


/// <summary>
/// Redesign gun script 
/// </summary>
public class gun : NetworkBehaviour
{
    //public float range;
    //public float damage;
    //public int maxAmmo;
    //public int currentAmmo;
    private AudioSource gunfire;
    GameObject cam;
    //public Camera cam;
    public RaycastHit ray;


    GunSystem equippedWeapon;
    TeamManager teamManager;


    private void Start()
    {
        //playerCamera = transform.parent.GetComponentInChildren<Camera>();
        //cam = transform.parent.GetComponentInChildren<Camera>();
        cam = gameObject.transform.GetChild(1).gameObject;
        //currentAmmo = maxAmmo;
        //gunfire = GetComponent<AudioSource>();

        equippedWeapon = gameObject.GetComponentInChildren<GunSystem>();
        teamManager = GameObject.Find("TeamManager").gameObject.GetComponent<TeamManager>();

    }

    public GunSystem getEquipped()
    {
        return equippedWeapon;
    }


    private void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }
        if (!equippedWeapon.isGunEquipped)
        {
            return;
        }

        PlayerInput();

        //equippedWeapon.PlayerInput();
    }


    public void PlayerInput()
    {
        if (equippedWeapon.rapidFire) equippedWeapon.setIsShootingStatus(Input.GetKey(KeyCode.Z));
        else equippedWeapon.setIsShootingStatus(Input.GetKey(KeyCode.Z));


        if (Input.GetKeyDown(KeyCode.Tab) && equippedWeapon.magCount > 0 && equippedWeapon.getCurAmmoCount() < equippedWeapon.magSize && !equippedWeapon.getReloadSatus()) Reload();

        //shooting

        if (equippedWeapon.getCanShootStatus() && equippedWeapon.getIsShootingStatus() && !equippedWeapon.getReloadSatus() && equippedWeapon.getCurAmmoCount() > 0)
        {
            //equippedWeapon.shotsFired = equippedWeapon.bulletsPerShot;
            equippedWeapon.updateCurShotsFired('+', equippedWeapon.bulletsPerShot);
            OnFire();
        }

    }




    public void OnFire()
    {
        //while firing set shoot status to false
        equippedWeapon.setCanShootStatus(false);

        //spread for shotgun/splash/burst type weapons
        float x = Random.Range(-equippedWeapon.spread, equippedWeapon.spread);
        float y = Random.Range(-equippedWeapon.spread, equippedWeapon.spread);

        Vector3 shotDirection = cam.transform.forward + new Vector3(x, y, 0);


        //raycast for shot
        if (Physics.Raycast(cam.transform.position, shotDirection, out ray, equippedWeapon.range))
        {
            Debug.Log(ray.collider.name);
            Alive entity = ray.transform.GetComponentInParent<Alive>();

            //if raycast hits another player
            //if (entity)
            //{
            //    Debug.Log("IS player!!");
            //    int clientID = getPlayerIndex(netId);
            //    int oppID = getPlayerIndex(ray.collider.GetComponent<NetworkIdentity>().netId);
            //    bool oppTeam = teamManager.players[oppID].onRedTeam; //true if red, false if blue
            //    //if the player hit is on a different team
            //    Debug.Log(oppTeam);
            //    if (oppTeam != teamManager.players[clientID].onRedTeam)
            //    {
            //        entity.dealDamage(equippedWeapon.damage);
            //        //ray.collider.GetComponent<Alive>().dealDamage(equippedWeapon.damage);
            //    }
            //}

            Debug.Log(ray.transform.name);

            if (entity)
            {
                Debug.Log("IS player!!");
                int clientID = getPlayerIndex(netId);
                int oppID = getPlayerIndex(ray.transform.GetComponentInParent<NetworkIdentity>().netId);

                bool oppTeam = teamManager.players[oppID].onRedTeam; //true if red, false if blue
                bool clientTeam = teamManager.players[clientID].onRedTeam;

                Debug.Log("clientID: " + clientID + "Client team: " + clientTeam);
                Debug.Log("oppID: " + oppID + "opp team: " + oppTeam);
                Debug.Log("netID: " + netId + "Client netID: " + ray.transform.GetComponentInParent<NetworkIdentity>().netId);

                if (oppTeam != clientTeam)
                {
                    //entity.dealDamage(equippedWeapon.damage);
                    //ray.collider.GetComponent<Alive>().dealDamage(equippedWeapon.damage);

                    CmdDamagePlayer(ray.transform.GetComponentInParent<NetworkIdentity>().netId, equippedWeapon.damage);
                    
                }
            }

        }
        equippedWeapon.updateCurAmmoCount('-', 1);
        equippedWeapon.updateCurShotsFired('-', 1);

        if (!IsInvoking("ResetShot") && !equippedWeapon.getCanShootStatus())
        {
            Invoke("ResetShot", equippedWeapon.fireRate);
        }

        if (equippedWeapon.getCurShotsFired() > 0 && equippedWeapon.getCurAmmoCount() > 0)
        {
            Invoke("Shoot", equippedWeapon.timeBetweenShots);
        }



        // Must have a linked camera and some bullets
        //if (!playerCamera
        //|| currentAmmo<=0) return;

        //currentAmmo--;
        //gunfire.Play(0);
        //RaycastHit hit;
        //if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        //{
        //    // We got a hit, go up the Parents trying to find an Alive component
        //    Transform target = hit.transform;
        //    Alive entity = target.GetComponentInParent<Alive>();
        //    if (entity) {
        //        entity.dealDamage(damage);
        //    }
        //}



    }

    void ResetShot()
    {
        equippedWeapon.setCanShootStatus(true);
    }

    void Reload()
    {
        equippedWeapon.setReloadStatus(true);
        Debug.Log("Reloading");
        Invoke("ReloadFinished", equippedWeapon.reloadTime);
    }

    void ReloadFinished()
    {
        equippedWeapon.setCurAmmoCount(equippedWeapon.magSize);
        //equippedWeapon.maxAmmo -= equippedWeapon.magSize;
        equippedWeapon.magCount--;

        equippedWeapon.setReloadStatus(false);
    }


    int getPlayerIndex(uint id)
    {
        for (int i = 0; i < teamManager.players.Length; i++)
        {
            if (teamManager.players[i].id == id)
            {
                return i;
            }
        }
        return -1;
    }



    public delegate void DamageDelt(uint targetId, int damage);
    public static event DamageDelt OnGunFired;

    static void damageDeltCallback(uint targetId, int damage)
    {
        if (OnGunFired != null)
        {
            OnGunFired(targetId, damage);
        }
    }

    [Command]
    void CmdDamagePlayer(uint id, int damage)
    {
        RpcDamagePlayer(id, damage);
    }

    [ClientRpc]
    void RpcDamagePlayer(uint id, int damage)
    {
        Debug.Log("Dealing damage to id: " + id);
        damageDeltCallback(id, damage);
    }



}
