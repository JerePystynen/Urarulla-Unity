using System.Collections.Generic;
using UnityEngine;

namespace Urarulla
{
    public class GameManager : Singleton<GameManager>
    {
    // Data
        public GameDataSO gameData;
        public static GameDataSO GameData => Instance?.gameData;
        public static GameObject RandomCharacterModel => Instance.gameData.CharacterModels.Random();

        public GameSettingsSO gameSettings;
        internal static bool UseMultiplayer => Instance.gameSettings.UseMultiplayer;
        internal static bool CheckIfSingleplayer
        {
            get
            {
                if (!UseMultiplayer)
                {
                    Debug.LogWarning("This is a single-player game build. There cannot be more than one player at a time.");
                    return true;
                }
                return false;
            }
        }

        public Ala alat;
        public Kysymykset kysymykset;
        public Tutkinnot tutkinnot;
        public Yleisominaisuudet yleisominaisuudet;

    // References
        public LuckWheel luckWheel;

        [Space]
        Transform wheel;
        public bool isGameActive;

        internal MenusManager menusManager;
        internal void SetMenuActive(int index) => menusManager.SetMenu(index);

        public List<Player> players = new List<Player>();
        public static List<Player> Players => Instance?.players;
        public Player currentTurnPlayer;
        public static int currentTurnPlayerIndex { get; private set; }

        internal static ResponseManager responseManager => ResponseManager.Instance;
        internal static TextToSpeech textToSpeech { get; private set; }

        private void Awake()
        {
            menusManager = GetComponent<MenusManager>();

            // cache all json files
            alat = GetData<Ala>(gameData.AlaFile);
            kysymykset = GetData<Kysymykset>(gameData.KysymyksetFile);
            tutkinnot = GetData<Tutkinnot>(gameData.TutkinnotFile);
            yleisominaisuudet = GetData<Yleisominaisuudet>(gameData.YleisominaisuudetFile);

            textToSpeech = GetComponentInChildren<TextToSpeech>(true);
        }

        private void Start()
        {
            wheel = GameObject.Find("Wheel of Luck").transform.GetChild(0);
        }

        private T GetData<T>(TextAsset txt)
        {
            var json = JsonUtility.FromJson<T>(txt.text);
            if (json == null)
            {
                Debug.LogError($"Error: {txt.name} JSON could not be loaded.");
                return default(T);
            }
            return json;
        }

        internal static void StartGame()
        {
            Instance.SetMenuActive(3);
            UIMainScene.Instance.playerScoreManager.Ready();
        }

        public static void AddPlayer(string name)
        {
            if (CheckIfSingleplayer) return;
            Instance.players.Add(new Player($"Player {Instance.players.Count}"));
            Debug.Log("todo add a new player...");
        }

        public void NextTurn()
        {
            if (players.Count < 2) return;
            int index = System.Array.IndexOf(players.ToArray(), currentTurnPlayer) + 1;
            if (index >= players.Count - 1) index = 0;
            currentTurnPlayer = players[index];
            currentTurnPlayerIndex = System.Array.IndexOf(players.ToArray(), currentTurnPlayer);

            // if we do change the turn, check player's scores' sum:
            // if it's more than 20, that means the player has answered enough questions for the game to know about the player

            if (GetPlayerPersonalityScoreSum(currentTurnPlayer.characteristics) > 20)
            {
                // move onto the next question section...


            }
        }

        private int GetPlayerPersonalityScoreSum(Characteristics personality)
        {
            return personality.creativeScore + personality.physicalScore + personality.leaderScore + personality.teamScore + personality.greedScore;
        }
    }
}