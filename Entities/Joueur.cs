using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BananaBlast.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BananaBlast.Entities;

[XmlRoot("joueur",Namespace="http://www.univ-grenoble-alpes.fr/jeu_monstres")][Serializable]
public class Joueur : GameObject
{
    private string _name;
    
    [XmlElement("nom")] 
    public string _Name{ get=>_name; set => _name=value; }

    private int _age;
    [XmlElement("age")] 
    public string _Age{ get=>_age.ToString(); set=>_age=int.Parse(value); }

    private int _health;
    [XmlElement("health")] 
    public int _Health{ get=>_health; set=>_health=value; }
    
    [XmlElement("position")]
    public Vector2 _jposition { get=>_position; set => _position = value; }
    
    private static int _score { get => Scoreboard.getScore();set=>Scoreboard.setScore(value);}
    
    [XmlElement("score")]
    public int _Score { get=>_score; set=>_score = value; }
    
    [XmlIgnore]
    private int _sizeMax = 100;
    

    public Joueur():base(null,Vector2.Zero,new Vector2(100,100))
    {
        _Size = 100;
        setPos(_jposition.X,_jposition.Y);
        _health = _Health;
        _texture=Global._game.Content.Load<Texture2D >("ship2");
        
    }
    public Joueur(Texture2D texture, Vector2 position, Vector2 dim) : base(texture, position, dim)
    {
        _name = "";
        _age = 0;
        if (dim.X > _sizeMax)
        {
            dim.Y = _sizeMax;
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
                    bullets.Add(new Projectile(projectileTexture, this.getPos(),  new Vector2(25,34)));
                    Global._pressTime = 0;
                }
        }

        _position.X = _position.X + _speed.X;
        _position.Y = _position.Y + _speed.Y;
        if (_speed.X > 0) _speed.X -= 0.05f;
        if (_speed.X < 0) _speed.X += 0.1f;
        if (_speed.Y > 0) _speed.Y -= 0.1f;
        if (_speed.Y < 0) _speed.Y += 0.1f;
        if (_health <= 0)
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