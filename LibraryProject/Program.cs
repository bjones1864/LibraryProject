using LibraryProject;
using static System.Collections.Specialized.BitVector32;
using System.Reflection.Metadata;

Console.WriteLine("Welcome to Best Seller's Library!");

List<Book> bestSellers = new List<Book>();
bestSellers.Add(new Book("The Hunger Games", "Suzzanne Collins", true));
bestSellers.Add(new Book("Station Eleven", "Emily St.John Mandel", true));
bestSellers.Add(new Book("The Underground Railroad", "Colson Whitehead", true));
bestSellers.Add(new Book("Pachinko", "Min Jin Lee", true));
bestSellers.Add(new Book("The Song of Achilles", "Madeline Miller", true));
bestSellers.Add(new Book("The Hate U Give", "Angie Thomas", true));
bestSellers.Add(new Book("The Testaments", "Margaret Atwood", true));
bestSellers.Add(new Book("The Night Circus", "Erin Morgenstern", true));
bestSellers.Add(new Book("Educated", "Tara Westover", true));
bestSellers.Add(new Book("Where the Crawdads Sing", "Delia Owens", true));
bestSellers.Add(new Book("Circe", "Madeline Miller", true));
bestSellers.Add(new Book("The Girl on the Train", "Paula Hawkins", true));

do
{

} while (true);



static void DisplayBooks(List<Book> bookList)
{
    foreach(Book b in bookList)
    {
        Console.WriteLine(String.Format("{0, -40}{1, -40}{2, -40}",
            b.Title, b.Author, b.OnShelf()));
    }
}

static Book SearchByTitle(List<Book> bookList)
{
    Console.WriteLine("Please enter the title of the book you are looking for");
    string input = Console.ReadLine().Trim();
    foreach(Book b in bookList)
    {
        if(b.Title.ToLower().Contains(input.ToLower()))
        {
            return b;
        }
    }
    return null;
}

static Book SearchByAuthor(List<Book> bookList)
{
    Console.WriteLine("Please enter the author of the book you are looking for");
    string input = Console.ReadLine().Trim();
    foreach (Book b in bookList)
    {
        if (b.Author.ToLower().Contains(input.ToLower()))
        {
            return b;
        }
    }
    return null;
}