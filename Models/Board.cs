using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Board : ObservableCollection<Indexes>
    {

        public Board(int boardSize)
        {
            InitializeBoard(boardSize);
            InitializeSnake(boardSize);
        }

        private void InitializeBoard(int boardSize)
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    this.Add(new Indexes { Row = i, Column = j, Color = "White" });
                }
            }
        }

        private void InitializeSnake(int boardSize)
        {
            // Calculate initial snake positions based on board size
            int middleRow = boardSize / 2;

            for (int i = 1; i < boardSize - 1; i++)
            {
                this.Where(b => b.Row == middleRow && b.Column == i).FirstOrDefault().Color = "Green";
            }
        }

        public void MoveUp()
        {
            var snakeHead = this.Last();
            this.RemoveAt(0); // removes the tail
            this.Add(new Indexes { Row = snakeHead.Row - 1, Column = snakeHead.Column });
        }

        public void MoveRight()
        {
            var snakeHead = this.Last();
            this.RemoveAt(0); // removes the tail
            this.Add(new Indexes { Row = snakeHead.Row, Column = snakeHead.Column + 1 });
        }

        public void MoveDown()
        {
            var snakeHead = this.Last();
            this.RemoveAt(0); // removes the tail
            this.Add(new Indexes { Row = snakeHead.Row + 1, Column = snakeHead.Column });
        }

        public void MoveLeft()
        {
            var snakeHead = this.Last();
            this.RemoveAt(0); // removes the tail
            this.Add(new Indexes { Row = snakeHead.Row, Column = snakeHead.Column - 1 });
        }
    }

    public class Indexes
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public string Color { get; set; }
    }
}
