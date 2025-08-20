namespace backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using System.IO;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class VersionController : ControllerBase
{
    private BookDbContext Context { get; set; }

    public VersionController(BookDbContext context)
    {
        Context = context;
    }

    [HttpPost("AddVersion/{userId}/{bookId}/{fileType}/{language}/{content}")]
    public async Task<ActionResult> AddVersion(int userId, int bookId, string fileType, string language, string content)
    {
        try
        {
            Version version = new Version();
            User user = await Context.Users.FindAsync(userId);
            Book book = await Context.Books.FindAsync(bookId);

            if (book != null && user != null)
            {
                var utf8 = Encoding.UTF8.GetBytes(content);

                version.UserId = userId;
                version.BookId = bookId;
                version.FileType = fileType;
                version.Language = language;
                version.Content = Encoding.UTF8.GetString(utf8);
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

    [HttpGet("GetVersion/{id}")]
    public async Task<ActionResult> GetVersion(int id)
    {
        try
        {
            var version = await Context.Versions
                .Where(version => version.Id == id)
                .Include(version => version.Book)
                .FirstOrDefaultAsync();

            if (version != null)
                return Ok(version);
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetAllVersions")]
    public async Task<ActionResult> GetAllVersions()
    {
        try
        {
            var versions = await Context.Versions
                .Include(version => version.Book)
                .ToListAsync();

            return Ok(versions);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetVersionsByBookId/{bookId}")]
    public async Task<ActionResult> GetVersionsByBookId(int bookId)
    {
        try
        {
            var versions = await Context.Versions
                .Include(version => version.Book)
                .Where(version => version.Book.Id == bookId)
                .ToListAsync();

            if (versions != null)
                return Ok(versions);
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetVersionsByUserId/{userId}")]
    public async Task<ActionResult> GetVersionsByUserId(int userId)
    {
        try
        {
            var versions = await Context.Versions
                .Where(version => version.User.Id == userId)
                .ToListAsync();

            if (versions != null)
                return Ok(versions);
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetContentByVersionId/{id}")]
    public async Task<ActionResult> GetContentByVersionId(int id)
    {
        try
        {
            var content = await Context.Versions
                .Where(version => version.Id == id)
                .Select(version => version.Content)
                .FirstOrDefaultAsync();

            if (content != null)
                return Ok(Encoding.UTF8.GetBytes(content));
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("UpdateVersion/{id}/{fileType}/{language}")]
    public async Task<ActionResult> UpdateVersion(int id, string fileType, string language)
    {
        try
        {
            var version = await Context.Versions!.FindAsync(id);

            if (version != null)
            {
                version.FileType = fileType;
                version.Language = language;

                await Context.SaveChangesAsync();
                return Ok("Version updated successfully.");
            }
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("DeleteVersion/{id}")]
    public async Task<ActionResult> DeleteVersion(int id)
    {
        try
        {
            var version = await Context.Versions.FindAsync(id);

            if (version != null)
            {
                Context.Versions.Remove(version);
                await Context.SaveChangesAsync();
                return Ok("Version deleted succesfully.");
            }
            else
                return BadRequest("UNSUCCESSFUL.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetPage")]
    public async Task<ActionResult<string>> GetPage(int id, int page = 0, int pageSize = 10)
    {
        var content = await Context.Versions
            .Where(version => version.Id == id)
            .Select(version => version.Content)
            .FirstOrDefaultAsync();

        if (string.IsNullOrEmpty(content))
            return NotFound();

        var start = page * pageSize;
        if (start >= content.Length)
            return "";

        var pageText = content
            .Substring(start, Math.Min(pageSize, content.Length - start));

        return Ok(pageText);
    }

}