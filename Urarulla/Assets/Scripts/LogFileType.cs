using System.Linq;
using UnityEngine;

namespace DiMe.Urarulla
{
    public class LogFileType : MonoBehaviour
    {
        void Start()
        {
            string url = @"http://example.com/file.jpg?par=x";
            string ext = GetFileExtensionFromUrl(url);
            Debug.Log(ext);
        }

        public string GetFileExtensionFromUrl(string url)
        {
            url = url.Split('?')[0];
            url = url.Split('/').Last();
            return url.Contains('.') ? url.Substring(url.LastIndexOf('.')) : "";
        }
    }
}
