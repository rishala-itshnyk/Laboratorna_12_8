namespace Laboratorna_12_8.Tests;

[TestFixture]
public class Tests
{

    [Test]
    public void AddBook_ShouldAddBookToCatalog()
    {
        // Arrange
        var catalog = new LibraryCatalog();

        // Act
        catalog.AddBook("Author 1", "Book 1", 100);
        catalog.AddBook("Author 2", "Book 2", 200);

        // Assert
        Assert.NotNull(catalog.FindBook("Book 1"));
        Assert.NotNull(catalog.FindBook("Book 2"));
    }

    [Test]
    public void RemoveBook_ShouldRemoveBookFromCatalog()
    {
        // Arrange
        var catalog = new LibraryCatalog();
        catalog.AddBook("Author 1", "Book 1", 100);
        catalog.AddBook("Author 2", "Book 2", 200);

        // Act
        catalog.RemoveBook("Book 1");

        // Assert
        Assert.Null(catalog.FindBook("Book 1"));
        Assert.NotNull(catalog.FindBook("Book 2"));
    }

    [Test]
    public void FindBook_ShouldReturnBookFromCatalog()
    {
        // Arrange
        var catalog = new LibraryCatalog();
        catalog.AddBook("Author 1", "Book 1", 100);
        catalog.AddBook("Author 2", "Book 2", 200);

        // Act
        var foundBook = catalog.FindBook("Book 1");

        // Assert
        Assert.NotNull(foundBook);
        Assert.AreEqual("Author 1", foundBook.Author);
        Assert.AreEqual("Book 1", foundBook.Title);
        Assert.AreEqual(100, foundBook.Reviews);
    }
}