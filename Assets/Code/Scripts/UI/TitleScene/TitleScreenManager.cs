using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tartarus
{
    public class TitleScreenManager : MonoBehaviour
    {

        public static TitleScreenManager instance;

        [Header ("Menu objects")]
        [SerializeField] GameObject titleScreenMainMenu;
        [SerializeField] GameObject titleSreenLoadMenu;

        [Header ("Pop-up objects")]
        [SerializeField] GameObject noFreeCharacterSlotsPopUp;
        [SerializeField] GameObject deleteCharacterSlotPopUp;

        [Header("Buttons")]
        [SerializeField] Button mainMenuPressStartButton;
        [SerializeField] Button loadMenuReturnButton;
        [SerializeField] Button maineMenuLoadGameButton;
        [SerializeField] Button mainMenuNewGameButton;
        [SerializeField] Button mainMenuNoFreeCharacterSlotsButton;
        [SerializeField] Button deleteCharacterPopUpConfirmButton;

        [Header("Save slots")]
        public CharacterSlot currentSelectedSlot = CharacterSlot.NO_CHARACTER_SLOT;

        [Header("Title screen inputs")]
        [SerializeField] bool deleteCharacterSlot;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            mainMenuPressStartButton.Select();

        }

        #region Menu Navigation

        public void StartNewGame()
        {

            WorldSaveManager.instance.AttemptToCreateNewGame();
        }

        public void OpenNewSaveOrLoadMenu()
        {
            mainMenuPressStartButton.gameObject.SetActive(false);
            titleScreenMainMenu.SetActive(true);

            mainMenuNewGameButton.Select();

        }

        public void OpenLoadGameMenu()
        {

            // Close the main menu and open the load menu

            titleScreenMainMenu.SetActive(false);
            titleSreenLoadMenu.SetActive(true);

            loadMenuReturnButton.Select();

        }

        public void CloseLoadGameMenu()
        {

            // Close the load menu and open the main menu

            titleScreenMainMenu.SetActive(true);
            titleSreenLoadMenu.SetActive(false);

            mainMenuNewGameButton.Select();

        }

        #endregion

        #region Pop-ups

        public void DisplayNoFreeCharacterSlotsPopUp()
        {
            titleScreenMainMenu.SetActive(false);
            noFreeCharacterSlotsPopUp.SetActive(true);
            mainMenuNoFreeCharacterSlotsButton.Select();

        }

        public void CloseNoFreeCharacterSlotsPopUp()
        {
            noFreeCharacterSlotsPopUp.SetActive(false);
            titleScreenMainMenu.SetActive(true);
            mainMenuNewGameButton.Select();
        }

        #endregion

        #region Character Slot

        public void SelectCharacterSlot(CharacterSlot characterSlot)
        {
            currentSelectedSlot = characterSlot;
            Debug.Log(characterSlot.ToString() + " is selected");
        }

        public void SelectNoSlot()
        {
            currentSelectedSlot = CharacterSlot.NO_CHARACTER_SLOT;
        }

        public void AttemptToDeleteCharacterSlot()
        {

            if(currentSelectedSlot != CharacterSlot.NO_CHARACTER_SLOT)
            {
                deleteCharacterSlotPopUp.SetActive(true);
                deleteCharacterPopUpConfirmButton.Select();
            }
        }

        public void DeleteCharacterSlot()
        {
           deleteCharacterSlotPopUp.SetActive(false);
            WorldSaveManager.instance.DeleteGame(currentSelectedSlot);

            // Refersh the load menu
            titleSreenLoadMenu.SetActive(false);
            titleSreenLoadMenu.SetActive(true);  

            loadMenuReturnButton.Select();
        }

        public void CloseDeleteCharacterPopUp()
        {
            deleteCharacterSlotPopUp.SetActive(false);
            loadMenuReturnButton.Select();
        }

        #endregion
    }
}
    