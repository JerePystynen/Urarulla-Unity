using UnityEngine;
using UnityEngine.UI;

namespace DiMe.Urarulla
{
    public class UISetupMenu : MonoBehaviour
    {
        private GameObject _addPlayerBtn;
        private GameObject _readyBtn;

        private void Start()
        {
            _addPlayerBtn = transform.Find("add-player-btn").gameObject;
            _readyBtn = transform.Find("ready-btn").gameObject;

            _addPlayerBtn.GetComponent<Button>().onClick.AddListener(delegate { AddNewPlayer("PLAYER 2"); });
            _readyBtn.GetComponent<Button>().onClick.AddListener(delegate { GameManager.StartGame(); });
            
            foreach (var player in SetupMenu.GetPlayerPedestals())
            {
                var removeBtn = player.Find("");
                removeBtn.GetComponent<Button>().onClick.AddListener(delegate { RemoveAddedPlayer(player.name); });
            }

            SetMultiplayerSetup(GameManager.IsMultiplayer);
        }

        private void SetMultiplayerSetup(bool value)
        {
            _addPlayerBtn.SetActive(value);
        }

        internal static void EditPlayer() => GameManager.Instance.SetMenuActive(2);

        private void AddNewPlayer(string name) => GameManager.AddPlayer(name);

        private void RemoveAddedPlayer(string name) => GameManager.RemovePlayer(name);
    }
}