using System.Collections.Generic;
using Microsoft. Xna. Framework;
using Microsoft. Xna. Framework. Graphics;
using Microsoft. Xna. Framework. Input ;
using Microsoft.Xna.Framework.Graphics;
namespace BasicMonoGame;

public class Player : GameObject
{
    private static float _shootTime;
    private static float _shootCooldown = 0.15f;
    
    private int sizeMax = 100;
    public Player(Texture2D texture, Vector2 position, int size) : base(texture, position, size)
    {
        if (size > sizeMax)
        {
            size = sizeMax;
        }
    }

    public void Update(GameTime gameTime,List<Projectile> bullets,Texture2D projectileTexture)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Right))
        {
            _speed.X += 0.5f;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.Left))
        {
            _speed.X -= 0.5f;
        }

        if (Keyboard.GetState().IsKeyDown(Keys.X))
        {
            _shootTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_shootTime<=0 && bullets.Count<15)
            {
                bullets.Add(new Projectile(projectileTexture, this.getPos(), 20));
                _shootTime = _shootCooldown;
            }
        }
        

        _position.X = _position.X + _speed.X;
        _position.Y = _position.Y + _speed.Y;
        if (_speed.X > 0) _speed.X -= 0.05f;
        if (_speed.X < 0) _speed.X += 0.1f;
        if (_speed.Y > 0) _speed.Y -= 0.1f;
        if (_speed.Y < 0) _speed.Y += 0.1f;
        

    }
    

}