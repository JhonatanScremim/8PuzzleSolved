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
            this.openStates = openStates;
            this.closedStates = closedStates;

            hasAnswer = CheckSolvable(inicial);

            if (hasAnswer)
            {
                openStates.Add(inicial);
                PuzzleState resolution = solve();
            }
        }

        PuzzleState solve()
        {
            PuzzleState actualState = getLeastCostState();
            closeState(actualState);
            if (verifyFinished(actualState))
            {
                return actualState;
            }
            addNewStates(actualState);
            return solve();
        }

        PuzzleState getLeastCostState()
        {
            PuzzleState leastCostState = null;
            foreach (PuzzleState item in openStates)
            {
                //Pega o PuzzleState de menor custo
                if (leastCostState == null)
                {
                    leastCostState = item;
                }
                else if (leastCostState.Cost > item.Cost)
                {
                    leastCostState = item;
                }
            }
            return leastCostState;
        }

        bool verifyFinished(PuzzleState state)
        {
            if (state.Equals(final))
            {
                return true;
            }
            return false;
        }

        void closeState(PuzzleState state)
        {//Remove da lista de estados abertos e coloca na de estados fechados
            closedStates.Add(state);
            openStates.Remove(openStates.Find(x => x.Equals(state)));
        }

        void addNewStates(PuzzleState state)
        {
            List<PuzzleState> list = state.generateChildren();
            foreach (PuzzleState item in list)
            {
                openStates.Add(item);
            }
        }

        bool CheckSolvable(PuzzleState state)
        {
            var array = state.Numbers.Cast<int>().ToArray();
            int count = 0;

            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = i + 1; j < array.GetLength(1); j++)
                    if (array[i] > array[j])
                        count++;

            if (count % 2 == 0)
                return true;

            return false;
        }
    }
}
