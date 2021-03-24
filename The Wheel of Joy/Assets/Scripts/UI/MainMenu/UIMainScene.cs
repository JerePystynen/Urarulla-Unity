using UnityEngine;
using UnityEngine.UI;

namespace Urarulla
{
    public class UIMainScene : Singleton<UIMainScene>
    {
        private void Start()
        {
            transform.Find("exit-btn").GetComponent<Button>().onClick.AddListener(delegate { Exit(); });
        }

        public void AskQuestion()
        {
            Debug.Log("asking a question...");
        }

        private void Exit()
        {
            GameManager.Instance.SetMenuActive(0);
        }
    }
}