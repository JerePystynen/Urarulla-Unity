using UnityEngine;

namespace DiMe.Urarulla
{
    public class RPGCreator : MonoBehaviour
    {
        internal CreatorCategories categories;
        internal CreatorConfidence confidence;
        internal CreatorAttributes attributes;

        private void Start()
        {
            categories = GetComponentInChildren<CreatorCategories>(true);
            confidence = GetComponentInChildren<CreatorConfidence>(true);
            attributes = GetComponentInChildren<CreatorAttributes>(true);

            categories.CreateSliders();
        }
    }
}