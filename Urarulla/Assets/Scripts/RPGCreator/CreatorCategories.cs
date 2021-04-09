using UnityEngine;
using UnityEngine.UI;

namespace Urarulla
{
    public class CreatorCategories : MonoBehaviour
    {
        private void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                var part = child.GetComponent<BodyPart>();
                var slider = child.GetComponentInChildren<Slider>();
                slider.onValueChanged.AddListener(delegate { part.SetTargets(slider.value); });
            }
        }
    }
}