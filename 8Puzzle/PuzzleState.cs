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

        public int Cost { get => cost; set => cost = value; }
        public int PathCost { get => pathCost; set => pathCost = value; }
        public int[,] Numbers { get => numbers; set => numbers = value; }
        internal List<PuzzleState> ListOfChildren { get => listOfChildren; set => listOfChildren = value; }

        public PuzzleState(int[,] numbers)
        {
            this.Numbers = numbers;

            //Pega a posição do 0 dentro do arrays
            for (int x = 0; x < numbers.GetLength(0); x++)
            {
                for (int y = 0; y < numbers.GetLength(1); y++)
                {
                    if (numbers[x,y] == 0)
                    {
                        xPosOf0 = x;
                        yPosOf0 = y;
                        break;
                    }
                }
            }
        }

        public List<PuzzleState> GenerateChildren()
        {//Gera os filhos do puzzle e retorna
            if(xPosOf0 + 1 < Numbers.GetLength(0))
            {
                //Cria um filho, modifica a posição do 0 e adiciona
                PuzzleState children = this;
                children.Move0Position(xPosOf0 + 1, yPosOf0);
                children.xPosOf0 += 1;
                children.PathCost = this.pathCost + 1;
                ListOfChildren.Add(children);
            }
            if(xPosOf0 - 1 >= 0)
            {
                PuzzleState children = this;
                children.Move0Position(xPosOf0 - 1, yPosOf0);
                children.xPosOf0 -= 1;
                children.PathCost = this.pathCost + 1;
                ListOfChildren.Add(children);
            }
            if (yPosOf0 + 1 < Numbers.GetLength(0))
            {
                PuzzleState children = this;
                children.Move0Position(xPosOf0, yPosOf0 + 1);
                children.yPosOf0 += 1;
                children.PathCost = this.pathCost + 1;
                ListOfChildren.Add(children);
            }
            if (yPosOf0 - 1 >= 0)
            {
                PuzzleState children = this;
                children.Move0Position(xPosOf0, yPosOf0 - 1);
                children.yPosOf0 -= 1;
                children.PathCost = this.pathCost + 1;
                ListOfChildren.Add(children);
            }
            return ListOfChildren;
        }

        void Move0Position(int newXPos, int newYPos)
        {//inverte a posição do 0 com o da nova posição
            Numbers[xPosOf0, yPosOf0] = Numbers[newXPos, newYPos];
            Numbers[newXPos, newYPos] = 0;
        }

        void CalculateLikenessCost()
        {
            this.cost = this.pathCost + HeuristicCost();
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
    }
}
