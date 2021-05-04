using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DiMe.Urarulla
{
    public class MainMenuStartBtn : MonoBehaviour
    {
        private Image _background;
        private Camera _camera;
        private Coroutine startCoroutine;

        private UIMainMenu mainMenu;

        private void Start()
        {
            _background = transform.Find("background").GetComponent<Image>();
            _camera = transform.root.GetComponentInChildren<Camera>();

            mainMenu = GetComponentInParent<UIMainMenu>();

            GetComponent<Button>().onClick.AddListener(delegate { OnStartButtonPressed(); });
            ResetButton();
        }

        internal void ResetButton()
        {
            _background.sprite = GameManager.Data.StartButtonUpSprite;
        }

        private void OnStartButtonPressed()
        {
            if (startCoroutine == null)
            {
                startCoroutine = StartCoroutine(StartCoroutine());
            }
        }

        private IEnumerator StartCoroutine()
        {
            _background.sprite = GameManager.Data.StartButtonDownSprite;
            yield return new WaitForSeconds(.225f);
            mainMenu.StartButton(this);
        }
    }
}