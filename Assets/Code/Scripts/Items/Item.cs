using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class Item : ScriptableObject
    {
        [Header("Item Info")]
        public string itemName;
        [TextArea] public string itemDescription;
        public Sprite itemIcon;
        public int itemID;
    }
}
