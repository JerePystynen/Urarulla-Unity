namespace Urarulla
{
    [System.Serializable]
    public struct Yleisominaisuudet
    {
        public Yleisominaisuus[] yleisominaisuudet;
    }

    [System.Serializable]
    public struct Yleisominaisuus
    {
        public string type;
        public string name;
    }
}