using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BasicMonoGame;
//Cette classe sert pour la serialisation et deserialisation 
[Serializable]
[XmlRoot("monstres", Namespace = "http://www.univ-grenoble-alpes.fr/jeu_monstres")]
public class Monstres
{
    [XmlElement("monstre")]
    public List<Monstre> ListeMonstres { get; set; } = new();//cette liste sera egale a la liste dans InGameScreen
}