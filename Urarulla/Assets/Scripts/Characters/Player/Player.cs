using System;
using UnityEngine;

namespace DiMe.Urarulla
{
    [Serializable]
    public class Player
    {
        public Player(string name)
        {
            this.name = name;
        }

        [Header("Info")]
        public string name;
        public ModelShape shape = new ModelShape();
        public Characteristics characteristics = new Characteristics();
        
        [Header("Stands")]
        public Transform setupStand;
        public Transform normalStand;

        [Header("Gameplay")]
        [Range(0f, 1f)] public float scoreProgress;
    }
}