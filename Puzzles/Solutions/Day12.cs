namespace Puzzles.Solutions
{
    public sealed class Day12 : IPuzzle
    {
        public int Day => 12;

        private class MapMarker
        {
            public char Letter { get; set; }

            public bool Visited { get; set; }
        }

        private class Location
        {
            public int X { get; set; }

            public int Y { get; set; }

        }

        private class Node
        {
            public Location Coordinates { get; set; }

            public int Depth { get; set; }

            public string Path { get; set; } = string.Empty;

            public Node(Location coordinates, int depth)
            {
                Coordinates = coordinates;
                Depth = depth;
            }

            public Node(Location coordinates, int depth, string path)
            {
                Coordinates = coordinates;
                Depth = depth;
                Path = path;
            }
        }

        public string Puzzle1(string[] input)
        {
            var map = BuildMap(input);

            Location startingLocation = new Location();

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j].Letter == 'S')
                    {
                        map[i][j].Letter = 'a';
                        map[i][j].Visited = true;
                        startingLocation = new Location { X = j, Y = i };
                        break;
                    }
                }
            }

            return FindBestPath(startingLocation, map).ToString();
        }

        public string Puzzle2(string[] input)
        {
            var map = BuildMap(input);

            Location startingLocation = new Location();

            int shortestPath = 100000;

            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j].Letter == 'S' || map[i][j].Letter == 'a')
                    {
                        var workingMap = BuildMap(input);
                        workingMap[i][j].Letter = 'a';
                        workingMap[i][j].Visited = true;
                        startingLocation = new Location { X = j, Y = i };
                        var currentPath = FindBestPath(startingLocation, workingMap);
                        shortestPath = currentPath < shortestPath && currentPath != 0 ? currentPath : shortestPath;
                    }
                }
            }

            return shortestPath.ToString();
        }

        private List<List<MapMarker>> BuildMap(string[] input)
        {
            List<List<MapMarker>> map = new List<List<MapMarker>>();
            foreach (var line in input)
            {
                var markers = new List<MapMarker>();

                foreach (var character in line.ToCharArray().ToList())
                {
                    markers.Add(new MapMarker { Letter = character, Visited = false });
                }

                map.Add(markers);
            }
            return map;
        }

        private int FindBestPath(Location startLocation, List<List<MapMarker>> map)
        {
            Node startNode = new Node(startLocation, 0);
            Queue<Node> depthFirstQueue = new Queue<Node>();
            depthFirstQueue.Enqueue(startNode);
           

            while (depthFirstQueue.Count > 0)
            {
                var currentnode = depthFirstQueue.Dequeue();
                var children = GetChildren(currentnode, map);

                var goal = children.Where(c => map[c.Coordinates.Y][c.Coordinates.X].Letter == 'E').FirstOrDefault();
                if (goal != null)
                {
                    return goal.Depth;
                }
                else 
                {
                    foreach(var child in children) 
                    {
                        depthFirstQueue.Enqueue(child);
                    }
                }
            }

            return 0;
        }

        private List<Node> GetChildren(Node parent, List<List<MapMarker>> map)
        {
            List<Node> children = new List<Node>();
            if (parent.Coordinates.X > 0
                && IsAccessable(map[parent.Coordinates.Y][parent.Coordinates.X].Letter, map[parent.Coordinates.Y][parent.Coordinates.X - 1].Letter) 
                && !map[parent.Coordinates.Y][parent.Coordinates.X - 1].Visited)
            {
                map[parent.Coordinates.Y][parent.Coordinates.X - 1].Visited = true;
                children.Add(new Node (new Location { Y = parent.Coordinates.Y, X = parent.Coordinates.X - 1 }, parent.Depth+1, parent.Path+"<"));
            }
            if (parent.Coordinates.Y > 0
                && IsAccessable(map[parent.Coordinates.Y][parent.Coordinates.X].Letter, map[parent.Coordinates.Y - 1][parent.Coordinates.X].Letter)
                && !map[parent.Coordinates.Y - 1][parent.Coordinates.X].Visited)
            {
                map[parent.Coordinates.Y - 1][parent.Coordinates.X].Visited = true;
                children.Add(new Node(new Location { Y = parent.Coordinates.Y - 1, X = parent.Coordinates.X}, parent.Depth + 1, parent.Path + "^"));
            }
            if (parent.Coordinates.X < map[0].Count-1
                && IsAccessable(map[parent.Coordinates.Y][parent.Coordinates.X].Letter, map[parent.Coordinates.Y][parent.Coordinates.X + 1].Letter)
                && !map[parent.Coordinates.Y][parent.Coordinates.X + 1].Visited)
            {
                map[parent.Coordinates.Y][parent.Coordinates.X + 1].Visited = true;
                children.Add(new Node(new Location { Y = parent.Coordinates.Y, X = parent.Coordinates.X + 1 }, parent.Depth + 1, parent.Path + ">"));
            }
            if (parent.Coordinates.Y < map.Count - 1
                && IsAccessable(map[parent.Coordinates.Y][parent.Coordinates.X].Letter, map[parent.Coordinates.Y + 1][parent.Coordinates.X].Letter)
                && !map[parent.Coordinates.Y + 1][parent.Coordinates.X].Visited)
            {
                map[parent.Coordinates.Y + 1][parent.Coordinates.X].Visited = true;
                children.Add(new Node(new Location { Y = parent.Coordinates.Y + 1, X = parent.Coordinates.X }, parent.Depth + 1, parent.Path + "v"));
            }
            return children;
        }

        private bool IsAccessable(char current, char target)
        {
            target = target == 'E' ? 'z' : target;
            if (current - target >= -1)
            {
                return true;
            }
            return false;
        }
    }
}
