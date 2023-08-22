using System;
using System.Collections.Generic;

class Program {
    static string nos;

    static void Main() {
        Console.WriteLine("Enter your name: ");
        nos = Console.ReadLine();
        Console.WriteLine("Enter the number of subjects: ");
        int num = int.Parse(Console.ReadLine());

        Dictionary<string, int> store = new Dictionary<string, int>();

        for (int i = 0; i < num; i++) {
            Console.WriteLine("Enter the name of the subject: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the grade of the subject: ");
            int grade = int.Parse(Console.ReadLine());

            while (grade < 0 || grade > 100) {
                Console.WriteLine("You have entered an invalid grade. Enter between 0 and 100");
                grade = int.Parse(Console.ReadLine());
            }

            store.Add(name, grade);
        }

        calc_avg(store);
    }

    static void calc_avg(Dictionary<string, int> store) {
        double val = 0;
        double divider = 0;

        Console.WriteLine($"Your name is {nos}");

        foreach (KeyValuePair<string, int> kvp in store) {
            Console.WriteLine($"The subject is {kvp.Key} and the grade is {kvp.Value}");
            val += kvp.Value;
            divider += 1;
        }

        double avg = val / divider;
        Console.WriteLine($"{nos}'s average is {avg}");
    }
}

