using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiMe.Urarulla
{
    public class ResponseManager : Singleton<ResponseManager>
    {
        private Transform speechBubble;
        private Image speechBubbleImg;
        private TMP_Text speechTxt;

        [SerializeField] private Sprite talkBubble;
        [SerializeField] private Sprite yellBubble;
        [SerializeField] private Sprite smugBubble;

        private void Start()
        {
            speechBubble = transform.Find("speech-bubble");
            speechBubbleImg = speechBubble.GetComponent<Image>();
            speechTxt = speechBubble.Find("dialogue-txt").GetComponent<TMP_Text>();
            speechBubble.gameObject.SetActive(false);
        }

        internal void Speak(string input, SpeechType type = SpeechType.talk) => StartCoroutine(SpeakCoroutine(input, type));

        private IEnumerator SpeakCoroutine(string input, SpeechType type)
        {
            if (input.Length == 0) yield break;

            speechBubble.gameObject.SetActive(true);
            speechBubbleImg.sprite = type switch
            {
                SpeechType.talk => talkBubble,
                SpeechType.yell => yellBubble,
                SpeechType.smug => smugBubble,
                _ => null
            };

            speechTxt.text = "";
            var delay = new WaitForSeconds(.04f);
            foreach (var letter in input)
            {
                speechTxt.text = speechTxt.text + letter;
                yield return delay;
            }

            yield return new WaitForSeconds(1.6f);
            speechBubble.gameObject.SetActive(false);
            GameManager.Instance.NextTurn();
        }
    }
}