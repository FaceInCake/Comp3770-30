using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutSelect : MonoBehaviour
{

    public static bool isLoadoutSelect = false;
    //public CharacterCamera playerCam;
    public MovePlayer playerController;

    public GameObject loadoutSelectUI;
    public GameObject loadoutManager;
    Text primary;
    Text secondary;
    gun player;


    // Start is called before the first frame update
    void Start()
    {
        loadoutSelectUI.SetActive(false);
        loadoutManager = gameObject.transform.parent.Find("Loadout").gameObject;
        player = gameObject.GetComponentInParent<gun>();
        primary = loadoutSelectUI.transform.Find("PrimaryInfo").GetComponent<Text>();
        secondary = loadoutSelectUI.transform.Find("SecondaryInfo").GetComponent<Text>();

    }
    // Update is called once per frame
    void Update()
    {

        //change this to when the player spwans/respawns
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PanelToggle();
        }
    }

    public void PanelToggle()
    {
        

        isLoadoutSelect = !isLoadoutSelect;
        loadoutSelectUI.SetActive(isLoadoutSelect);
        if (isLoadoutSelect)
        {

            //playerCam.enabled = false;
            playerController.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            //playerCam.enabled = true;
            playerController.enabled = true;
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
        
    }


    //fill out weapond details
    public void loadLoadoutInfo(int i)
    {
        //player.loadOutSelect(i);
        GameObject selectedWeapon = loadoutManager.transform.GetChild(i).gameObject;


        primary.text = "Primary Weapon\n" + selectedWeapon.transform.GetChild(0).gameObject.GetComponent<GunSystem>().WeaponName;
        secondary.text = "Secondary Weapon\n" +  selectedWeapon.transform.GetChild(1).gameObject.GetComponent<GunSystem>().WeaponName;

    }


    //void SelectLoadout()
    //{
    //    loadoutSelectUI.SetActive(true);
    //    isLoadoutSelect = true;
    //}
}
