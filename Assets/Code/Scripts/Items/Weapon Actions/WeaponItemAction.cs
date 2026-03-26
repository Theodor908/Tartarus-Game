using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    [CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Test Action")]
    public class WeaponItemAction : ScriptableObject
    {
        public int actionID;

        public virtual void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            //Need to keep track of the weapon that is performing the action
            playerPerformingAction.currentWeaponBeingUsed = weaponPerformingAction.itemID;
        
        }
    }
}