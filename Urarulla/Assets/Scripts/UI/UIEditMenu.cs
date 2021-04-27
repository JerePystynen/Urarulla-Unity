using UnityEngine;
using UnityEngine.UI;

namespace DiMe.Urarulla
{
    public class UIEditMenu : MonoBehaviour
    {
        private void Start()
        {
            transform.Find("ready-btn").GetComponent<Button>().onClick.AddListener(delegate { DoneEdit(); });
        }

        private void DoneEdit()
        {
            GameManager.Instance.SetMenuActive(1);
        }
    }
}