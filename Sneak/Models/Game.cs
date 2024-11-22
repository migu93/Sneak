using System;
using System.Collections.Generic;
using System.Drawing;

namespace Sneak.Models
{
    public class Game
    {
        public Snake Snake { get; private set; }
        public Food Food { get; private set; }
        public List<Wall> Walls { get; private set; }
        public List<Obstacle> Obstacles { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool GameOver { get; private set; }
        public DateTime StartTime { get; private set; }
        public GameSettings Settings { get; private set; }
        public ScoreManager ScoreManager { get; private set; }
        public SoundManager SoundManager { get; private set; }

        public Game(GameSettings settings)
        {
            Settings = settings;
            Width = settings.GridWidth;
            Height = settings.GridHeight;
            Snake = new Snake(Width, Height);
            Walls = new List<Wall>(); // Инициализация списка стен
            Obstacles = new List<Obstacle>(); // Инициализация списка препятствий
            GameOver = false;
            StartTime = DateTime.Now;
            ScoreManager = new ScoreManager();
            SoundManager = new SoundManager();

            InitializeWalls();
            InitializeObstacles();

            // Инициализация еды после инициализации стен и препятствий
            Food = new Food(Width, Height, GetOccupiedPositions());
        }

        private void InitializeWalls()
        {
            // Добавляем стены по периметру игрового поля
            for (int x = 0; x < Width; x++)
            {
                Walls.Add(new Wall(x, 0));
                Walls.Add(new Wall(x, Height - 1));
            }
            for (int y = 0; y < Height; y++)
            {
                Walls.Add(new Wall(0, y));
                Walls.Add(new Wall(Width - 1, y));
            }
        }

        private void InitializeObstacles()
        {
            // Добавляем несколько препятствий в произвольных местах
            Obstacles.Add(new Obstacle(10, 10));
            Obstacles.Add(new Obstacle(15, 15));
            Obstacles.Add(new Obstacle(20, 5));
        }

        public void Update()
        {
            if (!GameOver)
            {
                Snake.Move();

                // Проверка на столкновение со стенами
                foreach (var wall in Walls)
                {
                    if (Snake.Position.Equals(wall.Position))
                    {
                        GameOver = true;
                        SoundManager.PlayGameOverSound();
                        break;
                    }
                }

                // Проверка на столкновение с препятствиями
                foreach (var obstacle in Obstacles)
                {
                    if (Snake.Position.Equals(obstacle.Position))
                    {
                        GameOver = true;
                        SoundManager.PlayGameOverSound();
                        break;
                    }
                }

                // Проверка на столкновение с самим собой
                for (int i = 1; i < Snake.Body.Count; i++)
                {
                    if (Snake.Position.Equals(Snake.Body[i]))
                    {
                        GameOver = true;
                        SoundManager.PlayGameOverSound();
                        break;
                    }
                }

                // Проверка на поедание еды
                if (Snake.Position.Equals(Food.Position))
                {
                    Snake.Grow();
                    ScoreManager.AddScore(10);
                    SoundManager.PlayEatSound();
                    Food.GenerateNewPosition(Width, Height, GetOccupiedPositions());
                }
            }
        }

        private List<Point> GetOccupiedPositions()
        {
            List<Point> occupied = new List<Point>(Snake.Body);

            foreach (var wall in Walls)
            {
                occupied.Add(wall.Position);
            }

            foreach (var obstacle in Obstacles)
            {
                occupied.Add(obstacle.Position);
            }

            return occupied;
        }

        public void ChangeDirection(Direction direction)
        {
            Snake.ChangeDirection(direction);
        }
    }
}
