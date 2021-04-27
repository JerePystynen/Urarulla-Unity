using UnityEngine;

namespace DiMe.Urarulla
{
    public class SelectionRayProvider : MonoBehaviour, IRayProvider
    {
        private Camera _camera;

        public Ray CreateRay()
        {
            if (_camera == null) _camera = Camera.main;
            return _camera.ScreenPointToRay(Input.mousePosition);
        }
    }
}