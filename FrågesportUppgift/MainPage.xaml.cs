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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FrågesportUppgift
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Manager manager;
        private List<Question> questions;
        private int correctAnswers = 0;
        private Question currentQuestion;
        private Random rand = new Random();
        private int noQuestions = 2;

        public MainPage()
        {
            this.InitializeComponent();
            questions = new List<Question>();

            questions.Add(new Question("Vad heter Sveriges huvudstad?", new List<string> { "Oslo", "Stockholm", "Köpenhamn"}, 1));
            questions.Add(new Question("Vad heter Norges huvudstad?", new List<string> { "Stockholm", "Storbritannien", "Oslo" }, 2));

            //Applicera
            PrintQuestion(questions[rand.Next(0, questions.Count)]);
        }



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
                scoreBox.Text = "";
            }
        }


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

        private void PrintQuestion(Question q)
        {
            currentQuestion = q;
            scoreBox.Text = correctAnswers.ToString() + "/" + noQuestions.ToString() + " poäng";
            qBox.Text = q.GetText;
            a1.Content = q.GetAnswers[0];
            a2.Content = q.GetAnswers[1];
            a3.Content = q.GetAnswers[2];
        }




        private void a1_Click(object sender, RoutedEventArgs e)
        {
            Answer(0);
        }

        private void a2_Click(object sender, RoutedEventArgs e)
        {
            Answer(1);
        }

        private void a3_Click(object sender, RoutedEventArgs e)
        {
            Answer(2);
        }
    }
}
