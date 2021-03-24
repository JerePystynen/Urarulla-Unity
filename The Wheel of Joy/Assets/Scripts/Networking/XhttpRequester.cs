using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Linq;

namespace Urarulla
{
    public class XhttpRequester : MonoBehaviour
    {
        public TextAsset jsonTutkintoFile;
        public List<TutkintoNimike> tutkintoLista = new List<TutkintoNimike>();
        private int _ilmoitusCount = 6;

        public TutkintoUIHelper uiHelper;

        public static Ala
            Ajoneuvoasentaja = new Ala("Ajoneuvoasentaja", -1),
            Artesaani = new Ala("Artesaani", -1),
            IT_Tukihenkilo = new Ala("IT-Tukihenkilö", 4),
            Tietoverkkoasentaja = new Ala("Tietoverkkoasentaja", 4),
            Ohjelmistokehittaja = new Ala("Ohjelmistokehittäjä", 4),
            Merkonomi = new Ala("Merkonomi", -1),
            Kokki = new Ala("Kokki", -1),
            Sahkoasentaja = new Ala("Sähköasentaja", -1),
            Lahihoitaja = new Ala("Lähihoitaja", -1),
            Talonrakentaja = new Ala("Talonrakentaja", -1),
            Putkiasentaja = new Ala("Putkiasentaja", -1),
            Metsakoneenkuljettaja = new Ala("Metsäkoneenkuljettaja", -1)
        ;

        public Ala[] alat = {
            Ajoneuvoasentaja,
            Artesaani,
            IT_Tukihenkilo,
            Tietoverkkoasentaja,
            Ohjelmistokehittaja,
            Merkonomi,
            Kokki,
            Sahkoasentaja,
            Lahihoitaja,
            Talonrakentaja,
            Putkiasentaja,
            Metsakoneenkuljettaja
        };

        internal void SetData(int index)
        {
            tutkintoLista.Clear();
            GetData(alat[index]);
        }

        private void GetData(Ala ala) => StartCoroutine(GetDataCoroutine(ala));

        private IEnumerator GetDataCoroutine(Ala ala)
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
                var uri = $"https://paikat.te-palvelut.fi/tpt-api/tyopaikat.rss?alueet={stad}&valitutAmmattialat={ala.index}";

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

            var tutkinnotNullable = (Tutkinnot?)JsonUtility.FromJson<Tutkinnot>(jsonTutkintoFile.text);
            if (tutkinnotNullable == null)
            {
                Debug.LogError("Error: JSON data could not be loaded.");
                yield break;
            }
            var tutkinnot = ((Tutkinnot)tutkinnotNullable).tutkinnot;


            Tutkinto? target = new Tutkinto();
            foreach (var _tutkinto in tutkinnot)
                if (_tutkinto.id == ala.name)
                {
                    target = _tutkinto;
                    break;
                }
            
            if (target == null) yield break;
            var tutkinto = (Tutkinto)target;
            
            Debug.Log(ala.name + " | " + tutkinto.nimi);

            SetTutkintoData(tutkinto, mikkeliAdverts, helsinkiAdverts);
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

        private void SetTutkintoData(Tutkinto tutkinto, List<Ad> mikkeliAds, List<Ad> helsinkiAds)
        {
            var mikkeliWage = tutkinto.työtilanne.mikkeli.keskipalkka;
            var mikkeliEmployment = GetJobEmploymentStatus(mikkeliAds);

            var helsinkiWage = tutkinto.työtilanne.helsinki.keskipalkka;
            var helsinkiEmployment = GetJobEmploymentStatus(helsinkiAds);

            var middleWage = GetAverage(mikkeliWage, helsinkiWage);

            var nimike = new TutkintoNimike(
                tutkinto.nimi,
                tutkinto.kuvaus,
                middleWage,
                mikkeliWage,
                mikkeliEmployment,
                mikkeliAds,
                helsinkiWage,
                helsinkiEmployment,
                helsinkiAds
            );
            
            tutkintoLista.Add(nimike);
            uiHelper.SetData(nimike);
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