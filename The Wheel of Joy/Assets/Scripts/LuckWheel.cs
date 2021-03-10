using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfJoy
{
    public class LuckWheel : MonoBehaviour
    {
        // public GameObject spinButton;
        // public GameObject questionPanel;
        // private GameObject pointerObj;

        // public Animator questionPanelAnimator;
        // public Animator startButtonAnimator;

        // private GameObject lights;
        // private Light _mainLight;
        // private Light _centerLight;
        // private Light _rightLight;
        // private Light _leftLight;
        // private Light _audienceLight;

        // public Pointer pointerScript;

        // public bool canSpin;

        // private float spinTimer;
        // private float enableTimer;
        // private float buttonActivationTimer;

        // private float lightIntensity = 2f;

        // private float spinTime;
        // private float startRotateSpeed;
        // private float rotateDecrease;

        // private float rotateSpeed = 1f;

        private Transform _wheel;

        private bool IsMainMenuAsBackgroundActive => GameManager.Instance.menusManager.CurrentActiveMenu == 0;

        // This script should only care about spinning itself, nothing else
        private Coroutine _spinWheelCoroutine;

        private void Start()
        {
            _wheel = transform.GetChild(0);

            // pointerScript = FindObjectOfType<Pointer>();
            // pointerObj = pointerScript.gameObject;

            // Transform lights = GameObject.FindGameObjectWithTag("Lights").transform;

            // Transform mainLight = lights.GetChild(0);
            // Transform rightLight = lights.GetChild(1);
            // Transform centerLight = lights.GetChild(2);
            // Transform leftLight = lights.GetChild(3);
            // Transform audienceLight = lights.GetChild(4);

            // _mainLight = mainLight.GetComponent<Light>();
            // _rightLight = rightLight.GetComponent<Light>();
            // _centerLight = centerLight.GetComponent<Light>();
            // _leftLight = leftLight.GetComponent<Light>();
            // _audienceLight = audienceLight.GetComponent<Light>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SpinWheel();

            // buttonActivationTimer += Time.deltaTime;
            // enableTimer += Time.deltaTime;

            if (IsMainMenuAsBackgroundActive)
            {
                _wheel.Rotate(new Vector3(0f, 0f, -8f), Space.Self);
            }

            // if (canSpin)
            // {
            //     // if (Input.GetKeyDown(KeyCode.Space))
            //     // {
            //     //     if (buttonActivationTimer >= 15.25f)
            //     //     {
            //     //         SpinWheel();
            //     //     }
            //     // }

            //     // AddToTimer();

            //     spinButton.SetActive(false);

            //     // intensifies side lights according to calculation
            //     _leftLight.intensity = spinTimer;
            //     _rightLight.intensity = spinTimer;

            //     // intensifies center light according to calculation
            //     _centerLight.intensity = spinTimer * lightIntensity;

            //     // rotates Object(Wheel) on The z-axis according to speed set in inspector
            //     transform.Rotate(new Vector3(1 * rotateSpeed, 0, 0));

            //     // currentspeed is decreased according to calculation 
            //     rotateSpeed -= (rotateDecrease * 2f) * Time.deltaTime;
            // }

            // bool spinTimerReachesSetTimeAndRotateHasStopped = spinTimer >= spinTime;
            // if (spinTimerReachesSetTimeAndRotateHasStopped)
            // {
            //     if (currentRotateSpeed <= 0)
            //     {
            //         canSpin = false;
            //         startButtonAnimator.SetBool("Play", false);
            //         questionPanelAnimator.SetBool("Play", true);
            //         currentRotateSpeed = startRotateSpeed + Random.Range(-1f, 2f);
            //     }
            // }

            // if (canSpin)
            // {
            //     if (enableTimer >= 14f)
            //     {
            //         startButtonAnimator.SetBool("Play2", true);
            //     }

            //     if (buttonActivationTimer >= 15.25f)
            //     {
            //         spinButton.SetActive(true);
            //     }
            // }
        }

        // private void AddToTimer()
        // {
        //     spinTimer += Time.deltaTime;
        // }

        internal void SpinWheel()
        {
            if (_spinWheelCoroutine == null)
                _spinWheelCoroutine = StartCoroutine(SpinWheelCoroutine());

            // // sets the timer to 0 and starts the spin
            // spinTimer = 0;
            // enableTimer = 0;
            // buttonActivationTimer = 0;

            // startButtonAnimator.SetBool("Play2", false);
            // questionPanelAnimator.SetBool("Play", false);
            // startButtonAnimator.SetBool("Play", true);

            // canSpin = true;
            // pointerScript.firstSpin = true;
        }

        private IEnumerator SpinWheelCoroutine()
        {
            var wait = new WaitForSeconds(Time.deltaTime);
            int slots = Random.Range(100, 400);

            while (slots % 8 != 0)
            {
                slots++;
            }

            Debug.Log(slots);

            for (int i = slots; i > 0; i--)
            {
                float w = (i * (i * 0.5f)) / 100;
                _wheel.Rotate(new Vector3(0, 0, -w), Space.Self);
                yield return wait;
            }
            _spinWheelCoroutine = null;
            OnWheelSpinEnded();
        }

        private void OnWheelSpinEnded()
        {
            Debug.Log("spinned: " + _wheel.localEulerAngles.z);
        }
    }
}