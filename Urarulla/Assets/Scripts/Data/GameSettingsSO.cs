using UnityEngine;

namespace DiMe.Urarulla
{
    [CreateAssetMenu(menuName = "Urarulla/GameSettings")]
    public class GameSettingsSO : ScriptableObject
    {
        // multiplayer
        private enum State { Singleplayer, Multiplayer }
        [SerializeField] private State gameBuild;
        /// <summary>
        /// If true, multiple people can play together.
        /// If false, people doesn't get connected to the cloud host.
        /// </summary>
        internal bool IsMultiplayer => gameBuild == State.Multiplayer;

        // url debugging
        public bool DebugTtsUrl;
    }
}