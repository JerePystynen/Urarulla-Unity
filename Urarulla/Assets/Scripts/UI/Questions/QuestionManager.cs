using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Urarulla
{
    public class QuestionManager : MonoBehaviour
    {
        private const float likeCategoryReward = 0.8f;
        private const float passCategoryReward = 0.2f;
        private const float greedCategoryReward = 0.6f;

        private Kysymykset kysymykset;
        private Question _currentQuestion;

        private TMP_Text _titleTxt;
        private TMP_Text _questionTxt;
        private UIAnswerManager _answers;

        internal void Ready()
        {
            _titleTxt = transform.Find("title-txt").GetComponent<TMP_Text>();
            _questionTxt = transform.Find("question-txt").GetComponent<TMP_Text>();
            _answers = transform.Find("answer-buttons").GetComponent<UIAnswerManager>();

            kysymykset = GameManager.Instance.kysymykset;
        }

        internal void AskRandomOminaisuus()
        {
            if (kysymykset.ominaisuuskysymykset == null)
            {
                Debug.LogError("Error: ominaisuuskysymys data is missing!");
                return;
            }
            SetQuestion(kysymykset.ominaisuuskysymykset.Random());
        }

        internal void AskRandomKompa()
        {
            if (kysymykset.kompakysymykset == null)
            {
                Debug.LogError("Error: ominaisuuskysymys data is missing!");
                return;
            }
            SetQuestion(kysymykset.kompakysymykset.Random());
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

            var categories = GetRewardedCategories(answer.type, _currentQuestion.categories);
            var greed = 0;
            foreach (var category in categories)
            {
                Debug.Log(category);
                switch (category)
                {
                    case "creative":
                        GameManager.Instance.currentTurnPlayer.characteristics.creativeScore++;
                        break;
                    case "physical":
                        GameManager.Instance.currentTurnPlayer.characteristics.physicalScore++;
                        break;
                    case "handy":
                        GameManager.Instance.currentTurnPlayer.characteristics.handyScore++;
                        break;
                    case "team":
                        GameManager.Instance.currentTurnPlayer.characteristics.teamScore++;
                        break;
                    case "leader":
                        GameManager.Instance.currentTurnPlayer.characteristics.leaderScore++;
                        break;
                    case "greed":
                        GameManager.Instance.currentTurnPlayer.characteristics.greedScore++;
                        greed++;
                        break;
                }
            }

            var speechType = SpeechType.talk;
            if (greed > 0) speechType = SpeechType.smug;
            else if (categories.Length > 3) speechType = SpeechType.yell;

            gameObject.SetActive(false);
            
            var response = answer.responses.Random();
            GameManager.responseManager.Speak(response, speechType);
            GameManager.textToSpeech.TTS(response);
        }

        private string[] GetRewardedCategories(string type, string[] categories)
        {
            var reward = new string[] {};
            switch (type)
            {
                case "love":return categories;
                case "like":return GetPercentageOfArray(categories, likeCategoryReward).ToArray();
                default:
                case "pass":return GetPercentageOfArray(categories, passCategoryReward).ToArray();
                case "greed":
                    var list = GetPercentageOfArray(categories, greedCategoryReward);
                    list.Add("greed");
                    return list.ToArray();
            }
        }

        private List<string> GetPercentageOfArray(string[] array, float percentage)
        {
            var list = new List<string>();
            for (int i = 0; i < Mathf.RoundToInt((float)array.Length * percentage); i++)
                list.Add(array[i]);
            return list;
        }
    }
}