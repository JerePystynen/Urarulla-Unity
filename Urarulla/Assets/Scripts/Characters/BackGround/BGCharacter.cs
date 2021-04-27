using System.Collections;
using UnityEngine;

namespace DiMe.Urarulla
{
    public class BGCharacter : MonoBehaviour
    {
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C)) SetState("clap");
        }

        internal void SetState(string anim)
        {
            switch (anim)
            {
                case "clap":
                    StartCoroutine(SetStateCoroutine($"clap {Random.Range(0, 1)}"));
                    break;
            }
        }

        private IEnumerator SetStateCoroutine(string anim)
        {
            yield return new WaitForSeconds(Random.Range(0f, 2f));
            animator.speed = Random.Range(0.9f, 1.4f);
            animator.Play(anim);
        }
    }
}