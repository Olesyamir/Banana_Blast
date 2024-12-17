using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace jeu_monstre;

public class InGameScreen : Screen
{
    private readonly GraphicsDeviceManager _graphics;
    private Player _ship;//instance de Player
    private BackgroundManager _background;
    private Texture2D exploTexture;
    List<Projectile> _projectiles;
    List<Projectile> _killprojectiles;
    private List<Explosion> explosions;
    private static InGameScreen Instance;

    public InGameScreen()
    {
        _graphics = Global._graphics;
        Global._game.IsMouseVisible = true;
        Instance = this;
    }
    

    public override void Initialize()
    {
        ChangeScreenSize(_graphics,500,780);
        CreatureManager.Init();     // init liste de créatures
        Scoreboard.Init();          // init le scoreboard
        Global.IsMenu = false;
        Global.IsPaused = false;
        Global.IsGame = true;
        Global.IsGameOver = false;
        _background = new BackgroundManager();
    }

    public override void LoadContent()
    {
        Texture2D shipTexture = Global._game.Content.Load < Texture2D >("ship2") ;
        Texture2D bgTexture = Global._game.Content.Load<Texture2D>("Purple_Nebula");
        _background.Initialize(bgTexture); 
        exploTexture = Global._game.Content.Load<Texture2D>("rb_11922");
        _ship = new Player( shipTexture , new Vector2 (250 , 720),100 ) ;
        _projectiles = new List<Projectile>();
        _killprojectiles = new List<Projectile>();
        explosions = new List<Explosion>();    
    }

    public override void UnloadContent()
    {
        
    }

public override void Update(GameTime gameTime)
{
    if (Global.IsGame)
    {
        _background.Update(gameTime);
        // Si on appuie sur Échap ou sur le bouton "Back" du gamepad, on retourne au menu
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            // Marque l'intention de changer d'écran, mais ne le fait pas immédiatement
            Global.IsPaused = true;
            Global._ScreenManager.ChangeScreen(new PauseScreen(this));
            Global._ScreenManager.Update(gameTime);
        }
        Scoreboard.Update(gameTime);

        // Logique du jeu
        float x, y;

        // Mise à jour du vaisseau
        _ship.Update(gameTime, _projectiles, Global._Content.Load<Texture2D>("missile1"));

        // Mise à jour des projectiles
   
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
        CreatureManager.Update(gameTime);

        // Gestion des collisions
        foreach (var s in CreatureManager._Creatures)
        {
            s.Update(gameTime);

            // Si la créature sort de l'écran, elle est détruite
            if (s.getPos().Y >= 600)
            {
                _ship.playerGotHit(s.getDamage());//les monstres sont passés le joueur perd de la vie
                s.MonsterGotHit(50);
            }
            else
            {
                foreach (var p in _projectiles)
                {
                    if (p._Rect.Intersects(s._Rect))
                    {
                        var la = p.getPos().X;
                        var lo= p.getPos().Y;
                        
                        explosions.Add(new Explosion(exploTexture, new Vector2(la- (p._Rect.Width*1.5f),lo- (p._Rect.Height*3f)),60));
                        _killprojectiles.Add(p);   // modifier le score après avoir tué un virus
                        Scoreboard.addScore(1);
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

        // Retirer les projectiles à tuer
        foreach (var p in _killprojectiles)
        {
            _projectiles.Remove(p);
        }
        

        

        // Vider la liste des projectiles à tuer
        _killprojectiles.Clear();
    }
    
    base.Update(gameTime);
}



    public override void Draw(GameTime gameTime)
    {
        // TODO: Add your drawing code here

            if (Instance is InGameScreen && Global.IsGame)
            {
                Global._game.GraphicsDevice.Clear(Color.CornflowerBlue);
                _background.Draw(gameTime);
                _ship.Draw(Global._spriteBatch);
                CreatureManager.Draw();
                Scoreboard.Draw(Global._spriteBatch);
                foreach (var p in _projectiles)
                {
                    p.Draw(Global._spriteBatch);
                }
                
                for (int i = explosions.Count - 1; i >= 0; i--)
                {
                    explosions[i].Draw(Global._spriteBatch);
                }


                base.Draw(gameTime);
            }
    }
}
