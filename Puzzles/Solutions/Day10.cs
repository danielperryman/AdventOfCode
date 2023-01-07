namespace Puzzles.Solutions
{
    public sealed class Day10 : IPuzzle
    {
        public int Day => 10;

        public class Device
        {
            private int CurrentCycle { get; set; }

            public int RunningTotal { get; set; }

            private int RegisterValue { get; set; }

            private int TargetCycle { get; set; }

            private int CycleIncrement { get; }

            private List<string> OutputLines { get; set; }

            private string CurrentLine { get; set; }

            public Device(int initialCycle, int cycleIncrement)
            {
                TargetCycle = initialCycle;
                CycleIncrement = cycleIncrement;
                RegisterValue = 1;
                OutputLines = new List<string>();
                CurrentLine = string.Empty;
            }

            public void ExecuteCommand(string command)
            {
                string[] splitCommand = command.Split(' ');
                if (splitCommand[0] == "noop")
                {
                    Noop();
                }
                if (splitCommand[0] == "addx")
                {
                    AddX(Convert.ToInt16(splitCommand[1]));
                }
            }

            public void PrintScreen()
            {
                using (StreamWriter file = new("Screen.txt"))
                {

                    foreach (string line in OutputLines)
                    {
                        file.WriteLine(line);
                    }
                }
            }

            private void ExecuteCycles(int cycleCount)
            {
                while(cycleCount > 0)
                {
                    CurrentCycle++;

                    if (Math.Abs(((CurrentCycle - 1) % 40) - RegisterValue) <= 1)
                    {
                        CurrentLine += '#';
                    }
                    else
                    {
                        CurrentLine += '.';
                    }

                    if (CurrentCycle == TargetCycle)
                    {
                        RunningTotal += (RegisterValue * CurrentCycle);
                        TargetCycle += CycleIncrement;
                        OutputLines.Add(CurrentLine);
                        CurrentLine = string.Empty;
                    }

                    cycleCount--;
                };
            }

            private void Noop()
            {
                ExecuteCycles(1);
            }

            private void AddX(int amount)
            {
                ExecuteCycles(2);
                RegisterValue += amount;
            }

        }

        public string Puzzle1(string[] input)
        {
            var device = new Device(20, 40);
            foreach (var command in input)
            {
                device.ExecuteCommand(command);
            }

            return device.RunningTotal.ToString();
                
        }

        public string Puzzle2(string[] input)
        {
            var device = new Device(40, 40);
            foreach (var command in input)
            {
                device.ExecuteCommand(command);
            }

            device.PrintScreen();

            return "X";
        }
    }
}
