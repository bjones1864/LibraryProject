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
            SearchByTitle(bestSellers); 
            break; 
        case 3: SearchByAuthor(bestSellers);
            break;
        case 4:  
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