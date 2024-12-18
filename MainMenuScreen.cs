using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft. Xna. Framework. Input ;
namespace BasicMonoGame;

public class MainMenuScreen : Screen
{
    private SpriteFont font;
    private string[] menuItems = { "Start Game", "Load", "Exit" };//liste des choix possibles
    private int selectedIndex = 0;
    

    public override void Initialize()
    {
        Global._screenState = ScreenState.IsMenu;
    }
    
    public override void LoadContent()
    {
        // Charger la police
        font = Global._Content.Load<SpriteFont>("outerspace");
    }

    public override void UnloadContent()
    {
        //si besoin
    }

    public override void Update(GameTime gameTime)
    {
        Scoreboard.Update(gameTime);
        if (Global._screenState== ScreenState.IsMenu)
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
                Global._ScreenManager.ChangeScreen(new InitializeScreen());
                break;
            case 1:
                // Load
                XMLManager<InGameScreen> GameSerializer = new XMLManager<InGameScreen>();
                var jeu = GameSerializer.Load("../../../data/xml/Sauvegarde.xml");
                //jeu.Initialize();
               // jeu.LoadContent();
                Global._joueur = jeu._ship;
                Global.IsLoad = true;
                Global._ScreenManager.ChangeScreen(jeu);
                break;
            case 2:
                // Quitter le jeu
                Global._game.Exit();
                break;
        }
    }

    public override void Draw(GameTime gameTime)
    {
        
        if (Global._screenState==ScreenState.IsMenu)
        {
            Global._game.GraphicsDevice.Clear(Color.CornflowerBlue);
            for (int i = 0; i < menuItems.Length; i++)
            {
                Color color = (i == selectedIndex) ? Color.Navy : Color.White;//change couleur si selectionne
                Global._spriteBatch.DrawString(font, menuItems[i], new Vector2(100, 100 + i * 30), color);
                //Dessine chaque choix possibles 
            }
        }
        base.Draw(gameTime);
    }
}