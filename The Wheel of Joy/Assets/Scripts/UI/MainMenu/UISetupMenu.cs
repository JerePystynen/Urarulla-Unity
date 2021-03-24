using UnityEngine;
using UnityEngine.UI;

namespace Urarulla
{
    public class UISetupMenu : MonoBehaviour
    {
        private void Start()
        {
            transform.Find("edit-player-btn").GetComponent<Button>().onClick.AddListener(delegate { EditPlayer(); });
            transform.Find("add-player-btn").GetComponent<Button>().onClick.AddListener(delegate { AddNewPlayer(); });
            transform.Find("ready-btn").GetComponent<Button>().onClick.AddListener(delegate { Ready(); });
        }

        private void EditPlayer()
        {
            GameManager.Instance.SetMenuActive(2);
        }

        private void AddNewPlayer()
        {
            Debug.Log("todo: add a new player");
        }
        
        private void Ready()
        {
            GameManager.Instance.SetMenuActive(3);
        }
    }
}