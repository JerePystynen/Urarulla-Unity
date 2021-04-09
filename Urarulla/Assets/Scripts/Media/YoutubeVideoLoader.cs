using UnityEngine;
using UnityEngine.Video;
using YoutubePlayer;

namespace Urarulla
{
    public class YoutubeVideoLoader : MonoBehaviour
    {
        private VideoPlayer videoPlayer;

        private void Start()
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        internal void Load(string url) => videoPlayer.PlayYoutubeVideoAsync(url);
    }
}