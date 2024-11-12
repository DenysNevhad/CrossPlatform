using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    struct Item
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

    static void Main()
    {
        // Зчитуємо дані з файлу INPUT.TXT
        var input = File.ReadAllLines("INPUT.TXT");
        var firstLine = input[0].Split();
        int n = int.Parse(firstLine[0]);
        long r = long.Parse(firstLine[1]);

        var items = new List<Item>();

        // Зчитуємо інформацію про кожну річ
        for (int i = 0; i < n; i++)
        {
            var line = input[i + 1].Split();
            long wi = long.Parse(line[0]);
            long di = long.Parse(line[1]);
            items.Add(new Item(i + 1, wi, di));
        }

        // Сортуємо речі за дедлайном di (чим раніше дедлайн, тим пріоритетніше)
        items = items.OrderBy(item => item.Di).ToList();

        var plan = new List<(long time, int id)>();
        long currentTime = 0;

        // Перебираємо кожну річ та плануємо її сушіння
        foreach (var item in items)
        {
            // Розрахунок часу, щоб висушити річ на мотузці
            long timeOnLine = item.Wi;

            // Якщо річ може висохнути до дедлайну на мотузці
            if (currentTime + timeOnLine <= item.Di)
            {
                currentTime += timeOnLine;
            }
            else
            {
                // Якщо потрібно ставити річ на батарею
                long timeNeededOnBattery = item.Wi - r;

                if (currentTime + timeNeededOnBattery > item.Di)
                {
                    File.WriteAllText("OUTPUT.TXT", "Impossible");
                    return;
                }

                plan.Add((currentTime, item.Id));
                currentTime += timeNeededOnBattery;
            }
        }

        // Записуємо план у файл OUTPUT.TXT
        using (var writer = new StreamWriter("OUTPUT.TXT"))
        {
            foreach (var (time, id) in plan)
            {
                writer.WriteLine($"{time} {id}");
            }
        }
    }
}
