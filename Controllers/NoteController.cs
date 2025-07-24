namespace backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
    private BookDbContext Context { get; set; }

    public NoteController(BookDbContext context)
    {
        Context = context;
    }

    [HttpPost("AddNote/{bookId}/{userId}/{content}")]
    public async Task<ActionResult> AddNote(int bookId, int userId, string content)
    {
        try
        {
            Note note = new Note();
            User user = await Context.Users.FindAsync(userId);
            Version book = await Context.Versions.FindAsync(bookId);

            if (book != null && user != null)
            {
                note.BookId = bookId;
                note.BookVersion = book;
                note.UserId = userId;
                note.User = user;
                note.Content= content;

                await Context.Notes.AddAsync(note);
                await Context.SaveChangesAsync();
                return Ok($"Note added with id {note.Id}.");
            }
            else
                return BadRequest("UNSUCCESSFUL.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetNote/{id}")]
    public async Task<ActionResult> GetNote(int id)
    {
        try
        {
            var note = await Context.Notes.FindAsync(id);

            if (note != null)
                return Ok(note);
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("UpdateNote/{id}/{content}")]
    public async Task<ActionResult> UpdateUser(int id, string content)
    {
        try
        {
            var note = await Context.Notes!.FindAsync(id);

            if (note != null)
            {
                note.Content = content;

                await Context.SaveChangesAsync();
                return Ok("Note updated successfully.");
            }
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("DeleteNote/{id}")]
    public async Task<ActionResult> DeleteNote(int id)
    {
        try
        {
            var note = await Context.Notes.FindAsync(id);

            if (note != null)
            {
                Context.Notes.Remove(note);
                await Context.SaveChangesAsync();
                return Ok("Note deleted succesfully.");
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