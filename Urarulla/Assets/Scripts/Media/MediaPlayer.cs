using UnityEngine;

namespace DiMe.Urarulla
{
    public class MediaPlayer : MonoBehaviour
    {
        internal void PlayDegree(Degree degree)
        {
            var videoLoaders = transform.GetComponentsInChildren<YoutubeVideoLoader>(true);
            for (int i = 0; i < videoLoaders.Length; i++)
            {
                if (i >= degree.videos.Length)
                    break;
                videoLoaders[i].Load(degree.videos[i]);
            }

            var imageLoaders = transform.GetComponentsInChildren<ImageLoader>(true);
            for (int i = 0; i < imageLoaders.Length; i++)
            {
                if (i >= degree.images.Length)
                    break;
                imageLoaders[i].Load(degree.images[i]);
            }
        }
    }
}