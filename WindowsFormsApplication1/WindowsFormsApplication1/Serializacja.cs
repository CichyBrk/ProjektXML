using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml.XPath;

namespace WindowsFormsApplication1
{
    class Serializacja
    {
        static public void serialize(List<Book> bookList)
        {
            XmlRootAttribute oRootAttr = new XmlRootAttribute();
            oRootAttr.ElementName = "Library";
            oRootAttr.IsNullable = true;
            XmlSerializer oSerializer = new XmlSerializer(typeof(List<Book>), oRootAttr);
            StreamWriter oStreamWriter = null;
            try
            {
                oStreamWriter = new StreamWriter("library.xml");
                oSerializer.Serialize(oStreamWriter, bookList);
            }
            catch (Exception oException)
            {
                Console.WriteLine("Aplikacja wygenerowała następujący wyjątek: " + oException.Message);
            }
            finally
            {
                if (null != oStreamWriter)
                {
                    oStreamWriter.Dispose();
                }
            }
        }

        static public List<Book> Deserializacja()
        {
            XPathDocument oXPathDocument = new XPathDocument("library.xml");
            XPathNavigator oXPathNavigator = oXPathDocument.CreateNavigator();
            XPathNodeIterator oPersonNodesIterator = oXPathNavigator.Select("/Library/Book");

            List<Book> Library = new List<Book>();
            foreach (XPathNavigator oCurrentPerson in oPersonNodesIterator)
            {
                Book book = new Book(oCurrentPerson.SelectSingleNode("Name").Value,
                oCurrentPerson.SelectSingleNode("Author").Value,
                Convert.ToInt32(oCurrentPerson.SelectSingleNode("PublicationYear").Value),
                oCurrentPerson.SelectSingleNode("Type").Value,
                oCurrentPerson.SelectSingleNode("Leanguage").Value,
                oCurrentPerson.SelectSingleNode("Avibility").Value);

                Library.Add(book);
            }

            return Library;
        }
    }
}
