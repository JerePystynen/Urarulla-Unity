using UnityEngine;

namespace DiMe.Urarulla
{
    [CreateAssetMenu(menuName = "Urarulla/GameData")]
    public class GameDataSO : ScriptableObject
    {
        [Header("Files")]
        public TextAsset CharacteristicsFile;
        public TextAsset DegreeFile;

        [Header("Questions")]
        public TextAsset IndustryQuestionsFile;
        public TextAsset TrickyQuestionsFile;
        public TextAsset CharacteristicsQuestionsFile;

        [Header("Prefabs")]
        public GameObject[] BackGroundCharacterModels;
        public GameObject[] PlayerCharacterModels;

        public GameObject CategorySlider;

        // Difference between setup and normal stands is being able to go to EditMenu from clicking the stand's character.
        [Header("Stands")]
        public GameObject PlayerSetupStand;
        public GameObject PlayerNormalStand;

        [Header("UI")]
        public GameObject PlayerProgressionBar;

        [Header("Sound Effects")]
        public AudioClip GameStartSound;
        public AudioClip SelectButtonClickedSound;
        public AudioClip DenySelectionSound;
        public AudioClip LuckWheelSpinningSound;
        
        [Header("Sprites")]
        public Sprite StartButtonUpSprite;
        public Sprite StartButtonDownSprite;
    }
}