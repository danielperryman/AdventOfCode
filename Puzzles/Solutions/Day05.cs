using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Puzzles.Solutions
{
    public sealed class Day05 : IPuzzle
    {
        public int Day => 5;

        public string Puzzle1(string[] input)
        {
            bool toInstructions = false;
            var stacksStrings = new List<string>();
            List<Stack<char>> stacks = new List<Stack<char>>();
            string returnString = string.Empty;

            foreach (var line in input)
            {
                if (!toInstructions)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        stacksStrings.Add(line);
                    }
                    else
                    {
                        stacks = ReadStacks(stacksStrings);
                        toInstructions = true;
                    }
                }
                else 
                {
                    ExecuteMove(stacks, line);
                }
            }

            foreach (var stack in stacks)
            {
                if (stack.Count > 0)
                {
                    returnString += stack.Pop();
                }
            }    

            return returnString;
        }

        public string Puzzle2(string[] input)
        {
            bool toInstructions = false;
            var stacksStrings = new List<string>();
            List<Stack<char>> stacks = new List<Stack<char>>();
            string returnString = string.Empty;

            foreach (var line in input)
            {
                if (!toInstructions)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        stacksStrings.Add(line);
                    }
                    else
                    {
                        stacks = ReadStacks(stacksStrings);
                        toInstructions = true;
                    }
                }
                else
                {
                    ExecuteNewMove(stacks, line);
                }
            }

            foreach (var stack in stacks)
            {
                if (stack.Count > 0)
                {
                    returnString += stack.Pop();
                }
            }

            return returnString;
        }

        List<Stack<char>> ReadStacks(List<string> input)
        {
            var stacks = new List<Stack<char>>();
            int stacksCount = (input[0].Length + 1) / 4;
            for (int x = 0; x < stacksCount; x++)
            {
                stacks.Add(new Stack<char>());
            }

            for (int x = input.Count - 2; x >= 0 ; x--)
            {
                for (int y = 0; y < stacksCount; y++)
                {
                    int stringLocation = (y * 4) + 1;
                    if (input[x][stringLocation] != ' ')
                    {
                        stacks[y].Push(input[x][stringLocation]);
                    }
                }
            }

            return stacks;
        }

        void ExecuteMove(List<Stack<char>> stacks, string move)
        {
            List<string> moveParts = move.Split(' ').ToList();
            int moveCount = Convert.ToInt16(moveParts[1]);
            int sourceStack = Convert.ToInt16(moveParts[3]) - 1;
            int targetStack = Convert.ToInt16(moveParts[5]) -1 ;

            while (moveCount > 0)
            {
                stacks[targetStack].Push(stacks[sourceStack].Pop());
                moveCount--;
            }
        }

        void ExecuteNewMove(List<Stack<char>> stacks, string move)
        {
            List<string> moveParts = move.Split(' ').ToList();
            int moveCount = Convert.ToInt16(moveParts[1]);
            int sourceStack = Convert.ToInt16(moveParts[3]) - 1;
            int targetStack = Convert.ToInt16(moveParts[5]) - 1;
            Stack<char> intermediateStack = new Stack<char>();
            while (moveCount > 0)
            {
                intermediateStack.Push(stacks[sourceStack].Pop());
                moveCount--;
            }

            while (intermediateStack.Count > 0)
            {
                stacks[targetStack].Push(intermediateStack.Pop());
            }
        }
    }
}
