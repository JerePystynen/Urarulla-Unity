using System;
using System.Collections.Generic;

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

            public int middle_wage;
            public int mikkeli_wage;
            public int helsinki_wage;

            public List<Ad> mikkeli_ads;
            public string mikkeli_employment_status;

            public List<Ad> helsinki_ads;
            public string helsinki_employment_status;

            public string[] links;

            public Employment(
                int index,
                string description,
                int middle_wage,
                int mikkeli_wage,
                int helsinki_wage,
                List<Ad> mikkeli_ads,
                string mikkeli_employment_status,
                List<Ad> helsinki_ads,
                string helsinki_employment_status,
                string[] links)
            {
                this.index = index;
                this.description = description;
                this.middle_wage = middle_wage;
                this.mikkeli_wage = mikkeli_wage;
                this.helsinki_wage = helsinki_wage;
                this.mikkeli_ads = mikkeli_ads;
                this.mikkeli_employment_status = mikkeli_employment_status;
                this.helsinki_ads = helsinki_ads;
                this.helsinki_employment_status = helsinki_employment_status;
                this.links = links;
            }
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

            public Example(
                string image,
                string description)
            {
                this.image = image;
                this.description = description;
            }
        }
        public Example example;

        public Degree(
            string id,
            string ala,
            string name,
            Characteristics characteristics,
            string description,
            string[] requirements,
            Employment employment,
            string[] links,
            string[] videos,
            string[] images,
            Example example)
        {
            this.id = id;
            this.ala = ala;
            this.name = name;
            this.characteristics = characteristics;
            this.description = description;
            this.requirements = requirements;
            this.employment = employment;
            this.links = links;
            this.videos = videos;
            this.images = images;
            this.example = example;
        }
    }
}