using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BasicMonoGame;
//Cette classe sert pour la serialisation et deserialisation
[Serializable]
[XmlRoot("projectiles")]
public class Projectiles
{
    [XmlElement("projectile")]
    public List<Projectile> ListeProjectiles{ get; set; } = new();//cette liste sera egale a la liste dans InGameScreen
}
