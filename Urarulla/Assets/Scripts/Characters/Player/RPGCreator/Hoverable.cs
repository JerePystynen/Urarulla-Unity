using UnityEngine;
using QuickOutline;

namespace DiMe.Urarulla
{
    public class Hoverable : MonoBehaviour
    {
        internal new Camera camera;
        public Gradient gradient;
        internal Outline outline;
        private float strobeDuration = 2.75f;

        private void Start()
        {
            camera = GetComponent<Camera>();
        }

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
            if (camera == null)
                return false;
            if (!camera.enabled)
                return false;
            RaycastHit hit;
            if (!Physics.Raycast(camera.transform.position, camera.ScreenPointToRay(Input.mousePosition).direction, out hit))
                return false;
            if (hit.transform.parent == null)
                return false;
            if (hit.transform.parent.parent == null)
                return false;
            return hit.transform.parent.parent == transform;
        }
    }
}