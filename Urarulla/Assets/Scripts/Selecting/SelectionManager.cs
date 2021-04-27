using UnityEngine;

namespace DiMe.Urarulla
{
    public class SelectionManager : MonoBehaviour
    {
        private IRayProvider _rayProvider;
        private ISelector _selector;
        private ISelectionResponse _selectionResponse;

        private Transform _currentSelection;

        private void Awake()
        {
            _rayProvider = GetComponent<IRayProvider>();
            _selector = GetComponent<ISelector>();
            _selectionResponse = GetComponent<ISelectionResponse>();
        }

        private void Update()
        {
            CheckSelection();
        }

        private void CheckSelection()
        {
            // Deselection / Selection Response
            if (_currentSelection != null)
            {
                _selectionResponse.OnDeselect(_currentSelection);
            }

            _selector.Check(_rayProvider.CreateRay());
            _currentSelection = _selector.GetSelection();

            // Deselection / Selection Response
            if (_currentSelection != null)
            {
                _selectionResponse.OnSelect(_currentSelection);
            }
        }
    }
}