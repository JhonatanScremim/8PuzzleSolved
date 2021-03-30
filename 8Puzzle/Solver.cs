using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Puzzle
{
    class Solver
    {
        #region Atributos
        public PuzzleState initial { get; set; }
        public PuzzleState final { get; set; }
        public List<PuzzleState> closedStates { get; set; }
        public List<PuzzleState> openStates { get; set; }
        public bool hasAnswer = false;
        int count = 0;

        #endregion

        #region Construtor

        public Solver(PuzzleState initial, PuzzleState final)
        {
            this.initial = initial;
            this.final = final;
            this.openStates = new List<PuzzleState>();
            this.closedStates = new List<PuzzleState>();

            hasAnswer = CheckSolvable(initial);

            if (hasAnswer)
            {
                openStates.Add(initial);
            }
        }

        #endregion

        #region Solver

        public List<PuzzleState> Solve()
        {
            PuzzleState actualState = GetLeastCostState();

            if (actualState == null)
                return null;

            CloseState(actualState);
            if (VerifyFinished(actualState, final))
            {
                return GetPath(actualState);
            }
            AddNewStates(actualState);
            Solve();
            return null;
        }

        #endregion

        #region Listar Resultado

        List<PuzzleState> GetPath(PuzzleState lastState)
        {
            List<PuzzleState> list = new List<PuzzleState>();
            while (lastState.Father != null)
            {
                list.Insert(0, lastState);
                lastState = lastState.Father;
            }
            list.Insert(0, initial);
            WriteList(list);
            return list;
        }

        #endregion

        #region Obter menor custo

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

        #endregion

        #region Adicionar na lista fechada
        void CloseState(PuzzleState state)
        {
            //Remove da lista de estados abertos e coloca na de estados fechados
            closedStates.Add(state);
            // Dá pra criar uma lista que só adiciona os que tem pouco custo
            openStates.Remove(openStates.Find(x => x.Equals(state)));
        }

        #endregion

        #region Adicionar novo estado
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

        #endregion

        #region Verificar estado na lista fechada

        public bool IsOnThisList(List<PuzzleState> list, PuzzleState state)
        {
            foreach (var item in list)
            {
                if (VerifyFinished(item, state))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Verificar se existe solução
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

        #endregion

        #region Escrever as interações
        public string WriteList(List<PuzzleState> list)
        {
            string result = "";

            foreach (var item in list)
            {
                Console.WriteLine("\niteration:" + count++ + "\n" + item.WriteState());
            }
            return result;
        }

        #endregion

        #region Verifica se o estado inicial é igual ao final
        bool VerifyFinished(PuzzleState one, PuzzleState two)
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

        #endregion
    }
}
