using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        const int NUMSTUDENTS = 30;
        const int NUMTRIALS = 100000000;
        static Student[] students = new Student[NUMSTUDENTS];
        static Random random = new Random();
        static int trialTrue = 0;
        static int trialFalse = 0;
        static void Main(string[] args)
        {
            for (int i = 0; i < NUMTRIALS; i++)
            {
                trial();
            }
            Console.WriteLine(trialTrue);
            Console.WriteLine(trialFalse);
            Console.WriteLine((double)trialTrue / ((double)trialTrue + (double)trialFalse));
            Console.ReadLine();
        }

        static void trial()
        {
            for (int a = 0; a < NUMSTUDENTS; a++)
            {
                students[a] = new Student();
            }
            List<int> values = Enumerable.Range(0, NUMSTUDENTS).ToList<int>();
            for (int counter = 0; counter < values.Count; counter++)
            {
                int number = values.ElementAt(random.Next(values.Count));
                values.Remove(number);
                students[counter].nextStudent = number;
            }
            floodFill(0);
            bool result = true;
            for (int x = 0; x < NUMSTUDENTS; x++)
            {
                if (!students[x].visited)
                    result = false;
            }
            if (result)
                trialTrue++;
            else
                trialFalse++;
        }
        
        static void floodFill(int studentNumber)
        {
            students[studentNumber].visited = true;
            if (!students[studentNumber].visited)
                floodFill(students[studentNumber].nextStudent);
        }
    }
}
