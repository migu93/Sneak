using System.Collections.Generic;
using System.Drawing;

namespace Sneak.Models
{
    public class Snake : GameObject
    {
        public List<Point> Body { get; private set; }
        private Direction direction;
        private Direction growlink;
        private Direction nextDirection;

        public Snake(int gridWidth, int gridHeight)
            : base(gridWidth / 2, gridHeight / 2) // Начальная позиция головы змейки
        {
            int startX = gridWidth / 2;
            int startY = gridHeight / 2;

            Body = new List<Point>
            {
                new Point(startX, startY),
                new Point(startX - 1, startY),
                new Point(startX - 2, startY)
            };
            direction = Direction.Right;
            nextDirection = Direction.Right;
        }

        public void Move()
        {
            direction = nextDirection;

            Point newHead = new Point(Position.X, Position.Y);

            switch (direction)
            {
                case Direction.Up:
                    newHead.Y -= 1;
                    break;
                case Direction.Down:
                    newHead.Y += 1;
                    break;
                case Direction.Left:
                    newHead.X -= 1;
                    break;
                case Direction.Right:
                    newHead.X += 1;
                    break;
            }

            Body.Insert(0, newHead);
            Body.RemoveAt(Body.Count - 1);

            Position = newHead; // Обновляем позицию головы
        }

        public void Grow()
        {
            // Добавляем копию последнего сегмента
            Body.Add(Body[Body.Count - 1]);
        }

        public void ChangeDirection(Direction newDirection)
        {
            // Предотвращаем обратное направление
            if ((direction == Direction.Left && newDirection != Direction.Right) ||
                (direction == Direction.Right && newDirection != Direction.Left) ||
                (direction == Direction.Up && newDirection != Direction.Down) ||
                (direction == Direction.Down && newDirection != Direction.Up))
            {
                nextDirection = newDirection;
            }
        }
    }
}
