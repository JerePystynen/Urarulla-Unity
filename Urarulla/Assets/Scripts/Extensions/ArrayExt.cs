public static partial class ArrayExt
{
    public static T Random<T>(this T[] array)
    {
        if (array.Length > 0)
            return array[UnityEngine.Random.Range(0, array.Length)];
        return default(T);
    }
}