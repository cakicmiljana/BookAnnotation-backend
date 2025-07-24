namespace backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private BookDbContext Context { get; set; }

    public BookController(BookDbContext context)
    {
        Context = context;
    }

    [HttpPost("AddBook/{title}/{author}/{country}/{language}/{pages}/{year}")]
    public async Task<ActionResult> AddBook(string title, string author, string country, string language, int pages, string description, string year)
    {
        try
        {
            Book book = new Book();
            book.Title = title;
            book.Author = author;
            book.Country = country;
            book.OriginalLanguage = language;
            book.Pages = pages;
            book.Description = description;
            book.PublicationYear = year;

            await Context.Books.AddAsync(book);
            await Context.SaveChangesAsync();
            return Ok($"Book added with id {book.Id}.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetBook/{id}")]
    public async Task<ActionResult> GetBook(int id)
    {
        try
        {
            var book = await Context.Books.FindAsync(id);

            if (book != null)
                return Ok(book);
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("UpdateBook/{id}/{title}/{author}/{country}/{language}/{pages}/{year}")]
    public async Task<ActionResult> UpdateBook(int id, string title, string author, string country, string language, int pages, string description, string year)
    {
        try
        {
            var book = await Context.Books!.FindAsync(id);

            if (book != null)
            {
                book.Title = title;
                book.Author = author;
                book.Country = country;
                book.OriginalLanguage = language;
                book.Pages = pages;
                book.Description = description;
                book.PublicationYear = year;

                await Context.SaveChangesAsync();
                return Ok("Book updated successfully.");
            }
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("DeleteBook/{id}")]
    public async Task<ActionResult> DeleteBook(int id)
    {
        try
        {
            var book = await Context.Books.FindAsync(id);

            if (book != null)
            {
                Context.Books.Remove(book);
                await Context.SaveChangesAsync();
                return Ok("Book deleted successfully.");
            }
            else
                return BadRequest("UNSUCCESSFUL.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}