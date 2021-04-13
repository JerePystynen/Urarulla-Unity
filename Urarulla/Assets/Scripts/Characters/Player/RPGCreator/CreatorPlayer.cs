using QuickOutline;
using UnityEngine;

namespace Urarulla
{
    public class CreatorPlayer : MonoBehaviour
    {
        private Camera _camera;
        private Hoverable _hoverable;

        private void Start()
        {
            _hoverable = GetComponent<Hoverable>();
            if (transform.Find("character") != null)
                _hoverable.outline = transform.Find("character/outline").GetComponentInChildren<MeshRenderer>().transform.GetComponentInChildren<Outline>();
            _camera = transform.root.GetComponentInChildren<Camera>();
            if (_hoverable != null)
                _hoverable._camera = _camera;
        }

        private void Update()
        {
            if (_hoverable != null)
                if (_hoverable.CheckMouseHover() && Input.GetMouseButtonDown(0))
                    UISetupMenu.EditPlayer();
        }
    }
}
