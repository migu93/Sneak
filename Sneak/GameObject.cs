using System.Drawing;

namespace Sneak.Models
{
    public abstract class GameObject
    {
        public Point Position { get; set; }

        public GameObject(int x, int y)
        {
            Position = new Point(x, y);
        }

        public GameObject(Point position)
        {
            Position = position;
        }
    }
}
