using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;

namespace Puzzles.Solutions
{
    public sealed class Day14 : IPuzzle
    {
        public int Day => 14;

        private static int LargeBoardExtension = 10000;

        private class Board
        {
            public List<List<bool>> Grid { get; set; } = new List<List<bool>>();
            public int XOffset { get; set; }
        }

        private class Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private class CaveParts
        {
            public List<List<Coordinate>> Pieces { get; set; } = new List<List<Coordinate>>();
        }

        public string Puzzle1(string[] input)
        {
            return FillSand(BuildBoard(input)).ToString();
        }

        public string Puzzle2(string[] input)
        {
            return FillSand2(BuildBoard(input,true)).ToString();
        }

        private Board BuildBoard(string[] input, bool largeBoard = false)
        {
            int maxX = 0;
            int maxY = 0;
            Board board = new Board();

            int extension = largeBoard ? LargeBoardExtension : 0;

            board.XOffset = 100000;

            CaveParts caveParts = new CaveParts();

            //initial loop to determine boundries
            foreach (var line in input)
            {
                var commands = line.Split(' ').ToList();
                commands = commands.Where(c => c != "->").ToList();

                List<Coordinate> coordinates = new List<Coordinate>();

                foreach (var command in commands)
                {
                    var slitCoordinates = command.Split(',');
                    Coordinate newCoordinate = new Coordinate { X = int.Parse(slitCoordinates[0]), Y = int.Parse(slitCoordinates[1]) };
                    if (newCoordinate.X > maxX)
                    {
                        maxX = newCoordinate.X;
                    }
                    if (newCoordinate.X < board.XOffset)
                    {
                        board.XOffset = newCoordinate.X;
                    }
                    if (newCoordinate.Y > maxY)
                    {
                        maxY = newCoordinate.Y;
                    }

                    coordinates.Add(newCoordinate);
                }

                caveParts.Pieces.Add(coordinates);
            }
            int xCount = 0;

             xCount = board.XOffset + extension * 2;
            
            for (int x = 0; x <= xCount; x++)
            {
                List<bool> newRow = new List<bool>();
                for (int y = 0; y <= maxY; y++)
                {
                    newRow.Add(false);
                }
                if (largeBoard)
                {
                    newRow.Add(false);
                    newRow.Add(true);
                }

                board.Grid.Add(newRow);
            }

            foreach (var piece in caveParts.Pieces)
            {
                board.Grid[(piece[0].X - board.XOffset) + extension][piece[0].Y] = true;

                for (int i = 1; i < piece.Count; i++)
                {
                    if (piece[i].Y == piece[i - 1].Y)
                    {
                        int newX = (piece[i].X - board.XOffset) + extension;
                        int lastX = (piece[i - 1].X - board.XOffset) + extension;
                        int lowerX = newX < lastX ? newX : lastX;
                        int higherX = newX > lastX ? newX : lastX;

                        for (int x = lowerX; x <= higherX; x++)
                        {
                            board.Grid[x][piece[i].Y] = true;
                        }
                    }
                    else
                    {
                        int newY = piece[i].Y;
                        int lastY = piece[i - 1].Y;
                        int lowerY = newY < lastY ? newY : lastY;
                        int higherY = newY > lastY ? newY : lastY;

                        for (int y = lowerY; y <= higherY; y++)
                        {
                            board.Grid[(piece[i].X - board.XOffset) + extension][y] = true;
                        }
                    }
                }
            }

            return board;
        }

        private int FillSand(Board board)
        {
            int count = 0;

            int currentX = 500 - board.XOffset;
            int currentY = 0;

            try
            {
                while (true)
                {
                    if (!board.Grid[currentX][currentY + 1])
                    {
                        currentY++;
                    }
                    else if (!board.Grid[currentX - 1][currentY + 1])
                    {
                        currentX--;
                        currentY++;
                    }
                    else if (!board.Grid[currentX + 1][currentY + 1])
                    {
                        currentX++;
                        currentY++;
                    }
                    else
                    {
                        count++;
                        board.Grid[currentX][currentY] = true;
                        currentX = 500 - board.XOffset;
                        currentY = 0;
                    }
                }
            }
            catch { }
            return count;
        }

        private int FillSand2(Board board)
        {
            int count = 0;

            int currentX = (500 - board.XOffset) + LargeBoardExtension;
            int currentY = 0;

            while (true)
            {
                if (!board.Grid[currentX][currentY + 1])
                {
                    currentY++;
                }
                else if (!board.Grid[currentX - 1][currentY + 1])
                {
                    currentX--;
                    currentY++;
                }
                else if (!board.Grid[currentX + 1][currentY + 1])
                {
                    currentX++;
                    currentY++;
                }
                else
                {
                    count++;
                    if (currentX == (500 - board.XOffset) + LargeBoardExtension && currentY == 0)
                    {
                        break;
                    }
                    board.Grid[currentX][currentY] = true;
                    currentX = (500 - board.XOffset) + LargeBoardExtension;
                    currentY = 0;
                }
            }

            return count;
        }
    }
}
