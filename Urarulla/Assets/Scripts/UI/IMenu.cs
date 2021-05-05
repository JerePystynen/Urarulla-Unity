using UnityEngine;

namespace DiMe.Urarulla
{
    public interface IMenu
    {
        public float Depth { get; }
        public string CanvasName { get; }
        public Camera camera { get; }

        public void SetActive();
        public void SetDeactive();
    }
}