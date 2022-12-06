using System.Security.Cryptography.X509Certificates;

namespace Puzzles.Solutions
{
    public sealed class Day06 : IPuzzle
    {
        public int Day => 6;

        public string Puzzle1(string[] input)
        {
            foreach (var item in input)
            {
                int currentPosition = 0;
                while (currentPosition < item.Length)
                {
                    string currentSegment = item.Substring(currentPosition, 4);
                    if (currentSegment.Distinct().Count() == 4)
                    {
                        return (currentPosition + 4).ToString();
                    }

                    currentPosition++;
                }
            }

            return String.Empty;
        }

        public string Puzzle2(string[] input)
        {
            foreach (var item in input)
            {
                int currentPosition = 0;
                while (currentPosition < item.Length - 14)
                {
                    string currentSegment = item.Substring(currentPosition, 14);
                    if (currentSegment.Distinct().Count() == 14)
                    {
                        return (currentPosition + 14).ToString();
                    }

                    currentPosition++;
                }
            }

            return String.Empty;
        }
    }
}
