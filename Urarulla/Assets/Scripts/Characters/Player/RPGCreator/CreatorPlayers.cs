using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urarulla
{
    public class CreatorPlayers : MonoBehaviour
    {
        public GameObject PlayerObj;
        public List<GameObject> players = new List<GameObject>();

        private void Start()
        {
        
        }

        internal void AddPlayer()
        {
            if (GameManager.CheckIfSingleplayer)
                return;
            if (players.Count > 0)
            {
                Debug.LogWarning("There is already 1 created player!");
                return;
            }
            Instantiate(PlayerObj);
        }
    }
}
