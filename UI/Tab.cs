using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Synergy_HW.UI
{
    internal class Tab
    {
        Rectangle Source;
        Vector2 Position;
        float Scale;

        public Tab()
        {
            
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Game1.SpriteSheet, Position, Source, Color.White, 0, Vector2.Zero, Scale, 0, 0);
        }
    }
}
