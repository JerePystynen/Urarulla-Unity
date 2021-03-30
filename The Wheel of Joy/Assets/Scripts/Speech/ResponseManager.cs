using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Urarulla
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

        internal void Display(string input, string type) => StartCoroutine(DisplayCoroutine(input, type));

        private IEnumerator DisplayCoroutine(string input, string type)
        {
            speechBubble.gameObject.SetActive(true);
            speechBubbleImg.sprite = type switch
            {
                "talk" => talkBubble,
                "yell" => yellBubble,
                "smug" => smugBubble,
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