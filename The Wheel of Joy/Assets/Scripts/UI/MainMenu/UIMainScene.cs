using UnityEngine;
using UnityEngine.UI;

namespace Urarulla
{
    public class UIMainScene : Singleton<UIMainScene>
    {
        private GameObject questionsObj;
        internal QuestionManager questionManager;
        private UIStarChoice starChoice;

        private void Start()
        {
            questionsObj = transform.Find("questions").gameObject;
            questionManager = questionsObj.GetComponent<QuestionManager>();
            questionManager.Ready();
            questionsObj.SetActive(false);

            starChoice = transform.GetComponentInChildren<UIStarChoice>(true);

            transform.Find("exit-btn").GetComponent<Button>().onClick.AddListener(delegate { Exit(); });
        }

        internal void AskQuestion(int index)
        {
            if (index == 0)
            {
                // Debug.Log("asking a very difficult question...");
                // questionManager.
            }
            else if (index == 4)
            {
                starChoice.Get();
            }
            else
            {
                questionManager.AskRandomOminaisuus();
            }
        }

        private void Exit()
        {
            GameManager.Instance.SetMenuActive(0);
        }
    }
}