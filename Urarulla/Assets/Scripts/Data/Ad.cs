using System;
using UnityEngine;

namespace DiMe.Urarulla
{
    /// <summary>
    /// This is the singular job advertisement that contains basic information about the job.
    /// </summary>
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
