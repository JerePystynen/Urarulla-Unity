using UnityEngine;
using UnityEngine.UI;

namespace DiMe.Urarulla
{
    public class SetupStand : MonoBehaviour
    {
        private void Start()
        {
            var ui = transform.Find("UI");
            ui.GetComponent<Canvas>().worldCamera = SetupMenu.Instance.camera;
            ui.Find("add-btn").GetComponent<Button>().onClick.AddListener(delegate { AddStand(); });
            ui.Find("remove-btn").GetComponent<Button>().onClick.AddListener(delegate { RemoveStand(); });
            GetComponent<Hoverable>().outline.enabled = true;
        }

        private void AddStand()
        {
            Debug.Log("add the stand");
        }

        private void RemoveStand()
        {
            Debug.Log("remove the stand");
        }
    }
}