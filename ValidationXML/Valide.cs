
namespace BasicMonoGame.ValidationXML;

public class Test
{
    public static void main()
    {
        //valide le xml contenant la liste de tout les fichiers
        XmlUtils.ValidateXmlFileAsync("http://www.univ-grenoble-alpes.fr/l3miage/jeu_video", "data/xsd/SavedGames.xsd", "data/xml/SavedGames.xml");
    }
}