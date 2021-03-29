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
        int count = 0;

        public Solver(PuzzleState inicial, PuzzleState final)
        {
            this.inicial = inicial;
            this.final = final;
            this.openStates = new List<PuzzleState>();
            this.closedStates = new List<PuzzleState>();

            hasAnswer = CheckSolvable(inicial);

            if (hasAnswer)
            {
                openStates.Add(inicial);
            }
        }

        public List<PuzzleState> Solve()
        {
            PuzzleState actualState = GetLeastCostState();

            if (actualState == null)
                return null;

            CloseState(actualState);
            if (VerifyFinished(actualState))
            {
                return GetPath(actualState);
            }
            AddNewStates(actualState);
            Solve();
            return null;
        }

        List<PuzzleState> GetPath(PuzzleState lastState)
        {
            PuzzleState x;
            List<PuzzleState> list = new List<PuzzleState>();
            x = lastState;
            while (x.Father != null)
            {
                list.Add(x);
                x = x.Father;
            }
            list.Add(inicial);
            WriteList(list);
            return list;
        }

        PuzzleState GetLeastCostState()
        {
            PuzzleState leastCostState = null;
            foreach (PuzzleState item in openStates)
            {//Pega o PuzzleState de menor custo
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

        bool VerifyFinished(PuzzleState state)
        {
            for (int x = 0; x < state.Numbers.GetLength(0); x++)
            {
                for (int y = 0; y < state.Numbers.GetLength(1); y++)
                {
                    if (state.Numbers[x, y] != final.Numbers[x, y]) { return false; }
                }
            }
            return true;
        }

        void CloseState(PuzzleState state)
        {//Remove da lista de estados abertos e coloca na de estados fechados
            closedStates.Add(state);
            // Dá pra criar uma lista que só adiciona os que tem pouco custo
            openStates.Remove(openStates.Find(x => x.Equals(state)));
        }

        void AddNewStates(PuzzleState state)
        {
            List<PuzzleState> list = state.GenerateChildren();
            foreach (PuzzleState item in list)
            {
                openStates.Add(item);
            }
        }

        bool CheckSolvable(PuzzleState state)
        {
            var array = state.Numbers.Cast<int>().ToArray();
            int count = 0;

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] > array[j] && array[j] != 0 && array[i] != 0)
                    {
                        count++;
                    }
                }
            }

            if (count % 2 == 0) { return true; }

            return false;
        }

        public string WriteList(List<PuzzleState> list)
        {
            string result = "";
            foreach (var item in list)
            {
                Console.WriteLine("iteration:" + count++ + "\n" + item.WriteState());
            }
            return result;
        }
    }
}
