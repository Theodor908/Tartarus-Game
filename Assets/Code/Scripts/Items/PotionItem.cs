using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{

    [CreateAssetMenu(menuName = "Items/Potions/Potion Item")]
    public class PotionItem : Item
    {

        public float mitigatedQuantitiy = 20;
        public float maxNumberOfUses = 4;
        public float numberOfUses = 4;
        public GameObject potionModel;

        public PotionItemAction potionItemAction;

    }

}
