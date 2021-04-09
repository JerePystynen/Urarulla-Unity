using System.Collections.Generic;
using UnityEngine;

namespace Urarulla
{
    public class ResponsiveSelector : MonoBehaviour, ISelector
    {
        [SerializeField] private LuckWheel selectable;

        private Transform _selection;

        public void Check(Ray ray)
        {
            _selection = null;

            var vector1 = ray.direction;
            var target = selectable.wheel.position;
            var vector2 = target - ray.origin;
            
            var lookPercentage = Vector3.Dot(vector1.normalized, vector2.normalized);
            selectable.SetDotProduct(lookPercentage);
        }

        public Transform GetSelection()
        {
            return _selection;
        }
    }
}