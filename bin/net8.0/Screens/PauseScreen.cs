using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft. Xna. Framework. Input ;
using BasicMonoGame.Managers;

namespace BasicMonoGame.Screens;

public class PauseScreen : Screen
{
    //liste des choix possibles dans l'ecrans de pause
    private string[] pauseItems = { "Resume", "Save", "Profile","Quit" };
    private int selectedIndex = 0;

    private InGameScreen jeu;
    private SpriteFont font;
    public PauseScreen(InGameScreen jeu)
    {
        this.jeu = jeu;//pour pouvoir revenir au jeu et continuer
    }
    public override void Initialize()
    {
        Global._screenState = ScreenState.IsPaused;
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
        if (Global._screenState==ScreenState.IsPaused)
        {
            Global._pressTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Global._pressTime >= Global._pressCooldown)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    //on fait modulo length de pauseItems pour revenir au coté opposé si on essaye de depasser
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
                    Global.IsEntree = true;//se maintient tant que entrée est Down
                }
                if (Keyboard.GetState().IsKeyUp(Keys.Enter) && Global.IsEntree)
                {
                    //tant que entrée down on ne passe pas a l'action pour eviter les entrees indesirables
                    HandlePauseSelection(gameTime);//choisi l'action en fonction de l'item choisi
                    Global.IsEntree = false;//Evite ain
                    Global._pressTime = 0;
                } 

            }
            //Met à jour les monstres pour qu'ils s'arrêtent
            MonstreManager.Update(gameTime);
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
                //Sauvegarde de la partie et mise à jour des stats du joueurs
                XMLManager<InGameScreen> GameDeserializer = new XMLManager<InGameScreen>();
                //nom du fichier de sauvegarde créé    
                string path = "Sauvegarde.xml";
                GameDeserializer.Save("./data/xml/"+path,jeu);
                //Sauvegarde du nom du fichier dans le XML qui contient la liste des fichiers
                Global.IsSaved=true;
                break;
            case 2:
                //Code xslt pour génerer le html du profile du joueur
                string nom = Global._joueur.getName();
                XmlUtils.XslTransform("./data/xml/SavedGames.xml", "./data/xslt/profile_joueur.xslt", "./data/html/profile_joueur.html",nom);
                //ouvre la page
                XmlUtils.OpenHtmlFile("data\\html\\profile_joueur.html");
                break;
            case 3:
                Global._game.Content.Unload();
                Global._ScreenManager.ChangeScreen(new MainMenuScreen());
                //Clear l'ecran de tout les objets
                Global._game.GraphicsDevice.Clear(Color.CornflowerBlue);
                //remet l'ecran a la taille par defaut
                ChangeScreenSize(Global._graphics,800,480);
                break;
        }
    }
    public override void Draw(GameTime gameTime)
    {
     Global._game.GraphicsDevice.Clear(Color.CornflowerBlue);
     //affiche les choix
     for (int i = 0; i < pauseItems.Length; i++)
     {
         Color color = (i== selectedIndex) ? Color.Navy : Color.White;//si selectionné alors change couleur
         Global._spriteBatch.DrawString(font,pauseItems[i],new Vector2(100, 100 + i * 30), color);
     }

     if (Global.IsSaved == true)
     {
         Global._spriteBatch.DrawString(font,"Sauvegarde reussie",new Vector2(100,760), Color.MintCream);
     }
    }
}