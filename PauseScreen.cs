using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft. Xna. Framework. Input ;

namespace jeu_monstre;

public class PauseScreen : Screen
{

    private string[] pauseItems = { "Resume", "Save", "Quit" };
    private int selectedIndex = 0;

    private InGameScreen jeu;
    private SpriteFont font;
    public PauseScreen(InGameScreen jeu)
    {
        this.jeu = jeu;
    }
    public override void Initialize()
    {
        Global.IsMenu = false;
        Global.IsPaused = true;
        Global.IsGame = false;
        Global.IsGameOver = false;
    }

    public override void LoadContent()
    {
        font = Global._Content.Load<SpriteFont>("outerspace");
    }

    public override void UnloadContent()
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        // Gérer les entrées utilisateur
        if (Global.IsPaused)
        {
            Global._pressTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Global._pressTime >= Global._pressCooldown)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    selectedIndex = (selectedIndex - 1 + pauseItems.Length) % pauseItems.Length;
                    
                    Global._pressTime = 0;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    selectedIndex = (selectedIndex + 1) % pauseItems.Length;
                    Global._pressTime = 0;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    HandlePauseSelection(gameTime);
                    Global._pressTime = 0;
                }

            }
            
            CreatureManager.Update(gameTime);
            Scoreboard.Update(gameTime);
            
        }

        base.Update(gameTime);
    }

    private void HandlePauseSelection(GameTime gameTime)
    {
        switch (selectedIndex)
        {
            case 0:
                Global._ScreenManager.ChangeScreen(jeu);
                break;
            case 1:
                //Global._ScreenManager.ChangeScreen(new SaveScreen());
                break;
            case 2:
                
                Global._game.Content.Unload();
                Global._ScreenManager.ChangeScreen(new MainMenuScreen());
                Global._game.GraphicsDevice.Clear(Color.CornflowerBlue);
                ChangeScreenSize(Global._graphics,800,480);
                break;
        }
    }
    public override void Draw(GameTime gameTime)
    {
     Global._game.GraphicsDevice.Clear(Color.CornflowerBlue);
     for (int i = 0; i < pauseItems.Length; i++)
     {
         Color color = (i== selectedIndex) ? Color.Navy : Color.White;
         Global._spriteBatch.DrawString(font,pauseItems[i],new Vector2(100, 100 + i * 30), color);
     }
    }
}