using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Resolvers;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace BasicMonoGame;

public static class XmlUtils
{
    public static void XslTransform(string xmlFilePath, string xsltFilePath, string
        htmlFilePath,string? monnom=null)
    {
        
        XsltArgumentList argList = null;
        
        XsltSettings settings = new XsltSettings(true,true);
        
        XmlResolver resolver =new XmlUrlResolver();
        resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;
        
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltFilePath,settings,resolver);
        
        XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
        XmlTextWriter htmlWriter = new XmlTextWriter(htmlFilePath, Encoding.UTF8);

        if (monnom != null)
        {
            argList = new XsltArgumentList();
            argList.AddParam("monnom","",monnom);
        }
        xslt.Transform(xpathDoc, argList, htmlWriter, resolver);
        htmlWriter.Close();
        Console.WriteLine($"Transformation complete. Check file at {htmlFilePath}");
    }
    
    public static void OpenHtmlFile(string htmlPath)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = htmlPath,
            UseShellExecute = true // Utiliser l'exécution par le système pour ouvrir avec le programme par défaut
        });
    }
    
    public static async Task ValidateXmlFileAsync(string schemaNamespace,
        string xsdFilePath, string xmlFilePath)
    {
        var settings = new XmlReaderSettings();
        settings.Schemas.Add(schemaNamespace, xsdFilePath);
        settings.ValidationType = ValidationType.Schema;
        Console.WriteLine("Nombre de schemas utilises dans la validation : " +
                          settings.Schemas.Count);
        settings.ValidationEventHandler += ValidationCallBack;
        var readItems = XmlReader.Create(xmlFilePath, settings);
        while(readItems.Read())
        {
        }
    }

    private static void ValidationCallBack(object? sender, ValidationEventArgs e)
    {
        if (e.Severity.Equals(XmlSeverityType.Warning))
        {
            Console.Write("WARNING: ");
            Console.WriteLine(e.Message);
        }
        else if (e.Severity.Equals(XmlSeverityType.Error))
        {
            Console.Write("ERROR: ");
            Console.WriteLine(e.Message);

        }
    }



}

