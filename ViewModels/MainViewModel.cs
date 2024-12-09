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
    public class MainViewModel : INotifyPropertyChanged
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
                    OnPropertyChanged(nameof(BoardSize));
                    // Ensure any other initialization or logic related to BoardSize is handled here
                }
            }
        }

        private Snake _snake;

        public Snake Snake
        {
            get { return _snake; }
            set
            {
                if (_snake != value)
                {
                    _snake = value;
                    OnPropertyChanged(nameof(Snake));
                }
            }
        }

        public MainViewModel(int boardSize)
        {
            BoardSize = boardSize;
            InitializeSnake(boardSize);
            InitializeCommands();
        }

        public ICommand MoveUpCommand { get; private set; }
        public ICommand MoveRightCommand { get; private set; }
        public ICommand MoveDownCommand { get; private set; }
        public ICommand MoveLeftCommand { get; private set; }

        private void InitializeSnake(int boardSize)
        {
            // Calculate initial snake positions based on board size
            var initialIndexes = new ObservableCollection<Indexes>();
            int middleRow = boardSize / 2;

            for (int i = 1; i < boardSize - 1; i++)
            {
                initialIndexes.Add(new Indexes { Row = middleRow, Column = i });
            }

            Snake = new Snake(initialIndexes);
        }

        private void InitializeCommands()
        {
            MoveUpCommand = new RelayCommand(() => {
                Snake.MoveUp();
                OnPropertyChanged(nameof(Snake));
                }, () => CanMoveUp());
            MoveRightCommand = new RelayCommand(() => {
                Snake.MoveRight();
                OnPropertyChanged(nameof(Snake));
                }, () => CanMoveRight());
            MoveDownCommand = new RelayCommand(() => {
                Snake.MoveDown();
                OnPropertyChanged(nameof(Snake));
                }, () => CanMoveDown());
            MoveLeftCommand = new RelayCommand(() => {
                Snake.MoveLeft();
                OnPropertyChanged(nameof(Snake));
                }, () => CanMoveLeft());
        }

        private bool CanMoveUp()
        {
            if (Snake.Count <= 1)
                return true; // Snake can always move if it's just one segment

            var head = Snake.Last();
            var beforeHead = Snake[Snake.Count - 2];

            // Snake can't move in reverse (downwards)
            if (head.Row < beforeHead.Row && head.Column == beforeHead.Column)
                return false;

            // Snake can't cross the board borders
            if (head.Row - 1 < 0)
                return false;

            return true;
        }

        private bool CanMoveRight()
        {
            if (Snake.Count <= 1)
                return true; // Snake can always move if it's just one segment

            var head = Snake.Last();
            var beforeHead = Snake[Snake.Count - 2];

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
            if (Snake.Count <= 1)
                return true; // Snake can always move if it's just one segment

            var head = Snake.Last();
            var beforeHead = Snake[Snake.Count - 2];

            // Snake can't move in reverse (upwards)
            if (head.Row > beforeHead.Row && head.Column == beforeHead.Column)
                return false;

            // Snake can't cross the board borders
            if (head.Row + 1 >= _boardSize)
                return false;

            return true;
        }

        private bool CanMoveLeft()
        {
            if (Snake.Count <= 1)
                return true; // Snake can always move if it's just one segment

            var head = Snake.Last();
            var tail = Snake[Snake.Count - 2];

            // Snake can't move in reverse (rightwards)
            if (head.Column > tail.Column && head.Row == tail.Row)
                return false;

            // Snake can't cross the board borders
            if (head.Column - 1 < 0)
                return false;

            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
