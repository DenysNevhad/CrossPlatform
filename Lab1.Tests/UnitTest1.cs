using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Lab1.Tests
{
    public class ProgramTests
    {
        public class Item
        {
            public int Id;
            public long Wi;
            public long Di;

            public Item(int id, long wi, long di)
            {
                Id = id;
                Wi = wi;
                Di = di;
            }
        }

        public static List<Item> GetItemsFromInput(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var firstLine = lines[0].Split();
            int n = int.Parse(firstLine[0]);
            long r = long.Parse(firstLine[1]);

            var items = new List<Item>();

            for (int i = 0; i < n; i++)
            {
                var line = lines[i + 1].Split();
                long wi = long.Parse(line[0]);
                long di = long.Parse(line[1]);
                items.Add(new Item(i + 1, wi, di));
            }

            return items;
        }

        public static string PlanDrying(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var firstLine = lines[0].Split();
            int n = int.Parse(firstLine[0]);
            long r = long.Parse(firstLine[1]);

            var items = GetItemsFromInput(input);
            items = items.OrderBy(item => item.Di).ToList();

            var plan = new List<(long time, int id)>();
            long currentTime = 0;

            foreach (var item in items)
            {
                long timeToDryOnBattery = Math.Max(0, item.Wi - (item.Di - currentTime));

                if (currentTime + timeToDryOnBattery > item.Di)
                {
                    return "Impossible";
                }

                if (timeToDryOnBattery > 0)
                {
                    plan.Add((currentTime, item.Id));
                    currentTime += timeToDryOnBattery;
                }

                currentTime += item.Wi - timeToDryOnBattery;
            }

            return string.Join("\n", plan.Select(p => $"{p.time} {p.id}"));
        }

        [Fact]
        public void TestImpossiblePlan()
        {
            var input = "3 1\n5 3\n6 4\n7 5\n";
            var expected = "Impossible";

            var result = PlanDrying(input);

            Assert.Equal(expected, result);
        }
    }
}
