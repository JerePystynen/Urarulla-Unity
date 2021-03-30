using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Urarulla
{
    public class QuestionManager : MonoBehaviour
    {
        private float likeCategoryReward = 0.8f;
        private float passCategoryReward = 0.2f;
        private float greedCategoryReward = 0.6f;

        public TextAsset KysymyksetFile;
        internal Kysymykset kysymykset;

        private Question _currentQuestion;

        private TMP_Text _titleTxt;
        private TMP_Text _questionTxt;
        private UIAnswerManager _answers;

        internal void Ready()
        {
            _titleTxt = transform.Find("title-txt").GetComponent<TMP_Text>();
            _questionTxt = transform.Find("question-txt").GetComponent<TMP_Text>();
            _answers = transform.Find("answer-buttons").GetComponent<UIAnswerManager>();

            var kysymyksetNullable = (Kysymykset?)JsonUtility.FromJson<Kysymykset>(KysymyksetFile.text);
            if (kysymyksetNullable == null)
            {
                Debug.LogError("Error: JSON data could not be loaded.");
                return;
            }
            kysymykset = (Kysymykset)kysymyksetNullable;
        }

        internal void AskRandomOminaisuus()
        {
            if (kysymykset.ominaisuuskysymykset == null)
            {
                Debug.LogError("Error: JSON kysymys data is missing");
                return;
            }
            SetQuestion(kysymykset.ominaisuuskysymykset[Random.Range(0, kysymykset.ominaisuuskysymykset.Length - 1)]);
        }

        internal void SetQuestion(Question question)
        {
            gameObject.SetActive(true);
            _currentQuestion = question;
            _titleTxt.text = "Ominaisuuskysymykset";
            _questionTxt.text = question.titles[Random.Range(0, question.titles.Length)];
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

            foreach (var category in GetRewardedCategories(answer.type, _currentQuestion.categories))
            {
                Debug.Log(category);
                switch (category)
                {
                    case "creative":
                        GameManager.Instance.currentTurnPlayer.personality.creativeScore++;
                        break;
                    case "physical":
                        GameManager.Instance.currentTurnPlayer.personality.physicalScore++;
                        break;
                    case "handy":
                        GameManager.Instance.currentTurnPlayer.personality.handyScore++;
                        break;
                    case "team":
                        GameManager.Instance.currentTurnPlayer.personality.teamScore++;
                        break;
                    case "leader":
                        GameManager.Instance.currentTurnPlayer.personality.leaderScore++;
                        break;
                    case "greed":
                        GameManager.Instance.currentTurnPlayer.personality.greedScore++;
                        break;
                }
            }
            gameObject.SetActive(false);
            ResponseManager.Instance.Display(answer.responses[Random.Range(0, answer.responses.Length)], "talk");
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