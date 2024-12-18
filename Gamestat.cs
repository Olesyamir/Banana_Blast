/*Cest moi qui a voulu faire ca*/
/*using Microsoft.Xna.Framework;

namespace BasicMonoGame;

public class GameStats
{
    public static int Score { get; set; }
    public static int EnemiesKilled { get; set; }



    //initialisation des données
    public static void ResetStats()
    {
        Score = 0;
        EnemiesKilled = 0;
    }

    // Méthode qd le joueur tue un ennemi
    public static void EnemyKilled(int points)
    {
        EnemiesKilled++;
        Score += points;
    }


    public static void SaveGameData()
    {
        string playerName = "Player1";
        int score = GameStats.Score;
        int enemiesKilled = GameStats.EnemiesKilled;


        // génère et sauvegarde les données en XML
        GameDataManager.SaveGameData(playerName, score, enemiesKilled);
    }

    /*private void HandleGameOverSelection(GameTime gameTime)
    {
        switch (selectedIndex)
        {
            case 0: // Restart
                // Réinitialiser les statistiques du jeu avant de redémarrer
                GameStats.ResetStats();
                Global._game.Content.Unload();
                Global._ScreenManager.ChangeScreen(new InGameScreen());
                break;

            case 1: // Quit
                // Sauvegarder les données du jeu avant de quitter
                GameDataManager.SaveGameData();
                Global._game.Content.Unload();
                Global._game.GraphicsDevice.Clear(Color.CornflowerBlue);
                Global._ScreenManager.ChangeScreen(new MainMenuScreen());
                break;
        }
    }
    }*///fin de la classe

    

    
