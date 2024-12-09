using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Board : ObservableCollection<Indexes>
    {
        private IList<Indexes> _snake;

        public IList<Indexes> SnakeIndexes { get => _snake; set => _snake = value; }

        public int SnakeLength { get; set; }

        public Board(int boardSize)
        {
            _snake = new List<Indexes>();
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
            SnakeLength = 0;
            int middleRow = boardSize / 2;

            for (int i = 1; i < boardSize - 1; i++)
            {
                this.Where(b => b.Row == middleRow && b.Column == i).Single().Color = "Green";
                SnakeLength++;
                _snake.Add(new Indexes { Row = middleRow, Column = i, Color = "Green"});
            }
        }

        public void MoveUp()
        {
            var snakeHead = _snake[SnakeLength-1];
            this.Where(b => b.Row == _snake[0].Row && b.Column == _snake[0].Column).Single().Color = "White";
            _snake.RemoveAt(0); // removes the tail
            this.Where(b => b.Row == snakeHead.Row - 1 && b.Column == snakeHead.Column).Single().Color = "Green";
            _snake.Add(new Indexes { Row = snakeHead.Row - 1, Column = snakeHead.Column, Color = "Green" });
        }

        public void MoveRight()
        {
            var snakeHead = _snake[SnakeLength - 1];
            this.Where(b => b.Row == _snake[0].Row && b.Column == _snake[0].Column).Single().Color = "White";
            _snake.RemoveAt(0); // removes the tail
            this.Where(b => b.Row == snakeHead.Row && b.Column == snakeHead.Column + 1).Single().Color = "Green";
            _snake.Add(new Indexes { Row = snakeHead.Row, Column = snakeHead.Column + 1, Color = "Green" });
        }

        public void MoveDown()
        {
            var snakeHead = _snake[SnakeLength - 1];
            this.Where(b => b.Row == _snake[0].Row && b.Column == _snake[0].Column).Single().Color = "White";
            _snake.RemoveAt(0); // removes the tail
            this.Where(b => b.Row == snakeHead.Row + 1 && b.Column == snakeHead.Column).Single().Color = "Green";
            _snake.Add(new Indexes { Row = snakeHead.Row + 1, Column = snakeHead.Column, Color = "Green" });
        }

        public void MoveLeft()
        {
            var snakeHead = _snake[SnakeLength - 1];
            this.Where(b => b.Row == _snake[0].Row && b.Column == _snake[0].Column).Single().Color = "White";
            _snake.RemoveAt(0); // removes the tail
            this.Where(b => b.Row == snakeHead.Row && b.Column == snakeHead.Column - 1).Single().Color = "Green";
            _snake.Add(new Indexes { Row = snakeHead.Row, Column = snakeHead.Column - 1, Color = "Green" });
            
        }
    }

    public class Indexes : INotifyPropertyChanged
    {
        private int _row;
        private int _column;
        private string _color;

        public int Row
        {
            get { return _row; }
            set
            {
                _row = value;
                OnPropertyChanged("Row");
            }
        }
        public int Column
        {
            get { return _column; }
            set
            {
                _column = value;
                OnPropertyChanged("Column");
            }
        }
        public string Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
