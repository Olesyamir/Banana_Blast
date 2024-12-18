using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft. Xna. Framework. Input ;

namespace BasicMonoGame;

public class PauseScreen : Screen
{
    //liste des choix possibles dans l'ecrans de pause
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
                GameDeserializer.Save("../../../data/xml/Sauvegarde.xml",jeu);
                break;
            case 2:
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
    }
}