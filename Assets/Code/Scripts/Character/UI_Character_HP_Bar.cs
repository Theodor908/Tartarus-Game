using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Tartarus
{
    public class UI_Character_HP_Bar : UI_StatusBar
    {
        private CharacterManager characterManager;
        private AICharacterManager aiCharacterManager;
        private PlayerManager playerManager;

        [SerializeField] bool displayCharacterNameOnDamage = false;
        [SerializeField] float defaultTimeBeforeBarHides = 3;
        [SerializeField] float hideTimer = 0;
        [SerializeField] int currentDamageTaken = 0;

        [SerializeField] TextMeshProUGUI characterNameText;
        [SerializeField] TextMeshProUGUI characterDamageText;

        protected override void Awake()
        {
            base.Awake();
            characterManager = GetComponentInParent<CharacterManager>();
            if (characterManager != null)
            {
                aiCharacterManager = characterManager as AICharacterManager;
                playerManager = characterManager as PlayerManager;
            }
        }

        protected override void Start()
        {
            base.Start();
            gameObject.SetActive(false);
        }

        private void Update()
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);

            if(hideTimer > 0)
            {
                hideTimer -= Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
            }

        }

        private void OnDisable()
        {
            currentDamageTaken = 0;
        }

        override public void SetStatus(float value)
        {
            if (displayCharacterNameOnDamage)
            {
                characterNameText.gameObject.SetActive(true);
                if(aiCharacterManager != null)
                {
                    characterNameText.text = aiCharacterManager.characterName;
                }
                else if(playerManager != null)
                {
                    characterNameText.text = playerManager.characterName;
                }

            }

            slider.maxValue = characterManager.maxHealth;

            currentDamageTaken = Mathf.RoundToInt(currentDamageTaken + value);

            if (currentDamageTaken < 0)
            {
                currentDamageTaken = Mathf.RoundToInt(currentDamageTaken + value);
                characterDamageText.text = "+ " + currentDamageTaken.ToString();
            }
            else
            {
                characterDamageText.text = "- " + currentDamageTaken.ToString();
            }

            slider.value = characterManager.currentHealth;
            if (characterManager.currentHealth != characterManager.maxHealth)
            {
                hideTimer = defaultTimeBeforeBarHides;
                gameObject.SetActive(true);
            }

        }

    }
}
