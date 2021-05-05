using UnityEngine;
using QuickOutline;

namespace DiMe.Urarulla
{
    public class Hoverable : MonoBehaviour
    {
        public Gradient gradient;
        
        private float _strobeDuration = 2.75f;
        
        internal new Camera camera;
        internal Outline outline;

        internal bool CheckMouseHover()
        {
            var isHover = IsMouseHoveringOnModelCollider();
            if (outline != null)
            {
                outline.enabled = isHover;
                if (isHover)
                {
                    float t = Mathf.PingPong(Time.time / _strobeDuration, 1f);
                    outline.OutlineColor = gradient.Evaluate(t);
                }
            }
            return isHover;
        }

        private bool IsMouseHoveringOnModelCollider()
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
            return hit.transform.parent == transform;
        }
    }
}