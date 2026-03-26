using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class PlayerInputManager : MonoBehaviour
    {

        PlayerControls playerControls;
        public static PlayerInputManager instance;
        public PlayerManager playerManager;

        [Header("Player Move Input")]
        [SerializeField] Vector2 movementInput;
        public float horizontalInput;
        public float verticalInput;
        public float moveAmount;

        [Header("Player Action Input")]
        [SerializeField] bool walkInput = false;
        [SerializeField] bool dodgeInput = false;
        [SerializeField] bool jumpInput = false;
        [SerializeField] bool sprintInput = false;
        [SerializeField] bool interactInput = false;
        [SerializeField] bool drinkPotionInput = false;
        [SerializeField] bool useSpellInput = false;

        [Header("Qued Inputs")]
        [SerializeField] bool input_Que_Is_Active = false;
        [SerializeField] float que_Input_Timer = 0;
        [SerializeField] float default_Que_Input_Timer = 0.35f;
        [SerializeField] bool que_RMB_Input = false;
        [SerializeField] bool que_LMB_Input = false;

        [Header ("Weapon Action Button Inputs")]
        [SerializeField] bool RMB_Input = false;
        [SerializeField] bool LMB_Input = false;
        [SerializeField] bool LBMB_Input = false;

        [Header("Quick Slot Inputs")]
        [SerializeField] bool switchSpellInput = false;
        [SerializeField] bool switchWeaponInput = false;
        [SerializeField] bool switchPotionInput = false;

        [Header ("Trigger inputs")]
        [SerializeField] bool Hold_RMB_Input = false;
        [SerializeField] bool Hold_LMB_Input = false;
        

        [Header("Camera Move Input")]
        [SerializeField] Vector2 cameraInput;
        public float cameraVerticalInput;
        public float cameraHorizontalInput;

        [Header ("Lock On Input")]
        [SerializeField] bool lockOnInput = false;
        [SerializeField] bool lockOn_LeftInput = false;
        [SerializeField] bool lockOn_RightInput = false;
        private Coroutine lockOnCoroutine;

        [Header("Player Menu Actions")]
        [SerializeField] bool openMenu = false;


        public static PlayerInputManager Instance { get => instance; }

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

        private void OnEnable()
        {
            if (playerControls == null)
            {
                // Movement
                playerControls = new PlayerControls();
                playerControls.PlayerMovement.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
                playerControls.CameraMovement.Movement.performed += ctx => cameraInput = ctx.ReadValue<Vector2>();

                // Walk
                playerControls.PlayerActions.Walk.performed += ctx => walkInput = true;
                playerControls.PlayerActions.Walk.canceled += ctx => walkInput = false;

                // Dodge
                playerControls.PlayerActions.Dodge.performed += ctx => dodgeInput = true;

                // Jump
                playerControls.PlayerActions.Jump.performed += ctx => jumpInput = true;

                // Drink potion 

                playerControls.PlayerActions.PotionDrink.performed += ctx => drinkPotionInput = true;

                // Use Spell

                playerControls.PlayerActions.SpellUse.performed += ctx => useSpellInput = true;

                // RMB
                playerControls.PlayerActions.RMB.performed += ctx => RMB_Input = true;

                // Hold RMB

                playerControls.PlayerActions.HoldRMB.performed += ctx => Hold_RMB_Input = true;
                playerControls.PlayerActions.HoldRMB.canceled += ctx => Hold_RMB_Input = false;

                // LMB
                playerControls.PlayerActions.LMB.performed += ctx => LMB_Input = true;

                // Hold LMB
                playerControls.PlayerActions.HoldLMB.performed += ctx => Hold_LMB_Input = true;
                playerControls.PlayerActions.HoldLMB.canceled += ctx => Hold_LMB_Input = false;

                // LBMB
                playerControls.PlayerActions.LBMB.performed += ctx => LBMB_Input = true;
                playerControls.PlayerActions.LBMB.canceled += ctx => playerManager.isBlocking = false;

                // Lock on
                playerControls.PlayerActions.Lockon.performed += ctx => lockOnInput = true;
                playerControls.PlayerActions.SeekLeftLockOnTarget.performed += ctx => lockOn_LeftInput = true;
                playerControls.PlayerActions.SeekRightLockOnTarget.performed += ctx => lockOn_RightInput = true;

                // Interact
                playerControls.PlayerActions.Interact.performed += ctx => interactInput = true;

                // Sprint
                playerControls.PlayerActions.Sprint.performed += ctx => sprintInput = true;
                playerControls.PlayerActions.Sprint.canceled += ctx => sprintInput = false;


                //Switch Spell

                playerControls.PlayerActions.SwitchSpell.performed += ctx => switchSpellInput = true;

                //Switch Weapon
                playerControls.PlayerActions.SwitchWeapon.performed += ctx => switchWeaponInput = true;

                //Switch Potion

                playerControls.PlayerActions.SwitchPotion.performed += ctx => switchPotionInput = true;

                //Qued Inputs
                playerControls.PlayerActions.QueRMB.performed += ctx => QueInput(ref que_RMB_Input);
                playerControls.PlayerActions.QueLMB.performed += ctx => QueInput(ref que_LMB_Input);

                // Open Inventory

                playerControls.PlayerActions.OpenMenu.performed += ctx => openMenu = true;

            }

            playerControls.Enable();

        }

        private void Update()
        {

            HandleRMBInput();
            HandleChargeRMBInput();
            HandleLMBInput();
            HandleJumpInput();
            HandleUseSpellInput();
            HandleDrinkPotionInput();
            HandleDodgeInput();
            HandleSprintInput();
            HandleLockOnInput();
            HandleMovementInput();
            HandleChargeLMBInput();
            HandleCameraMovementInput();
            HandleSwitchSpellInput();
            HandleSwitchWeaponInput();
            HandleSwitchPotionInput();
            HandleLockOnSwitchTargetInput();
            HandleQuedInputs();
            HandleInteractInput();
            HandleMenu();
            HandleLBMBInput();

        }

        #region MovementInput

        private void HandleMovementInput()
        {

            horizontalInput = movementInput.x;
            verticalInput = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

            if (moveAmount > 0 && walkInput)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0 && !sprintInput)
            {
                moveAmount = 1f;
            }
            else if (moveAmount > 0 && sprintInput)
            {
                moveAmount = 2f;
            }

            if (moveAmount != 0)
            {
                playerManager.isMoving = true;
            }
            else
            {
                playerManager.isMoving = false;
            }

            if (!playerManager.isLockedOn || playerManager.playerCombatManager.isThrowing || playerManager.isSprinting)
            {
                // Not locked-on
                playerManager.playerAnimationManager.UpdateAnimatorMovementParameters(0, moveAmount);
            }
            else
            {
                // Locked-on
                playerManager.playerAnimationManager.UpdateAnimatorMovementParameters(horizontalInput, verticalInput);
            }

        }

        private void HandleCameraMovementInput()
        {
            cameraHorizontalInput = cameraInput.x;
            cameraVerticalInput = cameraInput.y;
        }

        #endregion

        #region ActionInput

        private void HandleDodgeInput()
        {
            if (dodgeInput)
            {
                dodgeInput = false;
                playerManager.playerLocomotionManager.AttemptToPerformDodge();
            }
        }

        private void HandleSprintInput()
        {
            playerManager.isSprinting = sprintInput;
            if (sprintInput)
            {
                playerManager.playerLocomotionManager.HandleSprinting();
            }
        }

        private void HandleJumpInput()
        {
            if(jumpInput)
            {
                jumpInput = false;
                playerManager.playerLocomotionManager.AttemptToPerformJump();
            }
        }

        private void HandleUseSpellInput()
        {

            if(useSpellInput)
            {
                useSpellInput = false;

                if(playerManager.isInteracting)
                {
                    return;
                }

                if (PlayerUIManager.instance.menuWindowIsOpen)
                {
                    return;
                }

                playerManager.playerCombatManager.PerformSpellBasedAction(playerManager.playerInventoryManager.currentSpell.spellItemAction, playerManager.playerInventoryManager.currentSpell);
            }

        }

        private void HandleDrinkPotionInput()
        { 
        
            if(drinkPotionInput)
            {
                drinkPotionInput = false;

                if(playerManager.isInteracting)
                {
                    return;
                }

                if (PlayerUIManager.instance.menuWindowIsOpen)
                {
                    return;
                }

                playerManager.playerCombatManager.PerformPotionBasedAction(playerManager.playerInventoryManager.currentPotion.potionItemAction, playerManager.playerInventoryManager.currentPotion);
            }

        }

        public void HandleRMBInput()
        {
            if (RMB_Input)
            {
                RMB_Input = false;
                // If ui is opne return

                if (PlayerUIManager.instance.menuWindowIsOpen)
                {
                    return;
                }

                // Maybe some two handed weapon logic ? No

                playerManager.playerCombatManager.PerformWeaponBasedAction(playerManager.playerInventoryManager.currentWeapon.RMB_Action, playerManager.playerInventoryManager.currentWeapon);

            }
        }

        public void HandleLBMBInput()
        {
            if (LBMB_Input)
            {
                LBMB_Input = false;
                // If ui is opne return

                if (PlayerUIManager.instance.menuWindowIsOpen)
                {
                    return;
                }

                if(playerManager.playerInventoryManager.currentWeapon.hasShield == false)
                {
                    return;
                }
               
                // Maybe some two handed weapon logic ?

                playerManager.playerCombatManager.PerformWeaponBasedAction(playerManager.playerInventoryManager.currentWeapon.LBMB_Action, playerManager.playerInventoryManager.currentWeapon);

            }
        }

        private void HandleChargeRMBInput()
        {

            //Check for charge if the action is a chargeable action

            if (playerManager.isInteracting)
            {
                playerManager.specialCharge = Hold_RMB_Input;
                playerManager.animator.SetBool("specialCharged", playerManager.specialCharge);
            }

        }

        private void HandleLMBInput()
        {
            if (LMB_Input)
            {
                LMB_Input = false;

                if (PlayerUIManager.instance.menuWindowIsOpen)
                {
                    return;
                }

                // If ui is open return

                // Maybe some two handed weapon logic ?
                playerManager.playerCombatManager.PerformWeaponBasedAction(playerManager.playerInventoryManager.currentWeapon.LMB_Action, playerManager.playerInventoryManager.currentWeapon);

            }
        }

        private void HandleChargeLMBInput()
        {
            //Check for charge if the action is a chargeable action

            if (playerManager.isInteracting)
            {
                playerManager.isChargingAttack = Hold_LMB_Input;
                playerManager.animator.SetBool("isChargingAttack", playerManager.isChargingAttack);
            }
        }

        private void HandleLockOnInput()
        {
            // if using a bow return

            if (PlayerUIManager.instance.menuWindowIsOpen)
            {
                return;
            }

            // Dead targets
            if (playerManager.isLockedOn)
            {
                if (playerManager.playerCombatManager.currentTarget == null)
                {
                    return;
                }

                if (playerManager.playerCombatManager.currentTarget.isDead)
                {
                    playerManager.isLockedOn = false;

                    if (lockOnCoroutine != null)
                    {
                        StopCoroutine(lockOnCoroutine);
                    }

                    lockOnCoroutine = StartCoroutine(PlayerCamera.instance.WaitThenFindNewTarget());
                }

            }

            if (lockOnInput && playerManager.isLockedOn)
            {
                lockOnInput = false;
                PlayerCamera.instance.ClearLockOnTargets();
                playerManager.isLockedOn = false;

                return;
            }

            if (lockOnInput && !playerManager.isLockedOn)
            {
                lockOnInput = false;
                //Debug.Log("Attempting to lock on");
                PlayerCamera.instance.HandleLocatingLockOnTargets();

                if (PlayerCamera.instance.nearestLockOnTarget != null)
                {
                    playerManager.characterCombatManager.SetLockOnTarget(PlayerCamera.instance.nearestLockOnTarget);
                    playerManager.isLockedOn = true;
                }

                return;
            }


        }

        private void HandleInteractInput()
        {
            if(interactInput)
            {
                interactInput = false;

                if (PlayerUIManager.instance.menuWindowIsOpen)
                {
                    return;
                }

                playerManager.playerInteractionManager.Interact();
            }
        }

        private void HandleLockOnSwitchTargetInput()
        {
            if (lockOn_LeftInput)
            {
                lockOn_LeftInput = false;

                if (playerManager.isLockedOn)
                {
                    PlayerCamera.instance.HandleLocatingLockOnTargets();

                    if (PlayerCamera.instance.leftLockOnTarget != null)
                    {
                        playerManager.characterCombatManager.SetLockOnTarget(PlayerCamera.instance.leftLockOnTarget);
                    }

                }

            }

            if (lockOn_RightInput)
            {
                lockOn_RightInput = false;

                if (playerManager.isLockedOn)
                {
                    PlayerCamera.instance.HandleLocatingLockOnTargets();

                    if (PlayerCamera.instance.rightLockOnTarget != null)
                    {
                        playerManager.characterCombatManager.SetLockOnTarget(PlayerCamera.instance.rightLockOnTarget);
                    }

                }

            }

        }

        private void HandleSwitchSpellInput()
        {

            if (switchSpellInput)
            {
                switchSpellInput = false;

                if (PlayerUIManager.instance.menuWindowIsOpen)
                {
                    return;
                }

                playerManager.playerEquipmentManager.SwitchSpell();
            }

        }

        private void HandleSwitchWeaponInput()
        {

            if (switchWeaponInput)
            {
                switchWeaponInput = false;

                if (PlayerUIManager.instance.menuWindowIsOpen)
                {
                    return;
                }

                playerManager.playerEquipmentManager.SwitchWeapon();
            }
        }

        private void HandleSwitchPotionInput()
        {

            if (switchPotionInput)
            {
                switchPotionInput = false;

                if (PlayerUIManager.instance.menuWindowIsOpen)
                {
                    return;
                }

                playerManager.playerEquipmentManager.SwitchPotion();
            }

        }

        private void HandleMenu()
        {

            if(openMenu)
            {
                openMenu = false;
                PlayerUIMenuManager.instance.OpenDefaultMenu();
            }

        }

        private void QueInput(ref bool quedInput)
        {
            // Reset all inputs

            quedInput = false;

            if(playerManager.isInteracting || playerManager.playerLocomotionManager.isJumping)
            {
                quedInput = true;
                que_Input_Timer = default_Que_Input_Timer;
                input_Que_Is_Active = true;
            }



        }

        private void ProcessQuedInput()
        {
            if(playerManager.isDead)
            {
                return;
            }

            if(que_RMB_Input)
            {
                RMB_Input = true;
            }

            if(que_LMB_Input)
            {
                LMB_Input = true;
            }

        }

        private void HandleQuedInputs()
        {
            if(input_Que_Is_Active)
            {
                
                if(que_Input_Timer > 0)
                {
                    que_Input_Timer -= Time.deltaTime;
                    ProcessQuedInput();
                }
                else
                {
                    // Reset all inputs
                    que_RMB_Input = false;
                    que_LMB_Input = false;

                    input_Que_Is_Active = false;
                    que_Input_Timer = 0;
                }

            }
        }

        #endregion

    }
}
