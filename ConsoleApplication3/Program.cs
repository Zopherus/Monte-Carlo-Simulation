using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        const int NUMSTUDENTS = 100;
        const int NUMTRIALS = 10000000;
        static int[] students = new int[NUMSTUDENTS];
        static Random random = new Random();
        static void Main(string[] args)
        {
            long successes = 0;
            long i;
            for (i = 0; i < NUMTRIALS; i++)
            {
                if (trial())
                    successes++;
            }
            Console.WriteLine((double)successes / (double)i);
            Console.ReadLine();
        }

        static bool trial()
        {
            List<int> values = Enumerable.Range(0, NUMSTUDENTS).ToList<int>();
            int max = values.Count;
            for (int counter = 0; counter < max; counter++)
            {
                int number = values.ElementAt(random.Next(values.Count));
                if (counter == max - 1 && number == max - 1)
                    return false;
                if (number != counter)
                {
                    values.Remove(number);
                    students[counter] = number;
                }
                else
                    counter--;
            }
            floodFill(0);
            bool result = true;
            for (int x = 0; x < NUMSTUDENTS; x++)
            {
                if (students[x] != -1)
                    result = false;
            }
            return result;
        }

        static void floodFill(int studentNumber)
        {
            int nextStudent = students[studentNumber];
            students[studentNumber] = -1;
            if (students[nextStudent] != -1)
                floodFill(nextStudent);
        }
    }
}
