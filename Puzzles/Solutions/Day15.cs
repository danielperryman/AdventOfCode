namespace Puzzles.Solutions
{
    public sealed class Day15 : IPuzzle
    {
        public int Day => 15;

        public string Puzzle1(string[] input)
        {
            throw new NotImplementedException();
        }

        public string Puzzle2(string[] input)
        {
            throw new NotImplementedException();
        }
        private class Board
        {
            public List<List<bool>> Grid { get; set; } = new List<List<bool>>();
            public int XOffset { get; set; }
            public int YOffset { get; set; }
        }
        private class Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private class Sensor
        {
            public Coordinate SensorPosition { get; set; }

            public Coordinate ClosestBeacon { get; set; }

            public int ManhattanDistance { get; set; }
        }


        private Board BuildBoard(string[] input)
        {
            int maxX = 0;
            int maxY = 0;
            Board board = new Board();

            board.XOffset = 100000;
            board.YOffset = 100000;

            List<Sensor> sensors = new List<Sensor>();

            //initial loop to determine boundries
            foreach (var line in input)
            {
                //2389
                var commands = line.Split(' ').ToList();

                Sensor sensor= new Sensor();

               // int sensorX = commands[2].Substring(2, )

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

            }
            int xCount = 0;

            //xCount = board.XOffset +  * 2;

            for (int x = 0; x <= xCount; x++)
            {
                List<bool> newRow = new List<bool>();
                for (int y = 0; y <= maxY; y++)
                {
                    newRow.Add(false);
                }

                board.Grid.Add(newRow);
            }

            return board;
        }
    }
}
