using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class AIUndeadWarriorCombatManager : AICharacterCombatManager
    {
        [Header ("Damage Collider")]
        [SerializeField] UndeadWarriorDamageCollider rightHandSwordCollider;

        [Header("Damage")]
        [SerializeField] int baseDamage = 25;
        [SerializeField] int basePoiseDamage = 10;
        [SerializeField] float attack01DamageModifier = 1.0f;
        [SerializeField] float attack02DamageModifier = 1.4f;

        public void SetAttack01Damage()
        {
            rightHandSwordCollider.physicalDamage = baseDamage * attack01DamageModifier;
            rightHandSwordCollider.poiseDamage = basePoiseDamage * attack01DamageModifier;
        }

        public void SetAttack02Damage()
        {
            rightHandSwordCollider.physicalDamage = baseDamage * attack02DamageModifier;
            rightHandSwordCollider.poiseDamage = basePoiseDamage * attack02DamageModifier;
        }

        public void OpenUndeadRightHandSwordDamageCollider()
        {
            rightHandSwordCollider.EnableDamageCollider();
        }

        public void DisableUndeadRightHandSwordDamageCollider()
        {
            rightHandSwordCollider.DisableDamageCollider();
        }

    }
}
