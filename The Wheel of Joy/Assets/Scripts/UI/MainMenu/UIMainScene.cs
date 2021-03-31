using UnityEngine;
using UnityEngine.UI;

namespace Urarulla
{
    public class UIMainScene : Singleton<UIMainScene>
    {
        internal MediaPlayer mediaPlayer;

        private GameObject questionsObj;
        internal QuestionManager questionManager;
        private UIStarChoice starChoice;
        
        private Transform tutkintoInfo;

        private const int indexOfBlackSection = 0;
        private const int indexOfOrangeSection = 4;

        private void Start()
        {
            mediaPlayer = transform.Find("tutkinto-overview-scrollview/Viewport/Content").GetComponent<MediaPlayer>();

            questionsObj = transform.Find("questions").gameObject;
            questionManager = questionsObj.GetComponent<QuestionManager>();
            questionManager.Ready();
            questionsObj.SetActive(false);
            
            tutkintoInfo = transform.Find("tutkinto");

            starChoice = transform.GetComponentInChildren<UIStarChoice>(true);

            transform.Find("exit-btn").GetComponent<Button>().onClick.AddListener(delegate { Exit(); });
        }

        internal void AskQuestion(int index)
        {
            switch (index)
            {
                case indexOfBlackSection:
                    questionManager.AskRandomKompa();
                    break;
                case indexOfOrangeSection:
                    starChoice.GivePlayerChoice();
                    break;
                default:
                    questionManager.AskRandomOminaisuus();
                    break;
            }
        }

        internal void SetTutkintoInfoActive(int player, TutkintoNimike tutkinto)
        {
            SetTutkintoInfo(true);
            mediaPlayer.PlayTutkinto(tutkinto);

            // set that the player 'player' wins the game
        }
        internal void SetTutkintoInfoDeactive() => SetTutkintoInfo(false);
        private void SetTutkintoInfo(bool value)
        {
            tutkintoInfo.gameObject.SetActive(value);
        }

        private void Exit()
        {
            GameManager.Instance.SetMenuActive(0);
        }
    }
}