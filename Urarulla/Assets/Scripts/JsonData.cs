using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Linq;

namespace DiMe.Urarulla
{
    public class JsonData : MonoBehaviour
    {
        private Questions questionsInJsonTEMP;
        public Questionss answeredQuestions;

        public TextAsset JSONfile;

        public Button spinButton;
        public Button showQuestionButton;

        public Animator questionPanelAnimator;

        public TMP_Text questionField;
        public TMP_Text answerField;
        public TMP_Text lahdeField;

        public int currentQuestionColorNum;

        private int previousColorNum;
        private int currentQuestionIndex;
        private int amountOfQuestions;
        private int questionNum;

        private bool canShowQuestion;
        private bool questionEnded;

        private float timer;

        private string currentQuestion;
        private string currentAnswer;
        private string currentSource;

        private void Start()
        {
            answeredQuestions.answeredsList = new List<List<int>>() {
                answeredQuestions.answeredsGreen,
                answeredQuestions.answeredsYellow,
                answeredQuestions.answeredsOrange,
                answeredQuestions.answeredsPink,
                answeredQuestions.answeredsPurple,
                answeredQuestions.answeredsBlue,
                answeredQuestions.answeredsBlack,
                answeredQuestions.answeredsWhite,
            };

            // todo: load json from file

        }

        private int AmountOfQuestions => questionsInJsonTEMP.questions.Length + 1;

        public void Run(int colorNum)
        {
            colorNum = currentQuestionColorNum;
            
            // Reads the json file which is manualy inputted (Temporary)
            questionsInJsonTEMP = JsonUtility.FromJson<Questions>(JSONfile.text);
            
            // saves the number of question availeable to be used everywhere
            var randomNumber = Random.Range(1, AmountOfQuestions);

            if (randomNumber == 0)
            {
                rollNum();
            }
            else
            {
                bool answered = false;
                foreach (var list in answeredQuestions.answeredsList)
                {
                    if (answered)
                        break;

                    foreach (var number in from int number in list where list.Contains(number) select number)
                    {
                        answered = true;
                        rollNum(number);
                        break;
                    }
                }

                if (answered)
                    return;

                RunQuestion(randomNumber);
            }
        }

        public void rollNum(int number = 0)
        {
            Run(number);
        }

        private void Update()
        {
            if (canShowQuestion)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0f;
            }

            // Change that sometime
            if (canShowQuestion)
            {
                if (timer >= 5f)
                {
                    canShowQuestion = false;
                    questionEnded = false;
                }
            }
        }

        public void RunQuestion(int currentQuestionIndex)
        {
            foreach (Question question in questionsInJsonTEMP.questions)
            {
                // Debug.Log(question.KysymysNro.Equals(questionNum));

                // if the randomized "questionNum" and the one of the questions have the same number (which it always has)
                // saves the information of the question temporarily (which is refreshed every time this code runs)
                if (!question.index.Equals(questionNum))
                    continue;

                // saving process
                currentQuestionIndex = question.index;
                currentQuestion = question.questionText;
                currentAnswer = question.answerTxt;
                currentSource = question.source;

                // adds current question to lists that makes so that it doesnt come up without restrating

                foreach (var list in answeredQuestions.answeredsList)
                {
                    // if (currentQuestionColorNum != System.Array.IndexOf(list.ToArray(), ))
                    //     continue;

                    if (list.Count == amountOfQuestions)
                        continue;

                    if (questionEnded)
                        continue;

                    list.Add(currentQuestionIndex);
                }

                previousColorNum = currentQuestionColorNum;

                Debug.Log($"Question {currentQuestionIndex} {currentQuestion} {currentAnswer} {currentSource}");

                // Plays the animation that shows the question
                questionPanelAnimator.Play("Play");
                // questionPanelAnimator.SetBool("Play", true);

                showQuestionButton.gameObject.SetActive(true);

                if (questionEnded)
                {
                    questionField.text = question.questionText;
                }
                else
                {
                    questionField.text = "GAME OVER IDIOT";
                }

                ClearInputFields();
            }
        }

        private void ClearInputFields()
        {
            answerField.text = (" ");
            lahdeField.text = (" ");
        }

        public void showQuestion()
        {
            canShowQuestion = true;
            showQuestionButton.gameObject.SetActive(false);
            answerField.text = currentAnswer;

            if (currentSource != null)
            {
                lahdeField.text = currentSource;
            }
        }

        [System.Serializable]
        public class Questionss
        {
            public List<int> answeredsGreen = new List<int>();
            public List<int> answeredsYellow = new List<int>();
            public List<int> answeredsOrange = new List<int>();
            public List<int> answeredsPink = new List<int>();
            public List<int> answeredsPurple = new List<int>();
            public List<int> answeredsBlue = new List<int>();
            public List<int> answeredsBlack = new List<int>();
            public List<int> answeredsWhite = new List<int>();

            public List<List<int>> answeredsList = new List<List<int>>();
        }

        [System.Serializable]
        public class Questions
        {
            public Question[] questions;
        }

        [System.Serializable]
        public class Question
        {
            public int index;
            public string questionText;
            public string answerTxt;
            public string source;
        }
    }
}