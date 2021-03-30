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
            if (VerifyEqual(actualState, final))
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
                list.Insert(0, x);
                x = x.Father;
            }
            list.Insert(0, inicial);
            WriteList(list);
            return list;
        }

        PuzzleState GetLeastCostState()
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

        void CloseState(PuzzleState state)
        {
            //Remove da lista de estados abertos e coloca na de estados fechados
            closedStates.Add(state);
            // Dá pra criar uma lista que só adiciona os que tem pouco custo
            openStates.Remove(openStates.Find(x => x.Equals(state)));
        }

        void AddNewStates(PuzzleState state)
        {
            List<PuzzleState> list = state.GenerateChildren();
            foreach (PuzzleState item in list)
            {
                if (IsOnThisList(closedStates, item) == false)
                {
                    openStates.Add(item);
                }
            }
        }
        public bool IsOnThisList(List<PuzzleState> list, PuzzleState state)
        {
            foreach (var item in list)
            {
                if (VerifyEqual(item, state))
                {
                    return true;
                }
            }
            return false;
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
                Console.WriteLine("\niteration:" + count++ + "\n" + item.WriteState());
            }
            return result;
        }

        bool VerifyEqual(PuzzleState one, PuzzleState two)
        {
            for (int x = 0; x < one.Numbers.GetLength(0); x++)
            {
                for (int y = 0; y < one.Numbers.GetLength(1); y++)
                {
                    if (one.Numbers[x, y] != two.Numbers[x, y]) { return false; }
                }
            }
            return true;
        }
    }
}
