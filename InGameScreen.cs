using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BasicMonoGame;

[XmlRoot("jeu",Namespace ="http://www.univ-grenoble-alpes.fr/jeu_monstres" )][Serializable]
public class InGameScreen : Screen
{
    [XmlIgnore]
    public Joueur _ship;//instance de Player

    [XmlIgnore]
    public static String date=((DateTime.Today).ToShortDateString());
    [XmlIgnore]
    public String _date=(date.Substring(6,4))+"-"+(date.Substring(3,2))+"-"+(date.Substring(0,2));

            
    [XmlIgnore]
    private readonly GraphicsDeviceManager _graphics;
    
    [XmlIgnore]
    private SpriteFont font;
    
    [XmlIgnore]
    private BackgroundManager _background;
    
    [XmlIgnore]
    private Texture2D exploTexture;//explosion texture
    
    [XmlIgnore]
    public List<Projectile> _projectiles;
    
    [XmlIgnore]
    private List<Projectile> _killprojectiles;
    
    [XmlIgnore]
    private List<Explosion> explosions;
    
    [XmlElement("date")]
    public String _Date { get => _date;set=>_date=value; }
    
    [XmlElement("joueur")]
    public Joueur _Ship { get=>_ship; set => _ship = value; }//instance de Player


    [XmlElement("monstres")] 
    public Monstres _Monstres { get=>MonstreManager.GetMonstres(); set=>MonstreManager.SetMonstres(value); }

    [XmlElement("projectiles")]
    public Projectiles _Projectiles
    {
        get => new Projectiles{ ListeProjectiles = _projectiles };//getter pour _projectiles dans le contexte de serialisation
        set => _projectiles = value?.ListeProjectiles ?? new List<Projectile>();//setter _projectiles dans le contexte de deserialisation
    }

    public InGameScreen()
    {
        Scoreboard.Init();
        _graphics = Global._graphics;
        Global._game.IsMouseVisible = true;
        //_Ship = _ship;
    }

    public InGameScreen(Joueur joueur)
    {
        Scoreboard.Init();
        _graphics = Global._graphics;
        Global._game.IsMouseVisible = true;
        _ship = joueur;
        MonstreManager.SetMonstres(_Monstres);
        
    }
    

    public override void Initialize()
    {
        ChangeScreenSize(_graphics,500,780);
        Global._screenState = ScreenState.IsGame;
        _background = new BackgroundManager();
        MonstreManager.Init();
        if (!Global.IsLoad) _ship.resetHealth(); //(re)initialise vie à 100 (aussi dans le cas d'un restart)
    }

    public override void LoadContent()
    {
        Texture2D bgTexture = Global._game.Content.Load<Texture2D>("Purple_Nebula");
        _background.Initialize(bgTexture); 
        exploTexture = Global._game.Content.Load<Texture2D>("rb_11922");
        if (!Global.IsLoad)
        {
            _projectiles = new List<Projectile>();
        }
        _killprojectiles = new List<Projectile>();
        explosions = new List<Explosion>();
    }

    public override void UnloadContent()
    {
        
    }

public override void Update(GameTime gameTime)
{
    if (Global._screenState==ScreenState.IsGame)
    {
        _background.Update(gameTime);
        // Si on appuie sur Échap ou sur le bouton "Back" du gamepad, on retourne au menu
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            // Change ecran met jeu sur pause
            //Global.IsPaused = true;
            Global._ScreenManager.ChangeScreen(new PauseScreen(this));
            Global._ScreenManager.Update(gameTime);
        }

        // Logique du jeu
        
        
        float x, y;

        // Mise à jour du vaisseau
        _ship.Update(gameTime, _projectiles, Global._Content.Load<Texture2D>("missile1"));

        // Mise à jour des projectiles et des explosions
   
        foreach (var p in _projectiles)
        {
            p.Update(gameTime);
        }

        for (int i = explosions.Count - 1; i >= 0; i--)
        {
            explosions[i].Update(gameTime);
            if (explosions[i]._animation._active==false)
            {
                explosions.RemoveAt(i);
            }
        }


        // Gérer la position du vaisseau
        (x, y) = _ship.getPos();

        if (x >= Global._game.Window.ClientBounds.Width - (_ship._Rect.Width / 2))
        {
            x = Global._game.Window.ClientBounds.Width - (_ship._Rect.Width / 2);
            _ship.setPos(x, y);
        }

        if (x < (_ship._Rect.Width / 2))
        {
            x = (_ship._Rect.Width / 2);
            _ship.setPos(x, y);
        }

        // Mise à jour des créatures
        MonstreManager.Update(gameTime);

        // Gestion des collisions
        foreach (var s in MonstreManager._monstres)
        {
            s.Update(gameTime);

            // Si la créature sort de l'écran, elle disparaît
            if (s.getPos().Y >= 600)
            {
                _ship.playerGotHit(s.getDamage());//les monstres sont passés le joueur perd de la vie
                s.MonsterGotHit(s._Health);
            }
            else
            {
                foreach (var p in _projectiles)
                {
                    if (p._Rect.Intersects(s._Rect))
                    {
                        Scoreboard.addScore(1);
                        var la = p.getPos().X;
                        var lo= p.getPos().Y;
                        
                        explosions.Add(new Explosion(exploTexture, new Vector2(la- (p._Rect.Width*1.5f),lo- (p._Rect.Height*3f)),60));
                        _killprojectiles.Add(p);
                        s.MonsterGotHit(50);
                    }

                    // Retirer les projectiles qui sortent de l'écran
                    if (p._Rect.Top < 0)
                    {
                        _killprojectiles.Add(p);
                    }
                    
                }
            }
        }

        // Retirer les projectiles qui ont exploses ou qui sont sorties de l'ecran
        foreach (var p in _killprojectiles)
        {
            _projectiles.Remove(p);
        }
        // Vider la liste des projectiles a enlever
       _killprojectiles.Clear();
    }
    
    base.Update(gameTime);
}



    public override void Draw(GameTime gameTime)
    {
        
            if (Global._screenState==ScreenState.IsGame)
            {
                Global._game.GraphicsDevice.Clear(Color.CornflowerBlue);
                _background.Draw(gameTime);
                MonstreManager.Draw();
                _ship.Draw(Global._spriteBatch);
                
                //dessine chaque projectiles 
                foreach (var p in _projectiles)
                {
                    p.Draw(Global._spriteBatch);
                }
                
                //et chaque explosions
                for (int i = explosions.Count - 1; i >= 0; i--)
                {
                    explosions[i].Draw(Global._spriteBatch);
                }
                
                //affiche scoreboard
                Scoreboard.Draw(Global._spriteBatch);
                base.Draw(gameTime);
            }
    }
}
