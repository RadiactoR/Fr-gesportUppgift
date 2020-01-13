using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrågesportUppgift
{

    class Question
    {
        private List<String> answers;
        private String text;
        private int correctIndex;

        public Question(String t, string ans1, string ans2, string ans3, int i)
        {
            answers = new List<string>();
            text = t;
            answers.Add(ans1);
            answers.Add(ans2);
            answers.Add(ans3);
            correctIndex = i;
        }


        /// <summary>
        /// Returns the text of this question as a string
        /// </summary>
        public String GetText
        {
            get
            {
                return text;
            }
        }

        /// <summary>
        /// Returns the associated answers as a list of strings
        /// </summary>
        public List<String> GetAnswers
        {
            get
            {
                return answers;
            }
        }


        /// <summary>
        /// Returns true if the index matches the "correct" index, returns false otherwise
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool isCorrect(int index)
        { 
            //Returnera true om indexarna stämmer
            if(index == correctIndex)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
