using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Tartarus
{
    public class PlayerUIPopUpManager : MonoBehaviour
    {
        [Header("Message Pop Up")]
        [SerializeField] TextMeshProUGUI popUpMessageText;
        [SerializeField] GameObject popUpMessageGameObject;

        [Header("YOU DIED Pop Up")]
        [SerializeField] GameObject youDiedPopUpGameObject;
        [SerializeField] TextMeshProUGUI youDiedPopUpBackgroundText;
        [SerializeField] TextMeshProUGUI youDiedPopUpText;
        [SerializeField] CanvasGroup youDiedPopUpCanvasGroup; // Used to fade in and out the pop up

        [Header ("SITE OF GRACE RESTORED Pop Up")]
        [SerializeField] GameObject siteOfGraceRestoredPopUpGameObject;
        [SerializeField] TextMeshProUGUI siteOfGraceRestoredPopUpBackgroundText;
        [SerializeField] TextMeshProUGUI siteOfGraceRestoredPopUpText;
        [SerializeField] CanvasGroup siteOfGraceRestoredPopUpCanvasGroup; // Used to fade in and out the pop up

        [Header("BOSS DEFEATED Pop Up")]
        [SerializeField] GameObject bossDefeateadPopUpGameObject;
        [SerializeField] TextMeshProUGUI bossDefeateadPopUpBackgroundText;
        [SerializeField] TextMeshProUGUI bossDefeateadPopUpText;
        [SerializeField] CanvasGroup bossDefeateadPopUpCanvasGroup; // Used to fade in and out the pop up


        public void DisplayPopUp(string messageText)
        {
            PlayerUIManager.instance.popUpWindowIsOpen = true;
            popUpMessageText.text = messageText;
            popUpMessageGameObject.SetActive(true);
        }

        public void ShowYouDiedPopUp()
        {
            youDiedPopUpGameObject.SetActive(true);
            youDiedPopUpBackgroundText.characterSpacing = 0;
            StartCoroutine(StretchPopUpTextOverTime(youDiedPopUpBackgroundText, 8, 10));
            StartCoroutine(FadeInPopUpOverTime(youDiedPopUpCanvasGroup, 5));
            StartCoroutine(WaitThenFadeOutPopUpOverTime(youDiedPopUpCanvasGroup, 2, 5));

        }

        public void SendBossDefeatedPopUp(string bossDefeatedMessage)
        {
            bossDefeateadPopUpText.text = bossDefeatedMessage;
            bossDefeateadPopUpBackgroundText.text = bossDefeatedMessage;
            bossDefeateadPopUpGameObject.SetActive(true);
            bossDefeateadPopUpBackgroundText.characterSpacing = 0;
            StartCoroutine(StretchPopUpTextOverTime(bossDefeateadPopUpBackgroundText, 8, 10));
            StartCoroutine(FadeInPopUpOverTime(bossDefeateadPopUpCanvasGroup, 5));
            StartCoroutine(WaitThenFadeOutPopUpOverTime(bossDefeateadPopUpCanvasGroup, 2, 5));
        }

        public void SendSiteOfGraceRestoredPopUp(string siteOfGraceRestoredMessage)
        {
            siteOfGraceRestoredPopUpText.text = siteOfGraceRestoredMessage;
            siteOfGraceRestoredPopUpBackgroundText.text = siteOfGraceRestoredMessage;
            siteOfGraceRestoredPopUpGameObject.SetActive(true);
            siteOfGraceRestoredPopUpBackgroundText.characterSpacing = 0;
            StartCoroutine(StretchPopUpTextOverTime(siteOfGraceRestoredPopUpBackgroundText, 8, 10));
            StartCoroutine(FadeInPopUpOverTime(siteOfGraceRestoredPopUpCanvasGroup, 5));
            StartCoroutine(WaitThenFadeOutPopUpOverTime(siteOfGraceRestoredPopUpCanvasGroup, 2, 5));
        }

        private IEnumerator StretchPopUpTextOverTime(TextMeshProUGUI text, float duration, float stretchAmount)
        {
            if(duration > 0f)
            {
                text.characterSpacing = 0;
                float timer = 0f;

                yield return null;

                while(timer < duration)
                {
                    timer += Time.deltaTime;
                    text.characterSpacing = Mathf.Lerp(text.characterSpacing, stretchAmount, duration * (Time.deltaTime / 20));
                    yield return null;
                }
            }

            text.characterSpacing = stretchAmount;
            yield return null;

        }

        private IEnumerator FadeInPopUpOverTime(CanvasGroup canvas, float duration)
        {

            if(duration > 0f)
            {
                canvas.alpha = 0;
                float timer = 0f;

                yield return null;

                while(timer < duration)
                {
                    timer += Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 1, duration * Time.deltaTime);
                    yield return null;
                }
            }

            canvas.alpha = 1;

            yield return null;

        }

        private IEnumerator WaitThenFadeOutPopUpOverTime(CanvasGroup canvas, float duration, float delay)
        {
            if (duration > 0f)
            {

                while (delay > 0)
                {
                    delay -= Time.deltaTime;
                    yield return null;
                }

                canvas.alpha = 1;
                float timer = 0f;

                yield return null;

                while (timer < duration)
                {
                    timer += Time.deltaTime;
                    canvas.alpha = Mathf.Lerp(canvas.alpha, 0, duration * Time.deltaTime);
                    yield return null;
                }
            }

            canvas.alpha = 0;

            yield return null;

        }

        public void CloseAllPopUpWindows()
        {
            popUpMessageGameObject.SetActive(false);
            PlayerUIManager.instance.popUpWindowIsOpen = false;
        }

    }
}
