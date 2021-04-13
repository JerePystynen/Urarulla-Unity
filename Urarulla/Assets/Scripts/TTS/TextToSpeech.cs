using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace Urarulla
{
    public class TextToSpeech : MonoBehaviour
    {
        private AudioSource source;

        private void Start()
        {
            source = GetComponent<AudioSource>();
        }

        internal void TTS(params string[] input) => StartCoroutine(TTSCoroutine(input));
        private IEnumerator TTSCoroutine(string[] input)
        {
            if (input.Length == 0)
            {
                Debug.LogError("Error: TTS' input is empty!");
                yield break;
            }

            System.Collections.Generic.IEnumerable<string> enumerable()
            {
                foreach (var sentence in input)
                {
                    var split = sentence.Split(' ');
                    foreach (var str in split)
                    {
                        yield return Regex.Replace(str, @"[^0-9a-zA-Z:,]+", "").ToLower();
                    }
                }
            }
            var words = enumerable().ToArray();
            if (words.Length == 0) yield break;

            var content = "";
            for (int i = 0; i < words.Length; i++)
            {
                var str = words[i];
                content += $"{str}";
                if (i < words.Length - 1) content += "%20";
            }

            var url = $"http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=1024&client=tw-ob&q=+{content}&tl=fi";
            using (var www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
            {
                yield return www.SendWebRequest();
                if (www.result == UnityWebRequest.Result.ConnectionError
                || www.result == UnityWebRequest.Result.ProtocolError)
                    yield break;
                source.PlayOneShot(DownloadHandlerAudioClip.GetContent(www));
            }

            yield return null;
        }
    }
}