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
using Windows.Storage;
using System.Threading.Tasks;
using System.Globalization;

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

        private bool english = false;

        public MainPage()
        {
            this.InitializeComponent();
            questions = new List<Question>();

            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "en-US")
            {
                english = true;
            }

            if(english == true)
            {
                titleBox.Text = "Quiz Deluxe";
            }

            //Applicera
            Prepare();
            /*PrintQuestion(questions[rand.Next(0, questions.Count)]);*/

        }

        /// <summary>
        /// Sets up all the variables, questions and the interface
        /// </summary>
        /// 
        async Task LoadQuestions()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile file;
            string fileContent = "";

            if (english == false)
            {
                file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/questions_sv.txt"));
                fileContent = await Windows.Storage.FileIO.ReadTextAsync(file);
            }

            else if(english == true)
            {
                file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/questions_en.txt"));
                fileContent = await Windows.Storage.FileIO.ReadTextAsync(file);
            }

            //Dela upp i rader
            string[] lines = fileContent.Split("*");
            
            //Dela upp i parametrar
            foreach(string line in lines)
            {
                string[] info = line.Split(",");
                questions.Add(new Question(info[0].Trim(), info[1].Trim(), info[2].Trim(), info[3].Trim(), int.Parse(info[4].Trim())));

            }

        }

        private async void Prepare()
        {
            restart = false;
            correctAnswers = 0;
            progress = 0;

            a2.Visibility = Visibility.Visible;
            a3.Visibility = Visibility.Visible;

            //Här läggs alla frågor
            /*questions.Add(new Question("Vad blir 2 + 2?", new List<string> { "4", "24", "1^2" }, 0));
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
            */
            await Task.Run(() => LoadQuestions());
            noQuestions = questions.Count;
            PrintQuestion(questions[rand.Next(0, questions.Count)]);
        }


        /// <summary>
        /// Gets a random question from the ones that remain in the list
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
                if (english == false)
                {
                    qBox.Text = "Tack för att du spelade!";
                    restart = true;
                    a1.Content = "Spela igen";
                    scoreBox.Text = correctAnswers.ToString() + "/" + noQuestions.ToString() + " poäng";

                }

                else if (english == true)
                {
                    qBox.Text = "Thanks for playing!";
                    restart = true;
                    a1.Content = "Play again";
                    scoreBox.Text = correctAnswers.ToString() + "/" + noQuestions.ToString() + " points";
                }

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

            if (english == false)
            {
                scoreBox.Text = "Fråga " + progress.ToString() + " - " + correctAnswers.ToString() + "/" + noQuestions.ToString() + " poäng";
            }

            else if (english == true)
            {
                scoreBox.Text = "Question " + progress.ToString() + " - " + correctAnswers.ToString() + "/" + noQuestions.ToString() + " points";
            }

            qBox.Text = q.GetText + "?";
            a1.Content = q.GetAnswers[0];
            a2.Content = q.GetAnswers[1];
            a3.Content = q.GetAnswers[2];
        }



        /// <summary>
        /// Sends answer index 0
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
            }
        }


        /// <summary>
        /// Sends answer index 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void a2_Click(object sender, RoutedEventArgs e)
        {
            Answer(1);
        }


        /// <summary>
        /// Sends answer index 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void a3_Click(object sender, RoutedEventArgs e)
        {
            Answer(2);
        }
    }
}
