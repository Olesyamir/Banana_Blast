using System;
using Microsoft. Xna. Framework;
using Microsoft. Xna. Framework. Graphics;
namespace jeu_monstre.Virus;

public class Creature : GameObject
{

    public int _Health { get; set; }
    
    public Creature(TypeCreature creature, Texture2D texture, Vector2 position, int size) : base(texture, position, size)
    {
        if (creature == TypeCreature.Petit)
        {
            _Health = 50;
            this.setSpeedX(0.03f);
        }
        if (creature == TypeCreature.Bigboss)
        {
            _Health = 100;
            this.setSpeedX(0.02f);
        }
        if (creature == TypeCreature.Moyen)
        {
            _Health = 65;
            this.setSpeedX(0.025f);
        }

        if (creature == TypeCreature.Boss)
        {
            _Health = 80;
            this.setSpeedX(0.02f);
            
        }
    }

    // faire bouger les virus //
    public void Update(GameTime gameTime)   
    {
        _position.Y += 1+_speed.Y;
    }
    
    public void Draw()
    {
        base.Draw(Global._spriteBatch);
    }
}