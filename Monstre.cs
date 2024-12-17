using System;
using System.Xml.Serialization;
using Microsoft. Xna. Framework;
using Microsoft. Xna. Framework. Graphics;
namespace BasicMonoGame;

[Serializable][XmlRoot("monstre",Namespace="http://www.univ-grenoble-alpes.fr/jeu_monstres")]
public class Monstre : GameObject
{ 
    [XmlElement("health")]
    private int _Health{get; set; }
    
    [XmlIgnore]
    private readonly int _damage;
    
    [XmlIgnore]
    public Animation _animation;
    
    [XmlElement("typemonstre")]
    private TypeMonstre _typemonstre{ get; set; }
    public Monstre(TypeMonstre monstre, Texture2D texture, Vector2 position, int size) : base(texture, position, size)
    {
        _typemonstre = monstre;
        if (_typemonstre == TypeMonstre.Petit)
        {
            _animation = new Animation(texture,9,5,0.1f,true);
            _Health = 50;
            _damage = 100;
            setSpeedY(0.5f);
            setSpeedX(0.5f);
        }
        if (_typemonstre == TypeMonstre.Bigboss)
        {
            _Health = 100;
            _damage = 35;
            setSpeedX(0.02f);
            setSpeedY(0.05f);
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
        if (_typemonstre == TypeMonstre.Petit)
        {
            _position.Y += 1 + _speed.Y;
        }
        else if (_typemonstre == TypeMonstre.Bigboss)
        {
            _position.X = 1 + _speed.X;
            if (780 - _position.X == 5)
            {
                _position.X = 1 - _speed.X;
                _position.Y = 1 + _speed.Y;
            }
            else if (_position.X == 5)
            {
                _position.X = 1 + _speed.X;
                _position.Y = 2 + _speed.Y;
            }
        }

        _animation.Update(gameTime);
    }
    
    public void Draw()
    {
        _animation.Draw(getPos());
    }


}