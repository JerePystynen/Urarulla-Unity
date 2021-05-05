using UnityEngine;
using UnityEngine.UI;

namespace DiMe.Urarulla
{
    public class UISetupMenu : MonoBehaviour
    {
        private GameObject _readyBtn;

        private void Start()
        {
            _readyBtn = transform.Find("ready-btn").gameObject;

            _readyBtn.GetComponent<Button>().onClick.AddListener(delegate { GameManager.StartGame(); });
            
            foreach (var player in SetupMenu.GetPlayerStands())
            {
                var removeBtn = player.Find("");
                removeBtn.GetComponent<Button>().onClick.AddListener(delegate { RemoveAddedPlayer(player.name); });
            }
        }

        internal static void EditPlayer() => GameManager.Instance.SetMenuActive(2);

        private void AddNewPlayer(string name) => GameManager.AddPlayer(name);

        private void RemoveAddedPlayer(string name) => GameManager.RemovePlayer(name);
    }
}