namespace Puzzles.Solutions
{
    public sealed class Day08 : IPuzzle
    {
        public int Day => 8;

        public string Puzzle1(string[] input)
        {
            var grove = ReadFile(input);

            int count = 0;

            for (int x = 0; x < input.Count(); x++)
            {
                for (int y = 0; y < input[x].Count(); y++)
                {
                    if (x == 0 || y == 0 || x == input.Count() - 1 || y == input[x].Count() - 1 || CheckTree(grove, x, y))
                    {
                        count++;
                    }
                }
            }

            return count.ToString();
        }

        public string Puzzle2(string[] input)
        {
            var grove = ReadFile(input);

            int currentHighest = 0;

            for (int x = 1; x < input.Count() -1; x++)
            {
                for (int y = 1; y < input[x].Count() - 1; y++)
                {
                    int treeValue = CheckTreesVisible(grove, x, y);

                    if (treeValue > currentHighest)
                    {
                        currentHighest = treeValue;
                    }

                }
            }

            return currentHighest.ToString();
        }

        public List<List<int>> ReadFile(string[] input)
        {
            var array = new List<List<int>>();

            foreach (var item in input)
            {
                var currentRow = new List<int>();
                for (int x = 0; x < item.Length; x++)
                {
                    currentRow.Add(item.ElementAt(x) - '0');
                }
                array.Add(currentRow);
            }

            return array;
        }

        public bool CheckTree(List<List<int>> grove, int x, int y)
        {
            if (CheckHorizontal(grove[x], y - 1, grove[x][y], -1))
            {
                return true;
            }
            else if (CheckHorizontal(grove[x], y + 1, grove[x][y], +1))
            {
                return true;
            }
            else if (CheckVertical(grove, x - 1, y, grove[x][y], -1))
            {
                return true;
            }
            else if (CheckVertical(grove, x + 1, y, grove[x][y], +1))
            {
                return true;
            }

            return false;
        }

        public bool CheckHorizontal(List<int> row, int position, int value, int modifier)
        {
            if (row[position] >= value)
            {
                return false;
            }
            else if (position == 0 || position == row.Count() - 1)
            {
                return true;
            }
            else
            {
                return CheckHorizontal(row, position + modifier, value, modifier);
            }
        }

        public bool CheckVertical(List<List<int>> grove, int xPosition, int yPosition, int value, int modifier)
        {
            if (grove[xPosition][yPosition] >= value)
            {
                return false;
            }
            else if (xPosition == 0 || xPosition == grove.Count - 1)
            {
                return true;
            }
            else
            {
                return CheckVertical(grove, xPosition + modifier, yPosition, value, modifier);
            }
        }

        public int CheckTreesVisible(List<List<int>> grove, int x, int y)
        {
            var total = CheckHorizontalTreesVisible(grove[x], y - 1, grove[x][y], -1);

            total *= CheckHorizontalTreesVisible(grove[x], y + 1, grove[x][y], +1);

            total *= CheckVerticalTreesVisible(grove, x - 1, y, grove[x][y], -1);

            total *= CheckVerticalTreesVisible(grove, x + 1, y, grove[x][y], +1);

            return total;
        }

        public int CheckHorizontalTreesVisible(List<int> row, int position, int value, int modifier)
        {
            if (row[position] >= value || position == 0 || position == row.Count() - 1)
            {
                return 1;
            }
            else
            {
                return 1 + CheckHorizontalTreesVisible(row, position + modifier, value, modifier);
            }
        }

        public int CheckVerticalTreesVisible(List<List<int>> grove, int xPosition, int yPosition, int value, int modifier)
        {
            if (grove[xPosition][yPosition] >= value || xPosition == 0 || xPosition == grove.Count - 1)
            {
                return 1;
            }
            else
            {
                return 1 + CheckVerticalTreesVisible(grove, xPosition + modifier, yPosition, value, modifier);
            }
        }
    }
}
