using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;

namespace DiMe.Urarulla
{
    public class DegreeWebRequester : MonoBehaviour
    {
        public List<DegreeClean> degreeList = new List<DegreeClean>();
        private int _ilmoitusCount = 6;

        private DegreeUIHelper helper;

        private void Start()
        {
            helper = GetComponent<DegreeUIHelper>();
        }

        internal void SetData(int index)
        {
            degreeList.Clear();
            GetData(degree: GameManager.Instance.degrees.degrees[index]);
        }

        private void GetData(Degree degree) => StartCoroutine(GetDataCoroutine(degree));

        private IEnumerator GetDataCoroutine(Degree degree)
        {
            var mikkeliAdverts = new List<Ad>();
            var helsinkiAdverts = new List<Ad>();

            for (int x = 0; x < 2; x++)
            {
                var stad = x switch
                {
                    0 => "Mikkeli",
                    1 => "Helsinki",
                    _ => ""
                };
                var uri = $"https://paikat.te-palvelut.fi/tpt-api/tyopaikat.rss?alueet={stad}&valitutAmmattialat={degree.employment.index}";

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

                    for (int i = 0; i < _ilmoitusCount; i++)
                    {
                        if (i >= xmlList.Count) break;
                        var ad = GetAd(xmlList[i]);

                        // Add to list
                        switch (x)
                        {
                            case 0:
                                mikkeliAdverts.Add(ad);
                                break;
                            case 1:
                                helsinkiAdverts.Add(ad);
                                break;
                        }
                    }
                }
            }

            var ds = GameManager.Instance.degrees.degrees;
            Degree? target = new Degree();
            foreach (var d in ds)
                if (d.id == degree.name)
                {
                    target = d;
                    break;
                }

            if (target == null) yield break;
            var dt = (Degree)target;

            Debug.Log(degree.name + " | " + dt.name);

            SetTutkintoData(degree, mikkeliAdverts, helsinkiAdverts);
        }

        private Ad GetAd(XmlNode node)
        {
            var title = node.SelectSingleNode("title").InnerText;
            var advertData = node.SelectSingleNode("description").InnerText;
            var pieces = advertData.Split(new string[] { "<br>" }, System.StringSplitOptions.None);
            var description = pieces[1];

            var _date = node.SelectSingleNode("pubDate").InnerText;
            var date = DateTime.Parse(string.Format("{0:d}", _date));

            var link = node.SelectSingleNode("link").InnerText.Split('?')[0];
            if (string.IsNullOrEmpty(link)) return null;

            return new Ad(title, description, date, link);
        }

        private void SetTutkintoData(Degree degree, List<Ad> mikkeliAds, List<Ad> helsinkiAds)
        {
            var d = new DegreeClean(
                degree.name,
                degree.description,
                degree.requirements,
                // Wage
                GetAverage(degree.employment.mikkeli_wage, degree.employment.helsinki_wage),
                // Mikkeli
                mikkeliAds,
                degree.employment.mikkeli_wage,
                GetJobEmploymentStatus(mikkeliAds),
                // Helsinki
                helsinkiAds,
                degree.employment.helsinki_wage,
                GetJobEmploymentStatus(helsinkiAds)
            );
            degreeList.Add(d);
            helper.SetData(d);
        }

        private int GetAverage(params int[] nums)
        {
            var total = 0;
            foreach (var num in nums)
            {
                total += num;
            }
            return Mathf.RoundToInt((float)total / nums.Length);
        }

        private string[] _popularityStatuses = {
            "Töitä on hankala löytää tällä alalla.",
            "Töitä on siellä täällä saatavilla tällä alalla.",
            "Varmasti pääset töihin tällä alalla.",
            "Töitä löytyy hyvin tällä alalla.",
            "Sinut tullaan hakemaan tällä alalla.",
        };

        private string GetJobEmploymentStatus(List<Ad> ilmoitukset)
        {
            // katso ilmoituksista milloin ilmoitus on luotu ja jos ilmoituksia on luotu ja
            // jos ne ovat vanhentuneita (>3 päivää) => se tarkoittaa että "työvoimapulaa".

            var popularityScore = 0;
            if (ilmoitukset.Count > 3)
            {
                if (ilmoitukset.Count < 5) popularityScore += 2;
                else popularityScore += 5;
            }
            else popularityScore = ilmoitukset.Count;

            popularityScore -= (int)Mathf.Max((float)GetAverage((from ilmoitus in ilmoitukset
                                                                 select Convert.ToInt32((DateTime.UtcNow.Date - ilmoitus.date).TotalDays)).ToArray()) / 4);

            int score = Mathf.Clamp(popularityScore, 0, _popularityStatuses.Length - 1);
            return $"{score + 1}/5: {_popularityStatuses[score]}";
        }
    }
}