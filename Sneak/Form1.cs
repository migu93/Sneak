using Sneak.Models;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Sneak
{
    public partial class Form1 : Form
    {
        private Game game;
        private Timer gameTimer = new Timer();
        private Timer renderTimer = new Timer();
        private GameSettings settings = new GameSettings(); // Используем настройки

        public Form1()
        {
            InitializeComponent();

            // Установка свойств формы
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.ClientSize = new Size(settings.GridWidth * settings.CellSize + settings.Margin * 2, settings.GridHeight * settings.CellSize + settings.Margin * 2);
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;

            // Инициализация игры
            game = new Game(settings);

            // Подписка на события клавиатуры
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            // Инициализация таймеров
            this.gameTimer.Interval = settings.SnakeSpeed;
            this.gameTimer.Tick += new EventHandler(gameTimer_Tick);
            this.gameTimer.Start();

            this.renderTimer.Interval = 16; // ~60 FPS
            this.renderTimer.Tick += new EventHandler(renderTimer_Tick);
            this.renderTimer.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    game.ChangeDirection(Direction.Left);
                    break;
                case Keys.Right:
                    game.ChangeDirection(Direction.Right);
                    break;
                case Keys.Up:
                    game.ChangeDirection(Direction.Up);
                    break;
                case Keys.Down:
                    game.ChangeDirection(Direction.Down);
                    break;
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            this.Focus();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            game.Update();
            if (game.GameOver)
            {
                gameTimer.Stop();
                renderTimer.Stop();
                TimeSpan survivalTime = DateTime.Now - game.StartTime;
            }
        }

        private void renderTimer_Tick(object sender, EventArgs e)
        {
            Invalidate(); // Перерисовываем форму
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            int margin = settings.Margin;
            int cellSize = settings.CellSize;

            // Рисуем белый фон для игрового поля с учетом отступов
            g.FillRectangle(Brushes.White, margin, margin, settings.GridWidth * cellSize, settings.GridHeight * cellSize);

            // Рисуем линии сетки
            Pen gridPen = new Pen(Color.LightGray);
            for (int x = 0; x <= settings.GridWidth; x++)
            {
                g.DrawLine(gridPen, margin + x * cellSize, margin, margin + x * cellSize, margin + settings.GridHeight * cellSize);
            }
            for (int y = 0; y <= settings.GridHeight; y++)
            {
                g.DrawLine(gridPen, margin, margin + y * cellSize, margin + settings.GridWidth * cellSize, margin + y * cellSize);
            }

            // Рисуем стены
            foreach (var wall in game.Walls)
            {
                g.FillRectangle(Brushes.Gray, new Rectangle(margin + wall.Position.X * cellSize, margin + wall.Position.Y * cellSize, cellSize, cellSize));
            }

            // Рисуем препятствия
            foreach (var obstacle in game.Obstacles)
            {
                g.FillRectangle(Brushes.Brown, new Rectangle(margin + obstacle.Position.X * cellSize, margin + obstacle.Position.Y * cellSize, cellSize, cellSize));
            }

            // Рисуем змейку
            foreach (Point point in game.Snake.Body)
            {
                g.FillRectangle(Brushes.Green, new Rectangle(margin + point.X * cellSize, margin + point.Y * cellSize, cellSize, cellSize));
            }

            // Рисуем еду
            g.FillRectangle(Brushes.Red, new Rectangle(margin + game.Food.Position.X * cellSize, margin + game.Food.Position.Y * cellSize, cellSize, cellSize));

            // Отображаем счет и время
            g.DrawString($"Счет: {game.ScoreManager.CurrentScore}", new Font("Arial", 16), Brushes.White, new PointF(5, 5));
            g.DrawString($"Лучший: {game.ScoreManager.HighScore}", new Font("Arial", 16), Brushes.White, new PointF(5, 25));

            TimeSpan survivalTime = DateTime.Now - game.StartTime;
            g.DrawString($"Время: {survivalTime:mm\\:ss}", new Font("Arial", 16), Brushes.White, new PointF(5, 45));
        }
    }
}
