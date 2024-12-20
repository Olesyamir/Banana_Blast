using System;
using System.Linq;
using System.Xml.Linq;

namespace BananaBlast.Managers;

public class SaveFileManager
{
    public static int AddFileToMasterXml()
    {
        //namespace
        XNamespace ns = "http://www.univ-grenoble-alpes.fr/l3miage/files";

        // Chemin du fichier XML maître
        string masterXmlPath = "./data/xml/Fichiers.xml";

        // Charger le fichier XML maître avec XDocument
        XDocument doc = XDocument.Load(masterXmlPath);

        // Compter le nombre de fichiers déjà présents dans le fichier XML
        int numeroFichiers = doc.Root.Elements(ns+"fichier").Count();

        // Générer un nom unique pour la nouvelle sauvegarde
        string nomFichierSauvegarde = $"Partie{numeroFichiers}";

        // Construire le chemin complet du fichier de sauvegarde
        string cheminFichierSauvegarde = $"{nomFichierSauvegarde}.xml";

        // Ajouter un nouvel élément <fichier> avec le chemin
        doc.Root.Add(new XElement(ns+"fichier", cheminFichierSauvegarde));

        // Sauvegarder le fichier XML mis à jour
        doc.Save(masterXmlPath);

        // Affichage pour confirmation
        Console.WriteLine($"Fichier ajoute : {cheminFichierSauvegarde}");
        return numeroFichiers;
    }

    public static void EnregistrePartie(string nom,int score,string date,string path)
    {
        //namespace
        Partie partie = new Partie(nom,date,score);
        XNamespace ns = "http://www.univ-grenoble-alpes.fr/l3miage/files";
        XMLManager<Partie> PartieSerializer = new XMLManager<Partie>();
        PartieSerializer.Save("./data/xml/"+path,partie);
        
    }
}