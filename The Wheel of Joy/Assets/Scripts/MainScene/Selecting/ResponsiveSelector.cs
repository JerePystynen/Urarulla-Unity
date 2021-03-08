using System.Collections.Generic;
using UnityEngine;

namespace WheelOfJoy
{
    public class ResponsiveSelector : MonoBehaviour, ISelector
    {
        [SerializeField] private List<Selectable> selectables;

        private Transform _selection;

        public void Check(Ray ray)
        {
            _selection = null;

            for (int i = 0; i < selectables.Count; i++)
            {
                var vector1 = ray.direction;
                var target = selectables[i].surrogateDotTarget != null
                    ? selectables[i].surrogateDotTarget.position
                    : selectables[i].transform.position;

                var vector2 = target - ray.origin;
                var lookPercentage = Vector3.Dot(vector1.normalized, vector2.normalized);
                selectables[i].SetDotProduct(lookPercentage);
            }
        }

        public Transform GetSelection()
        {
            return _selection;
        }
    }
}