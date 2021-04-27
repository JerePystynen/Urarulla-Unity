using UnityEngine;
using UnityEngine.UI;

namespace DiMe.Urarulla
{
    public class UIMainMenu : MonoBehaviour
    {
        private Transform _startBtn;

        private void Start()
        {
            _startBtn = transform.Find("start-btn");
            _startBtn.GetComponent<Button>().onClick.AddListener(delegate{ StartButton(); });
        }
    
        private void StartButton()
        {
            if (GameManager.Players.Count == 0)
            {
                GameManager.AddPlayer($"Player {GameManager.Instance.players.Count}");
            }
            else
            {
                // foreach ()
                // {
                // }
            }

            GameManager.Instance.SetMenuActive(1);
        }
    }
}