using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WheelOfJoy
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public LuckWheel luckWheel;

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
            Debug.Log("LOREM IPSUM DOLON SIT AMET");
        }

        private void Update()
        {

        }

        private void Test()
        {
            SceneManager.LoadScene("MainMenu");
        }

        internal void LoadMainMenuScene()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}