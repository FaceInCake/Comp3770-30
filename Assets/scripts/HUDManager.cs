using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class HUDManager : NetworkBehaviour
{
    public bool isRed;

    public Slider healthBar;
    Alive playerHealth;
    GunSystem equippedWeapon;
    gun loadout;
    GameObject player;
    //public GameObject rightContainer;

    GameObject HUD;

    TeamManager teamManager;



    private void Start()
    {
        //healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();

        //HUD = GameObject.Find("HUD");
        //rightContainer = HUD.transform.Find("ContainerRight");

        //Debug.Log(HUD);
        //player = GameObject.Find("Player");

        //playerHealth = gameObject.transform.parent.GetComponent<Alive>();

        HUD = gameObject.transform.Find("HUD").gameObject;

        teamManager = GameObject.Find("TeamManager").gameObject.GetComponent<TeamManager>();

        playerHealth = gameObject.GetComponent<Alive>();

        //equippedWeapon = gameObject.transform.parent.GetComponentInChildren<GunSystem>();

        loadout = gameObject.GetComponent<gun>();
        equippedWeapon = loadout.getEquipped();
        //equippedWeapon = gameObject.GetComponentInChildren<GunSystem>();

        Debug.Log(equippedWeapon.WeaponName);




    }

    private void Update()
    {

        if (!isLocalPlayer)
            return;

        UpdateHealthBar();
        equippedWeapon = loadout.getEquipped();
        UpdateGunInfoBar(HUD.transform.Find("ContainerRight").gameObject);
        UpdateTeamFlag();
    }

    void UpdateHealthBar()
    {
        healthBar.value = playerHealth.getCurrentHealth();
        //healthBar.GetComponent<Text>().text = playerHealth.getCurrentHealth().ToString();

        healthBar.GetComponentInChildren<Text>().text = playerHealth.getCurrentHealth().ToString();
    }

    void UpdateGunInfoBar(GameObject rightContainer)
    {

        rightContainer.transform.Find("AmmoCurrent").GetComponent<Text>().text = equippedWeapon.getAmmo().ToString();
        rightContainer.transform.Find("WeaponName").GetComponent<Text>().text = equippedWeapon.WeaponName;
        rightContainer.transform.Find("AmmoMax").GetComponent<Text>().text = equippedWeapon.getCurMaxAmmo().ToString();

    }

    void UpdateTeamFlag()
    {

        //isRed = gameObject.transform.parent.GetComponent<PlayerBrain>().getPlayerTeam();
        int i = getPlayerIndex(netId);
        //isRed = gameObject.GetComponent<PlayerBrain>().getPlayerTeam();

        bool isOnRed = teamManager.players[i].onRedTeam;
        //gameObject.transform.parent.GetComponent<PlayerBrain>.get

        if (isOnRed)
        {
            healthBar.transform.Find("InnerBorder").transform.Find("Center").GetComponent<Image>().color = Color.red;
        }
        else
        {
            healthBar.transform.Find("InnerBorder").transform.Find("Center").GetComponent<Image>().color = Color.blue;

        }

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
}
