using UnityEngine;
using UnityEngine.UI;

namespace DiMe.Urarulla
{
    public class UIMainScene : Singleton<UIMainScene>
    {
        private GameObject questionsObj;

        internal MediaPlayer mediaPlayer { get; private set; }
        internal QuestionManager questionManager { get; private set; }
        internal UIStarChoice starChoice { get; private set; }
        internal ScoreUIManager scoreUIManager { get; private set; }

        private Transform tutkintoInfo;

        private const int indexOfBlackSection = 0;
        private const int indexOfOrangeSection = 4;

        private void Start()
        {
            mediaPlayer = transform.Find("tutkinto/tutkinto-overview-scrollview/Viewport/Content").GetComponent<MediaPlayer>();

            questionsObj = transform.Find("questions").gameObject;
            questionManager = questionsObj.GetComponent<QuestionManager>();
            questionManager.Ready();
            questionsObj.SetActive(false);
            
            tutkintoInfo = transform.Find("tutkinto");

            starChoice = transform.GetComponentInChildren<UIStarChoice>(true);
            scoreUIManager = transform.GetComponentInChildren<ScoreUIManager>(true);

            transform.Find("exit-btn").GetComponent<Button>().onClick.AddListener(delegate { Exit(); });

            SetupUI();
        }

        private void SetupUI()
        {
            transform.Find("star-choice").gameObject.SetActive(false);
            transform.Find("questions").gameObject.SetActive(false);
            tutkintoInfo.gameObject.SetActive(false);
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

        internal void SetTutkintoInfoActive(int player, DegreeClean degree)
        {
            SetTutkintoInfo(true);
            mediaPlayer.PlayTutkinto(degree);

            Debug.Log("TODO: set that the player 'player' wins the game");
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