using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfJoy
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public LuckWheel luckWheel;
        public JsonData jsonData;

        Transform wheel;
        public bool isGameActive;

        private void Awake()
        {
            // create singleton instance
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            wheel = GameObject.Find("Wheel of Luck").transform.GetChild(0);
        }

        private void Update()
        {
            wheel.Rotate(new Vector3(0f, 0f, -8f), Space.Self);
        }
    }
}