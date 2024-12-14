using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BasicMonoGame;

public class InGameScreen : Screen
{
    private GraphicsDeviceManager _graphics;
    
    private Player _ship;//instance de Player
    List<Projectile> _projectiles;
    List<Projectile> _killprojectiles;
    public static InGameScreen Instance;
    private MainMenuScreen menu;

    public InGameScreen(MainMenuScreen menu)
    {
        _graphics = Global._graphics;
        Global._game.IsMouseVisible = true;
        Instance = this;
        this.menu = menu;
    }
    

    public override void Initialize()
    {
        ChangeScreenSize(_graphics,500,780);
        CreatureManager.Init();
        Global.IsMenu = false;
        Global.IsPaused = false;
        Global.IsGame = true;
    }

    public override void LoadContent()
    {
        Texture2D shipTexture = Global._game.Content.Load < Texture2D >("ship2") ;
        _ship = new Player( shipTexture , new Vector2 (250 , 720),100 ) ;
        _projectiles = new List<Projectile>();
        _killprojectiles = new List<Projectile>();
    }

    public override void UnloadContent()
    {
        //_ship = null;
    }

public override void Update(GameTime gameTime)
{
    if (Global.IsGame)
    {
        // Si on appuie sur Échap ou sur le bouton "Back" du gamepad, on retourne au menu
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            // Marque l'intention de changer d'écran, mais ne le fait pas immédiatement
            Global.IsPaused = true;
            Global._ScreenManager.ChangeScreen(new PauseScreen(this,menu));
            Global._ScreenManager.Update(gameTime);
        }

        // Logique du jeu
        float x, y;

        // Mise à jour du vaisseau
        _ship.Update(gameTime, _projectiles, Global._Content.Load<Texture2D>("missile1"));

        // Mise à jour des projectiles
        foreach (var p in _projectiles)
        {
            p.Update(gameTime);
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
                s._Health = 0;
            }
            else
            {
                foreach (var p in _projectiles)
                {
                    if (p._Rect.Intersects(s._Rect))
                    {
                        _killprojectiles.Add(p);
                        s._Health = 0;
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
                _ship.Draw(Global._spriteBatch);
                CreatureManager.Draw();

                foreach (var p in _projectiles)
                {
                    p.Draw(Global._spriteBatch);
                }

                base.Draw(gameTime);
            }
    }
}
