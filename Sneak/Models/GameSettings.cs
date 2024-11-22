using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneak.Models
{
    public class GameSettings
    {
        public int CellSize { get; set; } = 20;
        public int GridWidth { get; set; } = 30;
        public int GridHeight { get; set; } = 20;
        public int SnakeSpeed { get; set; } = 100; // В миллисекундах
        public int Margin { get; set; } = 50;

        public GameSettings() { }

        public GameSettings(int cellSize, int gridWidth, int gridHeight, int snakeSpeed, int margin)
        {
            CellSize = cellSize;
            GridWidth = gridWidth;
            GridHeight = gridHeight;
            SnakeSpeed = snakeSpeed;
            Margin = margin;
        }
    }
}
