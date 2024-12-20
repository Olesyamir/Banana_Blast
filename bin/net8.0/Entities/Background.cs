using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BasicMonoGame.Entities;

public class Background :GameObject
{
    public Background(Texture2D texture, Vector2 position, Vector2 dim) : base(texture,position,dim)
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