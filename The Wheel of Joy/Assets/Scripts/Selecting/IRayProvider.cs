using UnityEngine;

namespace WheelOfJoy
{
    public interface IRayProvider
    {
        public Ray CreateRay();
    }
}