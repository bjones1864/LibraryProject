using LibraryProject;
using static System.Collections.Specialized.BitVector32;
using System.Reflection.Metadata;
using System.Diagnostics;
using System.Threading.Channels;

string filepath = "../../../books.txt";
if (!File.Exists(filepath))
{
    StreamWriter writer = new StreamWriter(filepath);
    writer.WriteLine("The Hunger Games|Suzzanne Collins|true|9.79");
    writer.WriteLine("Station Eleven|Emily St.John Mandel|true|11.01");
    writer.WriteLine("The Underground Railroad|Colson Whitehead|true|7.38");
    writer.WriteLine("Pachinko|Min Jin Lee|true|12.79");
    writer.WriteLine("The Song of Achilles|Madeline Miller|true|13.48");
    writer.WriteLine("The Hate U Give|Angie Thomas|true|10.20");
    writer.WriteLine("The Testaments|Margaret Atwood|true|8.00");
    writer.WriteLine("The Night Circus|Erin Morgenstern|true|13.40");
    writer.WriteLine("Educated|Tara Westover|true|12.35");
    writer.WriteLine("Where the Crawdads Sing|Delia Owens|true|13.00");
    writer.WriteLine("Circe|Madeline Miller|true|13.00");
    writer.WriteLine("The Girl on the Train|Paula Hawkins|true|9.99");
    writer.Close(); 
}

string movieFilepath = "../../../movies.txt";
if(!File.Exists(movieFilepath))
{
    StreamWriter writer = new StreamWriter(movieFilepath);
    writer.WriteLine("The Avengers|Joss Whedon|true|9.84");
    writer.WriteLine("The Dark Knight Rises|Christopher Nolan|true|9.99");
    writer.WriteLine("Frozen|Chris Buck and Jennifer Lee|true|19.52");
    writer.WriteLine("Star Wars: The Force Awakens|J.J. Abrams|true|13.99");
    writer.WriteLine("Guardians of the Galaxy|James Gunn|true|13.99");
    writer.WriteLine("Spider-Man: Into the Spider-Verse|Bob Persichetti|true|10.59");
    writer.WriteLine("Black Panther|Ryan Coogler|true|13.59");
    writer.WriteLine("Dune|Denis Villeneuve|true|9.96");
    writer.Close();
}

List<Book> bestSellers = ListBuilder(filepath);
List<Book> movieList = ListBuilder(movieFilepath);


Console.WriteLine("Welcome to Grand Circus Public Library!");
bool keepgoing = true;
double moneySaved = 0;
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
            DisplayBooks(movieList);
            break;
        case 3:
            DisplayBooks(SearchByTitle(bestSellers));
            break;
        case 4:
            DisplayBooks(SearchByMovieTitle(movieList));
            break;
        case 5:
            DisplayBooks(SearchByAuthor(bestSellers));
            break;
        case 6:
            DisplayBooks(SearchByDirector(movieList));
            break;
        case 7: 
            moneySaved += CheckOutbook(bestSellers);
            break;
        case 8:
            moneySaved += CheckOutMovie(movieList);
            break;
        case 9: 
            ReturnBook(bestSellers);
            break;
        case 10:
            ReturnMovie(movieList);
            break;
        case 11:
            DonateBook(bestSellers);
            break;
        case 12:
            DonateMovie(movieList);
            break;
        case 13:
            BurnItDown(bestSellers, movieList);
            break;
        case 14:
            keepgoing = false; 
            break;
    }
    
    
} while (keepgoing); 

Console.WriteLine($"Thank you for using our library! You saved ${moneySaved} today by checking out books instead of buying them!");
Console.WriteLine("Support your local library!");

StreamWriter writer2 = new StreamWriter(filepath);
foreach (Book b in bestSellers)
{ 
    writer2.WriteLine($"{b.Title}|{b.Author}|{b.IsOnShelf.ToString()}|{b.RetailPrice}");
    
} 
writer2.Close();

StreamWriter writer3 = new StreamWriter(movieFilepath);
foreach (Book b in movieList)
{
    writer3.WriteLine($"{b.Title}|{b.Author}|{b.IsOnShelf.ToString()}|{b.RetailPrice}");

}
writer3.Close();

static List<Book> ListBuilder(string file)
{
    List<Book> l = new List<Book>();
    StreamReader Reader = new StreamReader(file);
    do
    {
        string line = Reader.ReadLine();
        if (line == null)
        {
            break;
        }
        else
        {
            string[] words = line.Split("|");
            Book b = new Book(words[0], words[1], bool.Parse(words[2]), double.Parse(words[3]));
            l.Add(b);
        }
    } while (true);
    Reader.Close();
    return l;
}


