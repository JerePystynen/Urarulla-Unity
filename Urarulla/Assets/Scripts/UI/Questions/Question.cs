using System;
using UnityEngine;

namespace DiMe.Urarulla
{
    /// <summary>
    /// Contains all the data related to different kinds of questions.
    /// </summary>
    [Serializable]
    public class Questions
    {
        [Serializable]
        public struct CharacteristicsQuestions
        {
            public Question[] characteristicsQuestions;
        }

        [Serializable]
        public struct IndustryQuestions
        {
            public Question[] industryQuestions;
        }

        [Serializable]
        public struct TrickyQuestions
        {
            public Question[] trickyQuestions;
        }

        public Question[] characteristicsQuestions;
        public Question[] industryQuestions;
        public Question[] trickyQuestions;

        public Questions(Question[] characteristicsQuestions, Question[] industryQuestions, Question[] trickyQuestions)
        {
            this.characteristicsQuestions = characteristicsQuestions;
            this.industryQuestions = industryQuestions;
            this.trickyQuestions = trickyQuestions;
        }

        internal Question GetRandomCharacteristicsQuestion()
        {
            if (CheckNull()) return default;
            return characteristicsQuestions.Random();
        }

        internal Question GetRandomIndustryQuestion()
        {
            if (CheckNull()) return default;
            return industryQuestions.Random();
        }

        internal Question GetRandomTrickyQuestion()
        {
            if (CheckNull()) return default;
            return trickyQuestions.Random();
        }

        private Question[][] _questionArrays;
        private bool _initialized;

        internal Question GetRandomQuestion()
        {
            if (CheckNull()) return default;
            if (!_initialized)
            {
                _initialized = true;
                _questionArrays = new Question[][] {
                    characteristicsQuestions,
                    industryQuestions,
                    trickyQuestions,
                };
            }
            return _questionArrays.Random().Random();
        }

        private bool CheckNull()
        {
            if (characteristicsQuestions == null)
            {
                Debug.LogError("Error: characteristicsQuestions is null!");
                return true;
            }
            if (industryQuestions == null)
            {
                Debug.LogError("Error: industryQuestions is null!");
                return true;
            }
            if (trickyQuestions == null)
            {
                Debug.LogError("Error: trickyQuestions is null!");
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Contains the question (title) and its responses (in answers).
    /// </summary>
    [Serializable]
    public struct Question
    {
        public string[] positive_categories;
        public string[] negative_categories;
        public string[] titles;
        public Answer[] answers;
    }

    /// <summary>
    /// The singular possible answer to a Question. Has text and type of response to give.
    /// </summary>
    [Serializable]
    public struct Answer
    {
        public string text;
        public string type;
        public string[] responses;
    }
}