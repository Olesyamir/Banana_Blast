using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft. Xna. Framework;
using Microsoft. Xna. Framework. Graphics;
using BasicMonoGame.Animations;
namespace BasicMonoGame.Entities;




[XmlRoot("monstre",Namespace="http://www.univ-grenoble-alpes.fr/jeu_monstres")][Serializable]
public class Monstre : GameObject
{
    [XmlIgnore]
    private int _health;
    
    [XmlIgnore]
    private int _damage;
    
    [XmlIgnore]
    public Animation _animation;
    
    [XmlIgnore]
    private TypeMonstre _typemonstre;
    
    [XmlIgnore]
    public List<MoveMethod> _MoveMethods;
    
    [XmlElement("health")]
    public int _Health{get=>_health; set=>_health=value; }

    
    [XmlElement("position")]
    public Vector2 _jposition { get=>_position; set => _position = value; }
    
    [XmlElement("type")]
    public TypeMonstre _Typemonstre{ get => _typemonstre; set=>_typemonstre=value; }

    public delegate Vector2 MoveMethod(Vector2 position,float move,Vector2 speed);
    
    public Monstre() : base(null, Vector2.Zero, Vector2.Zero)
    {
         _MoveMethods = [Moves.Down, Moves.ZigZag, Moves.ZigZagReverse,Moves.Right];
        setSpeedY(0.5f);
        setSpeedX(0.5f);
    }
    
    public Monstre(TypeMonstre monstre, Texture2D texture, Vector2 position, Vector2 dim) : base(texture, position, dim)
    {

        _typemonstre = monstre;
        if (_typemonstre == TypeMonstre.Petit)
        {
            _texture = texture;
            _animation = new Animation(texture,9,5,0.1f,true);
            _health = 50;
            _damage = 50;
            setSpeedY(0.5f);
            setSpeedX(0.5f);
        }
        if (_typemonstre == TypeMonstre.Bigboss)
        {
            _texture = texture;
            _animation = new Animation(texture,4,4,0.1f,true);
            _health = 100;
            _damage = 100;
            setSpeedX(2.5f);
            setSpeedY(0.1f);
            _MoveMethods = [Moves.Down,Moves.ZigZag,Moves.ZigZagReverse];
        }
    }

    public MoveMethod? GetMove(int index)
    {
        if (index >= 0 && index < _MoveMethods.Count)
        {
            return _MoveMethods[index];
        }
        return null;
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
    
    public bool LimiterPositionGauche(int largeurEcran, int hauteurEcran)
    {
        var nouveauX = Math.Clamp(_position.X, 0, largeurEcran - 1);
        return nouveauX == 0;
    }
    public bool LimiterPositionDroite(int largeurEcran, int hauteurEcran)
    {
        var nouveauX = Math.Clamp(_position.X, 0, largeurEcran - 1);
        return  nouveauX == largeurEcran - 1;
    }


    public void Update(GameTime gameTime)
    {
        if (_texture == null)//lors de la serialisation ou du chargement au cas où la texture n'est pas chargé on remet toutes les valeurs
        {
            if (_typemonstre == TypeMonstre.Petit)
            {
                _dim = new Vector2(80,74);
                _texture = Global._Content.Load<Texture2D>("enemy2");
                _animation = new Animation(_texture,9,5,0.1f,true);
                _health = _Health;
                _damage = 50;
                setSpeedY(0.5f);
                setSpeedX(0.5f);
            }
            if (_typemonstre == TypeMonstre.Bigboss)
            {
                _dim =new Vector2(118,66);
                _texture = Global._Content.Load<Texture2D>("NpcWeed");
                _animation = new Animation(_texture,4,4,0.1f,true);
                _health = _Health;
                _damage = 100;
                setSpeedX(2.5f);
                setSpeedY(0.1f);
                _MoveMethods = [Moves.Down,Moves.ZigZag,Moves.ZigZagReverse];
            }
            
        }

        if (_typemonstre == TypeMonstre.Petit)
        {
            _position.Y += 1 + _speed.Y;
        }
        else if (_typemonstre == TypeMonstre.Bigboss)
        {
                var mafonction = GetMove(0);
                _position = mafonction(_position, 1f, _speed);
        }

        _animation?.Update(gameTime);
    }
    
    public void Draw()
    {
        _animation?.Draw(getPos(),_typemonstre);
    }


}