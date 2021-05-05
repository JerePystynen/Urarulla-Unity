using System.Collections;
using UnityEngine;
using Cinemachine;

namespace DiMe.Urarulla
{
    public class MainSceneStartupPathFollower : MonoBehaviour
    {
        private CinemachineDollyCart cart;
        public new Camera camera;

        private void Start()
        {
            cart = GetComponent<CinemachineDollyCart>();
            GameManager.OnGameStarted += OnGameStart;
        }

        private void OnGameStart()
        {
            StartCoroutine(FollowPathCoroutine());
        }
        
        private IEnumerator FollowPathCoroutine()
        {
            cart.m_Position = 0;
            var delay = new WaitForSeconds(Time.deltaTime * 2);
            var position = cart.m_Position;
            while (position < 1)
            {
                cart.m_Position += Time.deltaTime;
                camera.fieldOfView = 44f + 18f * position;
                yield return delay;
            }
        }
    }
}