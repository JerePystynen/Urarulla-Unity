using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace DiMe.Urarulla
{
    public class QuestionManager : MonoBehaviour
    {
        private const float __likeCategoryReward = 0.8f;
        private const float __passCategoryReward = 0.2f;
        private const float __greedCategoryReward = 0.6f;

        private Questions _questions;
        private Question _currentQuestion;

        private TMP_Text _titleTxt;
        private TMP_Text _questionTxt;
        private UIAnswerManager _answers;

        internal void Ready()
        {
            _titleTxt = transform.Find("title-txt").GetComponent<TMP_Text>();
            _questionTxt = transform.Find("question-txt").GetComponent<TMP_Text>();
            _answers = transform.Find("answer-buttons").GetComponent<UIAnswerManager>();

            _questions = GameManager.Questions;
        }

        internal void AskRandomOminaisuus()
        {
            if (_questions.characteristicsQuestions == null)
            {
                Debug.LogError("Error: ominaisuuskysymys data is missing!");
                return;
            }
            SetQuestion(_questions.characteristicsQuestions.Random());
        }

        internal void AskRandomKompa()
        {
            if (_questions.trickyQuestions == null)
            {
                Debug.LogError("Error: ominaisuuskysymys data is missing!");
                return;
            }
            SetQuestion(_questions.trickyQuestions.Random());
        }

        internal void SetQuestion(Question question)
        {
            gameObject.SetActive(true);
            _currentQuestion = question;
            _titleTxt.text = "Ominaisuuskysymykset";
            _questionTxt.text = question.titles.Random();
            _answers.SetAnswer(this, question);
        }
        
        internal void SelectChoice()
        {
            
        }

        internal void SelectAnswer(Answer answer)
        {
            // [aloituskysymys (ominaisuuskysymys)]
            // kysymys: "tykkäätkö tehdä fyysistä työtä?"

            // jos paljon positiivista putkeen, "Helppo peli. Tulevaisuudenkuviosi on selkeät."

            /*
                [answer-types:]
                love 	 take 100% of categories
                like 	 take 80% of categories
                dislike	 take 0% of categories
                pass	 take 20% of categories
                greed    take 60% of categories & +greed*

                greed changes odds in the wheel, negative or positive, randomly
                Boss likes greed.
                "Greed is good."
                
                // (smug) on monia tapoja tienata rahaa, katsotaas lisää (tarkemmin)

                [category:]
                creative
                physical
                handy
                team
                leader
            */

            // IF ANSWER IS POSITIVE


            // IF ANSWER IS NEGATIVE


            var categories = GetRewardedCategories(answer.type, _currentQuestion.positive_categories);
            var greed = 0;
            foreach (var category in categories)
            {
                Debug.Log(category);
                switch (category)
                {
                    case "creative":
                        // ScoreManager.
                        GameManager.Instance.currentTurnPlayer.characteristics.creative++;
                        break;
                    case "physical":
                        GameManager.Instance.currentTurnPlayer.characteristics.physical++;
                        break;
                    case "handy":
                        GameManager.Instance.currentTurnPlayer.characteristics.handy++;
                        break;
                    case "team":
                        GameManager.Instance.currentTurnPlayer.characteristics.team++;
                        break;
                    case "leader":
                        GameManager.Instance.currentTurnPlayer.characteristics.leader++;
                        break;
                    case "greed":
                        GameManager.Instance.currentTurnPlayer.characteristics.greed++;
                        greed++;
                        break;
                }
            }

            var speechType = SpeechType.talk;
            if (greed > 0) speechType = SpeechType.smug;
            else if (categories.Length > 3) speechType = SpeechType.yell;

            gameObject.SetActive(false);
            
            var response = answer.responses.Random();
            ResponseManager.Speak(response, speechType);
            TextToSpeech.TTS(response);
        }

        private string[] GetRewardedCategories(string type, string[] categories)
        {
            var reward = new string[] {};
            switch (type)
            {
                case "love":return categories;
                case "like":return GetPercentageOfArray(categories, __likeCategoryReward).ToArray();
                default:
                case "pass":return GetPercentageOfArray(categories, __passCategoryReward).ToArray();
                case "greed":
                    var list = GetPercentageOfArray(categories, __greedCategoryReward);
                    list.Add("greed");
                    return list.ToArray();
            }
        }

        private List<string> GetPercentageOfArray(string[] array, float percentage)
        {
            if (array == null)
            {
                Debug.LogError("Error: array is null!");
                return null;
            }
            
            if (array.Length == 0)
            {
                Debug.LogError("Error: array is empty!");
                return null;
            }

            var list = new List<string>();
            for (int i = 0; i < Mathf.RoundToInt((float)array.Length * percentage); i++)
                list.Add(array[i]);
            return list;
        }
    }
}