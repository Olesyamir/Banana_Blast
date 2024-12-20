using BananaBlast.Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BananaBlast.Entities;

public class Explosion:GameObject
{
    public ExplosionAnimation _animation;
    private Texture2D texture;
    public Explosion(Texture2D texture,Vector2 position, Vector2 dim):base(texture,position,dim)
    {
        _animation = new ExplosionAnimation(texture,6,0.04f);
        setPos(position.X,position.Y);
    }

    public void Update(GameTime gameTime)
    {
        _animation.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _animation.Draw(getPos());
    }
}