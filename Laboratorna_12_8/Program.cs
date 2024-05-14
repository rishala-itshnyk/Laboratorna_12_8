using System;
using System.IO;

public class Book
{
    public string Author { get; set; }
    public string Title { get; set; }
    public int Reviews { get; set; }
    public Book Previous { get; set; }
    public Book Next { get; set; }
}

public class LibraryCatalog
{
    private Book head;
    private Book tail;

    public void AddBook(string author, string title, int reviews)
    {
        Book newBook = new Book
        {
            Author = author,
            Title = title,
            Reviews = reviews,
            Previous = null,
            Next = null
        };

        if (head == null)
        {
            head = newBook;
            tail = newBook;
        }
        else
        {
            tail.Next = newBook;
            newBook.Previous = tail;
            tail = newBook;
        }
    }

    public void PrintCatalog()
    {
        Book current = head;
        while (current != null)
        {
            Console.WriteLine($"{current.Author}, \"{current.Title}\", Reviews: {current.Reviews}");
            current = current.Next;
        }
    }

    public void RemoveBook(string title)
    {
        Book current = head;
        while (current != null)
        {
            if (current.Title == title)
            {
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    head = current.Next;
                }

                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    tail = current.Previous;
                }
                return;
            }
            current = current.Next;
        }
        Console.WriteLine($"Book \"{title}\" not found.");
    }

    public Book FindBook(string title)
    {
        Book current = head;
        while (current != null)
        {
            if (current.Title == title)
            {
                return current;
            }
            current = current.Next;
        }
        return null;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        LibraryCatalog catalog = new LibraryCatalog();

        // Зчитування даних з файлу та додавання книг до каталогу
        ReadBooksFromFile("books.txt", catalog);

        // Головне меню
        ShowMainMenu(catalog);
    }

    static void ShowMainMenu(LibraryCatalog catalog)
    {
        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. View Catalog");
            Console.WriteLine("2. Add Book");
            Console.WriteLine("3. Remove Book");
            Console.WriteLine("4. Search Book");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\nLibrary Catalog:");
                    catalog.PrintCatalog();
                    break;
                case "2":
                    AddBookMenu(catalog);
                    break;
                case "3":
                    RemoveBookMenu(catalog);
                    break;
                case "4":
                    SearchBookMenu(catalog);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void AddBookMenu(LibraryCatalog catalog)
    {
        Console.WriteLine("\nAdd Book:");
        Console.Write("Enter author: ");
        string author = Console.ReadLine();
        Console.Write("Enter title: ");
        string title = Console.ReadLine();
        Console.Write("Enter number of reviews: ");
        int reviews = int.Parse(Console.ReadLine());

        catalog.AddBook(author, title, reviews);
        Console.WriteLine("Book added to catalog.");
    }

    static void RemoveBookMenu(LibraryCatalog catalog)
    {
        Console.WriteLine("\nRemove Book:");
        Console.Write("Enter title of the book to remove: ");
        string title = Console.ReadLine();

        catalog.RemoveBook(title);
    }

    static void SearchBookMenu(LibraryCatalog catalog)
    {
        Console.WriteLine("\nSearch Book:");
        Console.Write("Enter title of the book to search: ");
        string title = Console.ReadLine();

        Book foundBook = catalog.FindBook(title);
        if (foundBook != null)
        {
            Console.WriteLine($"Book found: {foundBook.Author}, \"{foundBook.Title}\", Reviews: {foundBook.Reviews}");
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
    }

    static void ReadBooksFromFile(string filename, LibraryCatalog catalog)
    {
        using (StreamReader file = new StreamReader(filename))
        {
            string line;
            while ((line = file.ReadLine()) != null)
            {
                string[] parts = line.Split(' ');
                if (parts.Length < 3)
                    continue;

                string author = parts[0];
                string title = string.Join(" ", parts, 1, parts.Length - 2);
                int reviews = int.Parse(parts[parts.Length - 1]);

                catalog.AddBook(author, title, reviews);
            }
        }
    }
}