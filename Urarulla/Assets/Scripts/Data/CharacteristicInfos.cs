namespace DiMe.Urarulla
{
    /// <summary>
    /// Contains all info
    /// </summary>
    [System.Serializable]
    public struct CharacteristicInfos
    {
        public CharacteristicInfo[] info;
        public CharacteristicSkill[] skills;
    }

    /// <summary>
    /// Contains extra information about all of the characteristics that can player can have.
    /// </summary>
    [System.Serializable]
    public struct CharacteristicInfo
    {
        public string type;
        public string name;
        public string description;
    }

    /// <summary>
    /// A singular skill the player has worked on, done, or achieveed, like "built robots".
    /// </summary>
    [System.Serializable]
    public struct CharacteristicSkill
    {
        public string name;
        public Characteristics characteristics;
    }
}