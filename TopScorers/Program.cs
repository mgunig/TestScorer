using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TopScorers.Models;
using TopScorers.Services;

namespace TopScorers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var _fileReaderService = new FileReaderService();
            string fileLocation="";
            string delimeter = ",";
            string hasHeaders = "no";

            if (args.Length > 0)
            {
                fileLocation = args[0];

                // these are optional, program can continue by using defaults
                if(args.Length > 1)
                {
                    delimeter = args[1];       
                }

                // these are optional, program can continue by using defaults
                if (args.Length > 2)
                {
                    hasHeaders = args[2];
                }
                
            }
            else
            {
                Console.WriteLine("Enter the test file location. (CSV File)");
                fileLocation = Console.ReadLine();

                // validate file location
                if (string.IsNullOrEmpty(fileLocation))
                {
                    Console.WriteLine("No file location provided..");
                    return;
                }

                Console.WriteLine("Enter CSV delimeter (Optinal: Default \",\")");
                delimeter = Console.ReadLine();

                Console.WriteLine("Does your file container headers? (Yes/No)");
                hasHeaders = Console.ReadLine();
            }

            // read CSV into TestScore model
            var data = _fileReaderService.ReadCSVToTestScores(fileLocation, delimeter, hasHeaders?.ToLower() =="yes");

            Console.WriteLine("\n");
            DetermineTopScorers(data);

            Console.ReadKey();
        }

        private static void DetermineTopScorers(List<TestScore> data)
        {
            var highestScore = data.OrderByDescending(g => g.Score).FirstOrDefault()?.Score;
            var peopleWithHighScore = data.Where(g => g.Score == highestScore).OrderBy(g => g.FirstName).ToList();

            Console.WriteLine("Top test scorers are:\n");
            peopleWithHighScore.ForEach(topscorer =>
            {
                Console.WriteLine($"{topscorer.FirstName} {topscorer.SecondName}");
            });

            Console.WriteLine($"Score: {highestScore}");
        
        }

    }
}
