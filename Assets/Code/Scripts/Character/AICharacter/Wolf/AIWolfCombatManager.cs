using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{

    public class AIWolfCombatManager : AICharacterCombatManager
    {
        AICharacterManager aiCharacterManager;

        [Header("Damage Collider")]
        [SerializeField] AIWolfDamageCollider wolfMouthDamageCollider;

        [Header ("Damage")]
        [SerializeField] int baseDamage = 15;
        [SerializeField] int basePoiseDamage = 7;

        protected override void Awake()
        {
            base.Awake();
            aiCharacterManager = GetComponent<AICharacterManager>();
        }

        public void SetAttackDamage()
        {
            wolfMouthDamageCollider.physicalDamage = baseDamage;
            wolfMouthDamageCollider.poiseDamage = basePoiseDamage;
        }

        public void OpenWolfDamageCollider()
        {
            wolfMouthDamageCollider.EnableDamageCollider();
        }

        public void CloseWolfDamageCollider()
        {
            wolfMouthDamageCollider.DisableDamageCollider();
        }

    }
}