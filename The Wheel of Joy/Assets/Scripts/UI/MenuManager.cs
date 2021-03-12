using UnityEngine;
using System.Linq;

namespace WheelOfJoy
{
    public class MenuManager : MonoBehaviour, IUI
    {
        internal new Transform camera;
        internal Camera cam;
        private AudioListener _audioListener;
        
        private Camera _uicam;
        private GameObject _uicamera;
        
        internal Canvas canvas;
        private GameObject _canvasObj;

        private string startName;

        private void Awake()
        {
            startName = name;

            cam = GetComponentInChildren<Camera>();
            camera = cam.transform;
            _audioListener = camera.GetComponent<AudioListener>();
            
            foreach (Transform element in camera)
            {
                Camera camera = element.GetComponent<Camera>();
                if (camera != null) 
                {
                    _uicam = camera;
                    break;
                }
            }
            _uicamera = _uicam?.gameObject;
            
            canvas = transform.GetComponentInChildren<Canvas>(true);
            _canvasObj = canvas?.gameObject;
        }
    
        public void SetActive() => SetState(true);
        public void SetDeactive() => SetState(false);

        private void SetState(bool value)
        {
            name = (value ? "(Active) " : "(Inactive) ") + startName;

            cam.enabled = value;
            _audioListener.enabled = value;
            
            _uicamera?.SetActive(value);

            if (_uicam != null) _uicam.enabled = value;
            
            _canvasObj?.SetActive(value);
            if (canvas != null) canvas.enabled = value;
        }
    }
}