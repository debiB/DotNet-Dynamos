using System;
using System.Collections.Generic;

public class Book {
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string ISBN { get; private set; }
    public int PublicationYear { get; private set; }

    public Book(string title, string author, string isbn, int publicationYear) {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
    }
}

public class MediaItem {
    public string Title { get; private set; }
    public string MediaType { get; private set; }
    public int Duration { get; private set; }
    
    public MediaItem(string title, string mediaType, int duration) {
        Title = title;
        MediaType = mediaType;
        Duration = duration;
    }
}

public class Library {
    private string Name { get; }
    private string Address { get; }
    private List<Book> Books { get; }
    private List<MediaItem> MediaItems { get; }

    public Library(string name, string address) {
        Name = name;
        Address = address;
        Books = new List<Book>();
        MediaItems = new List<MediaItem>();
    }

    public void AddBook(Book book) {
        Books.Add(book);
    }

    public void RemoveBook(Book book) {
        Books.Remove(book);
    }

    public void AddMediaItem(MediaItem mediaItem) {
        MediaItems.Add(mediaItem);
    }

    public void RemoveMediaItem(MediaItem mediaItem) {
        MediaItems.Remove(mediaItem);
    }

    public void PrintCatalog() {
        Console.WriteLine($"Catalog for {Name} at {Address}:");
        Console.WriteLine("Books:");
        foreach (var book in Books) {
            Console.WriteLine($"Title - {book.Title}, Author - {book.Author}, ISBN - {book.ISBN}, Publication year - {book.PublicationYear}");
        }

        Console.WriteLine("Media Items:");
        foreach (var mediaItem in MediaItems) {
            Console.WriteLine($"Title - {mediaItem.Title}, MediaType - {mediaItem.MediaType}, Duration - {mediaItem.Duration}");
        }
    }

    // Optional search feature
    public void SearchCatalog(string searchTerm) {
        Console.WriteLine($"Search results for '{searchTerm}':");

    
    Console.WriteLine("Books:");
    foreach (var book in Books) {
        if (book.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            book.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            book.ISBN.Equals(searchTerm, StringComparison.OrdinalIgnoreCase)) {
            Console.WriteLine($"Title - {book.Title}, Author - {book.Author}, ISBN - {book.ISBN}, Publication year - {book.PublicationYear}");
        }
    }

    
    Console.WriteLine("Media Items:");
    foreach (var mediaItem in MediaItems) {
        if (mediaItem.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            mediaItem.MediaType.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) {
            Console.WriteLine($"Title - {mediaItem.Title}, MediaType - {mediaItem.MediaType}, Duration - {mediaItem.Duration}");
        }
    }
    }
}

class Program {
    static void Main() {
        Console.WriteLine("Enter the name of the library: ");
        string libraryName = Console.ReadLine();
        Console.WriteLine("Enter the address of the library: ");
        string libraryAddress = Console.ReadLine();
        Library library = new Library(libraryName, libraryAddress);

        Console.WriteLine("Enter the number of books: ");
        int numBooks = int.Parse(Console.ReadLine());
        for (int i = 0; i < numBooks; i++) {
            Console.WriteLine("Enter a book title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Enter a book author: ");
            string author = Console.ReadLine();
            Console.WriteLine("Enter a book ISBN: ");
            string isbn = Console.ReadLine();
            Console.WriteLine("Enter a book publication year: ");
            int publicationYear = int.Parse(Console.ReadLine()); 
            Book book = new Book(title, author, isbn, publicationYear);
            library.AddBook(book);
        }

        Console.WriteLine("Enter the number of media items: ");
        int numMediaItems = int.Parse(Console.ReadLine());
        for (int j = 0; j < numMediaItems; j++) {
            Console.WriteLine("Enter a media title: ");
            string mediaTitle = Console.ReadLine();
            Console.WriteLine("Enter a media type: ");
            string mediaType = Console.ReadLine();
            Console.WriteLine("Enter media duration: ");
            int duration = int.Parse(Console.ReadLine());
            MediaItem mediaItem = new MediaItem(mediaTitle, mediaType, duration);
            library.AddMediaItem(mediaItem);
        }

        library.PrintCatalog();

        Console.WriteLine("Enter a search term: ");
        string searchTerm = Console.ReadLine();
        library.SearchCatalog(searchTerm);
    }
}
