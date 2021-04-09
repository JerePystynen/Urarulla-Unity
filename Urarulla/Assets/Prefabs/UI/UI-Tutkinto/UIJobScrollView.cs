using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Urarulla
{
    public class UIJobScrollView : MonoBehaviour
    {
        private ScrollRect scrollRect;
        private RectTransform viewportContent;
        [SerializeField] private GameObject textElement;
        
        private void Awake()
        {
            scrollRect = GetComponent<ScrollRect>();
            viewportContent = scrollRect.transform.Find("Viewport/Content").GetComponent<RectTransform>();
        }

        internal void ClearScrollView()
        {
            foreach (Transform element in viewportContent)
                Destroy(element.gameObject);
        }

        internal void AddPost(string input, string url)
        {
            var height = 100f;
            var yPos = height * viewportContent.childCount;
            viewportContent.sizeDelta = new Vector2(viewportContent.sizeDelta.x, yPos + height);
            var element = Instantiate(textElement, viewportContent).GetComponent<RectTransform>();
            element.anchoredPosition = new Vector2(0, -height / 2 - yPos);
            element.Find("txt").GetComponent<Button>().onClick.AddListener(delegate { OnLinkClicked(url); });
            var txt = element.Find("txt").GetComponent<TMP_Text>();
            txt.enabled = true;
            txt.text = input;
        }

        private void OnLinkClicked(string url)
        {
            Application.OpenURL(url);
        }
    }
}
