using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8Puzzle
{
    class PuzzleState
    {
        private int[][] numbers;
        private int xPosOf0;
        private int yPosOf0;
        public List<PuzzleState> listOfChildren;

        public PuzzleState(int[][] numbers)
        {
            this.numbers = numbers;

            //Pega a posição do 0 dentro do arrays
            for (int x = 0; x < numbers.GetLength(0); x++)
            {
                for (int y = 0; y < numbers.GetLength(1); y++)
                {
                    if (numbers[x][y] == 0)
                    {
                        xPosOf0 = x;
                        yPosOf0 = y;
                        break;
                    }
                }
            }
        }

        List<PuzzleState> generateChildren()
        {
            //Gera os filhos do puzzle e retorna
            if(xPosOf0 + 1 < numbers.GetLength(0))
            {
                //Cria um filho, modifica a posição do 0 e adiciona
                PuzzleState children = this;
                children.move0Position(xPosOf0 + 1, yPosOf0);
                children.xPosOf0 += 1;
                listOfChildren.Add(children);
            }
            if(xPosOf0 - 1 >= 0)
            {
                PuzzleState children = this;
                children.move0Position(xPosOf0 - 1, yPosOf0);
                children.xPosOf0 -= 1;
                listOfChildren.Add(children);
            }
            if (yPosOf0 + 1 < numbers.GetLength(0))
            {
                PuzzleState children = this;
                children.move0Position(xPosOf0, yPosOf0 + 1);
                children.yPosOf0 += 1;
                listOfChildren.Add(children);
            }
            if (yPosOf0 - 1 >= 0)
            {
                PuzzleState children = this;
                children.move0Position(xPosOf0, yPosOf0 - 1);
                children.yPosOf0 -= 1;
                listOfChildren.Add(children);
            }
            return listOfChildren;
        }

        void move0Position(int newXPos, int newYPos)
        {
            numbers[xPosOf0][yPosOf0] = numbers[newXPos][newYPos];
            numbers[newXPos][newYPos] = 0;
        }

        int[][] getNumbers()
        {
            return this.numbers;
        }

        public override bool Equals(object obj)
        {
            return obj is PuzzleState state &&
                   EqualityComparer<int[][]>.Default.Equals(numbers);
        }
    }
}
