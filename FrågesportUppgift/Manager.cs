using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrågesportUppgift
{
    class Manager
    {
        private List<Question> questions;
        private Question currentQuestion;

        public Manager(List<Question> q)
        {
            questions = q;
        }



        public Question CurrentQuestion
        {
            get
            {
                return currentQuestion;
            }
        }
    }
}
