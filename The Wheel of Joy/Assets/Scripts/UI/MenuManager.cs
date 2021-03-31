using UnityEngine;
using System.Linq;

namespace Urarulla
{
    public class MenuManager : MonoBehaviour, IUI
    {
        public float depth = 20;
        public float Depth => depth;
        
        public string canvasName;
        public string CanvasName => canvasName;

        internal new Transform camera;
        internal Camera cam;
        private AudioListener _audioListener;
        
        private Camera _uicam;
        private GameObject _uicamera;
        
        internal Canvas[] canvases;

        public void SetActive() => SetState(true);
        public void SetDeactive() => SetState(false);

        private bool _initialized;
        private void SetState(bool value)
        {
            if (!_initialized)
            {
                _initialized = true;

                cam = GetComponentInChildren<Camera>();
                cam.depth = depth;
                camera = cam.transform;
                _audioListener = camera.GetComponent<AudioListener>();
                
                foreach (
                    var camera in from Transform element in camera
                    let camera = element.GetComponent<Camera>()
                    where camera != null select camera
                ){
                    _uicam = camera;
                    _uicam.depth = depth + .1f;
                    _uicamera = _uicam.gameObject;
                    break;
                }
                
                canvases = camera?.GetComponentsInChildren<Canvas>(true);
            }

            name = $"{canvasName} {depth} " + (value ? "(Active) " : "(Inactive) ");

            if (cam != null)
                cam.enabled = value;
            
            if (_audioListener != null)
                _audioListener.enabled = value;
            
            if (_uicamera != null)
                _uicamera.SetActive(value);

            if (_uicam != null)
                _uicam.enabled = value;
            
            if (canvases != null)
            {
                foreach (var canvas in canvases)
                    canvas.gameObject.SetActive(value);
                canvases[0].enabled = value;
            }
        }
    }
}