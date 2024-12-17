using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft. Xna. Framework;
using Microsoft. Xna. Framework. Graphics;
using Microsoft. Xna. Framework. Input ;
namespace BasicMonoGame;

[Serializable][XmlRoot("joueur",Namespace="http://www.univ-grenoble-alpes.fr/jeu_monstres")]
public class Joueur : GameObject
{
    [XmlElement("nom")]
    private string _name { get; set; }
    
    [XmlElement("age")]
    private int _age { get; set; }
    
    [XmlElement("health")]
    private int _health { get; set; }
    
    [XmlElement("position")]
    private Vector2 _jposition { get=>_position; set => _position = value; }
    
    private static int score { get => Scoreboard.getScore();set=>Scoreboard.setScore(value); }
    
    [XmlIgnore]
    private int _sizeMax = 100;
    
    public Joueur(Texture2D texture, Vector2 position, int size) : base(texture, position, size)
    {
        _name = "";
        _age = 0;
        if (size > _sizeMax)
        {
            size = _sizeMax;
        }

        _health = 100;
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

        
        Global._pressTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (Global._pressTime >= Global._pressCooldown)
        {
            if (bullets.Count < 15)
                if (Keyboard.GetState().IsKeyDown(Keys.X))
                {
                    bullets.Add(new Projectile(projectileTexture, this.getPos(), 25));
                    Global._pressTime = 0;
                }
        }

        _position.X = _position.X + _speed.X;
        _position.Y = _position.Y + _speed.Y;
        if (_speed.X > 0) _speed.X -= 0.05f;
        if (_speed.X < 0) _speed.X += 0.1f;
        if (_speed.Y > 0) _speed.Y -= 0.1f;
        if (_speed.Y < 0) _speed.Y += 0.1f;
        if (_health == 0)
        {
            Global._ScreenManager.ChangeScreen(new GameOverScreen());
        }
    }

    public void playerGotHit(int damage)
    {
            _health -= damage;
    }

    public void setName(string name)
    {
        if (name.Length <= 15)
        {
            _name = name;
        }
        else
        {
            _name = name.Substring(0, 15);
        }
    }

    public string  getName()
    {
        return _name;
    }

    public int getAge()
    {
        return _age;
    }

    public void setAge(int age)
    {
        if (age > 0 && age < 100)
        {
            _age = age;
        }
        else
        {
            _age = 18;
        }
    }

    public void resetHealth()
    {
        _health = 100;
    }

}