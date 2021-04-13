using UnityEngine;

namespace Urarulla
{
    public class CreatorAttributes : MonoBehaviour
    {
        public Yleisominaisuudet yleisominaisuudet;

        private void Start()
        {
            yleisominaisuudet = GameManager.Instance.yleisominaisuudet;
        }
    }
}