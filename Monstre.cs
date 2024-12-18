using System;
using System.Xml.Serialization;
using Microsoft. Xna. Framework;
using Microsoft. Xna. Framework. Graphics;
namespace BasicMonoGame;

[XmlRoot("monstre",Namespace="http://www.univ-grenoble-alpes.fr/jeu_monstres")][Serializable]
public class Monstre : GameObject
{
    [XmlIgnore]
    private int _health;
    
    [XmlIgnore]
    private readonly int _damage;
    
    [XmlElement("health")]
    public int _Health{get=>_health; set=>_health=value; }
    
    [XmlIgnore]
    public Animation _animation;
    
    [XmlIgnore]
    private TypeMonstre _typemonstre;
    
    [XmlElement("position")]
    public Vector2 _jposition { get=>_position; set => _position = value; }
    
    [XmlElement("type")]
    public TypeMonstre _Typemonstre{ get => _typemonstre; set=>_typemonstre=value; }

    public Monstre() : base(null, Vector2.Zero, 0)
    {
        _Size = 100;
        _damage = 100;
        _texture=Global._Content.Load<Texture2D>("enemy2");
        _animation = new Animation(_texture,9,5,0.1f,true);
        setSpeedY(0.5f);
        setSpeedX(0.5f);
    }
    
    public Monstre(TypeMonstre monstre, Texture2D texture, Vector2 position, int size) : base(texture, position, size)
    {
        _typemonstre = monstre;
        if (_typemonstre == TypeMonstre.Petit)
        {
            _animation = new Animation(texture,9,5,0.1f,true);
            _health = 50;
            _damage = 100;
            setSpeedY(0.5f);
            setSpeedX(0.5f);
        }
        if (_typemonstre == TypeMonstre.Bigboss)
        {
            _health = 100;
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
        return _health;
    }
    public void MonsterGotHit(int damage)
    {
        _health -= damage;
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