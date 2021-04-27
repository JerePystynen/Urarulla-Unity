using System.Collections.Generic;
using UnityEngine;

namespace DiMe.Urarulla
{
    public class PlayerSetupStands : MonoBehaviour
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
            Instantiate(GameManager.Data.PlayerSetupStand);
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