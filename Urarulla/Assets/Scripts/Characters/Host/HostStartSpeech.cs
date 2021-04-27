using UnityEngine;

namespace DiMe.Urarulla
{
    public class HostStartSpeech : MonoBehaviour
    {
        private string[] startLines =
        {
            "Naiset ja herrat, tervetuloa tämän illan kilpailuun. Tämä on Urarulla.",
            "Naiset ja herrat, kuka on valmis tämän illan Urarullaan? Aloitetaan.",
            "Tervetuloa tämän illan kisaan. Olen juontajanne Jyrki Merkolainen ja tämä... on... Urarulla.",
            "Hyvät naiset ja herrat. Olemme saapuneet tänne. Kuka on valmis aloittamaan?",
        };

        private void Start() => GameManager.OnGameStart += OnGameStart;

        private void OnGameStart() => TextToSpeech.TTS(startLines.Random());
    }
}