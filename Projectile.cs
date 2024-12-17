using System;
using Microsoft. Xna. Framework;
using Microsoft. Xna. Framework. Graphics;

namespace BasicMonoGame;

public class Projectile : GameObject
{
    public Projectile(Texture2D texture, Vector2 position, int size) : base(texture, position, size)
    {
        this.setSpeedY(4f);
    }
    
    public void Update(GameTime gameTime)
    {
        
        _position.Y -= (1+_speed.Y);
    }
}