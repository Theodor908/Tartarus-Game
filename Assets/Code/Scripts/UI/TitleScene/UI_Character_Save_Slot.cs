using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Tartarus
{
    public class UI_Character_Save_Slot : MonoBehaviour
    {
        SaveFile saveFileDataWriter;

        [Header("Game Slot Info")]
        public CharacterSlot characterSlot;

        [Header("Character Info")]
        public TextMeshProUGUI characterName;
        public TextMeshProUGUI timePlayed;

        private void OnEnable()
        {
            LoadSaveSlotData();
        }

        private void LoadSaveSlotData()
        { 
        
            saveFileDataWriter = new SaveFile();
            saveFileDataWriter.saveDataPath = Application.persistentDataPath;

            for(int i = 0; i < WorldSaveManager.instance.characterSlots.Length; i++)
            {
                if(characterSlot == (CharacterSlot)i)
                {
                    saveFileDataWriter.saveFileName = WorldSaveManager.instance.DecideFileNameBasedOnCharacterSlotUsed(characterSlot);

                    if(saveFileDataWriter.CheckForSaveFile())
                    {
                        characterName.text = WorldSaveManager.instance.characterSlots[i].characterName;
                    }
                    else
                    {
                       gameObject.SetActive(false);
                    }
                }
            }

        }

        public void LoadGameFromCharacterSlot()
        {
            WorldSaveManager.instance.currentCharacterSlot = characterSlot;
            WorldSaveManager.instance.LoadGame();
        }

        public void SelectCurrentSlot()
        {
            TitleScreenManager.instance.SelectCharacterSlot(characterSlot); 
        }

    }
}