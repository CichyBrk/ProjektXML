using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Book
    {
        public string Name;
        public string Author;
        public int PublicationYear;
        public string Type;
        public string Leanguage;
        public string Avibility;

        public Book() //potrzebujemy pustego konstruktora, żeby serializacja zadziałała
        {

        }

        public Book(string name, string author, int publicationYear, string type, string leanguage, string avibility)
        {
            this.Name = name;
            this.Author = author;
            this.PublicationYear = publicationYear;
            this.Type = type;
            this.Leanguage = leanguage;
            this.Avibility = avibility;
        }
    }
}
