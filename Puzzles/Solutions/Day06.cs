using System.Security.Cryptography.X509Certificates;

namespace Puzzles.Solutions
{
    public sealed class Day06 : IPuzzle
    {
        public int Day => 6;

        public string Puzzle1(string[] input)
        {
            return ParseMessage(4, input[0]);
        }

        public string Puzzle2(string[] input)
        {
            return ParseMessage(14, input[0]);
        }

        private string ParseMessage(int numberOfUnique, string item)
        {
            int currentPosition = 0;
            while (currentPosition < item.Length - numberOfUnique)
            {
                string currentSegment = item.Substring(currentPosition, numberOfUnique);
                if (currentSegment.Distinct().Count() == numberOfUnique)
                {
                    return (currentPosition + numberOfUnique).ToString();
                }

                currentPosition++;
            }
            return string.Empty;
        }
    }
}
