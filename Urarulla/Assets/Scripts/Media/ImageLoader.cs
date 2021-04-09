using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Urarulla
{
    public class ImageLoader : MonoBehaviour
    {
        private Image image;

        private void Start()
        {
            image = GetComponent<Image>();
        }

        internal void Load(string url) => StartCoroutine(LoadImageCoroutine(url));

        private IEnumerator LoadImageCoroutine(string url)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                yield return www.SendWebRequest();
                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError(www.error);
                    yield break;
                }
                var tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
                var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
                image.overrideSprite = sprite;
            }
        }
    }
}