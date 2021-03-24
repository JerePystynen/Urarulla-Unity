using UnityEngine;

namespace Urarulla
{
    public interface ISelector
    {
        public void Check(Ray ray);
        public Transform GetSelection();
    }
}