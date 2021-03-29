using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Urarulla
{
    public class QuestionManager : MonoBehaviour
    {
        // Personality traits
        public int physicalScore;
        public int technicalScore;
        public int itScore;

        private float likeCategoryReward = 0.8f;
        private float passCategoryReward = 0.2f;
        private float greedCategoryReward = 0.6f;

        public TextAsset KysymyksetFile;
        private Kysymykset _kysymykset;

        private Question _currentQuestion;

        private TMP_Text _questionTxt;
        private UIAnswerManager _answers;

        internal void Ready()
        {
            _questionTxt = transform.Find("question-txt").GetComponent<TMP_Text>();
            _answers = transform.Find("answer-buttons").GetComponent<UIAnswerManager>();

            var kysymyksetNullable = (Kysymykset?)JsonUtility.FromJson<Kysymykset>(KysymyksetFile.text);
            if (kysymyksetNullable == null)
            {
                Debug.LogError("Error: JSON data could not be loaded.");
                return;
            }
            _kysymykset = (Kysymykset)kysymyksetNullable;
        }

        internal void AskRandom()
        {
            gameObject.SetActive(true);
            if (_kysymykset.ominaisuuskysymykset == null)
            {
                Debug.LogError("Error: JSON kysymys data is missing");
                return;
            }
            SetQuestion(_kysymykset.ominaisuuskysymykset[Random.Range(0, _kysymykset.ominaisuuskysymykset.Length - 1)]);
        }

        private void SetQuestion(Question question)
        {
            _currentQuestion = question;
            _questionTxt.text = question.titles[Random.Range(0, question.titles.Length)];
            _answers.SetAnswer(this, question);
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
                // add these categories to the player
                switch (category)
                {
                    case "creative":

                        break;
                    case "physical":

                        break;
                    case "handy":

                        break;
                    case "team":

                        break;
                    case "leader":

                        break;
                    case "greed":
                        
                        break;
                }
            }
            gameObject.SetActive(false);
            ResponseManager.Instance.Display(answer.responses[Random.Range(0, answer.responses.Length)]);
        }

        private string[] GetRewardedCategories(string type, string[] categories)
        {
            var list = new List<string>();
            var reward = new string[] {};
            switch (type)
            {
                case "love":return categories;
                case "like":
                    for (var i = 0; i < Mathf.RoundToInt((float)categories.Length * likeCategoryReward); i++)
                        list.Add(categories[i]);
                    return list.ToArray();
                default:
                case "pass":
                    for (int i = 0; i < Mathf.RoundToInt((float)categories.Length * passCategoryReward); i++)
                        list.Add(categories[i]);
                    return list.ToArray();
                case "greed":
                    for (int i = 0; i < Mathf.RoundToInt((float)categories.Length * greedCategoryReward); i++)
                        list.Add(categories[i]);
                    list.Add("greed");
                    return list.ToArray();
            }
        }
    }
}