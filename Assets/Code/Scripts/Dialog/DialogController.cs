using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Tartarus
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI NPCNameText;
        [SerializeField] private TextMeshProUGUI DialogText;
        [SerializeField] private float typeSpeed = 10f;

        private Queue<string> paragraphs = new Queue<string>();

        private bool conversationEnded;

        private string p;

        private Coroutine typeDialogCoroutine;
        private const string htmlAlpha = "<color=#00000000>";
        private const float maxTypeTime = 0.1f;
        private bool isTyping;

        public void DisplayNextParagraph(DialogText dialogText)
        {
            if(paragraphs.Count == 0)
            {
                if(!conversationEnded)
                {
                    StartConversation(dialogText);
                }
                else
                {
                    EndConversation();
                    return;
                }
            }

            if(!isTyping)
            {
                p = paragraphs.Dequeue();
                typeDialogCoroutine = StartCoroutine(TypeDialogText(p));
            }

            if(paragraphs.Count == 0)
            {
                conversationEnded = true;
            }

        }

        private void StartConversation(DialogText dialogText)
        {
            if(gameObject.activeSelf == false)
            {
                gameObject.SetActive(true);
            }

            NPCNameText.text = dialogText.speakerName;

            for(int i = 0; i < dialogText.paragraphs.Length; i++)
            {
                paragraphs.Enqueue(dialogText.paragraphs[i]);
            }

        }

        private void EndConversation()
        {
            paragraphs.Clear();
            conversationEnded = false;

            if(gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }

        }

        private IEnumerator TypeDialogText(string p)
        {
            isTyping = true;

            DialogText.text = "";

            string originalText = p;
            string dislayedText = "";
            int alphaIndex = 0;

            foreach (char c in p.ToCharArray())
            {
                alphaIndex++;
                DialogText.text = originalText;
                dislayedText = DialogText.text.Insert(alphaIndex, htmlAlpha);
                DialogText.text = dislayedText;

                yield return new WaitForSeconds(maxTypeTime / typeSpeed);
            }

            isTyping = false;
        }


    }
}   