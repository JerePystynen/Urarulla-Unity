using UnityEngine;

namespace DiMe.Urarulla
{
    public static partial class JsonUtilityExt
    {
        public static T GetData<T>(TextAsset txt)
        {
            var json = JsonUtility.FromJson<T>(txt.text);
            if (json == null)
            {
                Debug.LogError($"Error: {txt.name} JSON could not be loaded.");
                return default(T);
            }
            return json;
        }
    }
}