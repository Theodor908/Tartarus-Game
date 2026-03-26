using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class WeaponPickUp : Interactable
    {
        
        public WeaponItem weaponItem; // The weapon item that will be picked up
        public bool isRightHand; // Is the weapon item for the right hand?
        public bool isLeftHand; // Is the weapon item for the left hand?
        
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            PickUpWeapon(playerManager);
        }

        private void PickUpWeapon(PlayerManager playerManager)
        {
          
            playerManager.playerInventoryManager.AddWeaponToInventory(weaponItem);
            Destroy(gameObject);

        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
        }

    }
}