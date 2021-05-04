using System.Collections.Generic;
using UnityEngine;

namespace DiMe.Urarulla
{
    public class SetupStands : MonoBehaviour
    {
        public List<GameObject> players = new List<GameObject>();

        internal void Create(string name)
        {
            if (!GameManager.IsMultiplayer)
            {
                if (players.Count >= GameManager.MaxPlayerCount)
                {
                    Debug.Log("Note: All player stand slots are already filled.");
                    return;
                }
            }
            
            var target = transform.parent.Find("player-positions").GetChild(players.Count);
            var stand = Instantiate(GameManager.Data.PlayerSetupStand, target.position, target.rotation, transform).transform;
            stand.name = name == "" ? $"{players.Count}" : name;

            players.Add(stand.gameObject);

            SetupMenu.Instance.CenterCameraToStands();
        }

        internal void Remove(int index)
        {
            if (index >= players.Count)
            {
                Debug.Log("Error: index is more than the amount of players!");
                return;
            }
            var target = transform.GetChild(index).gameObject;
            players.Remove(target);
            Destroy(target);
        }
    }
}