using System;
using Microsoft. Xna. Framework;
using Microsoft. Xna. Framework. Graphics;
namespace BasicMonoGame;

public class Creature : GameObject
{

    private int _Health;
    private int _damage;
    
    public Creature(TypeCreature creature, Texture2D texture, Vector2 position, int size) : base(texture, position, size)
    {
        if (creature == TypeCreature.Petit)
        {
            _Health = 50;
            _damage = 100;
            this.setSpeedY(0.02f);
        }
        if (creature == TypeCreature.Bigboss)
        {
            _Health = 100;
            _damage = 35;
            this.setSpeedY(0.05f);
        }
    }

    public int getDamage()//retourne les dommages causés par la créature
    {
        return _damage;
    }

    public int getHealth()
    {
        return _Health;
    }
    public void MonsterGotHit(int damage)
    {
        _Health -= damage;
    }

    public void Update(GameTime gameTime)
    {
        _position.Y += 1+_speed.Y;
    }
    
    public void Draw()
    {
        base.Draw(Global._spriteBatch);
    }
}