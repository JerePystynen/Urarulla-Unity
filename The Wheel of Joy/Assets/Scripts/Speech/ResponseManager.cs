using System.Collections;
using TMPro;
using UnityEngine;

namespace Urarulla
{
    public class ResponseManager : Singleton<ResponseManager>
    {
        [SerializeField] private string textToSpeech;

        private Transform speechBubble;
        private TMP_Text speechTxt;

        private void Start()
        {
            speechBubble = transform.Find("speech-bubble");
            speechTxt = speechBubble.Find("dialogue-txt").GetComponent<TMP_Text>();

            Display(textToSpeech);
        }

        internal void Display(string input) => StartCoroutine(DisplayCoroutine(input));

        private IEnumerator DisplayCoroutine(string input)
        {
            speechBubble.gameObject.SetActive(true);
            speechTxt.text = "";

            var delay = new WaitForSeconds(.04f);
            foreach (var letter in input)
            {
                speechTxt.text = speechTxt.text + letter;
                yield return delay;
            }

            yield return new WaitForSeconds(1.6f);

            speechBubble.gameObject.SetActive(false);
        }
    }
}