using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class CharacterLocomotionManager : MonoBehaviour
    {

        CharacterManager characterManager;

        [Header("Movement Settings")]
        [HideInInspector] protected Vector3 moveDirection;
        [SerializeField] protected float walkingSpeed = 2;
        [SerializeField] protected float runningSpeed = 5;
        [SerializeField] protected float sprintingSpeed = 7;
        [SerializeField] protected float rotationSpeed = 15;
        [SerializeField] protected float sprintStaminaCost = 2;

        [Header("Dodge Settings")]
        protected Vector3 rollDirection;
        [SerializeField] protected float dodgeStaminaCost = 25;

        [Header("Jump Settings")]
        [SerializeField] protected Vector3 jumpDirection;
        [SerializeField] protected float jumpStaminaCost = 25;
        [SerializeField] protected float jumpHeight = 2f;
        [SerializeField] protected float jumpForwardSpeed = 3f;
        [SerializeField] protected float freeFallSpeed = 5;

        [Header("Ground check")]
        [SerializeField] LayerMask groundLayer;
        [SerializeField] float groundCheckSphereRadius = 0.2f;
        [SerializeField] protected float skipGroundCheckTimer = 0.2f;
        [SerializeField] protected float skipGroundCheckTimerCounter = 0;

        [Header("Gravity Settings")]
        [SerializeField] protected float gravityForce = -20f;
        [SerializeField] protected Vector3 yVelocity;
        [SerializeField] protected float groundedYVelocity = -20f;
        [SerializeField] protected float fallStartYVelocity = 0;
        protected bool fallingVelocityHasBeenSet = false;
        protected float inAirTimer = 0;

        [Header ("Flags")]
        public bool isRolling = false;
        public bool canRotate = true;
        public bool canMove = true;
        public bool isJumping = false;
        public bool isGrounded = false;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        protected virtual void Update()
        {

            HandleGroundCheck();

            if(characterManager.characterLocomotionManager.isGrounded)
            {

                if(yVelocity.y < -jumpHeight)
                {
                    inAirTimer = 0;
                    fallingVelocityHasBeenSet = false;
                    yVelocity.y = groundedYVelocity;
                }
            }
            else
            {
                if(!characterManager.characterLocomotionManager.isJumping && !fallingVelocityHasBeenSet)
                {
                    fallingVelocityHasBeenSet = true;
                    yVelocity.y = fallStartYVelocity;
                }

                inAirTimer += Time.deltaTime;
                characterManager.animator.SetFloat("inAirTimer", inAirTimer);

                yVelocity.y += gravityForce * Time.deltaTime;

            }

            yVelocity.y = Mathf.Clamp(yVelocity.y, -40, 40);

            characterManager.characterController.Move(yVelocity * Time.deltaTime);

        }

        protected void HandleGroundCheck()
        { 
            if(skipGroundCheckTimerCounter > 0)
            {
                characterManager.characterLocomotionManager.isGrounded = false;
                skipGroundCheckTimerCounter -= Time.deltaTime;
                return;
            }
            characterManager.characterLocomotionManager.isJumping = false;
            characterManager.characterLocomotionManager.isGrounded = Physics.CheckSphere(characterManager.transform.position, groundCheckSphereRadius, groundLayer);
        }

        protected void CalculateTimeToLand()
        {
            skipGroundCheckTimerCounter = 2 * Mathf.Sqrt(-2 * jumpHeight / gravityForce);
        }

        protected void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, groundCheckSphereRadius);
        }

        public void EnableCanRotate()
        {
            canRotate = true;
        }

        public void DisableCanRotate()
        {
            canRotate = false;
        }

    }
}
