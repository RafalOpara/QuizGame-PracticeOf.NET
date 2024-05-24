using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public  class Quiz
    {
        public List<Question> Questions { get; set; }

        public Player Player { get; set; }

        public Quiz() 
        
        {
            LoadQuestionFromFile("questions.txt");
        }
        private void LoadQuestionFromFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var counter = 0;
             Questions = new List<Question>();

            var currentQuestion = new Question();

            foreach (var line in lines)
            {
                if (counter == 6)
                {
                    counter = 0;
                }

                if (counter == 0)
                {
                    currentQuestion.Title = line;
                }
                if (counter == 1)
                {
                    currentQuestion.AnswerA = line;
                }
                if (counter == 2)
                {
                    currentQuestion.AnswerB = line;
                }
                if (counter == 3)
                {
                    currentQuestion.AnswerC = line;
                }
                if (counter == 4)
                {
                    currentQuestion.AnswerD = line;
                }
                if (counter == 5)
                {
                    currentQuestion.RightAnswerLetter = line[0].ToString();
                    currentQuestion.Score = int.Parse(line[1].ToString());

                    var newQuestion = new Question
                    {
                        Title = currentQuestion.Title,
                        AnswerA = currentQuestion.AnswerA,
                        AnswerB = currentQuestion.AnswerB,
                        AnswerC = currentQuestion.AnswerC,
                        AnswerD = currentQuestion.AnswerD,
                        RightAnswerLetter = currentQuestion.RightAnswerLetter,
                        Score = currentQuestion.Score,
                    };

                    Questions.Add(newQuestion);

                }

               

                counter++;
            }
        }


        public void Start()
        {
            Player = new Player();

            Console.WriteLine("tell me your name");

            Player.Name = Console.ReadLine();

            Player.Score = 0;

            Player.CurrentQuestion = 1;

            for (int i = 0; i < Questions.Count; i++)
            {
                var score = ShowQuestion(Player.CurrentQuestion);
                Player.Score += score;
                Player.CurrentQuestion++;
            }

            Console.WriteLine("quiz is finished");
            Console.WriteLine("your score" + Player.Score);
        }

        public int ShowQuestion(int questionCounter)
        {
            var currentQuestionToShow = Questions[questionCounter-1];

            Console.WriteLine(currentQuestionToShow.Title);
            Console.WriteLine(currentQuestionToShow.AnswerA);
            Console.WriteLine(currentQuestionToShow.AnswerB);
            Console.WriteLine(currentQuestionToShow.AnswerC);
            Console.WriteLine(currentQuestionToShow.AnswerD);

            var userResponde = Console.ReadLine();

            if(userResponde == currentQuestionToShow.RightAnswerLetter)
            {
                return currentQuestionToShow.Score;
            }
            else
            {
                Console.WriteLine("wrong answer");
                return 0;
            }
        }
    }
}
