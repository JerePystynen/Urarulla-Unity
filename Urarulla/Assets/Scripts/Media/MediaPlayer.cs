using UnityEngine;

namespace DiMe.Urarulla
{
    public class MediaPlayer : MonoBehaviour
    {
        internal void PlayTutkinto(DegreeClean degree)
        {
            var videoLoaders = transform.GetComponentsInChildren<YoutubeVideoLoader>(true);
            for (int i = 0; i < videoLoaders.Length; i++)
            {
                if (degree.videot.Length < i) break;
                videoLoaders[i].Load(degree.videot[i]);
            }

            var imageLoaders = transform.GetComponentsInChildren<ImageLoader>(true);
            for (int i = 0; i < imageLoaders.Length; i++)
            {
                if (degree.kuvat.Length < i) break;
                imageLoaders[i].Load(degree.kuvat[i]);
            }
        }
    }
}