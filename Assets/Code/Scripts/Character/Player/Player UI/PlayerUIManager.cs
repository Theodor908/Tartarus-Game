using System.Collections;
using System.Collections.Generic;
using Tartarus;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    
    public static PlayerUIManager instance;
    [HideInInspector] public PlayerUIHUDManager playerUIHudManager;
    [HideInInspector] public PlayerUIPopUpManager playerUIPopUpManager;
    [HideInInspector] public PlayerUIMenuManager playerUIMenuManager;

    [Header ("UI Flags")]
    public bool menuWindowIsOpen = false; // Inventory, Equipment, etc
    public bool popUpWindowIsOpen = false; // Item pick ups

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        playerUIHudManager = GetComponentInChildren<PlayerUIHUDManager>();
        playerUIPopUpManager = GetComponentInChildren<PlayerUIPopUpManager>();
        playerUIMenuManager = GetComponentInChildren<PlayerUIMenuManager>();

    }

    private void Update()
    {

    }

}
