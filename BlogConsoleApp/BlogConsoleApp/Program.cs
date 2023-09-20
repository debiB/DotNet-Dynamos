using BlogConsoleApp.Driver;
using BlogConsoleApp;
using System;

namespace Tasks
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                await new Driver().Menu();
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
            }
        }
    }
}

