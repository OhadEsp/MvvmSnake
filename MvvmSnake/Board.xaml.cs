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
                new PropertyMetadata(0));

        public Board()
        {
            InitializeComponent();
        }
    }
}
