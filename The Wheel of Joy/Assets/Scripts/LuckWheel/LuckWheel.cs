using System.Collections;
using UnityEngine;
using System.Linq;

namespace Urarulla
{
    public class LuckWheel : MonoBehaviour, ISelectionResponse
    {
        // private Light _mainLight;
        // private Light _centerLight;
        // private Light _rightLight;
        // private Light _leftLight;
        // private Light _audienceLight;

        internal Transform wheel { get; private set; }
        
        private Transform _lightsHolder;
        private Transform[] _lights;
        private GameObject[] _lightObjs;
        private LuckWheelLight[] _wheelLights;
        
        [SerializeField]
        private Transform _targetSection;
        
        private float _angle;
        private float _dotPercentage;

        private bool IsMainMenuAsBackgroundActive => GameManager.Instance.menusManager.CurrentActiveMenu == 0;

        private Outline _outline;
        private Coroutine _spinWheelCoroutine;

        private void Start()
        {
            wheel = transform.GetChild(0);
            
            _lightsHolder = transform.Find("Wheel/Lights");
            _lights = (from Transform child in _lightsHolder select child).ToArray();
            _lightObjs = (from Transform obj in _lights select obj.GetChild(0).gameObject).ToArray();
            _wheelLights = (from Transform obj in _lights select obj.GetComponent<LuckWheelLight>()).ToArray();
            _angle = 360 / 8;
            
            _outline = wheel.Find("wheel-outline").GetComponent<Outline>();
        }

        private void Update()
        {
            if (!IsMainMenuAsBackgroundActive && Input.GetKeyDown(KeyCode.Space))
            {
                SpinWheel();
            }

            if (IsMainMenuAsBackgroundActive)
            {
                wheel.Rotate(new Vector3(0f, 0f, -8f), Space.Self);
            }
        }

    // Rotate
        private void Rotate(float amount)
        {
            wheel.Rotate(new Vector3(0, 0, -amount), Space.Self);
            SetTopLight();
        }

        private void SetTopLight()
        {
            foreach (
                var (light, a, b, c) in from light in _lights
                let a = light.eulerAngles.z
                let b = a < _angle && a > -(_angle * 2f)
                let c = light.GetSiblingIndex()
                select (light, a, b, c)
            ){
                _lightObjs[c].SetActive(b);
                _wheelLights[c].angle = a;
                if (b) _targetSection = light;
            }
        }

    // SpinCoroutine
        internal void SpinWheel()
        {
            if (_spinWheelCoroutine == null)
                _spinWheelCoroutine = StartCoroutine(SpinWheelCoroutine());
        }

        private IEnumerator SpinWheelCoroutine()
        {
            var wait = new WaitForSeconds(Time.deltaTime);
            for (int i = Random.Range(350, 700); i > 0; i -= 5)
            {
                float w = (i * (i * 0.5f)) / 100;
                Rotate(w);
                yield return wait;
            }
            _spinWheelCoroutine = null;
            OnWheelSpinEnded();
        }

        private void OnWheelSpinEnded()
        {
            if (_targetSection == null)
                return;

            Debug.Log("target: " + _targetSection.name);

            UIMainScene.Instance.AskQuestion();
        }

        public void OnSelect(Transform selection)
        {
        }

        public void OnDeselect(Transform selection)
        {
        }

        public void SetDotProduct(float distance)
        {
            _dotPercentage = distance;

            bool isMouseOnWheelAndInGame = IsMouseOnWheel(distance) && !IsMainMenuAsBackgroundActive;

            SetWheelOutline(isMouseOnWheelAndInGame);

            if (isMouseOnWheelAndInGame)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SpinWheel();
                }
            }
        }

        private void SetWheelOutline(bool isMouseOnWheel)
        {
            _outline.enabled = isMouseOnWheel;
        }

        private bool IsMouseOnWheel(float amount)
        {
            return amount > .97f;
        }
    }
}