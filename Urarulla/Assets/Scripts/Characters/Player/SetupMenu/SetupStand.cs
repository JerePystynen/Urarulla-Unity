using UnityEngine;
using UnityEngine.UI;
using Outline = QuickOutline.Outline;

namespace DiMe.Urarulla
{
    public class SetupStand : MonoBehaviour
    {
        public int index;
        // internal int index { get; private set; }

        private GameObject _character;
        private GameObject _nameInputBlock;

        private GameObject _addStandCanvas;
        private GameObject _removeStandCanvas;
        private GameObject _characterAttributeCanvas;

        private Camera _camera;
        private Hoverable _hoverable;

        private void Start()
        {
            index = transform.GetSiblingIndex();

            // Objects
            _character = transform.Find("character").gameObject;
            _nameInputBlock = transform.Find("name-input-block").gameObject;

            // Camera
            _camera = transform.root.GetComponent<IMenu>().camera;
            _hoverable = GetComponentInChildren<Hoverable>();
            if (transform.Find("character") != null)
            {
                _hoverable.outline = transform.Find("character/outline").GetComponentInChildren<MeshRenderer>().transform.GetComponentInChildren<Outline>();
            }
            if (_hoverable != null)
            {
                _hoverable.camera = _camera;
                _hoverable.outline.enabled = true;
            }

            // Canvases
            _addStandCanvas = transform.Find("add-stand-canvas").gameObject;
            _addStandCanvas.GetComponent<Canvas>().worldCamera = _camera;
            _addStandCanvas.transform.Find("add-btn").GetComponent<Button>().onClick.AddListener(delegate { AddStand(); });

            _removeStandCanvas = transform.Find("remove-stand-canvas").gameObject;
            _removeStandCanvas.GetComponent<Canvas>().worldCamera = _camera;
            _removeStandCanvas.transform.Find("remove-btn").GetComponent<Button>().onClick.AddListener(delegate { RemoveStand(); });

            _characterAttributeCanvas = transform.Find("character-attribute-canvas").gameObject;
            _characterAttributeCanvas.GetComponent<Canvas>().worldCamera = _camera;

            RemoveStand();
        }

        private void Update()
        {
            if (_hoverable != null)
                if (_hoverable.CheckMouseHover() && Input.GetMouseButtonDown(0))
                    UISetupMenu.EditPlayer();
        }

        private void AddStand()
        {
            Debug.Log("add the stand");
            SetObjects(true);
        }

        private void RemoveStand()
        {
            Debug.Log("remove the stand");
            SetObjects(false);
        }

        private void SetObjects(bool value)
        {
            _character.SetActive(value);
            _nameInputBlock.SetActive(value);

            _addStandCanvas.SetActive(!value);
            _removeStandCanvas.SetActive(value);
        }
    }
}