using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Urarulla
{
    public class Closest : MonoBehaviour
    {
        public Block block = new Block(1, 6, 4, 2);

        internal void DebugClosestTutkinto()
        {
            if (GameManager.Instance == null)
            {
                Debug.LogError("Error: GameManager's instance is necessary for this to work!");
                return;
            }
            var tutkinto = GetClosestTutkinto(block);
            Debug.Log(tutkinto.nimi);
        }

        private Tutkinto GetClosestTutkinto(Characteristics characteristics)
        {
            var indexes = new List<int>();
            for (var i = 0; i < characteristics.categories.Length; i++)
            {
                var cell = (from e in Blocks.blocks select e.categories[i]).ToArray();
                var num = GetClosestCellsIndex(cell, characteristics.categories[i]);
                if (num < 0) continue;
                indexes.Add(num);
            }
            var index = GetMostOccuringIndex(indexes.ToArray());
            return GameManager.Instance.tutkinnot.tutkinnot[index];
        }

        private int GetClosestCellsIndex(int[] collection, int target)
        {
            if (collection == null)
            {
                Debug.LogError("Error: input array is null!");
                return -1;
            }
            if (collection.Length == 0)
            {
                Debug.LogError("Error: input array is empty!");
                return -1;
            }

            var index = -1;
            var closest = int.MaxValue;
            var minDifference = int.MaxValue;
            for (int i = 0; i < collection.Length; i++)
            {
                int element = collection[i];
                var difference = Mathf.Abs((long)element - target);
                if (minDifference > difference)
                {
                    minDifference = (int)difference;
                    closest = element;
                    index = i;
                }
            }
            return index;
        }
        
        private int GetMostOccuringIndex(int[] indexArray)
        {
            var dict = new Dictionary<int, int>();
            foreach (var value in indexArray)
            {
                if (dict.ContainsKey(value))
                    dict[value]++;
                else
                    dict.Add(value, 1);
            }
            var mostCommonValue = 0;
            var highestCount = 0;
            foreach (var pair in dict)
            {
                if (pair.Value > highestCount)
                {
                    mostCommonValue = pair.Key;
                    highestCount = pair.Value;
                }
            }
            return mostCommonValue;
        }
    }

    [System.Serializable]
    public class Block
    {
        public Block(params int[] categories)
        {
            this.categories = categories;
        }
        public int[] categories;
    }

    public static class Blocks
    {
        public static Block[] blocks =
        {
            new Block(0, 0, 0, 0),
            new Block(1, 6, 4, 2),
            new Block(3, 8, 7, 2),
            new Block(3, 0, 3, 0),
            new Block(6, 4, 0, 2),
            new Block(9, 9, 9, 0),
        };
    }
}