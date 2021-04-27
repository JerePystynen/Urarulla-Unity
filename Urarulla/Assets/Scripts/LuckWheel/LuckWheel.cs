using System.Collections;
using UnityEngine;
using System.Linq;
using QuickOutline;

namespace DiMe.Urarulla
{
    public class LuckWheel : MonoBehaviour, ISelectionResponse
    {
        internal Transform wheel { get; private set; }
        
        private Transform _lightsHolder;
        private Transform[] _lights;
        private GameObject[] _lightObjs;
        private LuckWheelLight[] _wheelLights;
        
        [SerializeField]
        private Transform _targetSection;
        
        private float _angle;
        private float _dotPercentage;
        private int _targetIndex;

        private bool CanSpinWheelAtBackground => GameManager.Instance.menusManager.CurrentActiveMenu == 0;
        private bool IsMainSceneActive => GameManager.Instance.menusManager.CurrentActiveMenu == 3;

        private Outline _outline;
        private Coroutine _spinWheelCoroutine;

        private AudioSource source;

        private void Start()
        {
            wheel = transform.GetChild(0);
            
            _lightsHolder = transform.Find("Wheel/Lights");
            _lights = (from Transform child in _lightsHolder select child).ToArray();
            _lightObjs = (from Transform obj in _lights select obj.GetChild(0).gameObject).ToArray();
            _wheelLights = (from Transform obj in _lights select obj.GetComponent<LuckWheelLight>()).ToArray();
            _angle = 360 / 8;
            
            _outline = wheel.Find("wheel-outline").GetComponent<Outline>();
            
            source = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (CanSpinWheelAtBackground)
                wheel.Rotate(new Vector3(0f, 0f, -8f), Space.Self);
            else if (IsMainSceneActive && Input.GetKeyDown(KeyCode.Space))
                SpinWheel();
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
                if (b)
                {
                    _targetSection = light;
                    _targetIndex = System.Array.IndexOf(_lights, light);
                }
            }
        }

    // SpinCoroutine
        internal void SpinWheel()
        {
            Debug.Log("TODO: add logic for checking if a question is being answered...");
            
            if (_spinWheelCoroutine == null)
                _spinWheelCoroutine = StartCoroutine(SpinWheelCoroutine());
        }

        private IEnumerator SpinWheelCoroutine()
        {
            source.PlayOneShot(GameManager.Data.LuckWheelSpinningSound);

            var wait = new WaitForSeconds(Time.deltaTime);
            for (int i = Random.Range(350, 700); i > 0; i -= 5)
            {
                float w = (i * (i * 0.5f)) / 100;
                Rotate(w);
                yield return wait;
            }
            _spinWheelCoroutine = null;
            
            if (_targetSection != null)
                UIMainScene.Instance?.AskQuestion(_targetIndex);
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
            var isMouseOnWheelAndInGame = IsMouseOnWheel(distance) && IsMainSceneActive;
            SetWheelOutline(isMouseOnWheelAndInGame);
            if (isMouseOnWheelAndInGame && Input.GetMouseButtonDown(0))
                SpinWheel();
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