using UnityEngine;
using UnityEngine.UI;

namespace WheelOfJoy
{
    public class UIMainScene : MonoBehaviour
    {
        private void Start()
        {
            transform.Find("exit-btn").GetComponent<Button>().onClick.AddListener(delegate { Exit(); });
        }

        private void Exit()
        {
            GameManager.Instance.SetMenuActive(0);
        }
    }
}