using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class Interactable : MonoBehaviour
    {
        public string interactableText; // The text that will be displayed when the player is in range of the interactable
        [SerializeField] protected Collider interactableCollider; // The collider that will be used to detect if the player is in range of the interactable
        [SerializeField] protected bool isInteractable; // Is the interactable currently in range of the player
        
        protected virtual void Awake()
        {
            if (interactableCollider == null)
            {
                interactableCollider = GetComponent<Collider>();
            }
        }

        protected virtual void Start()
        {
            if (isInteractable)
            {
                // Display the interactable text
            }
        }



        public virtual void Interact(PlayerManager playerManager)
        {

            interactableCollider.enabled = false;
            playerManager.playerInteractionManager.RemoveInteractionFromList(this);
            PlayerUIManager.instance.playerUIPopUpManager.CloseAllPopUpWindows();
            
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();

            if (playerManager != null)
            {
                playerManager.playerInteractionManager.AddInteractionToList(this);
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            PlayerManager playerManager = other.GetComponent<PlayerManager>();

            if (playerManager != null)
            {
                playerManager.playerInteractionManager.RemoveInteractionFromList(this);
            }

            PlayerUIManager.instance.playerUIPopUpManager.CloseAllPopUpWindows();

        }

    }
}