using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sneak.Models
{
    /// <summary>
    /// Класс, представляющий еду на игровом поле.
    /// </summary>
    public class Food : GameObject
    {
        private Random _random;

        /// <summary>
        /// Конструктор класса Food.
        /// </summary>
        /// <param name="width">Ширина игрового поля.</param>
        /// <param name="height">Высота игрового поля.</param>
        /// <param name="occupiedPositions">Список занятых позиций на поле.</param>
        public Food(int width, int height, List<Point> occupiedPositions)
            : base(0, 0) // Инициализируем временную позицию, которая будет изменена
        {
            _random = new Random();
            GenerateNewPosition(width, height, occupiedPositions);
        }

        /// <summary>
        /// Генерирует новую позицию для еды, избегая занятых позиций.
        /// </summary>
        /// <param name="width">Ширина игрового поля.</param>
        /// <param name="height">Высота игрового поля.</param>
        /// <param name="occupiedPositions">Список занятых позиций.</param>
        public void GenerateNewPosition(int width, int height, List<Point> occupiedPositions)
        {
            // Создаем список свободных клеток
            List<Point> freeCells = GetFreeCells(width, height, occupiedPositions);

            if (freeCells.Count == 0)
            {
                // Если нет свободных клеток, еда не может быть размещена
                return;
            }

            // Выбираем случайную свободную позицию
            Position = freeCells[_random.Next(freeCells.Count)];
        }

        /// <summary>
        /// Получает список свободных клеток на игровом поле.
        /// </summary>
        /// <param name="width">Ширина игрового поля.</param>
        /// <param name="height">Высота игрового поля.</param>
        /// <param name="occupiedPositions">Список занятых позиций.</param>
        /// <returns>Список свободных позиций.</returns>
        private List<Point> GetFreeCells(int width, int height, List<Point> occupiedPositions)
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

            return freeCells;
        }
    }
}
