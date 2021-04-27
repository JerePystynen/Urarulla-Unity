using System.Linq;
using UnityEngine;
using TMPro;

namespace DiMe.Urarulla
{
    public class UIAnswerManager : MonoBehaviour
    {
        private RectTransform[] _objs;
        private Answer[] _answers;
        private TMP_Text[] _texts;
        
        private QuestionManager manager;
        private bool _initialized;

        internal void SetAnswer(QuestionManager manager, Question question)
        {
            _answers = question.answers;

            if (!_initialized)
            {
                _initialized = true;
                this.manager = manager;

                _objs = new RectTransform[] {
                    transform.GetChild(0).GetComponent<RectTransform>(),
                    transform.GetChild(1).GetComponent<RectTransform>(),
                    transform.GetChild(2).GetComponent<RectTransform>(),
                    transform.GetChild(3).GetComponent<RectTransform>(),
                    transform.GetChild(4).GetComponent<RectTransform>(),
                    transform.GetChild(5).GetComponent<RectTransform>(),
                };

                _texts = (from e in _objs select e.GetComponentInChildren<TMP_Text>()).ToArray();
            }

            SetButtons(_answers);
        }

        private void SetButtons(Answer[] answers)
        {
            var count = Mathf.Clamp(answers.Length, 0, _objs.Length);
            
            for (int i = 0; i < _objs.Length; i++)
                _objs[i].gameObject.SetActive(i < count);

            for (int i = 0; i < _answers.Length; i++)
                _texts[i].text = answers[i].text;

            switch (count)
            {
                case 1:
                    _objs[0].anchoredPosition = new Vector2(0, 0);
                    break;
                case 2:
                    _objs[0].anchoredPosition = new Vector2(-120, 0);
                    _objs[1].anchoredPosition = new Vector2(120, 0);
                    break;
                case 3:
                    _objs[0].anchoredPosition = new Vector2(-120, 80);
                    _objs[1].anchoredPosition = new Vector2(120, 80);
                    _objs[2].anchoredPosition = new Vector2(0, 0);
                    break;
                case 4:
                    _objs[0].anchoredPosition = new Vector2(-120, 80);
                    _objs[1].anchoredPosition = new Vector2(120, 80);
                    _objs[2].anchoredPosition = new Vector2(-120, 0);
                    _objs[3].anchoredPosition = new Vector2(120, 0);
                    break;
                case 5:
                    _objs[0].anchoredPosition = new Vector2(-200, 80);
                    _objs[1].anchoredPosition = new Vector2(0, 80);
                    _objs[2].anchoredPosition = new Vector2(200, 80);
                    _objs[3].anchoredPosition = new Vector2(-120, 0);
                    _objs[4].anchoredPosition = new Vector2(120, 0);
                    break;
                case 6:
                    _objs[0].anchoredPosition = new Vector2(-200, 80);
                    _objs[1].anchoredPosition = new Vector2(0, 80);
                    _objs[2].anchoredPosition = new Vector2(200, 80);
                    _objs[3].anchoredPosition = new Vector2(-200, 0);
                    _objs[4].anchoredPosition = new Vector2(0, 0);
                    _objs[5].anchoredPosition = new Vector2(200, 0);
                    break;
            }
        }
        
        public void SelectAnswer(int index) => manager.SelectAnswer(_answers[index]);
    }
}