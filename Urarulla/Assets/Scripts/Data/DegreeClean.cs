using System.Collections.Generic;
using UnityEngine;

namespace DiMe.Urarulla
{
    [System.Serializable]
    public class DegreeClean
    {
        public string name;
        [TextArea] public string description;
        public string[] requirements;

        public int middle_wage;

        public List<Ad> mikkeliAds = new List<Ad>();
        public int mikkeli_wage;
        public string mikkeli_employment_status;

        public List<Ad> helsinkiAds = new List<Ad>();
        public int helsinki_wage;
        public string helsinki_employment_status;

        public string[] links;
        public string[] videos;
        public string[] images;

        public DegreeClean(
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
            this.requirements = vaatimukset;
            this.middle_wage = middleWage;
            this.mikkeliAds = mikkeliAds;
            this.mikkeli_wage = mikkeliWage;
            this.mikkeli_employment_status = mikkeliEmployment;
            this.helsinkiAds = helsinkiAds;
            this.helsinki_wage = helsinkiWage;
            this.helsinki_employment_status = helsinkiEmployment;
        }
    }
}