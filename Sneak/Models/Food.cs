using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sneak.Models
{
    public class Food : GameObject
    {
        private Random random;

        public Food(int width, int height, List<Point> occupiedPositions)
            : base(0, 0) // Временная инициализация позиции, будет изменена в GenerateNewPosition
        {
            random = new Random();
            GenerateNewPosition(width, height, occupiedPositions);
        }

        public void GenerateNewPosition(int width, int height, List<Point> occupiedPositions)
        {
            var freeCells = new List<Point>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var point = new Point(x, y);
                    if (!occupiedPositions.Contains(point))
                    {
                        freeCells.Add(point);
                    }
                }
            }

            if (freeCells.Count == 0)
            {
                // Все клетки заняты, игра завершена
                return;
            }

            Position = freeCells[random.Next(freeCells.Count)];
        }
    }
}
