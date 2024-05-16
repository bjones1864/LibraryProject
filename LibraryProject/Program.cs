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

List<Book> bestSellers = new List<Book>();
StreamReader Reader = new StreamReader(filepath);
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
        bestSellers.Add(b);
    } 
} while (true);
Reader.Close();
Console.WriteLine("Welcome to Grand Circus Public Library!");

//bestSellers.Add(new Book("The Hunger Games", "Suzzanne Collins", true));
//bestSellers.Add(new Book("Station Eleven", "Emily St.John Mandel", true));
//bestSellers.Add(new Book("The Underground Railroad", "Colson Whitehead", true));
//bestSellers.Add(new Book("Pachinko", "Min Jin Lee", true));
//bestSellers.Add(new Book("The Song of Achilles", "Madeline Miller", true));
//bestSellers.Add(new Book("The Hate U Give", "Angie Thomas", true));
//bestSellers.Add(new Book("The Testaments", "Margaret Atwood", true));
//bestSellers.Add(new Book("The Night Circus", "Erin Morgenstern", true));
//bestSellers.Add(new Book("Educated", "Tara Westover", true));
//bestSellers.Add(new Book("Where the Crawdads Sing", "Delia Owens", true));
//bestSellers.Add(new Book("Circe", "Madeline Miller", true));
//bestSellers.Add(new Book("The Girl on the Train", "Paula Hawkins", true));
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
            DisplayBooks(SearchByTitle(bestSellers));
            break; 
        case 3:
            DisplayBooks(SearchByAuthor(bestSellers));
            break;
        case 4: 
            moneySaved += CheckOutbook(bestSellers);
            break;
        case 5: 
            ReturnBook(bestSellers);
            break;
        case 6:
            DonateBook(bestSellers);
            break;
        case 7:
            keepgoing = false; 
            break;
    }
    
    
} while (keepgoing); 

Console.WriteLine($"Thank you for using our library!  You saved ${moneySaved} today by checking out books instead of buying them!");
Console.WriteLine("Support your local library!");

StreamWriter writer2 = new StreamWriter(filepath);
foreach (Book b in bestSellers)
{ 
    writer2.WriteLine($"{b.Title}|{b.Author}|{b.IsOnShelf.ToString()}|{b.RetailPrice}");
    
} 
writer2.Close();
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
static void DisplayBook( Book SingleBook)
{
     
    if(SingleBook == null)
    {
        Console.WriteLine("Error, not found in system.");
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine(String.Format("{0, -40}{1, -40}{2, -40}",
            "Title", "Author", "Status"));
        Console.WriteLine(String.Format("{0, -40}{1, -40}{2, -40}",
            "----------------", "----------------", "----------------"));

        Console.WriteLine(String.Format("{0, -40}{1, -40}{2, -40}",
     MakeSmall(SingleBook.Title, 33), SingleBook.Author, SingleBook.OnShelf()));

        Console.WriteLine(String.Format("{0, -40}{1, -40}{2, -40}",
        "----------------", "----------------", "----------------"));
        Console.WriteLine();
    }
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

static int GetUserChoice()
{
   
    Console.WriteLine("1. Display all books.");
    Console.WriteLine("2. Search by title.");
    Console.WriteLine("3. Search by author.");
    Console.WriteLine("4. Check out book.");
    Console.WriteLine("5. Return book.");
    Console.WriteLine("6. Donate book.");
    Console.WriteLine("7. Exit.");
    int choice;
    while(!int.TryParse(Console.ReadLine().Trim(), out choice) || choice < 1 || choice > 7)
    {
        
        Console.WriteLine("Invalid input."); 
        
    }

    return choice;
}
static List<Book> SearchByAuthor(List<Book> bookList)
{
    List<Book> newList = new List<Book>();
    Console.WriteLine("Please enter the author of the book you are looking for");
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
    Console.WriteLine("Please enter the exact name of the book you would like to check out");
    string name = Console.ReadLine().Trim();
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
        return 0;
    }
    else if (!userChoice.IsOnShelf)
    {

        Console.WriteLine($"Sorry, {userChoice.Title} is already checked out.");
        return 0;
    }
    else
    {
        userChoice.CheckOut();
        Console.WriteLine($"{userChoice.Title} is checked out. Your due date is {userChoice.DueDate} ");
        return userChoice.RetailPrice;
    }
}

static void ReturnBook(List<Book>bookList)
{
    Console.WriteLine("Please enter the exact name of the book you would like to return");
    string name = Console.ReadLine().Trim();
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
    }
    else if(userChoice.IsOnShelf) 
    {
        Console.WriteLine($"{userChoice.Title}is already checked in.");

    }
    else
    {
        userChoice.Return();
        Console.WriteLine($"Thank you for returning {userChoice.Title}");
    }

}

static void DonateBook(List<Book> booklist)
{
    Console.WriteLine("Please enter the name of the book you are donating");
    string title = Console.ReadLine().Trim();
    Console.WriteLine("Please enter the author's name of the book you are donating");
    string author = Console.ReadLine().Trim();
    Console.WriteLine("Please enter the retail price of the book you are donating");
    double price;
    while(!double.TryParse(Console.ReadLine(), out price))
    {
        Console.WriteLine("Price must be a number");
    }

    booklist.Add(new Book(title, author, true, price));

    Console.WriteLine($"Thank you for donating {title}!");
}

static string MakeSmall(string str, int maxChars)
{
    return str.Length <= maxChars ? str : str.Substring(0, maxChars) + "...";
}