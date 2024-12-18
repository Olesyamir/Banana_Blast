using System;
using System.Xml.Serialization;

namespace BasicMonoGame;

[Serializable]
[XmlRoot("partie", Namespace = "http://www.univ-grenoble-alpes.fr/partie")]
public class Partie
{
    // Propriétés pour la sérialisation
    [XmlElement("nomjoueur")]
    public string _NomJoueur { get; set; }

    [XmlElement("date")]
    public String _Date { get; set; }

    [XmlElement("score")]
    public int _Score { get; set; }

    // Constructeur sans paramètres pour la désérialisation
    public Partie()
    {
        _NomJoueur = "";
        _Date = Global.date;
        _Score = 0;
    }

    // Constructeur avec paramètres
    public Partie(string nom, String date, int score)
    {
        _NomJoueur = nom;
        _Date = date;
        _Score = score;
    }
}