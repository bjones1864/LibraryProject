using LibraryProject;
using static System.Collections.Specialized.BitVector32;
using System.Reflection.Metadata;

string filepath = "../../../books.txt";
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
    Console.WriteLine("Please choose what you would like to do.");
    int userChoice = GetUserChoice();
    switch (userChoice)
    {
        case 1:
            DisplayBooks(bestSellers);
            break;
        case 2:
            DisplayBook(SearchByTitle(bestSellers));
            break; 
        case 3: DisplayBook(SearchByAuthor(bestSellers));
            break;
        case 4:  CheckOutbook(bestSellers);
            break;
        case 5: ReturnBook(bestSellers);
            break;
    }
    
    
} while (true); 


static void DisplayBooks(List<Book> bookList)
{
    foreach(Book b in bookList)
    {
        Console.WriteLine(String.Format("{0, -40}{1, -40}{2, -40}",
            b.Title, b.Author, b.OnShelf()));
    }
}
static void DisplayBook( Book SingleBook)
{
     
    if(SingleBook == null)
    {
        Console.WriteLine("Error, not found in system.");
    }
    else
    {
        Console.WriteLine(String.Format("{0, -40}{1, -40}{2, -40}",
     SingleBook.Title, SingleBook.Author, SingleBook.OnShelf()));
    }
    }

    static Book SearchByTitle(List<Book> bookList)
{
    Console.WriteLine("Please enter the title of the book.");
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

static int GetUserChoice()
{
   
    Console.WriteLine("1. Display all books.");
    Console.WriteLine("2. Search by title.");
    Console.WriteLine("3. Search by author.");
    Console.WriteLine("4. Check out book.");
    Console.WriteLine("5. Return book.");
    int choice;
    while(!int.TryParse(Console.ReadLine().Trim(), out choice) || choice < 1 || choice > 5)
    {
        
        Console.WriteLine("Invalid input."); 
        
    }

    return choice;
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

static void CheckOutbook(List<Book> bookList)
    {
    Book bookB = SearchByTitle(bookList);
    if(bookB == null) 
    {
        Console.WriteLine("Sorry we don't have that book.");
    }
    else if ( !bookB.IsOnShelf)
    {

        Console.WriteLine($"Sorry, {bookB.Title} is already checked out.");
    }
    else
    {
        bookB.CheckOut();
        Console.WriteLine($"{bookB.Title} is checked out. Your due date is {bookB.DueDate} ");
    }
    }

static void ReturnBook(List<Book>booklist)
    {
    Book Bookc = SearchByTitle(booklist);
    if (Bookc == null)
    {
        Console.WriteLine("Sorry, that book is not apart of our inventory.");
    }
    else if(Bookc.IsOnShelf) 
    {
        Console.WriteLine($"{Bookc.Title}is already checked in.");

    }
    else
    {
        Bookc.Return();
        Console.WriteLine($"Thank you for returning {Bookc.Title}");
    }
   
}

