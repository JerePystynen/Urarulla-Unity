using UnityEngine;

namespace WheelOfJoy
{
    public interface ISelector
    {
        public void Check(Ray ray);
        public Transform GetSelection();
    }
}