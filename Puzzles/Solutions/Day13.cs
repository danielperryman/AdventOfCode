using System;

namespace Puzzles.Solutions
{
    public sealed class Day13 : IPuzzle
    {
        public int Day => 13;

        public string Puzzle1(string[] input)
        {
            List<object>? packet1 = null;
            List<object>? packet2 = null;

            int index = 1;

            int packetsInOrder = 0;

            foreach(var packet in input) 
            {
                if(packet1 == null) 
                {
                    packet1 = BuildPacket(packet.Substring(1, packet.Length - 2));
                }
                else if (packet2 == null)
                {
                    packet2 = BuildPacket(packet.Substring(1, packet.Length - 2));
                }
                else
                {
                    if (InOrder(packet1, packet2)!.Value)
                    {
                        packetsInOrder+=index;
                    }
                    packet1 = null;
                    packet2 = null;
                    index++;
                }
            }
            if (InOrder(packet1!, packet2!)!.Value)
            {
                packetsInOrder += index;
            }

            return packetsInOrder.ToString();
        }

        public string Puzzle2(string[] input)
        {
            List<Packet> allPackets = new List<Packet>
            {
                new Packet(BuildPacket("[2]"), true),
                new Packet(BuildPacket("[6]"), true)
            };

            List<object>? packet1 = null;
            List<object>? packet2 = null; 
            
            foreach (var packet in input)
            {
                if (packet1 == null)
                {
                    packet1 = BuildPacket(packet.Substring(1, packet.Length - 2));
                }
                else if (packet2 == null)
                {
                    packet2 = BuildPacket(packet.Substring(1, packet.Length - 2));
                }
                else
                {
                    allPackets.Add(new Packet(packet1));
                    allPackets.Add(new Packet(packet2));
                    packet1 = null;
                    packet2 = null;
                }
            }

            allPackets.Add(new Packet(packet1!));
            allPackets.Add(new Packet(packet2!));

            SortPackets(allPackets);

            int value = 1;

            for(int i = 0; i < allPackets.Count; i++) 
            {
                if (allPackets[i].IsMarkerPacket)
                {
                    value *= (i + 1);
                }
            }

            return value.ToString();
        }

        private class Packet
        {
            public bool IsMarkerPacket { get; set; }

            public List<object> PacketData { get; set; }

            public Packet(List<object> packetData, bool isMarkerPacket = false)
            {
                IsMarkerPacket = isMarkerPacket;
                PacketData = packetData;
            }
        }

        private void SortPackets(List<Packet> packets)
        {
            int n = packets.Count;
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    if (!InOrder(packets[j].PacketData, packets[j + 1].PacketData)!.Value)
                    {
                        // swap temp and arr[i]
                        var temp = packets[j];
                        packets[j] = packets[j + 1];
                        packets[j + 1] = temp;
                    }
        }

    

        private bool? InOrder(List<object> packet1, List<object> packet2) 
        {
            int shortestList = packet1.Count < packet2.Count ? packet1.Count : packet2.Count;

            for(int x = 0; x< shortestList; x++) 
            {
                if (packet1[x].GetType() == typeof(int))
                {
                    if (packet2[x].GetType() == typeof(int))
                    {
                        if ((int)(packet1[x]) < (int)(packet2[x]))
                        {
                            return true;
                        }
                        else if ((int)(packet1[x]) > (int)(packet2[x]))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        List<object> listified1 = new List<object>();
                        listified1.Add(packet1[x]);
                        var inOrderNow = InOrder(listified1, (List<object>)packet2[x]);
                        if (inOrderNow.HasValue)
                        {
                            return inOrderNow;
                        }
                    }
                }
                else 
                {

                    if (packet2[x].GetType() == typeof(int))
                    {
                        List<object> listified2 = new List<object>();
                        listified2.Add(packet2[x]);
                        var inOrderNow = InOrder((List<object>)packet1[x], listified2);
                        if (inOrderNow.HasValue)
                        {
                            return inOrderNow;
                        }
                    }
                    else
                    {
                        var inOrderNow = InOrder((List<object>)packet1[x], (List<object>)packet2[x]);
                        if (inOrderNow.HasValue)
                        {
                            return inOrderNow;
                        }
                    }
                }
            }

            if (packet1.Count == packet2.Count)
            {
                return null;
            }
            else if (shortestList == packet1.Count)
            {
                return true;
            }
            else
            {
                return false; 
            }
        }

        private List<object> BuildPacket(string input)
        {
            List<object> returnList = new List<object>();
            for(int x = 0; x< input.Length; x++) 
            {
                if (input[x] == '[')
                {
                    var newListString = input.Substring(x + 1, DetermineEndOfList(x, input) - x - 1);
                    returnList.Add(BuildPacket(newListString));
                    x = DetermineEndOfList(x, input) + 1;
                }
                else if (input[x] != ',')
                {
                    string num = input[x].ToString();

                    while (x != input.Length - 1 && input[x + 1] != ',')
                    {
                        x++;
                        num += input[x].ToString();
                    }

                    returnList.Add(int.Parse(num));

                }
            }

            return returnList;
        }

        public int DetermineEndOfList(int startIndex, string input)
        {
            int bracketCount = 1;

            for(int x = startIndex+1; x < input.Length; x++) 
            {
                if (input[x] == '[')
                {
                    bracketCount++;
                }
                else if (input[x] == ']')
                {
                    bracketCount--;
                }

                if (bracketCount == 0)
                {
                    return x;
                }
            }

            return 0;

        }
    }
}
