/*Cest moi qui a voulu faire ca*/


/*using System;
using System.IO;
using System.Xml.Linq;

namespace BasicMonoGame;

public static class GameDataManager//creer un xml pour sauvegarder les données
{
    public static void SaveGameData(string playerName, int score, int enemiesKilled)
    {
        // creer une XML avec les informations du joueur
        XElement gameData = new XElement("GameData",
            new XElement("Player",
                new XElement("Name", playerName),
                new XElement("Score", score),
                new XElement("EnemiesKilled", enemiesKilled),
                new XElement("Date", DateTime.Now.ToString("yyyy-MM-dd"))
            )
        );

        //  chemin du fichier XML dans LE dossier "Data"
        string directoryPath = "Data";  // Répertoire où stocker les fichiers XML
        

        // Nom du fichier, basé sur la date pour qu'il soit unique
        string filePath = Path.Combine(directoryPath, $"GameData_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xml");

        // Sauvegarder le fichier XML
        gameData.Save(filePath);

        Console.WriteLine($"Game data saved to {filePath}");
    }
}*/
