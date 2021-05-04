using UnityEngine;

namespace DiMe.Urarulla
{
    [CreateAssetMenu(menuName = "Urarulla/GameSettings")]
    public class GameSettingsSO : ScriptableObject
    {
        private enum State
        {
            Singleplayer_WebGL,
            Multiplayer_Exe
        }
        [Header("Local Multiplayer")]
        [SerializeField] private State gameBuild;
        /// <summary>
        /// If true, multiple people can play together.
        /// If false, people doesn't get connected to the cloud host.
        /// </summary>
        internal bool IsMultiplayer => gameBuild == State.Multiplayer_Exe;

        /// <summary>
        /// Use text-to-speech.
        /// </summary>
        [Header("Text-to-speech")]
        public bool UseTts = true;

        /// <summary>
        /// Debug.Log TTS' URL.
        /// </summary>
        public bool DebugTtsUrl;
    }
}