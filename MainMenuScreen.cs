using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft. Xna. Framework. Input ;
using System;
namespace BasicMonoGame;

public class MainMenuScreen : Screen
{
    private SpriteFont font;
    private string[] menuItems = { "Start Game", "Options", "Exit" };
    private int selectedIndex = 0;
    
    
    private static float _pressTime =0f;
    private static float _pressCooldown = 0.15f;

    public override void Initialize()
    {
        
    }
    
    public override void LoadContent()
    {
        // Charger la police et autres ressources nécessaires
        font = Global._Content.Load<SpriteFont>("outerspace");
    }

    public override void UnloadContent()
    {
        //font = null;
        //Global._Content.Unload();
        //Global._game.Content.Unload();
    }
    
    public override void Update(GameTime gameTime)
    {
        // Gérer les entrées utilisateur
        _pressTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (_pressTime>=_pressCooldown)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                selectedIndex = (selectedIndex - 1 + menuItems.Length) % menuItems.Length;

                _pressTime = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                selectedIndex = (selectedIndex + 1) % menuItems.Length;
                _pressTime = 0;
            }
            
        }
        
        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
        {
            HandleMenuSelection(gameTime);
        }
        base.Update(gameTime);
    }

    private void HandleMenuSelection(GameTime gameTime)
    {
        switch (selectedIndex)
        {
            case 0:
                // Lancer le jeu
                Global._ScreenManager.ChangeScreen(new InGameScreen());
                break;
            case 1:
                // Aller aux options
                break;
            case 2:
                // Quitter le jeu
                Global._game.Exit();
                break;
        }
    }

    public override void Draw(GameTime gameTime)
    {
        
        if (this is MainMenuScreen)
        {
            Console.WriteLine("dessin menu Appelé ");
            Global._game.GraphicsDevice.Clear(Color.CornflowerBlue);
            //Global._spriteBatch.Begin(samplerState : SamplerState.PointClamp);
            for (int i = 0; i < menuItems.Length; i++)
            {
                Color color = (i == selectedIndex) ? Color.Navy : Color.White;
                Global._spriteBatch.DrawString(font, menuItems[i], new Vector2(100, 100 + i * 30), color);
            }
            //Global._spriteBatch.End();
        }
        base.Draw(gameTime);
    }
}