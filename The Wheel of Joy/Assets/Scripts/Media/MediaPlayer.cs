using UnityEngine;

namespace Urarulla
{
    public class MediaPlayer : MonoBehaviour
    {
        private void Start()
        {
        }

        internal void PlayTutkinto(TutkintoNimike tutkinto)
        {
            var videoLoaders = transform.GetComponentsInChildren<YoutubeVideoLoader>(true);
            for (int i = 0; i < videoLoaders.Length; i++)
            {
                if (tutkinto.videot.Length < i) break;
                videoLoaders[i].Load(tutkinto.videot[i]);
            }

            var imageLoaders = transform.GetComponentsInChildren<ImageLoader>(true);
            for (int i = 0; i < imageLoaders.Length; i++)
            {
                if (tutkinto.kuvat.Length < i) break;
                imageLoaders[i].Load(tutkinto.kuvat[i]);
            }
        }
    }
}
