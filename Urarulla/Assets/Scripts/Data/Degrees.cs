using System;

namespace DiMe.Urarulla
{
    /// <summary>
    /// Contains all degrees.
    /// </summary>
    [Serializable]
    public struct Degrees
    {
        public Degree[] degrees;
    }

    /// <summary>
    /// This is the job degree that each player is trying to find their favourite one.
    /// </summary>
    [Serializable]
    public struct Degree
    {
        public string id;
        public string ala;
        public string name;
        public Characteristics characteristics;
        public string description;
        public string[] requirements;
        
        [Serializable]
        public struct Employment
        {
            public int index;
            public string description;
            public int mikkeli_wage;
            public int helsinki_wage;
            public string[] links;
        }
        public Employment employment;

        public string[] links;
        public string[] videos;
        public string[] images;

        [Serializable]
        public struct Example
        {
            public string image;
            public string description;
        }
        public Example example;
    }
}