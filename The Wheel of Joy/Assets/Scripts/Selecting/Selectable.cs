using UnityEngine;
using TMPro;

namespace Urarulla
{
    public class Selectable : MonoBehaviour, ISelectionResponse
    {
        public Transform surrogateDotTarget;

        [SerializeField] private TextMeshProUGUI lookPercentageLabel;

        internal float lookPercentage { get; private set; }

        private void Update()
        {
            lookPercentageLabel.text = lookPercentage.ToString("F3");
        }

        public void OnSelect(Transform selection)
        {
        }

        public void OnDeselect(Transform selection)
        {
        }

        public void SetDotProduct(float distance)
        {
            lookPercentage = distance;
        }
    }
}