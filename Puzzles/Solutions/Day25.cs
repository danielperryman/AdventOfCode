namespace Puzzles.Solutions
{
    public sealed class Day25 : IPuzzle
    {
        public int Day => 25;

        public string Puzzle1(string[] input)
        {
            double doubleTotal = 0; ;
            foreach(var line in input) 
            {
                doubleTotal += ConvertNonsenseTotDouble(line.Trim().Split(" ").ToList()[0]);
            }
            return ConvertDoubleToNonsense(doubleTotal);
        }

        public string Puzzle2(string[] input)
        {
            double doubleTotal = 0; ;
            foreach (var line in input)
            {
                doubleTotal += ConvertNonsenseTotDouble(line.Trim().Split(" ").ToList()[0]);
            }
            return ConvertDoubleToNonsense(doubleTotal);
        }

        public double ConvertNonsenseTotDouble(string nonsense)
        {
            int maxPosition = nonsense.Length - 1;
            double convertedInteger = 0;
            for (int x = 0; x < nonsense.Length; x++)
            {
                int currentDigit;
                if (nonsense[x] == '-')
                {
                    currentDigit = -1;
                }
                else if (nonsense[x] == '=')
                {

                    currentDigit = -2;
                }
                else
                {
                    currentDigit = int.Parse(nonsense[x].ToString());
                }

                convertedInteger += currentDigit * Math.Pow(5, maxPosition);
                maxPosition--;
            }
            return convertedInteger; 
        }

        public string ConvertDoubleToNonsense(double number)
        {
            double calculationNumber = 0;
            double currentPosition = 0;
            var returnValue = string.Empty;
            while (Math.Pow(5, currentPosition) * 2 < number)
            {
                currentPosition++;
            }

            while (currentPosition >= 0)
            {
                double remainingValues = 0;

                double currentDigit = Math.Pow(5, currentPosition);

                for (double x = currentPosition - 1; x >= 0; x--)
                {
                    remainingValues += Math.Pow(5, x) * 2;
                }

                if (number < calculationNumber && ((Math.Abs(number - (calculationNumber - currentDigit)) <= remainingValues) || ((Math.Abs(number - (calculationNumber - currentDigit * 2)) <= remainingValues))))
                {
                    if (calculationNumber + currentDigit * -1 <= number || Math.Abs(number - (calculationNumber - currentDigit *2)) >= remainingValues)
                    {
                        calculationNumber = calculationNumber - currentDigit;
                        returnValue += "-";
                    }
                    else
                    {
                        calculationNumber = calculationNumber - currentDigit * 2;
                        returnValue += "=";
                    }
                }
                else if (number > calculationNumber && calculationNumber + currentDigit - number < remainingValues)
                {

                    if (calculationNumber + currentDigit >= number || calculationNumber + currentDigit * 2 - number > remainingValues)
                    {
                        calculationNumber = calculationNumber + currentDigit;
                        returnValue += "1";
                    }
                    else
                    {
                        calculationNumber = calculationNumber + currentDigit * 2;
                        returnValue += "2";
                    }
                }
                else
                {
                    returnValue += "0";
                }
                currentPosition --; 
            }


           return returnValue;
        }
    }
}
