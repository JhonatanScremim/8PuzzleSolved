using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Puzzle
{
    class Solver
    {
        private PuzzleState inicial;
        private PuzzleState final;
        private List<PuzzleState> closedStates;
        private List<PuzzleState> openStates;
        public bool hasAnswer = false;

        public Solver(PuzzleState inicial, PuzzleState final, List<PuzzleState> closedStates, List<PuzzleState> openStates)
        {
            this.inicial = inicial;
            this.final = final;
            this.closedStates = closedStates;
            this.openStates = openStates;

            hasAnswer = solve();
        }

        bool solve()
        {
            if (verifyFinished())
            {
                return true;
            }
            generateStates(inicial);
            return false;
        }

        void generateStates(PuzzleState father)
        {
            
        }

        bool verifyFinished()
        {
            if (closedStates.Last().Equals(final))
            {
                return true;
            }
            return false;
        }

        void goToNextState()
        {

        }

        int calculateCost()
        {
            int cost = 0;
            return cost;
        }

        bool ePossivelResolver()
        {

        }
    }
}
