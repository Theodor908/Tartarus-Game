using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class PlayerInteractionManager : MonoBehaviour
    {
        PlayerManager playerManager;

        private List<Interactable> currentInteractables;

        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
        }

        private void Start()
        {
            currentInteractables = new List<Interactable>();
        }

        private void FixedUpdate()
        {
            if(PlayerUIManager.instance.menuWindowIsOpen || PlayerUIManager.instance.popUpWindowIsOpen)
            {
                return;
            }

            CheckForInteractable();

        }

        private void CheckForInteractable()
        { 
            if(currentInteractables.Count == 0)
            {
                return;
            }

            if (currentInteractables[0] == null)
            {
                currentInteractables.RemoveAt(0);
                return;
            }

            // If we have an interactable in range, we can interact with it

            if (currentInteractables[0] != null)
            {
                 PlayerUIManager.instance.playerUIPopUpManager.DisplayPopUp(currentInteractables[0].interactableText);
            }

        }

        private void RefreshInteractionList()
        {
            for (int i = currentInteractables.Count - 1; i > 0; i--)
            {
                if (currentInteractables[i] == null)
                {
                    currentInteractables.RemoveAt(i);
                }
            }
        }

        public void AddInteractionToList(Interactable interactable)
        {
            RefreshInteractionList();
            if (!currentInteractables.Contains(interactable))
            {
                currentInteractables.Add(interactable);
            }
        }

        public void RemoveInteractionFromList(Interactable interactable)
        {
            if (currentInteractables.Contains(interactable))
            {
                currentInteractables.Remove(interactable);
            }
            RefreshInteractionList();
        }

        public void Interact()
        {

            if(currentInteractables.Count == 0)
            {
                return;
            }

            if (currentInteractables[0] != null)
            {
                currentInteractables[0].Interact(playerManager);
                RefreshInteractionList();
            }
        }

    }
}