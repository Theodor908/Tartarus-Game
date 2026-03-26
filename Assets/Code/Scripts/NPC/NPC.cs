using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tartarus
{
    public abstract class NPC : MonoBehaviour, IInteractable
    {
        public SpriteRenderer _interactSprite;
        private Transform _playerTransorm;
        [SerializeField] const float INTERACT_DISTANCE = 1.5f;

        private void Start()
        {
            _playerTransorm = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {

            if (Keyboard.current.gKey.wasPressedThisFrame && isWithinInteractDistance())
            {
                Interact();
            }

            if(_interactSprite.gameObject.activeSelf && !isWithinInteractDistance())
            {
                _interactSprite.gameObject.SetActive(false);
            }
            else if (!_interactSprite.gameObject.activeSelf && isWithinInteractDistance())
            {
                _interactSprite.gameObject.SetActive(true);
            }

    }

        public abstract void Interact();

        private bool isWithinInteractDistance()
        {
            return Vector2.Distance(transform.position, _playerTransorm.position) < INTERACT_DISTANCE;
        }

    }
}
