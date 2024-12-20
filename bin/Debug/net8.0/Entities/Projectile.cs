using System;
using System.Xml.Serialization;
using Microsoft. Xna. Framework;
using Microsoft. Xna. Framework. Graphics;
namespace BasicMonoGame.Entities;

[Serializable][XmlRoot("projectile",Namespace = "http://www.univ-grenoble-alpes.fr/jeu_monstres")]
public class Projectile : GameObject
{
    [XmlElement("position")]
    public Vector2 _mposition { get => _position;set=>_position=value; }
    public Projectile() : base(null, Vector2.Zero, new Vector2(25,34))
    {
        _Size = 50;
        setPos(_mposition.X, _mposition.Y);
        _texture = Global._game.Content.Load<Texture2D>("missile1");
        
        setSpeedY(4f);
        
    }
    public Projectile(Texture2D texture, Vector2 position, Vector2 dim) : base(texture, position,dim )
    {
        setSpeedY(4f);
    }
    
    public void Update(GameTime gameTime)
    {
        _position.Y -= (1+_speed.Y);
    }
}