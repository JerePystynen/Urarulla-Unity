using System;

namespace DiMe.Urarulla
{
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
    public class ModelShape
    {
        public ModelPart[] parts;
    }
}