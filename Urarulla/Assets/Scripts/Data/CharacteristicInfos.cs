namespace DiMe.Urarulla
{
    [System.Serializable]
    public struct CharacteristicInfos
    {
        public CharacteristicInfo[] characteristicInfos;
    }

    [System.Serializable]
    public struct CharacteristicInfo
    {
        public string type;
        public string name;
        public string description;
    }

    [System.Serializable]
    public struct CharacteristicSkills
    {
        public CharacteristicSkill[] characteristicSkills;
    }

    [System.Serializable]
    public struct CharacteristicSkill
    {
        public string name;
        public Characteristics characteristics;
    }
}