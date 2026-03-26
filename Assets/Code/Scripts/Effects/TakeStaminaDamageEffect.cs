using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Stamina Damage")]
    public class TakeStaminaDamageEffect : InstantCharacterEffect
    {
        public float staminaDamage;
        public override void ProcessEffect(CharacterManager characterManager)
        {
            CalculateStaminaDamage(characterManager);
        }

        private void CalculateStaminaDamage(CharacterManager characterManager)
        {
            // Keep track of applied effects and change the value accordingly
            characterManager.currentStamina -= staminaDamage;
        }
    }
}