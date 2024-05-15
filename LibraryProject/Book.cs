using System;
using System.Security.Cryptography.X509Certificates;

namespace LibraryProject
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsOnShelf { get; set; }
        public DateTime DueDate;

        public Book(string _title, string _author, bool _isOnShelf)
        {
            Title = _title;
            Author = _author;
            IsOnShelf = _isOnShelf;
        }

        public string OnShelf()
        {
            return IsOnShelf ? "On Shelf" : "Checked Out";
        }

        public void CheckOut()
        {
            DueDate = DateTime.Now.AddDays(14);
            IsOnShelf = false;
        }

        public void Return()
        {
            IsOnShelf = true;
        }

    }
}
