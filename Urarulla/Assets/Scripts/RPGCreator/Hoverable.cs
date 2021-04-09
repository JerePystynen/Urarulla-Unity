using UnityEngine;
using QuickOutline;

namespace Urarulla
{
    public class Hoverable : MonoBehaviour
    {
        internal Camera _camera;
        public Gradient gradient;
        internal Outline outline;
        private float strobeDuration = 2.75f;

        internal bool CheckMouseHover()
        {
            var isHover = IsHover();
            if (outline != null)
            {
                outline.enabled = isHover;
                if (isHover)
                {
                    float t = Mathf.PingPong(Time.time / strobeDuration, 1f);
                    outline.OutlineColor = gradient.Evaluate(t);
                }
            }
            return isHover;
        }

        private bool IsHover()
        {
            if (!_camera.enabled)
                return false;
            RaycastHit hit;
            if (!Physics.Raycast(_camera.transform.position, _camera.ScreenPointToRay(Input.mousePosition).direction, out hit))
                return false;
            if (hit.transform.parent == null)
                return false;
            if (hit.transform.parent.parent == null)
                return false;
            return hit.transform.parent.parent == transform;
        }
    }
}
