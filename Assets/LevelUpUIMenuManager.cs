using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tartarus
{
    public class LevelUpUIMenuManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI vitalityLevelText;
        [SerializeField] TextMeshProUGUI enduranceLevelText;
        [SerializeField] TextMeshProUGUI attunementLevelText;
        [SerializeField] PlayerManager playerManager;

        private void OnEnable()
        {
            vitalityLevelText.text = playerManager.vitality.ToString();
            enduranceLevelText.text = playerManager.endurance.ToString();
            attunementLevelText.text = playerManager.attunement.ToString();
        }

        public void UpdateVitalityLevelText()
        {

            if(playerManager.playerStatsManager.essence >= 100)
            {
                playerManager.playerStatsManager.essence -= 100;
                playerManager.playerStatsManager.LevelUpVitality(1);
                vitalityLevelText.text = playerManager.vitality.ToString();
            }
        }

        public void UpdateEnduranceLevelText()
        {
            if(playerManager.playerStatsManager.essence >= 100)
            {
                playerManager.playerStatsManager.essence -= 100;
                playerManager.playerStatsManager.LevelUpEndurance(1);
                enduranceLevelText.text = playerManager.endurance.ToString();
            }
        }

        public void UpdateAttunementLevelText()
        {
            if(playerManager.playerStatsManager.essence >= 100)
            {
                playerManager.playerStatsManager.essence -= 100;
                playerManager.playerStatsManager.LevelUpAttunement(1);
                attunementLevelText.text = playerManager.attunement.ToString();
            }
        }

    }
}