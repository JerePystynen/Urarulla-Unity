namespace DiMe.Urarulla
{
    /// <summary>
    /// Contains all info
    /// </summary>
    [System.Serializable]
    public struct CharacteristicInfos
    {
        public CharacteristicInfo[] characteristicInfos;
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
}