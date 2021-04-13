using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Urarulla
{
    public class PlayerScoreManager : MonoBehaviour
    {
        private RectTransform flag;

        public bool goalReached;

        public GameObject progressBar;
        private List<Image> progressBars = new List<Image>();
        private List<float> progressions = new List<float>();

        private void Start()
        {
            flag = transform.Find("progression/flag-img").GetComponent<RectTransform>();

            for (var i = 0; i < GameManager.Instance.players.Count; i++)
            {
                var player = GameManager.Instance.players[i];
                var bar = Instantiate(progressBar, transform.Find("progression/bar")).transform;
                bar.name = $"progress-bar ({i})";
                var img = bar.GetComponent<Image>();
                progressBars.Add(img);
                img.fillAmount = 0;
                progressions.Add(0);
            }

            StartCoroutine(MoveFlagCoroutine());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) SetTurnPlayerProgression(0);
            if (Input.GetKeyDown(KeyCode.S)) SetTurnPlayerProgression(.2f);
            if (Input.GetKeyDown(KeyCode.D)) SetTurnPlayerProgression(.75f);
            if (Input.GetKeyDown(KeyCode.F)) SetTurnPlayerProgression(1);

            for (var i = 0; i < progressions.Count; i++)
            {
                var bar = progressBars[i];
                if (!bar.enabled) continue;
                if (bar.fillAmount >= .9985f)
                {
                    UIMainScene.Instance.SetTutkintoInfoActive(i, GetTutkinto());
                    bar.enabled = false;
                    continue;
                }
                bar.fillAmount = Mathf.SmoothStep(bar.fillAmount, Mathf.Clamp(progressions[i], 0, 1), 27 * Time.deltaTime);
            }
        }

        private TutkintoNimike GetTutkinto()
        {
            Debug.Log("todo: add a way to get which is the closest right player TutkintoNimike...");
            return null;
        }

        private IEnumerator MoveFlagCoroutine()
        {
            LeanTween.moveY(flag, 105, 2f).setEase(LeanTweenType.easeInBack);
            yield return new WaitForSeconds(2f);
            LeanTween.moveY(flag, 85, 2f).setEase(LeanTweenType.easeInBounce);
            yield return new WaitForSeconds(2f);
            StartCoroutine(MoveFlagCoroutine());
        }

        internal void AddProgression(int player, float value)
        {
            if (progressions.Count == 0)
            {
                Debug.LogError("Error: There are no players in the game!");
                return;
            }
            progressions[player] += value;
        }

        internal void SetProgression(int player, float value)
        {
            if (progressions.Count == 0)
            {
                Debug.LogError("Error: There are no players in the game!");
                return;
            }
            progressions[player] = value;
        }
        
        internal void SetTurnPlayerProgression(float value) => SetProgression(GameManager.currentTurnPlayerIndex, value);
    }
}