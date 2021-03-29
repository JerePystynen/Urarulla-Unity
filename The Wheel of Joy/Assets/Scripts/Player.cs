using System;
using UnityEngine;

namespace Urarulla
{
    [Serializable]
    public class Player
    {
        public string name;
        public GameObject model;

        public Player(string name, GameObject model)
        {
            this.name = name;
            this.model = model;
        }

        public int score;
    }
}