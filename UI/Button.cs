using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Synergy_HW.UI
{
    internal class Button
    {
        Rectangle Source;
        Vector2 Position;
        float Scale = 1;
        public string SetText { set { Text = value; origin = Game1.Font.MeasureString(value) * .5f; } }
        Vector2 origin;
        string Text = "";
        bool Opened;

        public Button()
        {
            
        }
        public Button(Vector2 position)
        {
            Position = position;
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Game1.SpriteSheet, Position, Source, Color.White, 0 , Vector2.Zero, Scale, 0, 0);
            if (Opened )
            {

            }
        }
    }
}
