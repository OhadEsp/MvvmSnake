using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Snake : ObservableCollection<Indexes>
    {

        public Snake(ObservableCollection<Indexes> indexes) : base(indexes as IEnumerable<Indexes>)
        {
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
    }
}
