using UnityEngine;

namespace Urarulla
{
    [CreateAssetMenu(menuName="Urarulla/GameData")]
    public class GameDataSO : ScriptableObject
    {
        public TextAsset alaFile;
        public TextAsset kysymyksetFile;
        public TextAsset tutkinnotFile;
        public TextAsset yleisominaisuudetFile;
    }
}
