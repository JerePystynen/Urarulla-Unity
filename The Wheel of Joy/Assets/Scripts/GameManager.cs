using System.Collections.Generic;
using UnityEngine;

namespace Urarulla
{
    public class GameManager : Singleton<GameManager>
    {
        public LuckWheel luckWheel;
        public JsonData jsonData;

        Transform wheel;
        public bool isGameActive;

        internal MenusManager menusManager;
        internal void SetMenuActive(int index) => menusManager.SetMenu(index);

        public List<Player> players = new List<Player>();
        public Player currentTurnPlayer;

        private void Awake()
        {
            menusManager = GetComponent<MenusManager>();
        }

        private void Start()
        {
            wheel = GameObject.Find("Wheel of Luck").transform.GetChild(0);
        }

        public static void AddPlayer(string name)
        {
            Instance.players.Add(new Player($"Player {Instance.players.Count}"));
            Debug.Log("todo add a new player...");
        }

        public void NextTurn()
        {
            if (players.Count < 2) return;
            int index = System.Array.IndexOf(players.ToArray(), currentTurnPlayer) + 1;
            if (index >= players.Count - 1) index = 0;
            currentTurnPlayer = players[index];

            // if we do change the turn, check player's scores' sum:
            // if it's more than 20, that means the player has answered enough questions for the game to know about the player

            if (GetPlayerPersonalityScoreSum(currentTurnPlayer.personality) > 20)
            {
                // move onto the next question section...


            }
        }

        private int GetPlayerPersonalityScoreSum(Personality personality)
        {
            return personality.creativeScore + personality.physicalScore + personality.leaderScore + personality.teamScore + personality.greedScore;
        }
    }
}