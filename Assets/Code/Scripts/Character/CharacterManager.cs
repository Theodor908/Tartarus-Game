using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class CharacterManager : MonoBehaviour
    {

        [Header ("UI")]
        protected UI_Character_HP_Bar characterHPBar;

        [Header ("Status of character")]
        public bool isDead = false;
        public bool isInvulnerable = false;
        public bool isMoving = false;
        public bool isAttacking = false;

        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public CharacterEffectsManager characterEffectsManager;
        [HideInInspector] public CharacterAnimationManager characterAnimationManager;
        [HideInInspector] public CharacterCombatManager characterCombatManager;
        [HideInInspector] public CharacterLocomotionManager characterLocomotionManager;
        [HideInInspector] public CharacterStatsManager characterStatsManager;
        [HideInInspector] public CharacterUIManager characterUIManager;
        [HideInInspector] public Animator animator;
        public string characterName = "";

        [Header ("Character Group")]
        public CharacterGroup characterGroup;

        [Header("Stats")]
        public float maxHealth = 100;
        public int vitality = 1;
        public float currentHealth;
        public float maxStamina = 100;
        public int endurance = 1;
        public float currentStamina;
        public float maxFocusPoints = 100;
        public int attunement = 1;
        public float currentFocusPoints;

        [Header("Flags")]
        public bool isBlocking = false;
        public bool isInteracting = false;
        public bool isSprinting = false;
        public bool isLockedOn = false;
        public bool isChargingAttack = false;
        public bool specialCharge = false;

        [Header("Change Over Time")]
        protected float previousHealth = 0;

        private bool firstTime = true;

        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
            characterEffectsManager = GetComponent<CharacterEffectsManager>();
            characterAnimationManager = GetComponent<CharacterAnimationManager>();
            animator = GetComponent<Animator>();
            characterCombatManager = GetComponent<CharacterCombatManager>();
            characterLocomotionManager = GetComponent<CharacterLocomotionManager>();
            characterUIManager = GetComponent<CharacterUIManager>();
            characterStatsManager = GetComponent<CharacterStatsManager>();
        }

        protected virtual void Start()
        {
            IgnoreMyOwnColliders();
            currentHealth = maxHealth;
        }

        protected virtual void Update()
        {
            animator.SetBool("isGrounded", characterLocomotionManager.isGrounded);
            animator.SetBool("isMoving", isMoving);
            CheckHealthPoints();
            ResetLockOn();

            if(firstTime)
            {
                previousHealth = currentHealth;
                firstTime = false;
            }


            if(previousHealth != currentHealth && characterUIManager.hasFloatingHealthBar)
            {
                characterUIManager.OnHpChanged((int)previousHealth, (int)currentHealth);
                previousHealth = currentHealth;
            }


        }

        protected virtual void FixedUpdate()
        {

        }

        protected virtual void LateUpdate()
        {

        }

        public virtual IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
        {
            currentHealth = 0;
            isDead = true;
            animator.SetBool("isDead", true);

            // Reset all flags

            // if in air death air anim

            // if on ground death ground anim

            if(!manuallySelectDeathAnimation)
            {
                characterAnimationManager.PlayTargetAnimation("Dead_01", true);
            }
            else
            {
                // Play default death animation
            }

            // Random death animation

            yield return new WaitForSeconds(5);

            // Award players with runes
            PlayerManager playerManager = GameObject.FindObjectOfType<PlayerManager>();

            if(playerManager != null)
            {
                playerManager.playerStatsManager.essence += characterStatsManager.essence;
            }

            // Disable character

        }

        public virtual void CheckHealthPoints()
        {
            if(currentHealth <= 0 && !isDead)
            {
                StartCoroutine(ProcessDeathEvent());
            }

            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

        }

        public virtual void ReviveCharacter()
        {

        }

        public virtual void IgnoreMyOwnColliders()
        {
            // Take every colliders on the character and ignore them
            Collider characterControllerCollider = GetComponent<Collider>();
            Collider[] damageableCharacterColliders = GetComponentsInChildren<Collider>();
            List<Collider> ignoreColliders = new List<Collider>();

            foreach (var collider in damageableCharacterColliders)
            {
                ignoreColliders.Add(collider);
            }

            ignoreColliders.Add(characterControllerCollider);

            foreach(var collider in ignoreColliders)
            {
                foreach(var otherCollider in ignoreColliders)
                {
                    Physics.IgnoreCollision(collider, otherCollider, true);
                }
            }

        }

        public virtual void ResetLockOn()
        {
            if (!isLockedOn)
                characterCombatManager.currentTarget = null;
        }



    }
}
