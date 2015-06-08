using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class emptyErrorLabel : Form
    {
        List<Book> Library = new List<Book>();
        Book pusta = new Book("", "", 0, "", "", "");

        public void OnStart() //co będzie się działo na starcie programu
        { 
            HideEdition();
            HideButtons();
            FillBookDescription(pusta);
            bookYear.Text = ""; //żeby ładniej było, to zamiast wpisywać zero nie wpisujemy nic
            Library = Serializacja.Deserializacja();
        }

        public void HideButtons() //ukrywa buttony, których możemy użyć tylko, gdy mamy wyszukaną książkę
        {
            
            bookBookButton.Visible = false;
            unBookBookButton.Visible = false;
            deleteBookButton.Visible = false;
            editBookButton.Visible = false;
        }

        public void ShowButtons() //pokazuje buttony, których możemy użyć tylko, gdy mamy wyszukaną książkę
        {
            bookBookButton.Visible = true;
            unBookBookButton.Visible = true;
            deleteBookButton.Visible = true;
            editBookButton.Visible = true;
        }

        public void HideEdition() //metoda ukrywa pola służące do edycji/dodawania książki 
        {
            newName.Visible = false;
            newAuthor.Visible = false;
            newPublicationYear.Visible = false;
            newType.Visible = false;
            newLeanguage.Visible = false;
            newAvailability.Visible = false;
            addChangesButton.Visible = false;
            anulujChangesButton.Visible = false;
            errorLabel.Visible = false; //wyświetla jak użytkownik chce dodać niepełną książkę
            InvalidDataLabel.Visible = false; //wyświetla jak użytkownik chce dodać niepoprawną datę
        }

        public void ShowEdition() //metoda pokazuje pola służące do edycji/dodawania książki 
        {
            newName.Visible = true;
            newAuthor.Visible = true;
            newPublicationYear.Visible = true;
            newType.Visible = true;
            newLeanguage.Visible = true;
            newAvailability.Visible = true;
            addChangesButton.Visible = true;
            anulujChangesButton.Visible = true;
            errorLabel.Visible = false; //wyświetla jak użytkownik chce dodać niepełną książkę
            InvalidDataLabel.Visible = false; //wyświetla jak użytkownik chce dodać niepoprawną datę
        }

        public void FillEdition(Book book) //metoda uzupełniająca pola edycji przekazaną książką
        {
            newName.Text = book.Name;
            newAuthor.Text = book.Author;
            newPublicationYear.Text = book.PublicationYear.ToString();
            newType.Text = book.Type;
            newLeanguage.Text = book.Leanguage;
            newAvailability.Text = book.Avibility;
        }

        public bool EditBook(Book book) //metoda edytująca książkę
        {
            if ((newName.Text.Length != 0) &&
                (newAuthor.Text.Length != 0) &&
                (newPublicationYear.Text.Length != 0) &&
                (newType.Text.Length != 0) &&
                (newLeanguage.Text.Length != 0) &&
                (newAvailability.Text.Length != 0))
            {
                book.Name = newName.Text;
                book.Author = newAuthor.Text;

                try
                {
                    book.PublicationYear = Convert.ToInt32(newPublicationYear.Text);
                }
                catch (FormatException)
                {
                    InvalidDataLabel.Visible = true;
                    return false;
                }

                book.Type = newType.Text;
                book.Leanguage = newLeanguage.Text;
                book.Avibility = newAvailability.Text;
                errorLabel.Visible = false; //wyświetla jak użytkownik chce dodać niepełną książkę
                InvalidDataLabel.Visible = false; //wyświetla jak użytkownik chce dodać niepoprawną datę
                //if (newAvailability.Text == "Dostępna")
                //{
                //    bookAvailability.ForeColor = Color.Green;
                //}
                //else if (newAvailability.Text == "Niedostępna")
                //{
                //    bookAvailability.ForeColor = Color.Red;
                //}

                return true;
            }
            else
            {
                errorLabel.Visible = true; //wyświetla jak użytkownik chce dodać niepełną książkę
                InvalidDataLabel.Visible = false; //wyświetla jak użytkownik chce dodać niepoprawną datę
                return false;
            }
        }

        public void FillBookDescription(Book book) //metoda uzupełnia pola opisujące książkę
        {
            bookName.Text = book.Name;
            bookAutorName.Text = book.Author;
            bookYear.Text = book.PublicationYear.ToString();
            bookType.Text = book.Type;
            bookLeanguage.Text = book.Leanguage;
            bookAvailability.Text = book.Avibility;
        }

        public Book FindBook()
        {
            //Book result = new Book();
            foreach (var book in Library)
            {
                if (searchTextBox.Text == "")
                {
                    return pusta;
                }
                else if (book.Name.Contains(searchTextBox.Text))
                {
                    return book;
                }
            }
            //Book result = Library.Find(delegate(Book p)
            //    { return p.Name == searchTextBox.Text; });

            return null;
        }


        public emptyErrorLabel() //punkt startowy klasy
        {
            InitializeComponent();
            OnStart();
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e) //wyszukiwanie książki
        {
            Book book = FindBook();

            if ((book == pusta) || (book == null))
            {
                FillBookDescription(pusta); //uzupełnia pola książką domyślną
                HideButtons();
                bookYear.Text = ""; //żeby ładniej było, to zamiast wpisywać zero nie wpisujemy nic
            }
            else
            {
                FillBookDescription(book); //uzupełnia pola znalezioną książką
                ShowButtons();
            }
        }

        private void bookBookButton_Click(object sender, EventArgs e)
        {
            Book book = FindBook();

            book.Avibility = "Niedostępna";
            FillBookDescription(book);
            //bookAvailability.ForeColor = Color.Red;
        }

        private void unBookBookButton_Click(object sender, EventArgs e)
        {
            Book book = FindBook();

            book.Avibility = "Dostępna";
            FillBookDescription(book);
            //bookAvailability.ForeColor = Color.Green;
        }

        private void deleteBookButton_Click(object sender, EventArgs e)
        {
            Book book = FindBook();

            Library.Remove(book);
            FillBookDescription(pusta);
            bookYear.Text = ""; //żeby ładniej było, to zamiast wpisywać zero nie wpisujemy nic
            HideButtons();
            searchTextBox.Text = "";
        }

        private void addBookButton_Click(object sender, EventArgs e)
        {
            FillBookDescription(pusta);
            FillEdition(pusta);
            bookYear.Text = ""; //żeby ładniej było, to zamiast wpisywać zero nie wpisujemy nic
            newPublicationYear.Text = ""; //żeby ładniej było, to zamiast wpisywać zero nie wpisujemy nic
            newAvailability.Text = "Dostępna";
            HideButtons();
            ShowEdition();
            searchTextBox.Text = "";
            addBookButton.Visible = false;
        }

        private void anulujChangesButton_Click(object sender, EventArgs e)
        {
            HideEdition();
        }

        private void addChangesButton_Click(object sender, EventArgs e)
        {
            Book book = new Book();

            if (EditBook(book))
            {
                Library.Add(book);
                FillEdition(pusta);
                HideEdition();
                Serializacja.serialize(Library);
                searchTextBox.Text = "";
                addBookButton.Visible = true;
            }
        }

        private void editBookButton_Click(object sender, EventArgs e)
        {
            Book book = FindBook();

            FillBookDescription(pusta);
            bookYear.Text = ""; //żeby ładniej było, to zamiast wpisywać zero nie wpisujemy nic
            HideButtons();
            FillEdition(book);
            ShowEdition();
            Library.Remove(book);
            searchTextBox.Text = "";
        }
    }
}
