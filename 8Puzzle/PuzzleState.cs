using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Puzzle
{
    class PuzzleState
    {
        #region Atributos
        public int[,] Numbers { get; set; }
        public int XPosOf0 { get; set; }
        public int YPosOf0 { get; set; }
        public List<PuzzleState> ListOfChildren { get; set; }
        public PuzzleState Father { get; set; }
        public int Cost { get; set; }
        public int PathCost { get; set; }
        public PuzzleState FinalState { get; set; }

        #endregion

        #region Construtor
        public PuzzleState(int[,] numbers, int pathCost, PuzzleState finalState)
        {
            this.Numbers = numbers;
            this.PathCost = pathCost;
            ListOfChildren = new List<PuzzleState>();
            this.FinalState = finalState;
            if (FinalState != null)
            {
                CalculateLikenessCost();
            }

            //Pega a posição do 0 dentro do arrays
            for (int x = 0; x < numbers.GetLength(0); x++)
            {
                for (int y = 0; y < numbers.GetLength(1); y++)
                {
                    if (numbers[x, y] == 0)
                    {
                        XPosOf0 = x;
                        YPosOf0 = y;
                        return;
                    }
                }
            }
        }

        #endregion

        #region Gerar filhos

        public List<PuzzleState> GenerateChildren()
        {
            //Gera os filhos do puzzle e retorna
            if (XPosOf0 + 1 < Numbers.GetLength(0))
            {
                //Cria um filho, modifica a posição do 0 e adiciona
                int[,] newNumbers = (int[,])Numbers.Clone();
                PuzzleState children = new PuzzleState(newNumbers, this.PathCost + 1, this.FinalState);
                children.Move0Position(XPosOf0 + 1, YPosOf0, children);
                children.XPosOf0 += 1;
                ListOfChildren.Add(children);
            }
            if (XPosOf0 - 1 >= 0)
            {
                int[,] newNumbers = (int[,])Numbers.Clone();
                PuzzleState children = new PuzzleState(newNumbers, this.PathCost + 1, this.FinalState);
                children.Move0Position(XPosOf0 - 1, YPosOf0, children);
                children.XPosOf0 -= 1;
                ListOfChildren.Add(children);
            }
            if (YPosOf0 + 1 < Numbers.GetLength(0))
            {
                int[,] newNumbers = (int[,])Numbers.Clone();
                PuzzleState children = new PuzzleState(newNumbers, this.PathCost + 1, this.FinalState);
                children.Move0Position(XPosOf0, YPosOf0 + 1, children);
                children.YPosOf0 += 1;
                ListOfChildren.Add(children);
            }
            if (YPosOf0 - 1 >= 0)
            {
                int[,] newNumbers = (int[,])Numbers.Clone();
                PuzzleState children = new PuzzleState(newNumbers, this.PathCost + 1, this.FinalState);
                children.Move0Position(XPosOf0, YPosOf0 - 1, children);
                children.YPosOf0 -= 1;
                ListOfChildren.Add(children);
            }

            foreach (var childrens in ListOfChildren)
            {
                childrens.Father = this;
            }

            return ListOfChildren;
        }

        #endregion

        #region Mover posição

        void Move0Position(int newXPos, int newYPos, PuzzleState state)
        {
            //inverte a posição do 0 com o da nova posição
            state.Numbers[state.XPosOf0, state.YPosOf0] = state.Numbers[newXPos, newYPos];
            state.Numbers[newXPos, newYPos] = 0;
        }

        #endregion

        #region Calculo heuristica

        public void CalculateLikenessCost()
        {
            this.Cost = this.PathCost + HeuristicCost();
        }

        private int HeuristicCost()
        {
            int heuristicCost = 0;
            for (int x = 0; x < Numbers.GetLength(0); x++)
            {
                for (int y = 0; y < Numbers.GetLength(1); y++)
                {
                    if (Numbers[x, y] != FinalState.Numbers[x, y])
                    {
                        //pega a posição final do número sendo verificado atualmente
                        int[] posIdeal = FinalState.GetPositionOfNumber(Numbers[x, y]);
                        heuristicCost += Math.Abs(posIdeal[0] - x) + Math.Abs(posIdeal[1] - y);
                    }
                }
            }
            return heuristicCost;
        }
        #endregion

        #region Pegar a posição do número
        private int[] GetPositionOfNumber(int number)
        {
            for (int x = 0; x < Numbers.GetLength(0); x++)
            {
                for (int y = 0; y < Numbers.GetLength(1); y++)
                {
                    if (Numbers[x, y].Equals(number))
                    {
                        return new int[] { x, y };
                    }
                }
            }
            return null;
        }
        #endregion

        #region Escrever estado
        public string WriteState()
        {
            string result = "";
            for (int x = 0; x < Numbers.GetLength(0); x++)
            {
                result += "[";
                for (int y = 0; y < Numbers.GetLength(1); y++)
                {
                    result += Numbers[x, y];
                    if (y != Numbers.GetLength(1) - 1)
                    {
                        result += ",";
                    }
                }
                if (x == Numbers.GetLength(0) - 1)
                {
                    result += "]";
                }
                else
                {
                    result += "]\n";
                }
            }
            //Codigo obsoleto * irrelevante = desnecessário
            //result += "\ncost:" + Cost;
            return result;
        }
        #endregion
    }
}
