namespace Puzzles.Solutions
{
    public sealed class Day09 : IPuzzle
    {
        public int Day => 9;

        public class Knot 
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public class Grid
        {
            private List<List<bool>> Positions { get; set; }

            private List<Knot> Knots { get; set; }

            private int MaxY { get; set; }

            public Grid(int knotsCount)
            {
                Positions = new List<List<bool>>();
                Positions.Add(new List<bool> { true });

                Knots = new List<Knot>();

                while (knotsCount > 0)
                {
                    Knots.Add(new Knot { X = 0, Y=0 }); 
                    knotsCount--;
                }

            }

            public void Move(char direction, int moves)
            {
                while (moves > 0)
                {
                    switch (direction)
                    {
                        case 'R':
                            Knots[0].X++;
                            break;
                        case 'L':
                            Knots[0].X--;
                            break;
                        case 'U':
                            Knots[0].Y++;
                            break;
                        case 'D':
                            Knots[0].Y--;
                            break;
                    }
                    MaxY = Knots[0].Y > MaxY ? Knots[0].Y : MaxY;
                    if (Knots[0].X >= Positions.Count || Knots[0].X < 0)
                    {
                        AddRows();
                    }
                    if (Knots[0].Y < 0)
                    {
                        AddColumn();
                    }
                    FillRows();
                    DetermineMoves();
                    moves--;
                }
            }

            private void DetermineMoves()
            {
                for (int x = 0; x < Knots.Count() - 1; x++)
                {
                    var lead = Knots[x];
                    var follow = Knots[x + 1];

                    if (lead.X == follow.X)
                    {
                        if (lead.Y == follow.Y)
                        {
                            //Same Spot
                            return;
                        }
                        else
                        {
                            //Vertical Move
                            if (Math.Abs(lead.Y - follow.Y) > 1)
                            {
                                MoveY(lead, follow);
                            }
                        }
                    }
                    else
                    {
                        if (lead.Y == follow.Y)
                        {
                            //Horizontal Move
                            if (Math.Abs(lead.X - follow.X) > 1)
                            {
                                MoveX(lead, follow);
                            }
                        }
                        else
                        {
                            if (Math.Abs(lead.Y - follow.Y) > 1 || Math.Abs(lead.X - follow.X) > 1)
                            {
                                //Diagonal Move
                                MoveX(lead, follow);
                                MoveY(lead, follow);
                            }
                        }
                    }
                }

                var tail = Knots[Knots.Count - 1];
                Positions[tail.X][tail.Y] = true;
            }

            private void AddRows()
            {
                if (Knots[0].X >= 0)
                {
                    for (int x = Knots[0].X - Positions.Count() + 1; x > 0; x--)
                    {
                        Positions.Add(new List<bool>());
                    }
                }
                else
                {
                    var newGrid = new List<List<bool>>();
                    newGrid.Add(new List<bool>());
                    newGrid.AddRange(Positions);
                    Positions = newGrid;
                    foreach (var knot in Knots)
                    {
                        knot.X++;
                    }
                }
            }

            private void AddColumn()
            {
                for (int x = 0; x < Positions.Count(); x++)
                {
                    var newRow = new List<bool>();
                    newRow.Add(false);
                    newRow.AddRange(Positions[x]);
                    Positions[x] = newRow;
                }
                foreach (var knot in Knots)
                {
                    knot.Y++;
                }
            }


            private void FillRows()
            {
                foreach(var row in Positions)
                {
                    for (int x = MaxY - row.Count; x>=0; x--)
                    {
                        row.Add(false);
                    }
                }
            }

            private void MoveX(Knot lead, Knot follow)
            {
                follow.X = lead.X > follow.X ? follow.X + 1 : follow.X - 1;
            }
            private void MoveY(Knot lead, Knot follow)
            {
                follow.Y = lead.Y > follow.Y ? follow.Y + 1 : follow.Y - 1;
            }

            public string CountVists()
            {
                int visits = 0;
                foreach (var row in Positions)
                {
                    visits += row.Count(r => r);
                }
                return visits.ToString();
            }

        }

        public string Puzzle1(string[] input)
        {
            var grid = new Grid(2);
            foreach (var instruction in input)
            {
                List<string> instructionParts = instruction.Split(' ').ToList();
                grid.Move(Convert.ToChar(instructionParts[0]), Convert.ToInt16(instructionParts[1]));
            }

            return grid.CountVists();
        }

        public string Puzzle2(string[] input)
        {
            var grid = new Grid(10);
            foreach (var instruction in input)
            {
                List<string> instructionParts = instruction.Split(' ').ToList();
                grid.Move(Convert.ToChar(instructionParts[0]), Convert.ToInt16(instructionParts[1]));
            }

            return grid.CountVists();
        }
    }
}
