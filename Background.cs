using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BasicMonoGame;

public class Background :GameObject
{
    public Background(Texture2D texture, Vector2 position, int size) : base(texture,position,size)
    {
        this.setSpeedY(0.004f);
    }

    public void Update(GameTime gameTime)
    {
        _position.Y += 0.8f+_speed.Y;
    }

    public void Draw(GameTime gameTime)
    {
        base.Draw(Global._spriteBatch);
    }
}