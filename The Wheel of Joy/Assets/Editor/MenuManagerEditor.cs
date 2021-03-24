using UnityEditor;
using UnityEngine;

namespace Urarulla
{
    [CustomEditor(typeof(MenuManager))]
    public class MenuManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MenuManager script = (MenuManager)target;

            if (GUILayout.Button("Select"))
            {
                MenusManager.Instance.SelectMenu(script);
            }
        }
    }
}