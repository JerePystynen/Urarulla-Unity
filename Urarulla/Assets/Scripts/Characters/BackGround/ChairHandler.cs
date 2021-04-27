using System.Collections.Generic;
using UnityEngine;

namespace DiMe.Urarulla
{
    public class ChairHandler : MonoBehaviour
    {
        private Transform[] chairPositions;
        private List<Transform> occupiedChairs = new List<Transform>();
        public int OpenChairCount => chairPositions.Length - occupiedChairs.Count;

        private void Awake() => AddChairPositions();

        private void AddChairPositions()
        {
            if (transform.childCount > 0)
            {
                var list = new List<Transform>();
                foreach (Transform row in transform)
                    if (row.childCount > 0)
                        foreach (Transform chair in row)
                            if (chair.gameObject.activeSelf)
                                list.Add(chair);
                if (list.Count > 0)
                    chairPositions = list.ToArray();
            }
        }

        public Transform GetChair()
        {
            if (occupiedChairs.Count < chairPositions.Length)
            {
                Transform target = null;
                while (target == null)
                {
                    var chair = chairPositions.Random();
                    if (!occupiedChairs.Contains(chair))
                    {
                        target = chair;
                        occupiedChairs.Add(chair);
                    }
                }
                return target;
            }
            return null;
        }
    }
}