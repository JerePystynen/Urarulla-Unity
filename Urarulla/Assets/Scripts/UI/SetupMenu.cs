using UnityEngine;
using System.Linq;

namespace DiMe.Urarulla
{
    public class SetupMenu : Singleton<SetupMenu>
    {
        private PlayerSetupStands _stands;

        private void Awake()
        {
            _stands = GetComponentInChildren<PlayerSetupStands>();
        }

        internal static Transform[] GetPlayerPedestals()
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
    }
}