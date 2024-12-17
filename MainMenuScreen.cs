using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft. Xna. Framework. Input ;
namespace jeu_monstre;

public class MainMenuScreen : Screen
{
    public SpriteFont font;
    private string[] menuItems = { "Start Game", "Load", "Guide", "Exit" };
    private int selectedIndex = 0;
    

    public override void Initialize()
    {
        Global.IsMenu = true;
        Global.IsPaused = false;
        Global.IsGame = false;
        Global.IsGameOver = false;
        //Scoreboard.Init();  // initialiser le scoreboard dès le lancement du jeu
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
        if (Global.IsMenu)
        {
            // Gérer les entrées utilisateur
            Global._pressTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Global._pressTime >= Global._pressCooldown)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    selectedIndex = (selectedIndex - 1 + menuItems.Length) % menuItems.Length;

                    Global._pressTime = 0;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    selectedIndex = (selectedIndex + 1) % menuItems.Length;
                    Global._pressTime = 0;
                }
                
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    HandleMenuSelection(gameTime);
                    Global._pressTime = 0;
                }

            }



            base.Update(gameTime);
        }
    }

    private void HandleMenuSelection(GameTime gameTime)
    {
        switch (selectedIndex)
        {
            case 0:
                // Lancer le jeu
                Global._game.Content.Unload();
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
        
        if (this is MainMenuScreen && Global.IsMenu)
        {
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