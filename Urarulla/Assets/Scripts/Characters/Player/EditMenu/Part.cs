using UnityEngine;

namespace DiMe.Urarulla
{
    [System.Serializable]
    public class Part
    {
        public Direction direction;
        public Action action;
        public Transform target;
        public Vector3 startPosition;

        internal void SetPosition(Vector3 move, float value)
        {
            target.localPosition = new Vector3(
                startPosition.x + move.x,
                startPosition.y + move.y,
                startPosition.z + move.z
            ) * value;
        }

        internal void SetScale(Vector3 move, float value)
        {
            target.localScale = new Vector3(
                0.2f + value * move.x,
                0.2f + value * move.y,
                0.2f + value * move.z
            );
        }
    
        public enum Direction
        {
            up,
            down,
            left,
            right,
            forward,
            back,
            ignore,
        }
        public enum Action
        {
            position,
            scale,
        }
    }
}