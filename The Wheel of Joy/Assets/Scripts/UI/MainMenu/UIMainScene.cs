using UnityEngine;
using UnityEngine.UI;

namespace Urarulla
{
    public class UIMainScene : Singleton<UIMainScene>
    {
        private GameObject questionsObj;
        private QuestionManager questionManager;

        private void Start()
        {
            questionsObj = transform.Find("questions").gameObject;
            questionManager = questionsObj.GetComponent<QuestionManager>();
            questionManager.Ready();
            questionsObj.SetActive(false);

            transform.Find("exit-btn").GetComponent<Button>().onClick.AddListener(delegate { Exit(); });
        }

        internal void AskQuestion(int index)
        {
            questionManager.AskRandom();
        }

        private void Exit()
        {
            GameManager.Instance.SetMenuActive(0);
        }
    }
}