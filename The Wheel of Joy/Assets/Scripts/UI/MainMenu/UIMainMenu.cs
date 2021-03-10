using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WheelOfJoy
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
        }
    }
}