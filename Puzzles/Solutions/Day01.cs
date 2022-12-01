namespace Puzzles.Solutions
{
    public sealed class Day01 : IPuzzle
    {
        public int Day => 1;

        public string Puzzle1(string[] input)
        {
            int maxCalories = 0;
            int currentCalories = 0;
            
            foreach (string line in input)
            {
                int conversion = 0;
                if (Int32.TryParse(line, out conversion))
                {
                    currentCalories += conversion;
                }
                else
                {
                    if (currentCalories > maxCalories)
                    {
                        maxCalories = currentCalories;
                    }
                    currentCalories = 0;
                }
            }

            return maxCalories.ToString();
        }

        public string Puzzle2(string[] input)
        {
            int currentCalories = 0;
            List<int> allTotals = new List<int>();

            foreach (string line in input)
            {
                int conversion = 0;
                if (Int32.TryParse(line, out conversion))
                {
                    currentCalories += conversion;
                }
                else
                {
                    allTotals.Add(currentCalories);
                    currentCalories = 0;
                }
            }

            allTotals = allTotals.OrderByDescending(x => x).ToList();

            return (allTotals[0] + allTotals[1] + allTotals[2]).ToString();
        }
    }
}
