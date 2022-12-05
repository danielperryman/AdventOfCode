using System.Net;

namespace Puzzles.Solutions
{
    public sealed class Day03 : IPuzzle
    {
        public int Day => 3;

        public string Puzzle1(string[] input)
        {
            int total = 0;
            foreach (var rucksack in input)
            {
                var pockets = SplitString(rucksack);

                var duplicateItems = new List<char>();

                for (int x = 0; x < pockets[0].Length; x++)
                {
                    if (pockets[1].Contains(pockets[0][x]) && !duplicateItems.Contains(pockets[0][x]))
                    {
                        duplicateItems.Add(pockets[0][x]);
                    }
                }

                foreach (char item in duplicateItems)
                {
                    total += DetermineLetterValue(item);
                }
            }

            return total.ToString();
        }

        public string Puzzle2(string[] input)
        {
            var badges = new List<char>();
            for (int x = 0; x < input.Length; x += 3)
            {
                for (int y = 0; y < input[x].Length; y++)
                {
                    if (input[x + 1].Contains(input[x][y]) && input[x + 2].Contains(input[x][y]))
                    {
                        badges.Add(input[x][y]);
                        break;
                    }
                }
            }

            int total = 0;
            foreach (char badge in badges)
            {
                total += DetermineLetterValue(badge);
            }

            return total.ToString();
        }

        public List<string> SplitString(string input)
        {
            var splitStrings = new List<string>();
            int newStringLengths = input.Length / 2;
            splitStrings.Add(input.Substring(0, newStringLengths));
            splitStrings.Add(input.Substring(newStringLengths));
            return splitStrings;

        }

        public int DetermineLetterValue(char letter)
        {
            if (letter < 97)
            {
                return letter - 38;
            }
            else 
            {
                return letter - 96;
            }
        }
    }
}
