using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft. Xna. Framework. Input ;
using BasicMonoGame.Managers;
using BasicMonoGame.Entities;

namespace BasicMonoGame.Screens;

public class MainMenuScreen : Screen
{
    //liste des choix possibles dans le menu
    private string[] menuItems = { "Start Game", "Load", "High score","Exit" };//liste des choix possibles
    private int selectedIndex = 0;
    
    private SpriteFont font;
    

    public override void Initialize()
    {
        //evite que le jeu considère une nouvelle partie comme une Load
        Global.IsLoad = false;
        Global._screenState = ScreenState.IsMenu;
        Global.IsSaved = false;
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
                    Global.IsEntree = true;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Global.IsEntree = false;                    
                    Global._pressTime = 0;
                    HandleMenuSelection(gameTime);
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
                var jeu = GameSerializer.Load("./data/xml/Sauvegarde.xml");
                //enregistre joueur dans Global._joueur a utiliser pour plutard
                Global._joueur = jeu._ship;
                Global.IsLoad = true;
                Global._ScreenManager.ChangeScreen(jeu);
                break;
            case 2:
                // Application des feuilles Xslt
                XmlUtils.XslTransform("./data/xml/Fichiers.xml", "./data/xslt/top10Meilleurs.xslt", "./data/html/Les10Meilleurs.html");
                //ouvre la page
                XmlUtils.OpenHtmlFile("data\\html\\Les10Meilleurs.html");
                break;
            case 3:
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