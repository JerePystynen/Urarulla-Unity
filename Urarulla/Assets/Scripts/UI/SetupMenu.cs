using UnityEngine;
using System.Linq;
using Cinemachine;

namespace DiMe.Urarulla
{
    public class SetupMenu : Singleton<SetupMenu>
    {
        internal new Camera camera { get; private set; }

        private SetupStands _stands;
        private CinemachineTargetGroup _targetGroup;

        private void Awake()
        {
            camera = GetComponentInChildren<Camera>();
            _stands = GetComponentInChildren<SetupStands>();
            _targetGroup = GetComponentInChildren<CinemachineTargetGroup>();
        }

        internal static Transform[] GetPlayerStands()
        {
            if (Instance._stands == null)
            {
                Debug.LogError("Error: _players is null!");
                return null;
            }
            return (from e in Instance._stands.players select e.transform).ToArray();
        }

        internal static void CreateNewPlayerSetupStand(string name = "") => Instance._stands.Create(name);

        internal static void RemovePlayerSetupStand(int index) => Instance._stands.Remove(index);

        internal void CenterCameraToStands()
        {
            for (var i = 0; i < _stands.players.Count; i++)
            {
                var stand = _stands.players[i];
                var target = _targetGroup.m_Targets[i];
                target.weight = stand == target.target == stand ? 1 : 0;
            }
        }
    }
}