static void DisplayBooks(List<Book> bookList)
{
    Console.WriteLine();
    Console.WriteLine(String.Format("{0, -40}{1, -40}{2, -40}",
        "Title", "Author", "Status"));
    Console.WriteLine(String.Format("{0, -40}{1, -40}{2, -40}",
        "----------------", "----------------", "----------------"));

    foreach (Book b in bookList)
    {
        Console.WriteLine(String.Format("{0, -40}{1, -40}{2, -40}",
            MakeSmall(b.Title, 33), b.Author, b.OnShelf()));
    }

    Console.WriteLine(String.Format("{0, -40}{1, -40}{2, -40}",
        "----------------", "----------------", "----------------"));
    Console.WriteLine();
}
static List<Book> SearchByTitle(List<Book> bookList)
{
    List<Book> newList = new List<Book>();
    Console.WriteLine("Please enter the title of the book.");
    string input = Console.ReadLine().Trim();
    foreach(Book b in bookList)
    {
        if(b.Title.ToLower().Contains(input.ToLower()))
        {
            newList.Add(b);
        }
    }
    return newList;
}
static List<Book> SearchByMovieTitle(List<Book> bookList)
{
    List<Book> newList = new List<Book>();
    Console.WriteLine("Please enter the title of the movie.");
    string input = Console.ReadLine().Trim();
    foreach (Book b in bookList)
    {
        if (b.Title.ToLower().Contains(input.ToLower()))
        {
            newList.Add(b);
        }
    }
    return newList;
}

static int GetUserChoice()
{
   
    Console.WriteLine("1.  Display all books.");
    Console.WriteLine("2.  Display all movies.");
    Console.WriteLine("3.  Search by book title.");
    Console.WriteLine("4.  Search by movie title.");
    Console.WriteLine("5.  Search by book author.");
    Console.WriteLine("6.  Search by movie director.");
    Console.WriteLine("7.  Check out book.");
    Console.WriteLine("8.  Check out movie.");
    Console.WriteLine("9.  Return book.");
    Console.WriteLine("10. Return movie.");
    Console.WriteLine("11. Donate book.");
    Console.WriteLine("12. Donate movie.");
    Console.WriteLine("13. Farenheit 451 Mode!");
    Console.WriteLine("14. Exit.");
    int choice;
    while(!int.TryParse(Console.ReadLine().Trim(), out choice) || choice < 1 || choice > 14)
    {
        
        Console.WriteLine("Invalid input."); 
        
    }

    return choice;
}
static List<Book> SearchByAuthor(List<Book> bookList)
{
    List<Book> newList = new List<Book>();
    Console.WriteLine("Please enter the author of the book you are looking for:");
    string input = Console.ReadLine().Trim();
    foreach (Book b in bookList)
    {
        if (b.Author.ToLower().Contains(input.ToLower()))
        {
            newList.Add(b);
        }
    }
    return newList;
}
static List<Book> SearchByDirector(List<Book> bookList)
{
    List<Book> newList = new List<Book>();
    Console.WriteLine("Please enter the director of the movie you are looking for:");
    string input = Console.ReadLine().Trim();
    foreach (Book b in bookList)
    {
        if (b.Author.ToLower().Contains(input.ToLower()))
        {
            newList.Add(b);
        }
    }
    return newList;
}

static double CheckOutbook(List<Book> bookList)
{
    // Book bookB = SearchByTitle(bookList)
    Console.WriteLine("Please enter the exact name of the book you would like to check out:");
    string name = Console.ReadLine().Trim();
    Console.WriteLine();
    Book userChoice = new Book("1234567890ABCDEFG", "1234567890ABCDEFG", false, 0.00);
    foreach(Book b in bookList)
    {
        if(b.Title.Equals(name, StringComparison.OrdinalIgnoreCase))
        {
            userChoice = b;
        }
    }


    if(userChoice.Author.Equals("1234567890ABCDEFG")) 
    {
        Console.WriteLine("Sorry we don't have that book.");
        Console.WriteLine();
        return 0;
    }
    else if (!userChoice.IsOnShelf)
    {

        Console.WriteLine($"Sorry, {userChoice.Title} is already checked out.");
        Console.WriteLine();
        return 0;
    }
    else
    {
        userChoice.CheckOut();
        Console.WriteLine($"{userChoice.Title} is checked out. Your due date is {userChoice.DueDate}.");
        Console.WriteLine();
        return userChoice.RetailPrice;
    }
}
static double CheckOutMovie(List<Book> bookList)
{
    Console.WriteLine("Please enter the exact name of the movie you would like to check out:");
    string name = Console.ReadLine().Trim();
    Console.WriteLine();
    Book userChoice = new Book("1234567890ABCDEFG", "1234567890ABCDEFG", false, 0.00);
    foreach (Book b in bookList)
    {
        if (b.Title.Equals(name, StringComparison.OrdinalIgnoreCase))
        {
            userChoice = b;
        }
    }


    if (userChoice.Author.Equals("1234567890ABCDEFG"))
    {
        Console.WriteLine("Sorry we don't have that movie.");
        Console.WriteLine();
        return 0;
    }
    else if (!userChoice.IsOnShelf)
    {

        Console.WriteLine($"Sorry, {userChoice.Title} is already checked out.");
        Console.WriteLine();
        return 0;
    }
    else
    {
        userChoice.CheckOut();
        Console.WriteLine($"{userChoice.Title} is checked out. Your due date is {userChoice.DueDate}.");
        Console.WriteLine();
        return userChoice.RetailPrice;
    }
}


