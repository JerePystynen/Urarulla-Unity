using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urarulla
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public LuckWheel luckWheel;
        public JsonData jsonData;

        Transform wheel;
        public bool isGameActive;

        internal MenusManager menusManager;
        internal void SetMenuActive(int index) => menusManager.SetMenu(index);

        public List<Player> players = new List<Player>();

        private void Awake()
        {
            // create singleton instance
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            menusManager = GetComponent<MenusManager>();
        }

        private void Start()
        {
            wheel = GameObject.Find("Wheel of Luck").transform.GetChild(0);
        }

        private void Update()
        {
        }

        public static void AddPlayer(string name)
        {
            Debug.Log("todo add a new player...");
        }
    }
}