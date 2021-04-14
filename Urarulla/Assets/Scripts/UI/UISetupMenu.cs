using UnityEngine;
using UnityEngine.UI;

namespace Urarulla
{
    public class UISetupMenu : MonoBehaviour
    {
        private GameObject _addPlayerBtn;
        private GameObject _readyBtn;

        private void Start()
        {
            _addPlayerBtn = transform.Find("add-player-btn").gameObject;
            _readyBtn = transform.Find("ready-btn").gameObject;

            _addPlayerBtn.GetComponent<Button>().onClick.AddListener(delegate { AddNewPlayer(); });
            _readyBtn.GetComponent<Button>().onClick.AddListener(delegate { GameManager.StartGame(); });
            
            SetMultiplayerSetup(GameManager.UseMultiplayer);
        }

        private void SetMultiplayerSetup(bool value)
        {
            _addPlayerBtn.SetActive(value);
        }

        internal static void EditPlayer() => GameManager.Instance.SetMenuActive(2);

        private void AddNewPlayer() => GameManager.AddPlayer("PLAYER 2");
    }
}