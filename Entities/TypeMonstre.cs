using System;
using System.Xml.Serialization;

namespace BasicMonoGame.Entities;

[Serializable][XmlRoot("type",Namespace = "http://www.univ-grenoble-alpes.fr/jeu_monstres")]
public enum TypeMonstre
{
    [XmlEnum("Petit")]
    Petit,
    [XmlEnum("Bigboss")]
    Bigboss
    
}