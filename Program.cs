using System;
using System.IO;
using System.Xml;

    class Program
    {
        static void Main(string[] args)
        {


            string filePathQuelle = "Invoice 2023-400037.pdf.xml";
            string filePathZiel = "Invoice 2023-400037.pdf.dwcontrol";
            // XML-Datei laden
            XmlDocument docQuelle = new XmlDocument();
            XmlDocument docZiel = new XmlDocument();
            
            if (!File.Exists(filePathQuelle))
        {
            Console.WriteLine("Die angegebene XML-Datei existiert nicht.");
            return;
        }
            
            try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePathQuelle);
            PrintXmlNode(xmlDoc.DocumentElement, "");
        }
        catch (Exception e)
        {
            Console.WriteLine("Fehler beim Einlesen der XML-Datei: " + e.Message);
        }
            
        static void PrintXmlNode(XmlNode node, string indent)
    {
        Console.WriteLine($"{indent}Node Name: {node.Name}, Value: {node.InnerText}");

        if (node.Attributes != null)
        {
            foreach (XmlAttribute attr in node.Attributes)
            {
                Console.WriteLine($"{indent}  Attribute: {attr.Name}, Value: {attr.Value}");
            }
        }

        foreach (XmlNode childNode in node.ChildNodes)
        {
            PrintXmlNode(childNode, indent + "  ");
        }
    }            
            
            
            
            
            docQuelle.Load(filePathQuelle);
            //docZiel.Load(filePathZiel);

            //Auslesen der Quellfelder die benötigt werden

            
            XmlNode AmagnoNode = docQuelle.SelectSingleNode("/ArrayOfAmagnoContentProperty/AmagnoContentProperty/Name");


            if (AmagnoNode != null)
            {
                XmlNode priceNode = AmagnoNode.SelectSingleNode("price");
                if (priceNode != null)
                {
                    priceNode.InnerText = "14.99"; // Preis ändern
                }
            }

            // Ein neues Buch hinzufügen
            XmlElement newBook = docZiel.CreateElement("book");
            newBook.SetAttribute("genre", "science fiction");
            newBook.SetAttribute("publicationdate", "2024-07-17");

            XmlElement title = docZiel.CreateElement("title");
            title.InnerText = "The Time Machine";
            newBook.AppendChild(title);

            XmlElement author = docZiel.CreateElement("author");

            XmlElement firstName = docZiel.CreateElement("first-name");
            firstName.InnerText = "H.G.";
            author.AppendChild(firstName);

            XmlElement lastName = docZiel.CreateElement("last-name");
            lastName.InnerText = "Wells";
            author.AppendChild(lastName);

            newBook.AppendChild(author);

            XmlElement price = docZiel.CreateElement("price");
            price.InnerText = "9.99";
            newBook.AppendChild(price);

            docZiel.DocumentElement.AppendChild(newBook);

            // Änderungen speichern
            docZiel.Save(filePathZiel);

            Console.WriteLine("XML-Datei wurde erfolgreich geändert und gespeichert.");
        }
    }

