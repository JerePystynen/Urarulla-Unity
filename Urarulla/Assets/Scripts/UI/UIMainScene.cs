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

        private const int indexOfBlackSection = 0;
        private const int indexOfOrangeSection = 4;

        private Transform _degreeInfo;

        private GameObject _playersBar;
        private GameObject _starChoiseMenu;
        private GameObject _questionMenu;
        private GameObject _degreeMenu;

        internal bool IsAnyMenuActive => _playersBar.activeSelf || _starChoiseMenu.activeSelf || _questionMenu.activeSelf || _degreeMenu.activeSelf;

        private void Start()
        {
            mediaPlayer = transform.Find("degree/degree-overview-scrollview/Viewport/Content").GetComponent<MediaPlayer>();

            questionsObj = transform.Find("questions").gameObject;
            questionManager = questionsObj.GetComponent<QuestionManager>();
            questionManager.Ready();
            questionsObj.SetActive(false);
            
            _degreeInfo = transform.Find("degree");

            starChoice = transform.GetComponentInChildren<UIStarChoice>(true);
            scoreUIManager = transform.GetComponentInChildren<ScoreUIManager>(true);

            transform.Find("exit-btn").GetComponent<Button>().onClick.AddListener(delegate { Exit(); });

            SetupUI();
        }

        private void SetupUI()
        {
            transform.Find("star-choice").gameObject.SetActive(false);
            transform.Find("questions").gameObject.SetActive(false);
            _degreeInfo.gameObject.SetActive(false);
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

        internal void SetDegreeInfoActive(int player, Degree degree)
        {
            SetDegreeInfo(true);
            mediaPlayer.PlayDegree(degree);

            Debug.Log("TODO: set the title to the degree recommended and then set that the player 'player' wins the game...");
        }

        internal void SetDegreeInfoDeactive() => SetDegreeInfo(false);
        
        private void SetDegreeInfo(bool value) => _degreeInfo.gameObject.SetActive(value);

        private void Exit()
        {
            GameManager.Instance.SetMenuActive(0);
        }
    }
}