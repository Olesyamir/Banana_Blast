using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BasicMonoGame;

public class GameOverScreen : Screen
{
    private string[] GameOverItems = { "Restart", "Quit" };
    private int selectedIndex = 0;
    private SpriteFont font;
    
    public override void Initialize()
    {
        Global.IsMenu = false;
        Global.IsPaused = false;
        Global.IsGame = false;
        Global.IsGameOver = true;
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
        if (Global.IsGameOver)
        {
            // Gérer les entrées utilisateur
            Global._pressTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Global._pressTime >= Global._pressCooldown)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    selectedIndex = (selectedIndex - 1 + GameOverItems.Length) % GameOverItems.Length;

                    Global._pressTime = 0;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    selectedIndex = (selectedIndex + 1) % GameOverItems.Length;
                    Global._pressTime = 0;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    HandleGameOverSelection(gameTime);
                    Global._pressTime = 0;
                }

            }



            base.Update(gameTime);
        }
    }

    private void HandleGameOverSelection(GameTime gameTime)
    {
        switch (selectedIndex)
        {
            case 0:
                Global._game.Content.Unload();
                Global._ScreenManager.ChangeScreen(new InGameScreen());
                break;
            case 1:
                Global._game.Content.Unload();
                Global._game.GraphicsDevice.Clear(Color.CornflowerBlue);
                Global._ScreenManager.ChangeScreen(new MainMenuScreen());
                ChangeScreenSize(Global._graphics,800,400);
                break;
        }
    }
    public override void Draw(GameTime gameTime)
    {
        Global._game.GraphicsDevice.Clear(Color.Black);
        for (int i = 0; i < GameOverItems.Length; i++)
        {
            Color color = (i== selectedIndex) ? Color.Red : Color.White;
            Global._spriteBatch.DrawString(font,GameOverItems[i],new Vector2(100, 100 + i * 30), color);
        }
        
    }
}