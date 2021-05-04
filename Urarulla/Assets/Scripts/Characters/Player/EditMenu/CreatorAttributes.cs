using UnityEngine;

namespace DiMe.Urarulla
{
    public class CreatorAttributes : MonoBehaviour
    {
        public CharacteristicInfos characteristicInfos;

        private void Start()
        {
            characteristicInfos = GameManager.Instance.characteristicInfos;
        }
    }
}