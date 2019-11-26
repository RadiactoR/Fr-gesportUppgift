using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FrågesportUppgift
{
    /// <summary>
    /// The main page
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Question> questions;
        private int correctAnswers = 0;
        private Question currentQuestion;
        private Random rand = new Random();
        private int noQuestions;
        private bool restart = false;
        private int progress = 0;

        public MainPage()
        {
            this.InitializeComponent();
            questions = new List<Question>();

            //Applicera
            Prepare();
            PrintQuestion(questions[rand.Next(0, questions.Count)]);
        }

        /// <summary>
        /// Sets up all the variables, questions and the interface
        /// </summary>
        private void Prepare()
        {
            restart = false;
            correctAnswers = 0;
            progress = 0;

            a2.Visibility = Visibility.Visible;
            a3.Visibility = Visibility.Visible;

            //Här läggs alla frågor
            questions.Add(new Question("Vad blir 2 + 2?", new List<string> { "4", "24", "1^2" }, 0));
            questions.Add(new Question("Vad heter Norges huvudstad?", new List<string> { "Stockholm", "Storbritannien", "Oslo" }, 2));
            questions.Add(new Question("Hur lång är en engelsk mile?", new List<string> { "10km", "1,6km", "6km" }, 1));
            questions.Add(new Question("Vad är Gabons huvudstad?", new List<string> { "Lusaka", "Libreville", "Lima" }, 1));
            questions.Add(new Question("Vem i serien Vänner blir aldrig gravid?", new List<string> { "Rachel", "Phoebe", "Monica" }, 2));
            questions.Add(new Question("Vilken är den största ön i världen?", new List<string> { "Grönland", "Australien", "Madagaskar" }, 0));
            questions.Add(new Question("Hur många tänder finns det i en vuxens mun?", new List<string> { "20", "32", "38" }, 1));
            questions.Add(new Question("Vem är den nuvarande påven?", new List<string> { "Francis", "Benedictus", "St. Paul John II" }, 0));
            questions.Add(new Question("Var ligger Niagara Falls?", new List<string> { "Ontario", "Vancouver", "Toronto" }, 0));
            questions.Add(new Question("Vad är USAs nationalsport?", new List<string> { "Rugby", "Hockey", "Baseball" }, 2));
            questions.Add(new Question("Vad är USAs nationalrätt?", new List<string> { "Hamburgare", "Pizza", "Friterad kyckling" }, 0));
            questions.Add(new Question("Vem googlas mer än Jesus?", new List<string> { "Justin Bieber", "Donald Trump", "Ed Sheeran" }, 0));

            noQuestions = questions.Count;
        }


        /// <summary>
        /// Gets the a random question from the ones that remain in the list
        /// </summary>
        private void GetNextQuestion()
        {
            questions.Remove(currentQuestion);
            if (questions.Count >= 1)
            {
                PrintQuestion(questions[rand.Next(0, questions.Count)]);
            }
            else
            {
                qBox.Text = "Tack för att du spelade!";
                scoreBox.Text = correctAnswers.ToString() + "/" + noQuestions.ToString() + " poäng";
                restart = true;
                a1.Content = "Spela igen";
                a2.Visibility = Visibility.Collapsed;
                a3.Visibility = Visibility.Collapsed;
            }
        }


        /// <summary>
        /// Checks if the answer is correct or not
        /// </summary>
        /// <param name="i">The question index</param>
        private void Answer(int i)
        {
            if (currentQuestion.isCorrect(i))
            {
                //Ny fråga
                correctAnswers += 1;
                GetNextQuestion();
            }
            else
            {
                //Ny fråga
                GetNextQuestion();
            }
        }

        /// <summary>
        /// Displays the specified question and sets the text of the buttons as the answers
        /// </summary>
        /// <param name="q">The question to be displayed</param>
        private void PrintQuestion(Question q)
        {
            currentQuestion = q;
            progress += 1;
            scoreBox.Text = "Fråga " + progress.ToString() + " - " + correctAnswers.ToString() + "/" + noQuestions.ToString() + " poäng";
            qBox.Text = q.GetText;
            a1.Content = q.GetAnswers[0];
            a2.Content = q.GetAnswers[1];
            a3.Content = q.GetAnswers[2];
        }



        /// <summary>
        /// Answer #1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void a1_Click(object sender, RoutedEventArgs e)
        {
            if (restart == false)
            {
                Answer(0);
            }
            else
            {
                Prepare();
                PrintQuestion(questions[rand.Next(0, questions.Count)]);
            }
        }


        /// <summary>
        /// Answer #2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void a2_Click(object sender, RoutedEventArgs e)
        {
            Answer(1);
        }


        /// <summary>
        /// Answer #3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void a3_Click(object sender, RoutedEventArgs e)
        {
            Answer(2);
        }
    }
}
