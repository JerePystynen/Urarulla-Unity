using System;

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
        public ModelShape shape = new ModelShape();
        public Characteristics characteristics = new Characteristics();
    }

    [Serializable]
    public class ModelShape
    {
        public ModelPart[] parts;
    }

    [Serializable]
    public class ModelPart
    {
        public ModelPart(string name, string name_fi)
        {
            this.name = name;
            this.name_fi = name_fi;
        }
        public string name;
        public string name_fi;
        public float distortion;
    }

    [Serializable]
    public class Characteristics
    {
        public int creativeScore;
        public int physicalScore;
        public int handyScore;
        public int teamScore;
        public int leaderScore;
        public int greedScore;
    }
}