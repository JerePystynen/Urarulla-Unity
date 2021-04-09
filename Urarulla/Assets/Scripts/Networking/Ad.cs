using System;
using UnityEngine;

namespace Urarulla
{
    [System.Serializable]
    public class Ad
    {
        public string title;
        
        [TextArea]
        public string description;
        
        public string url;
        
        public DateTime date;

        public Ad(string title, string description, DateTime date, string url)
        {
            this.title = title;
            this.description = description;
            this.date = date;
            this.url = url;
        }
    }
}
