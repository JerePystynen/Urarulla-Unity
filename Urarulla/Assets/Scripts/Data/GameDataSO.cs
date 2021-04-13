using UnityEngine;

namespace Urarulla
{
    [CreateAssetMenu(menuName="Urarulla/GameData")]
    public class GameDataSO : ScriptableObject
    {
        [Header("Files")]
        public TextAsset AlaFile;
        public TextAsset KysymyksetFile;
        public TextAsset TutkinnotFile;
        public TextAsset YleisominaisuudetFile;
        
        [Header("Prefabs")]
        public GameObject[] CharacterModels;
        public GameObject CategorySlider;
    }
}
