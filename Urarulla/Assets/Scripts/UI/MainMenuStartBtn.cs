using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DiMe.Urarulla
{
    public class MainMenuStartBtn : MonoBehaviour
    {
        private RectTransform _rect;
        private float _duration = 1;
        private Camera _camera;
        private Coroutine moveButtonCoroutine;

        private void Start()
        {
            _rect = transform.Find("background").GetComponent<RectTransform>();
            _camera = transform.root.GetComponentInChildren<Camera>();
        }

        private void Update()
        {
            CheckPointerOverUIElement();
        }

        private void CheckPointerOverUIElement()
        {
            if (IsPointerOverUIElement(GetEventSystemRaycastResults()) && moveButtonCoroutine == null)
                moveButtonCoroutine = StartCoroutine(MoveButtonCoroutine());
        }

        private bool IsPointerOverUIElement(List<RaycastResult> results)
        {
            for (int i = 0; i < results.Count; i++)
                if (results[i].gameObject.transform == _rect.transform || results[i].gameObject.transform == transform)
                    return true;
            return false;
        }

        private List<RaycastResult> GetEventSystemRaycastResults()
        {
            var data = new PointerEventData(EventSystem.current);
            data.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(data, results);
            return results;
        }

        private IEnumerator MoveButtonCoroutine()
        {
            LeanTween.moveY(_rect, 0, _duration).setEase(LeanTweenType.easeOutSine);
            yield return new WaitForSeconds(_duration);
            LeanTween.moveY(_rect, -220, _duration).setEase(LeanTweenType.easeInSine);
            yield return new WaitForSeconds(_duration);
            moveButtonCoroutine = null;
        }
    }
}