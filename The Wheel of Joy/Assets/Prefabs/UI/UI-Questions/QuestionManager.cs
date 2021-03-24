using TMPro;
using UnityEngine;

namespace Urarulla
{
    public class QuestionManager : MonoBehaviour
    {
        // pro-positive
        // semi-positive
        // neutral
        // 
        // 

        // Personality traits
        public int physicalScore;
        public int technicalScore;
        public int itScore;


        public TextAsset KysymyksetFile;

        private TMP_Text _questionTxt;
        private UIAnswerManager _answers;

        private void Start()
        {
            _questionTxt = transform.Find("question-txt").GetComponent<TMP_Text>();
            _answers = transform.Find("answer-buttons").GetComponent<UIAnswerManager>();

            var kysymyksetNullable = (Kysymykset?)JsonUtility.FromJson<Kysymykset>(KysymyksetFile.text);
            if (kysymyksetNullable == null)
            {
                Debug.LogError("Error: JSON data could not be loaded.");
                return;
            }
            var kysymykset = (Kysymykset)kysymyksetNullable;
            
            SetQuestion(kysymykset.ominaisuuskysymykset[0]);
        }

        private void SetQuestion(Question question)
        {
            _questionTxt.text = question.titles[Random.Range(0, question.titles.Length)];
            _answers.SetAnswer(this, question);
        }

        internal void SelectAnswer(string category)
        {
            // [aloituskysymys (ominaisuuskysymys)]
            // kysymys: "tykkäätkö tehdä fyysistä työtä?"


            // jos paljon positiivista putkeen, "Helppo peli. Tulevaisuudenkuviosi on selkeät."

            switch (category)
            {
                case "positive":
                    // Aah, (happy smug grin) niin sitä pitää!
                    break;
                case "semi-positive":
                    // fyysinen työ sopii
                    break;
                case "negative":
                    // on näitä muitakin
                    break;
                case "attitude-greed":
                    // (smug) on monia tapoja tienata rahaa, katsotaas lisää (tarkemmin)
                    break;
            }
        }
    }
}