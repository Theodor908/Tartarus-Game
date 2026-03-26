using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class CharacterUIManager : MonoBehaviour
    {
        [Header("UI")]
        public bool hasFloatingHealthBar = true;
        public UI_Character_HP_Bar characterHPBar;

        public void OnHpChanged(float oldValue, float newValue)
        {
            characterHPBar.SetStatus(oldValue - newValue);
        }
    }
}