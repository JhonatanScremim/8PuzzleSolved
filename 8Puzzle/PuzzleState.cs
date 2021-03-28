using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Puzzle
{
    class PuzzleState
    {
        private int[,] numbers;
        private int xPosOf0;
        private int yPosOf0;
        private List<PuzzleState> listOfChildren;
        private int cost = 0;
        private int pathCost = 0;

        public int[,] Numbers { get => numbers; set => numbers = value; }
        public int XPosOf0 { get => xPosOf0; set => xPosOf0 = value; }
        public int YPosOf0 { get => yPosOf0; set => yPosOf0 = value; }
        internal List<PuzzleState> ListOfChildren { get => listOfChildren; set => listOfChildren = value; }
        public int Cost { get => cost; set => cost = value; }
        public int PathCost { get => pathCost; set => pathCost = value; }

        public PuzzleState(int[,] numbers)
        {
            this.Numbers = numbers;
            ListOfChildren = new List<PuzzleState>();

            //Pega a posição do 0 dentro do arrays
            for (int x = 0; x < numbers.GetLength(0); x++)
            {
                for (int y = 0; y < numbers.GetLength(1); y++)
                {
                    if (numbers[x,y] == 0)
                    {
                        XPosOf0 = x;
                        YPosOf0 = y;
                        return;
                    }
                }
            }
        }

        public List<PuzzleState> GenerateChildren()
        {//Gera os filhos do puzzle e retorna
            int[,] newNumbers = (int[,])Numbers.Clone();
            if (XPosOf0 + 1 < newNumbers.GetLength(0))
            {
                //Cria um filho, modifica a posição do 0 e adiciona
                PuzzleState children = new PuzzleState(newNumbers);
                children.Move0Position(XPosOf0 + 1, YPosOf0, children);
                children.XPosOf0 += 1;
                children.PathCost = this.PathCost + 1;
                ListOfChildren.Add(children);
            }
            if(XPosOf0 - 1 >= 0)
            {
                PuzzleState children = new PuzzleState(newNumbers);
                children.Move0Position(XPosOf0 - 1, YPosOf0, children);
                children.XPosOf0 -= 1;
                children.PathCost = this.PathCost + 1;
                ListOfChildren.Add(children);
            }
            if (YPosOf0 + 1 < newNumbers.GetLength(0))
            {
                PuzzleState children = new PuzzleState(newNumbers);
                children.Move0Position(XPosOf0, YPosOf0 + 1, children);
                children.YPosOf0 += 1;
                children.PathCost = this.PathCost + 1;
                ListOfChildren.Add(children);
            }
            if (YPosOf0 - 1 >= 0)
            {
                PuzzleState children = new PuzzleState(newNumbers);
                children.Move0Position(XPosOf0, YPosOf0 - 1, children);
                children.YPosOf0 -= 1;
                children.PathCost = this.PathCost + 1;
                ListOfChildren.Add(children);
            }
            WriteList();
            return ListOfChildren;
        }

        void Move0Position(int newXPos, int newYPos, PuzzleState state)
        {//inverte a posição do 0 com o da nova posição
            state.Numbers[state.XPosOf0, state.YPosOf0] = state.Numbers[newXPos, newYPos];
            state.Numbers[newXPos, newYPos] = 0;
        }

        void CalculateLikenessCost()
        {
            this.Cost = this.PathCost + HeuristicCost();
        }

        int HeuristicCost()
        {
            return 3;
        }

        public override bool Equals(object obj)
        {
            return obj is PuzzleState state &&
                   EqualityComparer<int[][]>.Default.Equals(Numbers);
        }

        public string WriteList()
        {
            string result = "";
            int count = 0;
            Console.WriteLine(count + ":\n" + WriteState());
            foreach (var item in ListOfChildren)
            {
                Console.WriteLine("\n" + item.WriteState());
            }
            Console.ReadKey();
            return result;
        }

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
            return result;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
