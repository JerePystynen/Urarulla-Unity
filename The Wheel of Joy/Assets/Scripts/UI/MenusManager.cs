using System.Linq;
using UnityEngine;

namespace WheelOfJoy
{
    public class MenusManager : MonoBehaviour
    {
        [SerializeField] private MenuManager[] menus;
        internal int CurrentActiveMenu;

        private void Start()
        {
            OrderMenusByDepth();
            SetMenu();
        }

        private void Update()
        {
            // if (Application.isPlaying) return;
            if (Input.GetKeyDown(KeyCode.A)) GoPrevious();
            if (Input.GetKeyDown(KeyCode.D)) GoNext();
        }

        private void OrderMenusByDepth()
        {
            var ordered = menus.OrderByDescending(x => x.GetComponent<Camera>()?.depth);
            menus = (from el in ordered select el).ToArray();
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