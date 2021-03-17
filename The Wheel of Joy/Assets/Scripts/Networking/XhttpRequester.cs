using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

namespace WheelOfJoy
{
    public class XhttpRequester : MonoBehaviour
    {
        public List<JobAdvert> mikkeliList = new List<JobAdvert>();
        public List<JobAdvert> helsinkiList = new List<JobAdvert>();

        public Ala[] alat = {
            new Ala("IT", 4),
        };

        private void Start()
        {
            GetData();
        }

        private void GetData() => StartCoroutine(GetDataCoroutine());
        private IEnumerator GetDataCoroutine()
        {
            for (int x = 0; x < 2; x++)
            {
                var stad = x switch
                {
                    0 => "Mikkeli",
                    1 => "Helsinki",
                    _ => ""
                };
                var ala = 4;
                var uri = $"https://paikat.te-palvelut.fi/tpt-api/tyopaikat.rss?alueet={stad}&valitutAmmattialat={ala}";

                using (var www = UnityWebRequest.Get(uri))
                {
                    yield return www.SendWebRequest();

                    if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.LogError(www.error);
                        yield break;
                    }
                    
                    var data = www.downloadHandler.text;
                    var xml = new XmlDocument();
                    xml.LoadXml(data);
                    var xmlList = xml.SelectNodes("rss/channel/item");

                    for (int i = 0; i < 10; i++)
                    {
                        if (i >= xmlList.Count) break;
                        var node = xmlList[i];
                        
                        var title = node.SelectSingleNode("title").InnerText;
                        var link = node.SelectSingleNode("link").InnerText.Split('?')[0];
                        if (string.IsNullOrEmpty(link)) continue;

                        switch (x)
                        {
                            case 0:
                                mikkeliList.Add(new JobAdvert(title, link));
                                break;
                            case 1:
                                helsinkiList.Add(new JobAdvert(title, link));
                                break;
                        }
                    }
                }
            }
        }
    }
}
