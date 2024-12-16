using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using jeu_monstre.Player;
using jeu_monstre.Virus;
using jeu_monstre.Projectile;
using jeu_monstre.Scoreboard;
namespace jeu_monstre;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    
    private Player.Player _ship;//instance de Player
    private Scoreboard.Scoreboard _scoreboard; 
    List<Projectile.Projectile> _projectiles;
    List<Projectile.Projectile> _killprojectiles;
    
    Timer time = new Timer();
    
    //private Creature _creature;//instance de creature
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
   
        Global._Content = Content;
        CreatureManager.Init();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        // TODO: use this.Content to load your game content here
        Global._spriteBatch = new SpriteBatch(GraphicsDevice);
        Texture2D shipTexture = Content.Load < Texture2D >("ship2") ;
        _ship = new Player.Player( shipTexture , new Vector2 (250 , 720),80 ) ;
        Texture2D projectileTexture = Content.Load < Texture2D >("missile1") ;
        Texture2D scoreboardTexture = Content.Load < Texture2D >("scoreboard01") ;
        _scoreboard = new Scoreboard.Scoreboard(scoreboardTexture, new Vector2(200 , 720), 80);
        _scoreboard.LoadContent(Content, "Roboto");
        _projectiles = new List<Projectile.Projectile>();
        _killprojectiles = new List<Projectile.Projectile>();
    }

    protected override void Update(GameTime gameTime)
    {
        _graphics.PreferredBackBufferWidth = 500;
        _graphics.PreferredBackBufferHeight = 780;
        _graphics.ApplyChanges();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        
        
        float x;
        float y;

        
        _ship.Update(gameTime,_projectiles, (Content.Load<Texture2D>("missile1")));
        
        foreach (var p in _projectiles)
        {
            p.Update(gameTime); 
        }

        
        (x, y) = _ship.getPos();
        
        // Si la position de la vaisseau passe l'extrémité droite de l'écran, réinitialiser la à l'extrémité droite
        if (x >= Window.ClientBounds.Width - (_ship._Rect.Width / 2) )
        {
            x = Window.ClientBounds.Width - (_ship._Rect.Width / 2);
            _ship.setPos(x, y);
        }
        
        // Si la position de la vaisseau passe l'extrémité gauche de l'écran, réinitialiser la à l'extrémité gauche
        if (x < (_ship._Rect.Width/2))
        {
            x = (_ship._Rect.Width/2);
            _ship.setPos(x,y);
        }
        
        // Créer les virus au fur et à mesure du temps
        CreatureManager.Update(gameTime);
        
        foreach (var s in CreatureManager._Creatures)
        {
            s.Update(gameTime);
            if (s.getPos().Y >= 600)
            {
                s._Health = 0;
            }
            else
            {
                foreach (var p in _projectiles)
                {   
                    // Gérer la collision entre la projectile et les virus
                    if (p._Rect.Intersects(s._Rect))
                    {
                        s._Health -= p.getDegats();
                        if (s._Health <= 0)
                        {
                            _killprojectiles.Add(p);
                            _scoreboard.addScore(2);
                        }
                    }

                    if (p._Rect.Top < 0)
                    {
                        _killprojectiles.Add(p);
                    }
                }
            }
        }
        
        // enlever les virus qui sont déjà tués
        foreach (var p in _killprojectiles)
        {
            _projectiles.Remove(p);
        }
        
        base.Update(gameTime);
    }



    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        Global._spriteBatch.Begin(samplerState : SamplerState.PointClamp); 
        _ship.Draw(Global._spriteBatch);
        
        CreatureManager.Draw();
         
        foreach (var p in _projectiles)
        {
            p.Draw(Global._spriteBatch);
        }
        _scoreboard.Draw(Global._spriteBatch);
        Global._spriteBatch.End();
        base.Draw(gameTime);
    }
}
