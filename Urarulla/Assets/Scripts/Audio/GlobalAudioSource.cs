using UnityEngine;

namespace DiMe.Urarulla
{
    public class GlobalAudioSource : Singleton<GlobalAudioSource>
    {
        private AudioSource source;

        private void Start()
        {
            source = GetComponent<AudioSource>();
            PlayGameStartSound();
        }

        internal static void PlayGameStartSound() => Instance.source.PlayOneShot(GameManager.Data.GameStartSound, 0.8f);
        
        internal static void PlayButtonSelectSound() => Instance.source.PlayOneShot(GameManager.Data.SelectButtonClickedSound, 1f);

        internal static void PlayDenySelectionSound() => Instance.source.PlayOneShot(GameManager.Data.DenySelectionSound, 1f);
    }
}