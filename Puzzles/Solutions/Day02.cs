namespace Puzzles.Solutions
{
    public sealed class Day02 : IPuzzle
    {
        public int Day => 2;

        private Dictionary<char, int> SymbolScores = new Dictionary<char, int>{{'A', 1}, {'B', 2}, {'C', 3}};
        private Dictionary<char, char> SymbolTranslator = new Dictionary<char, char> {{'X', 'A'}, {'Y', 'B'}, {'Z', 'C'}};

        private int GameScore(char theirs, char mine)
        {
            int score = 0;
            if ((mine == 'A' && theirs == 'C') || (mine == 'B' && theirs == 'A') || (mine == 'C' && theirs == 'B'))
            {
                score = 6;
            }
            if ((theirs == mine))
            {
                score = 3;
            }
            return score += SymbolScores[mine];
        }

        private char DetermineMove(char theirs, char instruction)
        {
            if (instruction == 'X')
            {
                if (theirs == 'A')
                    return 'C';
                else if (theirs == 'B')
                    return 'A';
                else 
                    return 'B';
            }
            else if (instruction == 'Y')
            {
                return theirs;
            }
            else
            {
                if (theirs == 'A')
                    return 'B';
                else if (theirs == 'B')
                    return 'C';
                else
                    return 'A';
            }
        }

        public string Puzzle1(string[] input)
        {
            int total = 0;
            foreach(var line in input )
            {
                char mine = SymbolTranslator[line[2]];
                total += GameScore(line[0], mine);
            }

            return total.ToString();
        }

        public string Puzzle2(string[] input)
        {
            int total = 0;
            foreach (var line in input)
            {
                char mine = DetermineMove(line[0], line[2]);
                total += GameScore(line[0], mine);
            }

            return total.ToString();
        }

    }
}
