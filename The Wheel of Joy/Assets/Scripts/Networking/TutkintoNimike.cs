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
        
        public int middleWage;
        
        public List<Ad> mikkeliAds = new List<Ad>();
        public int mikkeliWage;
        public string mikkeliEmployment;

        public List<Ad> helsinkiAds = new List<Ad>();
        public int helsinkiWage;
        public string helsinkiEmployment;

        public TutkintoNimike(
            string name,
            string description,
            int middleWage,
            int mikkeliWage,
            string mikkeliEmployment,
            List<Ad> mikkeliAds,
            int helsinkiWage,
            string helsinkiEmployment,
            List<Ad> helsinkiAds
        ){
            this.name = name;
            this.description = description;
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