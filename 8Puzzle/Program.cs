using System;

namespace _8Puzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            start();
        }

        static void start()
        {
            int[,] inicial = new int[,] { { 1, 8, 2 }, { 0, 4, 3 }, { 7, 6, 5 } };
            int[,] final = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };
            PuzzleState inicialState = new PuzzleState(inicial);
            PuzzleState finalState = new PuzzleState(final);
            Solver solver = new Solver(inicialState, finalState);
            Console.WriteLine("solver.hasAnswer :: " + solver.hasAnswer + "\n");
            Console.WriteLine("inicial\n" + inicialState.WriteState() + "\n");
            Console.WriteLine("final\n" + finalState.WriteState() + "\n");
            Console.ReadKey(); 
        }

        
    }
}
