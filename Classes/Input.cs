using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace Synergym.Classes
{
    public struct Input
    {
        public static TouchCollection Touches;

        public static void Update()
        {
            Touches = TouchPanel.GetState();
        }

        public static bool Touched(Vector2 Position, float width, float height, float scale)
        {
            var d = Touches[0].Position;
            return (d.X > Position.X && d.X < Position.X + width * scale && d.Y > Position.Y && d.Y < Position.Y + height * scale);
        }
    }
}
