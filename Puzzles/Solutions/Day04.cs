using System.Diagnostics;

namespace Puzzles.Solutions
{
    public sealed class Day04 : IPuzzle
    {
        public int Day => 4;

        public string Puzzle1(string[] input)
        {
            int totalCount = 0;

            foreach (var group in input)
            {
                var assignments = ExpandedLists(group);

                var longerList = assignments[0].Count >= assignments[1].Count ? assignments[0] : assignments[1];
                var shorterList = assignments[0].Count < assignments[1].Count ? assignments[0] : assignments[1];

                int collisionCount = 0;

                for (int x = 0; x < longerList.Count; x++)
                {
                    if (shorterList.Contains(longerList[x]))
                    {
                        collisionCount++;
                    }
                }

                if (collisionCount == shorterList.Count())
                {
                    totalCount++;
                }
            }

            return totalCount.ToString();
        }

        public string Puzzle2(string[] input)
        {
            int totalCount = 0;

            foreach (var group in input)
            {
                var assignments = ExpandedLists(group);

                var longerList = assignments[0].Count >= assignments[1].Count ? assignments[0] : assignments[1];
                var shorterList = assignments[0].Count < assignments[1].Count ? assignments[0] : assignments[1];

                int collisionCount = 0;

                for (int x = 0; x < longerList.Count; x++)
                {
                    if (shorterList.Contains(longerList[x]))
                    {
                        collisionCount++;
                        break;
                    }
                }

                if (collisionCount > 0)
                {
                    totalCount++;
                }
            }

            return totalCount.ToString();
        }

        public List<List<int>> ExpandedLists(string group)
        {
            var assignments = group.Split(',');
            var returnLists = new List<List<int>>();

            foreach (var assignment in assignments)
            {

                var endpoints = assignment.Split('-');
                int start = Convert.ToInt16(endpoints[0]);
                int end = Convert.ToInt16(endpoints[1]);
                var returnList = new List<int>();

                for (int x = start; x <= end; x++)
                {
                    returnList.Add(x);
                }

                returnLists.Add(returnList);
            }

            return returnLists;
        }
    }
}


