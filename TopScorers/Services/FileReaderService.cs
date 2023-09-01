using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TopScorers.Models;

namespace TopScorers.Services
{
    public class FileReaderService
    {
        public List<TestScore> ReadCSVToTestScores(string fileLocation,string delimeter, bool hasHeaders = true)
        {
            if (string.IsNullOrEmpty(delimeter)) delimeter = ",";
            var lines = File.ReadAllLines(fileLocation);

            // no data found from the file, return null
            if (lines is null || lines.Length == 0) return null;

            var testScores = new List<TestScore>();

            // skip first line, this is the file header
            lines.ToList().Skip((hasHeaders ? 1 : 0)).ToList().ForEach(line =>
            {
                var testScore = line.Split(delimeter);

                testScores.Add(new TestScore()
                {
                    FirstName = testScore[0],
                    SecondName = testScore[1],
                    Score = Int32.Parse(testScore[2])
                });
            });

            return testScores;
        }
    }
}
