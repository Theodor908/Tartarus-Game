using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class AIGolemCharacterCombatManager : AICharacterCombatManager
    {

        AICharacterManager aiCharacterManager;

        [Header("Damage Collider")]
        [SerializeField] AIGolemDamageCollider handDamageCollider;
        [SerializeField] AIGolemDamageCollider footDamageCollider;

        [Header("Damage")]
        [SerializeField] int baseDamage = 15;
        [SerializeField] int basePoiseDamage = 7;

        protected override void Awake()
        {
            base.Awake();
            aiCharacterManager = GetComponent<AICharacterManager>();
        }

        public void OpenHandDamageCollider()
        {
            handDamageCollider.EnableDamageCollider();
        }

        public void CloseHandDamageCollider()
        {
            handDamageCollider.DisableDamageCollider();
        }

        public void OpenFootDamageCollider()
        {
            footDamageCollider.EnableDamageCollider();
        }

        public void CloseFootDamageCollider()
        {
            footDamageCollider.DisableDamageCollider();
        }

    }
}
