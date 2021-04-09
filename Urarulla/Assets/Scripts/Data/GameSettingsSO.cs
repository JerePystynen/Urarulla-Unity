using UnityEngine;

namespace Urarulla
{
    [CreateAssetMenu(menuName="Urarulla/GameSettings")]
    public class GameSettingsSO : ScriptableObject
    {
        private enum State { Singleplayer, Multiplayer }
        [SerializeField] private State gameBuild;
        /// <summary>
        /// If true, multiple people can play together.
        /// If false, people doesn't get connected to the cloud host.
        /// </summary>
        [HideInInspector] public bool UseMultiplayer => gameBuild == State.Multiplayer;
    }
}
