namespace backend.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using backend.Data;
using backend.Models;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private BookDbContext Context {  get; set; }

    public FileController(BookDbContext context)
    {
        Context = context;
    }

    [HttpPost("UploadPdf/{file}/{userId}/{bookId}/{fileType}/{language}")]
    public async Task<IActionResult> UploadPdf(IFormFile file, int userId, int bookId, string fileType, string language)
    {
        if (file == null || file.Length == 0 || !file.FileName.EndsWith(".pdf"))
            return BadRequest("Invalid PDF file.");

        try
        {
            Version version = new Version();
            User user = await Context.Users.FindAsync(userId);
            Book book = await Context.Books.FindAsync(bookId);

            if (book != null && user != null)
            {


                using var stream = file.OpenReadStream();

                string text = ExtractTextFromPdf(stream);
                var utf8 = Encoding.UTF8.GetBytes(text);

                version.UserId = userId;
                version.BookId = bookId;
                version.FileType = fileType;
                version.Language = language;
                version.Content = Encoding.UTF8.GetString(utf8);
                //version.Content = text;
                version.User = user;
                version.Book = book;
                await Context.Versions.AddAsync(version);
                await Context.SaveChangesAsync();
                return Ok($"Version added with id {version.Id}.");
            }
            else
                return BadRequest("UNSUCCESSFUL.");

        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }

    private string ExtractTextFromPdf(Stream filePath)
    {
        var sb = new StringBuilder();

        using (var document = PdfDocument.Open(filePath))
        {
            foreach (Page page in document.GetPages())
            {
                document.
                var words = page.GetWords();

                // Group words by line (roughly same Y coordinate)
                var lines = words
                    .GroupBy(w => Math.Round(w.BoundingBox.Bottom, 1)) // adjust precision if needed
                    .OrderByDescending(g => g.Key); // higher Y is higher on the page

                foreach (var line in lines)
                {
                    var orderedWords = line.OrderBy(w => w.BoundingBox.Left);

                    foreach (var word in orderedWords)
                    {
                        sb.Append(word.Text + " ");
                    }

                    sb.AppendLine(); // new line after each grouped Y-row
                }

                sb.AppendLine(); // new line between pages
            }
        }

        return sb.ToString();
    }
}
