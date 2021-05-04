using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DiMe.Urarulla
{
    public class DegreeUIHelper : MonoBehaviour
    {
        private TMP_Dropdown dropdown;
        private TMP_Text degreeTxt;
        private TMP_Text averageWageTxt;
        private TMP_Text degreeDescTxt;
        private TMP_Text requirementTxt;
        private TMP_Text mikkeliWageTxt;
        private TMP_Text mikkeliStatusTxt;
        private TMP_Text helsinkiWageTxt;
        private TMP_Text helsinkiStatusTxt;
        private UIJobScrollView[] scrollViews;

        private DegreeWebRequester requester;

        private void Start()
        {
            requester = GetComponent<DegreeWebRequester>();

            degreeTxt = transform.Find("degree-name-txt").GetComponent<TMP_Text>();
            dropdown = transform.Find("degree-dropdown").GetComponent<TMP_Dropdown>();

            var content = transform.Find("degree-overview-scrollview/Viewport/Content");
            averageWageTxt = content.Find("average-wage-txt").GetComponent<TMP_Text>();
            degreeDescTxt = content.Find("degree-description-txt").GetComponent<TMP_Text>();
            requirementTxt = content.Find("requirement-txt").GetComponent<TMP_Text>();

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
            foreach (var ala in requester.degreeList)
            {
                list.Add(new TMP_Dropdown.OptionData(ala.name));
            }
            dropdown.ClearOptions();
            dropdown.AddOptions(list);
            dropdown.onValueChanged.AddListener(delegate { requester.SetData(dropdown.value); });
        }

        internal void SetData(Degree degree)
        {
            degreeTxt.text = degree.name;
            averageWageTxt.text = $"Alan Keskipalkka: {degree.employment.middle_wage}€";
            degreeDescTxt.text = degree.employment.description;
            
            mikkeliWageTxt.text = $"Keskipalkka: {degree.employment.mikkeli_wage}€";
            mikkeliStatusTxt.text = degree.employment.mikkeli_employment_status;
            
            helsinkiWageTxt.text = $"Keskipalkka: {degree.employment.helsinki_wage}€";
            helsinkiStatusTxt.text = degree.employment.helsinki_employment_status;

            requirementTxt.text = "";
            if (degree.requirements != null)
                foreach (var vaatimus in degree.requirements)
                    requirementTxt.text += "\n➥ {vaatimus}";

            AddAds(degree.employment.mikkeli_ads, 0);
            AddAds(degree.employment.helsinki_ads, 1);
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
