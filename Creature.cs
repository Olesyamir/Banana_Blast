using System;
using Microsoft. Xna. Framework;
using Microsoft. Xna. Framework. Graphics;
namespace BasicMonoGame;

public class Creature : GameObject
{
    public int _Health { get; set; }
    
    public Creature(TypeCreature creature, Texture2D texture, Vector2 position, int size) : base(texture, position, size)
    {
        if (creature == TypeCreature.Petit)
        {
            _Health = 50;
            this.setSpeedX(0.02f);
        }
        if (creature == TypeCreature.Bigboss)
        {
            _Health = 100;
            this.setSpeedX(0.05f);
        }
    }

    public void Update(GameTime gameTime)
    {
        _position.Y += 1+_speed.Y;
    }
}