using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfJoy
{
    public class RaycastBasedTagSelector : MonoBehaviour, ISelector
    {
        [SerializeField] private string selectableTag = "Selectable";

        private Transform _selection;

        public void Check(Ray ray)
        {
            _selection = null;

            if (!Physics.Raycast(ray, out var hit))
                return;

            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
                return;

            _selection = selection;
        }

        public Transform GetSelection()
        {
            return _selection;
        }
    }
}