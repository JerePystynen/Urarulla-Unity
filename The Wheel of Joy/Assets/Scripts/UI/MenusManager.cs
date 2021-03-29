using UnityEngine;

namespace Urarulla
{
    [ExecuteAlways]
    public class MenusManager : Singleton<MenusManager>
    {
        [SerializeField]
        private MenuManager[] menus;

        internal int CurrentActiveMenu { get; private set; }

        private void Start()
        {
            SetMenu();
        }

        private void GoPrevious()
        {
            if (CurrentActiveMenu > 0) CurrentActiveMenu--;
            else CurrentActiveMenu = menus.Length - 1;
            SetMenu(CurrentActiveMenu);
        }

        private void GoNext()
        {
            if (CurrentActiveMenu < menus.Length - 1) CurrentActiveMenu++;
            else CurrentActiveMenu = 0;
            SetMenu(CurrentActiveMenu);
        }

        public void SelectMenu(MenuManager manager)
        {
            SetMenu(System.Array.IndexOf(menus, manager));
        }

        internal void SetMenu(int index = 0)
        {
            CurrentActiveMenu = index;
            for (int i = 0; i < menus.Length; i++)
            {
                MenuManager menu = menus[i];
                if (i == index) menu.SetActive();
                else menu.SetDeactive();
            }
        }
    }
}