using System;
using UnityEngine;

namespace Urarulla
{
    [Serializable]
    public class Player
    {
        public Player(string name)
        {
            this.name = name;
        }

        public string name;
        public GameObject model;

        public Personality personality = new Personality();
    }

    [Serializable]
    public class Personality
    {
        public int creativeScore;
        public int physicalScore;
        public int handyScore;
        public int teamScore;
        public int leaderScore;
        public int greedScore;
    }
}