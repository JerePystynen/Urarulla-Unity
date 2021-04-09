using System;

namespace Urarulla
{
    [Serializable]
    public struct Tutkinnot
    {
        public Tutkinto[] tutkinnot;
    }

    [Serializable]
    public struct Tutkinto
    {
        public string id;
        public string nimi;
        public string ala;
        public string[] erityisominaisuudet;
        public string kuvaus;
        public string[] työmahdollisuudet;
        public string[] vaatimukset;
        
        [Serializable]
        public struct Työtilanne
        {
            public string kuvaus;

            [Serializable]
            public struct Alue
            {
                public int keskipalkka;
            }
            public Alue mikkeli;
            public Alue helsinki;

            public string[] linkit;
        }
        public Työtilanne työtilanne;

        public string[] lisätietoa;
        public string[] videot;
        public string[] kuvat;
    }
}