using UnityEngine;

namespace DiMe.Urarulla
{
    public class EditMenu : Singleton<EditMenu>
    {
        internal RPGCreator creator;

        private void Awake()
        {
            creator = GetComponentInChildren<Camera>(true).GetComponentInChildren<RPGCreator>(true);
        }

        internal static void OnDataLoaded() => Instance.creator.attributes.SetDataAndDropdown();
    }
}