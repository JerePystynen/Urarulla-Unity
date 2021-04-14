using UnityEngine;
using UnityEngine.UI;

namespace Urarulla
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
            GameManager.Instance.SetMenuActive(1);
            GameManager.Instance.players.Add(new Player($"Player {GameManager.Instance.players.Count}"));
        }
    }
}