using System;
using System.IO;


namespace day00ex01
{
    class Program
    {
        static int LevenshteinDistance(string string1, string string2)
        {
            int diff;
            int[,] dist = new int[string1.Length + 1, string2.Length + 1];
            for (int i = 0; i <= string1.Length; i++) { dist[i, 0] = i; }
            for (int j = 0; j <= string2.Length; j++) { dist[0, j] = j; }
            for (int i = 1; i <= string1.Length; i++)
            {
                for (int j = 1; j <= string2.Length; j++)
                {
                    diff = (string1[i - 1] == string2[j - 1]) ? 0 : 1;
                    dist[i, j] = Math.Min(Math.Min(dist[i - 1, j] + 1, dist[i, j - 1] + 1), dist[i - 1, j - 1] + diff);
                }
            }
            return dist[string1.Length, string2.Length];
        }

        static string[] read_file()
        {
            string path;
            string[] file;

            file = null;
            path = "./us.txt";
            if (File.Exists(path) == true)
            {
                file = File.ReadAllLines(path);
            }

            return file;
        }

        static void serch_name(string[] rd_file, string name, int i)
        {
            int dist, save_index, save_dist;
            string check;

            dist = 0;
            save_index = 0;
            save_dist = 100;
            while (rd_file.Length > i)
            {
                dist = LevenshteinDistance(name, rd_file[i]);
                if (dist == 0)
                {
                    Console.WriteLine($"Hello, {rd_file[i]}");
                    return;
                }
                else if (dist < save_dist)
                {
                    save_dist = dist;
                    save_index = i;
                }

                i++;
            }

            if (save_dist < 3)
            {
                Console.Write($"Did you mean “{rd_file[save_index]}”? Y/N: ");
                check = Console.ReadLine();
                if (check == "Y")
                {
                    Console.WriteLine($"Hello, {rd_file[save_index]}");
                }else if (check == "Y")
                {
                    serch_name(rd_file, name, save_index + 1);
                }else
                    Console.WriteLine("Command not found");
            }
            else
            {
                Console.WriteLine("Your name was not found.");
            }
        }
        
        static void Main(string[] args)
        {
            string name;
            string[] rd_file;
            int dist, i, save_dist, save_index;
            rd_file = read_file();
            Console.Write("Enter name:");
            name = Console.ReadLine();
            if (name == "")
            {
                Console.WriteLine("Your name was not found");
                return;
            }

            i = 0;
            dist = 0;
            save_dist = 100;
            save_index = 0;
            serch_name(rd_file, name, i);
        }
    }
}