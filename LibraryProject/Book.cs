using System;
using System.Security.Cryptography.X509Certificates;

namespace LibraryProject
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsOnShelf { get; set; }
        public string DueDate { get; set; }

        public Book(string _title, string _author, bool _isOnShelf, string _dueDate)
        {
            Title = _title;
            Author = _author;
            IsOnShelf = _isOnShelf;
            DueDate = _dueDate;
        }

    }
}
