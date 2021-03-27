using System;

namespace _8Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("I am a 8-Puzzle Solver, use with care!");
            //Console.WriteLine("Do you want to start? (y/n)");
            //string response = Console.ReadKey().KeyChar.ToString();
            //if (response.Equals("y"))
            //{
            //    Console.WriteLine("Lovely, let's start!");
            //    start();
            //}
            start();
        }

        static void start()
        {
            int[,] inicial = new int[,] { { 2, 8, 3 }, { 1, 6, 4 }, { 7, 0, 5 } };
            int[,] final = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };
            PuzzleState inicialState = new PuzzleState(inicial);
            PuzzleState finalState = new PuzzleState(final);
            Solver solver = new Solver(inicialState, finalState);
            Console.WriteLine("solver.hasAnswer :: " + solver.hasAnswer);
            Console.ReadKey();
        }
    }
}
