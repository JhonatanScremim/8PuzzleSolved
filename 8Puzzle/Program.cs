using System;

namespace _8Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I am a 8-Puzzle Solver, use with care!");
            Console.WriteLine("Do you want to start? (y/n)");
            string response = Console.ReadKey().KeyChar.ToString();
            if (response.Equals("y"))
            {
                Console.WriteLine("Lovely, let's start!");
                start();
            }
        }

        static void start()
        {
            PuzzleState inicialState = new PuzzleState();
            PuzzleState finalState = new PuzzleState();
            Solver solver = new Solver();

        }
    }
}
