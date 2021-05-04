using UnityEngine;

namespace DiMe.Urarulla
{
    public class BodyPart : MonoBehaviour
    {
        public Part[] parts;

        private void Start()
        {
            foreach (var part in parts)
            {
                part.startPosition = part.target.localPosition;
            }
        }

        internal void SetTargets(float value)
        {
            if (parts.Length == 0) return;
            foreach (var part in parts)
            {
                var move = new Vector3();
                switch (part.action)
                {
                    case Part.Action.position:
                        switch (part.direction)
                        {
                            case Part.Direction.ignore:
                            case Part.Direction.up:
                                move = Vector3.up;
                                break;
                            case Part.Direction.down:
                                move = Vector3.down;
                                break;
                            case Part.Direction.left:
                                move = Vector3.left;
                                break;
                            case Part.Direction.right:
                                move = Vector3.right;
                                break;
                            case Part.Direction.forward:
                                move = Vector3.forward;
                                break;
                            case Part.Direction.back:
                                move = Vector3.back;
                                break;
                        }
                        part.SetPosition(move, value);
                        break;
                    case Part.Action.scale:
                        switch (part.direction)
                        {
                            case Part.Direction.left:
                            case Part.Direction.right:
                                move = Vector3.right;
                                break;
                            case Part.Direction.forward:
                            case Part.Direction.back:
                                move = Vector3.forward;
                                break;
                            case Part.Direction.up:
                            case Part.Direction.down:
                                move = Vector3.up;
                                break;
                            case Part.Direction.ignore:
                                move = Vector3.one;
                                break;
                        }
                        part.SetScale(move, value + 1);
                        break;
                }
            }
        }
    }
}