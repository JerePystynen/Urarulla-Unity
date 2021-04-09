using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Urarulla
{
    public class TutkintoUIHelper : MonoBehaviour
    {
        private TMP_Dropdown dropdown;
        private TMP_Text tutkintoTxt;
        private TMP_Text averageWageTxt;
        private TMP_Text tutkintoDescTxt;
        private TMP_Text vaatimusTxt;
        private TMP_Text mikkeliWageTxt;
        private TMP_Text mikkeliStatusTxt;
        private TMP_Text helsinkiWageTxt;
        private TMP_Text helsinkiStatusTxt;
        private UIJobScrollView[] scrollViews;

        private TutkintoXhttpRequester requester;

        private void Start()
        {
            requester = GetComponent<TutkintoXhttpRequester>();

            tutkintoTxt = transform.Find("tutkinto-name-txt").GetComponent<TMP_Text>();
            dropdown = transform.Find("tutkinto-dropdown").GetComponent<TMP_Dropdown>();

            var content = transform.Find("tutkinto-overview-scrollview/Viewport/Content");
            averageWageTxt = content.Find("average-wage-txt").GetComponent<TMP_Text>();
            tutkintoDescTxt = content.Find("tutkinto-description-txt").GetComponent<TMP_Text>();
            vaatimusTxt = content.Find("vaatimus-txt").GetComponent<TMP_Text>();

            mikkeliWageTxt = transform.Find("mikkeli/wage-txt").GetComponent<TMP_Text>();
            mikkeliStatusTxt = transform.Find("mikkeli/status-txt").GetComponent<TMP_Text>();
            helsinkiWageTxt = transform.Find("helsinki/wage-txt").GetComponent<TMP_Text>();
            helsinkiStatusTxt = transform.Find("helsinki/status-txt").GetComponent<TMP_Text>();
            
            scrollViews = new UIJobScrollView[] {
                transform.Find("mikkeli/links-scrollview").GetComponent<UIJobScrollView>(),
                transform.Find("helsinki/links-scrollview").GetComponent<UIJobScrollView>(),
            };

            ClearScrollViews();
            SetAlaDropdown();
        }
        
        private void SetAlaDropdown()
        {
            if (dropdown == null) return;
            var list = new List<TMP_Dropdown.OptionData>();
            foreach (var ala in requester.alat)
            {
                list.Add(new TMP_Dropdown.OptionData(ala.name));
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(list);
            dropdown.onValueChanged.AddListener(delegate { requester.SetData(dropdown.value); });
        }

        internal void SetData(TutkintoNimike tutkinto)
        {
            tutkintoTxt.text = tutkinto.name;
            averageWageTxt.text = $"Alan Keskipalkka: {tutkinto.middleWage}€";
            tutkintoDescTxt.text = tutkinto.description;
            
            mikkeliWageTxt.text = $"Keskipalkka: {tutkinto.mikkeliWage}€";
            mikkeliStatusTxt.text = tutkinto.mikkeliEmployment;
            
            helsinkiWageTxt.text = $"Keskipalkka: {tutkinto.helsinkiWage}€";
            helsinkiStatusTxt.text = tutkinto.helsinkiEmployment;

            vaatimusTxt.text = "";
            if (tutkinto.vaatimukset != null)
                foreach (var vaatimus in tutkinto.vaatimukset)
                    vaatimusTxt.text += "\n➥ {vaatimus}";

            AddAds(tutkinto.mikkeliAds, 0);
            AddAds(tutkinto.helsinkiAds, 1);
        }

        private void ClearScrollViews()
        {
            if (scrollViews == null) return;
            if (scrollViews.Length == 0) return;
            foreach (var scroll in scrollViews)
                scroll.ClearScrollView();
        }

        private void AddAds(List<Ad> ads, int target)
        {
            foreach (var ad in ads)
                AddText($"{ad.title}", ad.url, target);
        }

        internal void AddText(string input, string url, string target)
        {
            switch (target)
            {
                case "mikkeli":
                    AddText(input, url, 0);
                    break;
                case "helsinki":
                    AddText(input, url, 1);
                    break;
            }
        }

        internal void AddText(string input, string url, int target) => scrollViews[target].AddPost(input, url);
    }
}
