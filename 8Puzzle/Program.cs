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
            int[,] initial = new int[,] { { 13, 0, 10, 1 }, { 3, 14, 15, 4 }, { 8, 6, 7, 9 }, { 2, 12, 5, 11 } };
            int[,] final = new int[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 0 } };

            //int[,] initial = new int[,] { { 5, 8, 2 }, { 0, 3, 4 }, { 7, 6, 1 } };
            //int[,] final = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };

            PuzzleState finalState = new PuzzleState(final, 0, null);
            PuzzleState inicialState = new PuzzleState(initial, 0, finalState);
            Console.WriteLine("inicial\n" + inicialState.WriteState() + "\n");
            Solver solver = new Solver(inicialState, finalState);
            Console.WriteLine("Has an Answer :: " + solver.hasAnswer + "\n");
            List<PuzzleState> resolution = solver.Solve();
            Console.ReadKey();
        }
    }
}
