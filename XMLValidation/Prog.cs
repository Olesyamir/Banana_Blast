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
                "http://www.univ-grenoble-alpes.fr/l3miage/jeu_video", "../../../../Data/xsd/initialization.xsd",
                "../../../../Data/xml/initialization.xml").Wait();

            XMLUtils.ValidateXmlFileAsync(
                "http://www.univ-grenoble-alpes.fr/l3miage/profile", "../../../../Data/xsd/profile.xsd",
                "../../../../Data/xml/profile.xml").Wait();

            XMLUtils.ValidateXmlFileAsync(
                "http://www.univ-grenoble-alpes.fr/l3miage/jeu_video", "../../../../Data/xsd/savedGames.xsd",
                "../../../../Data/xml/savedGames.xml").Wait();
            
            // execution des fichiers  XSLT
        XMLUtils.XslTransform("../../../../Data/xml/savedGames.xml", "../../../../Data/xslt/scores.xslt", "../../../../Data/html/scores.html");
        XMLUtils.XslTransform("../../../../Data/xml/savedGames.xml", "../../../../Data/xslt/scores.xslt", "../../../../Data/html/scores.html");
        XMLUtils.XslTransform("../../../../Data/xml/Games.xml", "../../../../Data/xslt/parties.xslt", "../../../../Data/xml/Parties.xml");
        }
    }
}