namespace Puzzles.Solutions
{
    public sealed class Day07 : IPuzzle
    {
        public int Day => 7;

        public string Puzzle1(string[] input)
        {
            var directory = BuildDirectoryTree(input);

            int totalSize = 0;

            CalculateSizes(directory, ref totalSize);

            return totalSize.ToString();
        }

        public string Puzzle2(string[] input)
        {
            var directory = BuildDirectoryTree(input);

            int totalSize = 0;

            CalculateSizes(directory, ref totalSize);

            int neededSpace = 30000000 - (70000000 - directory.Size);

            int bestSize = 70000000;

            DetermineBestSize(directory, neededSpace, ref bestSize);

            return bestSize.ToString();
        }

        public class Directory
        {
            public Directory? Parent { get; set; } = null;

            public string Name { get; set; } = string.Empty;

            public int Size { get; set; } = 0;

            public List<int> FileSizes { get; set; } = new List<int>();

            public List<Directory> Directories { get; set; } = new List<Directory>();
        }

        public Directory BuildDirectoryTree(string[] input)
        {
            var root = new Directory { Name = "/" };

            Directory currentDirectory = root;

            for (int i = 1; i < input.Count(); i++)
            {
                var splitCommand = input[i].Split(' ').ToList();
                if (splitCommand[0] == "$")
                {
                    if (splitCommand[1] == "cd")
                    {
                        if (splitCommand[2] == "..")
                        {
                            currentDirectory = currentDirectory.Parent!;
                        }
                        else
                        {
                            currentDirectory = currentDirectory.Directories.Where(d => d.Name == splitCommand[2]).First();
                        }
                    }
                }
                else if (splitCommand[0] == "dir")
                {
                    currentDirectory.Directories.Add(new Directory { Name = splitCommand[1], Parent = currentDirectory });
                }
                else
                {
                    currentDirectory.FileSizes.Add(int.Parse(splitCommand[0]));
                }
            }

            return root;
        }


        public void CalculateSizes(Directory node, ref int runningTotal)
        {
            foreach (var directory in node.Directories)
            {
                CalculateSizes(directory, ref runningTotal);

                node.Size += directory.Size;
            }

            node.Size += node.FileSizes.Sum();

            if (node.Size <= 100000)
            {
                runningTotal += node.Size;
            }
        }

        public void DetermineBestSize(Directory node, int targetSize, ref int bestSize)
        {
            foreach (var directory in node.Directories)
            {
                DetermineBestSize(directory, targetSize, ref bestSize);
            }

            if (node.Size > targetSize && node.Size < bestSize)
            {
                bestSize = node.Size;
            }
        }
    }

}
