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

        public Question(String t, List<String> ans, int i)
        {
            text = t;
            answers = ans;
            correctIndex = i;
        }


        public String GetText
        {
            get
            {
                return text;
            }
        }

        public List<String> GetAnswers
        {
            get
            {
                return answers;
            }
        }


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