static void ReturnBook(List<Book>bookList)
{
    Console.WriteLine("Please enter the exact name of the book you would like to return:");
    string name = Console.ReadLine().Trim();
    Console.WriteLine();
    Book userChoice = new Book("1234567890ABCDEFG", "1234567890ABCDEFG", false, 0.00);
    foreach (Book b in bookList)
    {
        if (b.Title.Equals(name, StringComparison.OrdinalIgnoreCase))
        {
            userChoice = b;
        }
    }
    if (userChoice.Author.Equals("1234567890ABCDEFG"))
    {
        Console.WriteLine("Sorry, that book is not apart of our inventory.");
        Console.WriteLine();
    }
    else if(userChoice.IsOnShelf) 
    {
        Console.WriteLine($"{userChoice.Title}is already checked in.");
        Console.WriteLine();

    }
    else
    {
        userChoice.Return();
        Console.WriteLine($"Thank you for returning {userChoice.Title}.");
        Console.WriteLine();
    }

}
static void ReturnMovie(List<Book> bookList)
{
    Console.WriteLine("Please enter the exact name of the movie you would like to return:");
    string name = Console.ReadLine().Trim();
    Console.WriteLine();
    Book userChoice = new Book("1234567890ABCDEFG", "1234567890ABCDEFG", false, 0.00);
    foreach (Book b in bookList)
    {
        if (b.Title.Equals(name, StringComparison.OrdinalIgnoreCase))
        {
            userChoice = b;
        }
    }
    if (userChoice.Author.Equals("1234567890ABCDEFG"))
    {
        Console.WriteLine("Sorry, that movie is not apart of our inventory.");
        Console.WriteLine();
    }
    else if (userChoice.IsOnShelf)
    {
        Console.WriteLine($"{userChoice.Title}is already checked in.");
        Console.WriteLine();

    }
    else
    {
        userChoice.Return();
        Console.WriteLine($"Thank you for returning {userChoice.Title}.");
        Console.WriteLine();
    }

}
static void DonateBook(List<Book> booklist)
{
    Console.WriteLine("Please enter the name of the book you are donating:");
    string title = Console.ReadLine().Trim();
    Console.WriteLine("Please enter the author's name of the book you are donating:");
    string author = Console.ReadLine().Trim();
    Console.WriteLine("Please enter the retail price of the book you are donating:");
    double price;
    while(!double.TryParse(Console.ReadLine(), out price))
    {
        Console.WriteLine("Price must be a number.");
    }

    booklist.Add(new Book(title, author, true, price));

    Console.WriteLine($"Thank you for donating {title}!");
    Console.WriteLine();
}
static void DonateMovie(List<Book> booklist)
{
    Console.WriteLine("Please enter the name of the movie you are donating:");
    string title = Console.ReadLine().Trim();
    Console.WriteLine("Please enter the director's name of the movie you are donating:");
    string author = Console.ReadLine().Trim();
    Console.WriteLine("Please enter the retail price of the movie you are donating:");
    double price;
    while (!double.TryParse(Console.ReadLine(), out price))
    {
        Console.WriteLine("Price must be a number.");
    }

    booklist.Add(new Book(title, author, true, price));

    Console.WriteLine($"Thank you for donating {title}!");
    Console.WriteLine();
}

static string MakeSmall(string str, int maxChars)
{
    return str.Length <= maxChars ? str : str.Substring(0, maxChars) + "...";
}

static void BurnItDown(List<Book> booklist, List<Book> movielist)
{
    Random rnd = new Random();
    int smallList;

    if(booklist.Count > movielist.Count)
    {
        smallList = movielist.Count;
    }
    else
    {
        smallList = booklist.Count;
    }

    int redButton = rnd.Next(1, smallList);
    for (int i = 0; i < redButton; i++)
    {
        movielist.RemoveAt(rnd.Next(0, movielist.Count));
        booklist.RemoveAt(rnd.Next(0, booklist.Count));
    }
    Console.BackgroundColor = ConsoleColor.DarkRed;
    Console.Clear();
    Console.WriteLine("THE LIBRARY IS BURNING!!!");
    Console.WriteLine($"{redButton} books and {redButton} movies were lost to the flames!");
   
}