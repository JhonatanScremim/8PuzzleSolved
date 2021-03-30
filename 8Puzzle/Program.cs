using System;
using System.Collections.Generic;

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
            int[,] inicial = new int[,] { { 1, 3, 7 }, { 4, 0, 2 }, { 6, 8, 5 } };
            //int[,] inicial = new int[,] { { 1, 2, 3 }, { 4, 5, 0 }, { 7, 8, 6 } };
            int[,] final = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };
            PuzzleState finalState = new PuzzleState(final, 0, null);
            PuzzleState inicialState = new PuzzleState(inicial, 0, finalState);
            Console.WriteLine("inicial\n" + inicialState.WriteState() + "\n");
            Solver solver = new Solver(inicialState, finalState);
            Console.WriteLine("Has an Answer :: " + solver.hasAnswer + "\n");
            List<PuzzleState> resolution = null;
            while (resolution == null)
            {
                resolution = solver.Solve();
            }
            Console.ReadKey(); 
        }
    }
}
