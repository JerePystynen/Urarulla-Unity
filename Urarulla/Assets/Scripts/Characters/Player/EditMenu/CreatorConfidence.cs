using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace DiMe.Urarulla
{
    public class CreatorConfidence : MonoBehaviour
    {
        private TMP_Dropdown dropdown;

        private string[] options = {
            "Normi",
            "Ujo",
            "Ylimieli",
        };

        private void Start()
        {
            dropdown = transform.Find("dropdown").GetComponent<TMP_Dropdown>();
            SetDropdown();
        }

        private void SetDropdown()
        {
            dropdown.ClearOptions();
            var list = new List<TMP_Dropdown.OptionData>();
            foreach (var str in options)
                list.Add(new TMP_Dropdown.OptionData(str));
            dropdown.AddOptions(list);
            dropdown.onValueChanged.AddListener(delegate { Set(dropdown.value); });
        }

        private void Set(int index)
        {
            Debug.Log("set confidence to " + options[index]);
        }
    }
}