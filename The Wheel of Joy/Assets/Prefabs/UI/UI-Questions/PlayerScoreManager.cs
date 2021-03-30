using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

namespace Urarulla
{
    public class PlayerScoreManager : MonoBehaviour
    {
        private RectTransform flag;

        public bool goalReached;

        public GameObject progressBar;
        private Image[] progressBars;

        private void Start()
        {
            flag = transform.Find("progression/flag-img").GetComponent<RectTransform>();

            progressBars = (from player in GameManager.Instance.players
                let bar = Instantiate(progressBar, transform.Find("progression/bar")).transform
                select bar.GetComponent<Image>()).ToArray();

            StartCoroutine(MoveFlagCoroutine());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) SetProgression(0, 0);
            if (Input.GetKeyDown(KeyCode.S)) SetProgression(0, .2f);
            if (Input.GetKeyDown(KeyCode.D)) SetProgression(0, .75f);
            if (Input.GetKeyDown(KeyCode.F)) SetProgression(0, 1);
        }

        private IEnumerator MoveFlagCoroutine()
        {
            LeanTween.moveY(flag, 105, 2f).setEase(LeanTweenType.easeInBounce);
            yield return new WaitForSeconds(2f);
            LeanTween.moveY(flag, 85, 2f).setEase(LeanTweenType.easeInBounce);
            yield return new WaitForSeconds(2f);
            StartCoroutine(MoveFlagCoroutine());
        }

        internal void SetProgression(int player, float amount) => StartCoroutine(SetProgressionCoroutine(player, amount));
        private IEnumerator SetProgressionCoroutine(int player, float amount)
        {
            // // progressBars[player].fillAmount
            // DOTween.To(progressBars[player].fillAmount, 0f, amount, 0.2f);

            // var d = LeanTween.value(progressBars[player].fillAmount, amount, .7f).setEaseOutQuad();
            // while (d != null)
            // {
            //     progressBars[player].fillAmount = d.lastVal;
            //     yield return new WaitForSeconds(Time.deltaTime);
            // }

            yield return null;
        }
    }
}