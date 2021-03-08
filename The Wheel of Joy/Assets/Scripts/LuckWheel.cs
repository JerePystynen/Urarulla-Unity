using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfJoy
{
    public class LuckWheel : MonoBehaviour
    {
        public GameObject spinButton;
        public GameObject questionPanel;
        private GameObject pointerObj;

        public Animator questionPanelAnimator;
        public Animator startButtonAnimator;

        private GameObject lightsObj;
        private Light mainLight;
        private Light centerLight;
        private Light rightLight;
        private Light leftLight;
        private Light audienceLight;

        public Pointer pointerScript;

        public bool canSpin;

        private float spinTimer;
        private float enableTimer;
        private float buttonActivationTimer;

        private float lightIntensity = 2f;
        private float spinTime;
        private float currentRotateSpeed;
        private float startRotateSpeed;
        private float rotateDecrease;

        private void Start()
        {
            pointerScript = FindObjectOfType<Pointer>();
            pointerObj = pointerScript.gameObject;

            lightsObj = GameObject.FindGameObjectWithTag("Lights");
            Transform mainLightObj = lightsObj.transform.GetChild(0);
            Transform rightLightObj = lightsObj.transform.GetChild(1);
            Transform centerLightObj = lightsObj.transform.GetChild(2);
            Transform leftLightObj = lightsObj.transform.GetChild(3);
            Transform audienceLightObj = lightsObj.transform.GetChild(4);
            mainLight = mainLightObj.GetComponent<Light>();
            rightLight = rightLightObj.GetComponent<Light>();
            centerLight = centerLightObj.GetComponent<Light>();
            leftLight = leftLightObj.GetComponent<Light>();
            audienceLight = audienceLightObj.GetComponent<Light>();


        }

        private void Update()
        {
            buttonActivationTimer += Time.deltaTime;
            enableTimer += Time.deltaTime;

            if (canSpin)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (buttonActivationTimer >= 15.25f)
                    {
                        SpinWheel();
                    }
                }

                AddToTimer();

                spinButton.SetActive(false);

                // intensifies side lights according to calculation
                leftLight.intensity = spinTimer;
                rightLight.intensity = spinTimer;

                // intensifies center light according to calculation
                centerLight.intensity = spinTimer * lightIntensity;

                // rotates Object(Wheel) on The z-axis according to speed set in inspector
                transform.Rotate(new Vector3(1 * currentRotateSpeed, 0, 0));

                // currentspeed is decreased according to calculation 
                currentRotateSpeed -= (rotateDecrease * 2f) * Time.deltaTime;
            }

            bool spinTimerReachesSetTimeAndRotateHasStopped = spinTimer >= spinTime;
            if (spinTimerReachesSetTimeAndRotateHasStopped)
            {
                if (currentRotateSpeed <= 0)
                {
                    canSpin = false;
                    startButtonAnimator.SetBool("Play", false);
                    questionPanelAnimator.SetBool("Play", true);
                    currentRotateSpeed = startRotateSpeed + Random.Range(-1f, 2f);
                }
            }

            if (canSpin)
            {
                if (enableTimer >= 14f)
                {
                    startButtonAnimator.SetBool("Play2", true);
                }

                if (buttonActivationTimer >= 15.25f)
                {
                    spinButton.SetActive(true);
                }
            }
        }

        private void AddToTimer()
        {
            spinTimer += Time.deltaTime;
        }

        // This script should only care about spinning itself, nothing else

        private Coroutine _spinWheelCoroutine;

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
            // wheel is divided into 8 different question areas
            // choose a random amount to spin to
            var radiusSlots = Mathf.Round(Random.Range(10f, 50f)); // can go 1.3 - 6.2 circles

            var deltaTime = Time.deltaTime;
            // will take a second to rotate if framerate is 60 and speed is 1f
            var interpolation = (360f / 8f) * deltaTime * (1f + (1f / 3f));
            var wait = new WaitForSeconds(deltaTime);
            while (radiusSlots > 0)
            {
                radiusSlots -= interpolation;
                yield return wait;
            }

            _spinWheelCoroutine = null;
        }
    }
}