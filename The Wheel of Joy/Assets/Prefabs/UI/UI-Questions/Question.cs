using System;

namespace Urarulla
{
    [Serializable]
    public struct Kysymykset
    {
        public Question[] ominaisuuskysymykset;
    }

    [Serializable]
    public struct Question
    {
        public string[] titles;
        public Answer[] answers;
    }

    [Serializable]
    public struct Answer
    {
        public string text;
        public string category;
    }
}