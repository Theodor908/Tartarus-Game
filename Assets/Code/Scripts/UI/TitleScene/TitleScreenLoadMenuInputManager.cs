using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class TitleScreenLoadMenuInputManager : MonoBehaviour
    {
        PlayerControls playerControls;

        [Header("Title screen inputs")]
        [SerializeField] bool deleteCharacterSlot = false;

        private void Update()
        {
            if(deleteCharacterSlot)
            {
                deleteCharacterSlot = false;
                TitleScreenManager.instance.AttemptToDeleteCharacterSlot();
            }
        }

        private void OnEnable()
        {
            if(playerControls == null)
            {
                playerControls = new PlayerControls();
                playerControls.UI.Del.performed += ctx => deleteCharacterSlot = true;
                Debug.Log("Performed");
            }

            playerControls.Enable();

        }
        private void OnDisable()
        {
            playerControls.Disable();
        }

    }
}