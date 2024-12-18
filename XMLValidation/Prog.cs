using System;

namespace XMLValidation
{
    public class Prog
    {
        public static void Main(string[] args)
        {
            // Validation des schémas XML
            Console.WriteLine("_________________ Validation des  fichiers XML______________________________");
            
            
            AppContext.SetSwitch("Switch.System.Xml.AllowDefaultResolver", true);
            XMLUtils.ValidateXmlFileAsync(
                "http://www.univ-grenoble-alpes.fr/l3miage/jeu_video", "../../../../data/xsd/initialization.xsd",
                "../../../../data/xml/initialization.xml").Wait();

            XMLUtils.ValidateXmlFileAsync(
                "http://www.univ-grenoble-alpes.fr/l3miage/profile", "../../../../data/xsd/profile.xsd",
                "../../../../data/xml/profile.xml").Wait();

            XMLUtils.ValidateXmlFileAsync(
                "http://www.univ-grenoble-alpes.fr/l3miage/jeu_video", "../../../../data/xsd/savedGames.xsd",
                "../../../../data/xml/savedGames.xml").Wait();
            
            // execution des fichiers  XSLT
        XMLUtils.XslTransform("../../../../data/xml/savedGames.xml", "../../../../data/xslt/scores.xslt", "../../../../data/html/scores.html");
        XMLUtils.XslTransform("../../../../data/xml/savedGames.xml", "../../../../data/xslt/scores.xslt", "../../../../data/html/scores.html");
        XMLUtils.XslTransform("../../../../data/xml/Games.xml", "../../../../data/xslt/parties.xslt", "../../../../data/xml/Parties.xml");
        }
    }
}