using System.Collections.Generic;
using UnityEngine;

namespace DiMe.Urarulla
{
    public static partial class CharacteristicsExt
    {
        internal static Degree GetClosestDegree(this Characteristics characteristics)
        {
            if (GameManager.Instance == null)
            {
                Debug.LogError("Error: GameManager's instance is necessary for this to work!");
                return default(Degree);
            }
            
            var ourCategories = characteristics.GetScores();
            var degrees = GameManager.Instance.degrees.degrees;

            var closestIndexList = new List<int>();
            for (var x = 0; x < degrees.Length; x++)
            {
                var verticalCellList = new List<int>();
                var characteristicsScores = degrees[x].characteristics.GetScores();
                for (var y = 0; y < characteristicsScores.Length; y++)
                {
                    foreach (var degree2 in degrees)
                    {
                        if (Mathf.Abs(ourCategories[y] - characteristicsScores[y]) <= 1)
                        {
                            verticalCellList.Add(characteristicsScores[y]);
                        }
                    }
                    closestIndexList.Add(GetClosestCellsIndex(verticalCellList.ToArray(), ourCategories[y]));
                }
            }
            return degrees[GetMostOccuringIndex(closestIndexList.ToArray())];
        }

        private static int GetClosestCellsIndex(int[] collection, int target)
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
        
        private static int GetMostOccuringIndex(int[] indexArray)
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
}