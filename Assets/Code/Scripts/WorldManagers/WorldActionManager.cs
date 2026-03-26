using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tartarus
{
    public class WorldActionManager : MonoBehaviour
    {
        public static WorldActionManager instance;

        [Header ("Weapon item actions")]
        public SpellItemAction[] spellItemActions;
        public WeaponItemAction[] weaponItemActions;
        public PotionItemAction[] potionItemActions;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {

            for (int i = 0; i < spellItemActions.Length; i++)
            {
                spellItemActions[i].actionID = i;
            }

            for (int i = 0; i < weaponItemActions.Length; i++)
            {
                weaponItemActions[i].actionID = i;
            }

            for(int i = 0; i < potionItemActions.Length; i++)
            {
                potionItemActions[i].actionID = i;
            }

        }

        public SpellItemAction GetSpellItemAction(int actionID)
        {
            return spellItemActions.FirstOrDefault(x => x.actionID == actionID);
        }

        public WeaponItemAction GetWeaponItemAction(int actionID)
        {
            return weaponItemActions.FirstOrDefault(x => x.actionID == actionID);
        }
        public PotionItemAction GetPotionItemAction(int actionID)
        {
            return potionItemActions.FirstOrDefault(x => x.actionID == actionID);
        }

    }
}
