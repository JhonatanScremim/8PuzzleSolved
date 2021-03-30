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
            int[,] initial = new int[,] { { 5, 8, 2 }, { 0, 3, 4 }, { 7, 6, 1 } };
            int[,] final = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };

            PuzzleState finalState = new PuzzleState(final, 0, null);
            PuzzleState initialState = new PuzzleState(initial, 0, finalState);
            Console.WriteLine("inicial\n" + initialState.WriteState() + "\n");
            Solver solver = new Solver(initialState, finalState);
            Console.WriteLine("Has an Answer :: " + solver.hasAnswer);
            List<PuzzleState> resolution = solver.Solve();
            Console.ReadKey();
        }
    }
}
