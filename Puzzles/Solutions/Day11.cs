using System.Security.Cryptography.X509Certificates;

namespace Puzzles.Solutions
{
    public sealed class Day11 : IPuzzle
    {
        public int Day => 11;
        public class MonkeyGame
        {
            public static string StringAdd(List<string> numbers, bool isReversed)
            {
                if (!isReversed)
                {
                    for (int i = 0; i < numbers.Count; i++)
                    {
                        numbers[i] = new string(numbers[i].Reverse().ToArray());
                    }
                }

                var maxLength = numbers.Select(n => n.Length).Max();

                int carryOver = 0;
                string returnString = string.Empty;

                for (int i = 0; i < maxLength; i++)
                {
                    int currentDigit = 0;

                    foreach (var number in numbers)
                    {
                        if (i < number.Length)
                        {
                            currentDigit += int.Parse(number[i].ToString());
                        }
                    }

                    currentDigit += carryOver;

                    returnString += (currentDigit % 10).ToString();
                    carryOver = currentDigit / 10;
                }

                if (carryOver > 0)
                {
                    returnString += carryOver;
                }

                return new string(returnString.Reverse().ToArray());
            }

            public static string StringMultiply(List<string> numbers)
            {
                for (int i = 0; i < numbers.Count; i++)
                {
                    numbers[i] = new string(numbers[i].Reverse().ToArray());
                }


                List<string> lines = new List<string>();

                for (int i = 0; i < numbers[0].Length; i++)
                {

                    int currentMultiplier = int.Parse(numbers[0][i].ToString());

                    string newLine = string.Empty;

                    for(int j = 0; j < i; j++)
                    {
                        newLine += "0";
                    }

                    int currentDigit;
                    int carryOver = 0;

                    for (int j = 0; j < numbers[1].Length; j++)
                    {
                        currentDigit = int.Parse(numbers[1][j].ToString()) * currentMultiplier;

                        currentDigit += carryOver;

                        newLine += (currentDigit % 10).ToString();
                        carryOver = currentDigit / 10;
                    }

                    if (carryOver > 0)
                    {
                        newLine += carryOver;
                    }

                    lines.Add(newLine);
                }

                return StringAdd(lines, true);
            }

            public static string StringDivide(string dividend, int divisor, out bool hasRemainder)
            {
                int currentDividend = int.Parse(dividend[0].ToString());

                string returnValue = string.Empty;

                for (int i = 1; i < dividend.Length; i++)
                {
                    if (currentDividend >= divisor)
                    {
                        returnValue += (currentDividend / divisor).ToString();
                        currentDividend = (currentDividend % divisor);
                    }
                    else
                    {
                        if (returnValue.Length > 0)
                        {
                            returnValue += 0;
                        }
                    }
                    currentDividend *= 10;
                    currentDividend += int.Parse(dividend[i].ToString());
                }

                if (currentDividend >= divisor)
                {
                    returnValue += (currentDividend / divisor).ToString();
                    currentDividend = (currentDividend % divisor);
                }
                else
                {
                    if (returnValue.Length > 0)
                    {
                        returnValue += 0;
                    }
                }

                if (currentDividend == 0)
                {
                    hasRemainder = false;
                }
                else
                {
                    hasRemainder = true;
                }

                return returnValue;
            }

            public class Monkey
            {
                public List<string> Items { get; set; }

                public string OperationA { get; set; }

                public string OperationB { get; set; }

                public string OperationOperator { get; set; }

                public int TestDivisibleBy { get; set; }

                public int TrueMonkey { get; set; }

                public int FalseMonkey { get; set; }

                public string ItemsProcessed { get; set; }

                public int WorryDivider { get; set; }

                public Monkey(int worryDivider)
                {
                    Items = new List<string>();
                    WorryDivider = worryDivider;
                    OperationA = string.Empty;
                    OperationB = string.Empty;
                    OperationOperator = string.Empty;
                    ItemsProcessed = "0";
                }

                public int ProcessItem()
                {
                    ItemsProcessed = StringAdd(new List<string> { ItemsProcessed, "1" }, false);
                    string operationNumberA = OperationA == "old" ? Items[0] : OperationA;
                    string operationNumberB = OperationB == "old" ? Items[0] : OperationB;

                    if (OperationOperator == "*")
                    {
                        Items[0] = StringMultiply(new List<string> { operationNumberA, operationNumberB });
                    }
                    else if (OperationOperator == "+")
                    {
                        Items[0] = StringAdd(new List<string> { operationNumberA, operationNumberB }, false);
                    }

                    Items[0] = StringDivide(Items[0], WorryDivider, out bool junk);

                    StringDivide(Items[0], TestDivisibleBy, out bool isRemainder);

                    if (!isRemainder)
                    {
                        return TrueMonkey;
                    }

                    return FalseMonkey;
                }
            }

            public List<Monkey> Monkeys { get; set; }
            private int Round { get; set; }
            private int RoundCount { get; set; }

            public MonkeyGame(string[] input, int rounds, int worryDivider)
            {
                RoundCount = rounds;
                Monkeys = new List<Monkey>();
                for (int i = 0; i < input.Length; i += 7)
                {
                    Monkey monkey = new Monkey(worryDivider);
                    var items = input[i + 1].Replace("  Starting items: ", "").Split(' ');
                    for (int x = 0; x < items.Length; x++)
                    {
                        monkey.Items.Add(items[x].Replace(",", ""));
                    }
                    var operationParts = input[i + 2].Replace("  Operation: new = ", "").Split(' ');
                    monkey.OperationA = operationParts[0];
                    monkey.OperationB = operationParts[2];
                    monkey.OperationOperator = operationParts[1];
                    monkey.TestDivisibleBy = Convert.ToInt16(input[i + 3].Replace("  Test: divisible by ", ""));
                    monkey.TrueMonkey = Convert.ToInt16(input[i + 4].Replace("    If true: throw to monkey ", ""));
                    monkey.FalseMonkey = Convert.ToInt16(input[i + 5].Replace("    If false: throw to monkey ", ""));

                    Monkeys.Add(monkey);
                }
            }

            public string PlayGame()
            {
                while (Round < RoundCount)
                {
                    foreach (var monkey in Monkeys)
                    {
                        while (monkey.Items.Count > 0)
                        {
                            int target = monkey.ProcessItem();

                            Monkeys[target].Items.Add(monkey.Items[0]);

                            monkey.Items.RemoveAt(0);
                        }
                    }
                    Round++;
                }

                Monkeys = Monkeys.OrderByDescending(m => int.Parse(m.ItemsProcessed)).ToList();

                return StringMultiply(new List<string> { Monkeys[0].ItemsProcessed, Monkeys[1].ItemsProcessed });
            }
        }

        public string Puzzle1(string[] input)
        {
            var game = new MonkeyGame(input, 20, 3);

            return game.PlayGame();
        }

        public string Puzzle2(string[] input)
        {
            //var game = new MonkeyGame(input, 10000, 1);

            //return game.PlayGame();

            return "GAHHHH";
        }
    }
}
