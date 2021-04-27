using UnityEngine;
using TMPro;

namespace DiMe.Urarulla
{
    public class CreatorConfidence : MonoBehaviour
    {
        private string[] s = {
            "Normi",
            "Ujo",
            "Ylimieli",
        };

        private void Start()
        {
            var dropdown = transform.Find("dropdown").GetComponent<TMP_Dropdown>();
            dropdown.ClearOptions();
            dropdown.onValueChanged.AddListener(delegate { Set(dropdown.value); });
        }

        private void Set(int index)
        {
            Debug.Log("set confidence to " + s[index]);
        }
    }
}
