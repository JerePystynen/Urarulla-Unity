using UnityEngine;
using UnityEngine.UI;

namespace DiMe.Urarulla
{
    public class CreatorCategories : MonoBehaviour
    {
        internal void CreateSliders()
        {
            foreach (Transform e in transform)
                Destroy(e.gameObject);

            foreach (var category in ModelParts.modelParts)
            {
                var obj = Instantiate(GameManager.Data.CategorySlider, transform).transform;
                obj.name = $"slider-{category.name}";
                obj.GetComponentInChildren<TMPro.TMP_Text>().text = category.name_fi;
                var part = obj.GetComponent<BodyPart>();
                var slider = obj.GetComponentInChildren<Slider>();
                slider.onValueChanged.AddListener(delegate { part.SetTargets(slider.value); });
            }
        }
    }
}