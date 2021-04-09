using System.Collections.Generic;
using UnityEngine;

namespace Urarulla
{
    [System.Serializable]
    public class TutkintoNimike
    {
        public string name;

        [TextArea]
        public string description;

        public string[] vaatimukset;

        public int middleWage;

        public List<Ad> mikkeliAds = new List<Ad>();
        public int mikkeliWage;
        public string mikkeliEmployment;

        public List<Ad> helsinkiAds = new List<Ad>();
        public int helsinkiWage;
        public string helsinkiEmployment;

        public string[] kuvat;
        public string[] videot;
        public string[] lisätietoa;

        public TutkintoNimike(
            string name,
            string description,
            string[] vaatimukset,
            int middleWage,
            List<Ad> mikkeliAds,
            int mikkeliWage,
            string mikkeliEmployment,
            List<Ad> helsinkiAds,
            int helsinkiWage,
            string helsinkiEmployment)
        {
            this.name = name;
            this.description = description;
            this.vaatimukset = vaatimukset;
            this.middleWage = middleWage;
            this.mikkeliAds = mikkeliAds;
            this.mikkeliWage = mikkeliWage;
            this.mikkeliEmployment = mikkeliEmployment;
            this.helsinkiAds = helsinkiAds;
            this.helsinkiWage = helsinkiWage;
            this.helsinkiEmployment = helsinkiEmployment;
        }
    }
}