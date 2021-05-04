using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DiMe.Urarulla
{
    public class ScoreUIManager : Singleton<ScoreUIManager>
    {
        private RectTransform flag;

        public bool goalReached;

        private List<Image> progressBars = new List<Image>();
        private List<float> progressions = new List<float>();

        private void Start()
        {
            flag = transform.Find("progression/flag-img").GetComponent<RectTransform>();
            GameManager.OnGameStart += OnGameStart;
        }

        private void OnGameStart()
        {
            StartCoroutine(MoveFlagCoroutine());
            CreateBars();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) SetTurnPlayerProgression(0);
            if (Input.GetKeyDown(KeyCode.S)) SetTurnPlayerProgression(.2f);
            if (Input.GetKeyDown(KeyCode.D)) SetTurnPlayerProgression(.75f);
            if (Input.GetKeyDown(KeyCode.F)) SetTurnPlayerProgression(1);

            if (goalReached)
            {
                return;
            }
            for (var i = 0; i < progressions.Count; i++)
            {
                var bar = progressBars[i];
                if (!bar.enabled) continue;
                if (bar.fillAmount >= .9985f)
                {
                    var degreeN = (Degree?)GetDegreeClean(GameManager.Players[i].characteristics);
                    if (degreeN != null)
                    {
                        var degree = (Degree)degreeN;
                        UIMainScene.Instance.SetDegreeInfoActive(i, degree);
                        goalReached = true;
                    }
                    bar.enabled = false;
                    continue;
                }
                bar.fillAmount = Mathf.SmoothStep(bar.fillAmount, Mathf.Clamp(progressions[i], 0, 1), 27 * Time.deltaTime);
            }
        }

        private void CreateBars()
        {
            if (GameManager.Players.Count == 0)
            {
                Debug.LogError("Error: There are no players!");
                return;
            }
            var holder = transform.Find("progression/bar");
            for (var i = 0; i < GameManager.Players.Count; i++)
            {
                var player = GameManager.Instance.players[i];
                var bar = Instantiate(GameManager.Data.PlayerProgressionBar, holder).transform;
                bar.name = $"progress-bar ({i})";
                var img = bar.GetComponent<Image>();
                progressBars.Add(img);
                img.fillAmount = 0;
                progressions.Add(0);
            }
        }

        private Degree GetDegreeClean(Characteristics characteristics)
        {
            var t = characteristics.GetClosestDegree();
            return t;
        }

        private IEnumerator MoveFlagCoroutine()
        {
            LeanTween.moveY(flag, 105, 2f).setEase(LeanTweenType.easeInBack);
            yield return new WaitForSeconds(2f);
            LeanTween.moveY(flag, 85, 2f).setEase(LeanTweenType.easeInBounce);
            yield return new WaitForSeconds(2f);
            StartCoroutine(MoveFlagCoroutine());
        }

        internal void AddProgression(int player, float value) => SetProgression(player, progressions[player] + value);

        internal void SetTurnPlayerProgression(float value) => SetProgression(GameManager.CurrentTurnPlayerIndex, value);

        internal void SetProgression(int player, float value)
        {
            if (progressions.Count == 0)
            {
                Debug.LogError("Error: There are no player progression bars in the ui!");
                return;
            }
            progressions[player] = value;
        }
    }
}