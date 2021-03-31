using UnityEngine;

namespace Urarulla
{
    public class UIStarChoice : MonoBehaviour
    {
        public void GivePlayerChoice()
        {
            gameObject.SetActive(true);
        }

        public void Select(int index)
        {
            switch (index)
            {
                case 0:
                    // ominaisuus
                    UIMainScene.Instance.questionManager.AskRandomOminaisuus();
                    break;
                case 1:
                    // ala / koulutus

                    break;
                case 2:
                    // työ

                    break;
            }

            gameObject.SetActive(false);
        }
    }
}