using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Tartarus
{
    public class UI_Boss_Hp_Bar : UI_StatusBar
    {
        [SerializeField] AICharacterBossManager bossCharacter;
        public void EnableBossHPBar(AICharacterBossManager boss)
        {
            bossCharacter = boss;
            SetMaxStatus(bossCharacter.maxHealth);
            GetComponentInChildren<TextMeshProUGUI>().text = bossCharacter.characterName;
        }

        protected void FixedUpdate()
        {
            if (bossCharacter != null)
            {
                SetStatus(bossCharacter.currentHealth);

                if (bossCharacter.currentHealth <= 0)
                {
                    RemoveHpBar(2.5f);
                }

            }
        }

        public void RemoveHpBar(float time)
        {
            Destroy(gameObject, time);
        }

    }
}
