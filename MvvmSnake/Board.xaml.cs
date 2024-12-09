using MvvmSnake.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MvvmSnake.Views
{
    public partial class Board : UserControl
    {
        public int BoardSize
        {
            get { return (int)GetValue(BoardSizeProperty); }
            set { SetValue(BoardSizeProperty, value); }
        }

        public static readonly DependencyProperty BoardSizeProperty =
            DependencyProperty.Register(
                "BoardSize",
                typeof(int),
                typeof(Board),
                new PropertyMetadata(0, OnBoardSizePropertyChanged));

        public Board()
        {
            InitializeComponent();
            DrawBoard();
        }

        private static void OnBoardSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var board = (Board)d;
            board.DrawBoard();
        }

        private void DrawBoard()
        {
            boardGrid.ColumnDefinitions.Clear();
            boardGrid.RowDefinitions.Clear();

            for (int i = 0; i < BoardSize; i++)
            {
                var column = new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                };
                boardGrid.ColumnDefinitions.Add(column);

                var row = new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star)
                };
                boardGrid.RowDefinitions.Add(row);
            }
        }
    }
}
