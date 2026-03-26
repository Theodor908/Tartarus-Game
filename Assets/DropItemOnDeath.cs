using System.Collections;
using System.Collections.Generic;
using Tartarus;
using UnityEngine;

public class DropItemOnDeath : MonoBehaviour
{
    [SerializeField] Item itemToDrop;
    private CharacterManager characterManager;
    private PlayerManager playerManager;
    private bool itemDropped = false;

    public bool dropWeapon = false;
    public bool dropSpell = false;

    private void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        if(characterManager.currentHealth <= 0 && itemDropped == false)
        {
            if(dropWeapon)
            {
                DropWeapon();
            }
            if(dropSpell)
            {
                DropSpell();
            }
            itemDropped = true;
        }
    }

    private void DropWeapon()
    {
        playerManager.playerInventoryManager.AddWeaponToInventory(itemToDrop as WeaponItem);
    }

    private void DropSpell()
    {
        playerManager.playerInventoryManager.AddSpellToInventory(itemToDrop as SpellItem);
    }
}
