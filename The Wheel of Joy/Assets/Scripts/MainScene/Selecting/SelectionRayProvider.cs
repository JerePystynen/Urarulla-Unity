using UnityEngine;

namespace WheelOfJoy
{
    public class SelectionRayProvider : MonoBehaviour, IRayProvider
    {
        public Ray CreateRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}