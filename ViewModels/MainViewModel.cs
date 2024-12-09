using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmSnake.ViewModels
{
    public class MainViewModel
    {
        private int _boardSize;
        public int BoardSize
        {
            get { return _boardSize; }
            set
            {
                if (_boardSize != value)
                {
                    _boardSize = value;
                }
            }
        }

        private Board _snake;

        public Board Snake
        {
            get { return _snake; }
            set
            {
                if (_snake != value)
                {
                    _snake = value;
                }
            }
        }

        public MainViewModel(int boardSize)
        {
            BoardSize = boardSize;
            Snake = new Board(boardSize);
            InitializeCommands();
        }

        public ICommand MoveUpCommand { get; private set; }
        public ICommand MoveRightCommand { get; private set; }
        public ICommand MoveDownCommand { get; private set; }
        public ICommand MoveLeftCommand { get; private set; }

        private void InitializeCommands()
        {
            MoveUpCommand = new RelayCommand(() => {
                Snake.MoveUp();
                }, () => CanMoveUp());
            MoveRightCommand = new RelayCommand(() => {
                Snake.MoveRight();
                }, () => CanMoveRight());
            MoveDownCommand = new RelayCommand(() => {
                Snake.MoveDown();
                }, () => CanMoveDown());
            MoveLeftCommand = new RelayCommand(() => {
                Snake.MoveLeft();
                }, () => CanMoveLeft());
        }

        private bool CanMoveUp()
        {
            if (Snake.SnakeLength <= 1)
                return true; // Snake can always move if it's just one segment

            var head = Snake.SnakeIndexes[Snake.SnakeLength - 1];
            var beforeHead = Snake.SnakeIndexes[Snake.SnakeLength - 2];

            // Snake can't move up if above the head there is a green block.
            var above = Snake.SnakeIndexes.FirstOrDefault(b => b.Row == head.Row - 1 && b.Column == head.Column);
            if (above != null && above.Color == "Green")
                return false;

            // Snake can't move in reverse (downwards)
            if (head.Row > beforeHead.Row && head.Column == beforeHead.Column)
                return false;

            // Snake can't cross the board borders
            if (head.Row - 1 < 0)
                return false;

            return true;
        }

        private bool CanMoveRight()
        {
            if (Snake.SnakeLength <= 1)
                return true; // Snake can always move if it's just one segment

            var head = Snake.SnakeIndexes[Snake.SnakeLength - 1];
            var beforeHead = Snake.SnakeIndexes[Snake.SnakeLength - 2];

            // Snake can't move right if rightwards the head there is a green block.
            var rightwards = Snake.SnakeIndexes.FirstOrDefault(b => b.Row == head.Row && b.Column == head.Column + 1);
            if (rightwards != null && rightwards.Color == "Green")
                return false;

            // Snake can't move in reverse (leftwards)
            if (head.Column < beforeHead.Column && head.Row == beforeHead.Row)
                return false;

            // Snake can't cross the board borders
            if (head.Column + 1 >= _boardSize)
                return false;

            return true;
        }

        private bool CanMoveDown()
        {
            if (Snake.SnakeLength <= 1)
                return true; // Snake can always move if it's just one segment

            var head = Snake.SnakeIndexes[Snake.SnakeLength - 1];
            var beforeHead = Snake.SnakeIndexes[Snake.SnakeLength - 2];

            // Snake can't move down if below the head there is a green block.
            var below = Snake.SnakeIndexes.FirstOrDefault(b => b.Row == head.Row + 1 && b.Column == head.Column);
            if (below != null && below.Color == "Green")
                return false;

            // Snake can't move in reverse (upwards)
            if (head.Row < beforeHead.Row && head.Column == beforeHead.Column)
                return false;

            // Snake can't cross the board borders
            if (head.Row + 1 >= _boardSize)
                return false;

            return true;
        }

        private bool CanMoveLeft()
        {
            if (Snake.SnakeLength <= 1)
                return true; // Snake can always move if it's just one segment

            var head = Snake.SnakeIndexes[Snake.SnakeLength - 1];
            var beforeHead = Snake.SnakeIndexes[Snake.SnakeLength - 2];

            // Snake can't move left if leftwards the head there is a green block.
            var leftwards = Snake.SnakeIndexes.FirstOrDefault(b => b.Row == head.Row && b.Column == head.Column - 1);
            if (leftwards != null && leftwards.Color == "Green")
                return false;

            // Snake can't move in reverse (rightwards)
            if (head.Column > beforeHead.Column && head.Row == beforeHead.Row)
                return false;

            // Snake can't cross the board borders
            if (head.Column - 1 < 0)
                return false;

            return true;
        }
    }
}
