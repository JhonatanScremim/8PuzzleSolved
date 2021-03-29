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
            //int[,] inicial = new int[,] { { 1, 2, 3 }, { 4, 5, 0 }, { 7, 8, 6 } };
            int[,] final = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };
            PuzzleState finalState = new PuzzleState(final, 0, null);
            PuzzleState inicialState = new PuzzleState(inicial, 0, finalState);
            Console.WriteLine("inicial\n" + inicialState.WriteState() + "\n");
            Solver solver = new Solver(inicialState, finalState);
            Console.WriteLine("Has an Answer :: " + solver.hasAnswer + "\n");
            PuzzleState resolution = solver.Solve();
            Console.WriteLine("Resolution :: \n" + resolution.WriteState() + "\n");
            Console.ReadKey(); 
        }
    }
}
