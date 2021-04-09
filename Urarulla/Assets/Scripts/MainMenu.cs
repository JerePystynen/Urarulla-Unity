using UnityEngine;
using UnityEngine.SceneManagement;

namespace Urarulla
{
    public class MainMenu : MonoBehaviour
    {
        public string sceneToLoad;
        public Transform wheel;
        public float rotateSpeed = 1f;

        private void Update()
        {
            wheel.Rotate(rotateSpeed * Time.deltaTime, 0f, 0f);
        }

        public void StartButton()
        {
            SceneManager.LoadScene(sceneToLoad);
        }

        public void QuitButton()
        {
            Application.Quit();
        }
    }
}