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
                PuzzleState resolution = Solve();
                Console.WriteLine("\n\nResolution :: \n" + resolution.WriteState());
            }
        }

        PuzzleState Solve()
        {
            WriteList(false);
            PuzzleState actualState = GetLeastCostState();
            CloseState(actualState);
            if (VerifyFinished(actualState))
            {
                return actualState;
            }
            AddNewStates(actualState);
            return Solve();
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

        bool VerifyFinished(PuzzleState state)
        {
            if (state.Numbers.Equals(final.Numbers))
            {
                return true;
            }
            return false;
        }

        void CloseState(PuzzleState state)
        {//Remove da lista de estados abertos e coloca na de estados fechados
            closedStates.Add(state);
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
        
        public string WriteList(bool openOrClosed)
        {
            string result = "";
            if (openOrClosed)
            {
                result += "open states:\n";
                foreach (var item in openStates)
                {
                    Console.WriteLine(count++ + "\n" + item.WriteState());
                }
            }
            else
            {
                result += "closed states:\n";
                foreach (var item in closedStates)
                {
                    Console.WriteLine(count++ + "\n" + item.WriteState());
                }
            }
            Console.ReadKey();
            return result;
        }
    }
}
