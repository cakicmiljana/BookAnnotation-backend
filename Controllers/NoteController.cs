namespace backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.DTOs;

[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
    private BookDbContext Context { get; set; }

    public NoteController(BookDbContext context)
    {
        Context = context;
    }

    [HttpPost("AddNote")]
    public async Task<ActionResult> AddNote([FromBody] NoteDto noteDto)
    {
        try
        {
            Note note = new Note();
            User user = await Context.Users.FindAsync(noteDto.UserId);
            Version book = await Context.Versions.FindAsync(noteDto.BookId);

            if (book != null && user != null)
            {
                note.BookId = noteDto.BookId;
                note.BookVersion = book;
                note.UserId = noteDto.UserId;
                note.User = user;
                note.Content= noteDto.Content;

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

    [HttpGet("GetNoteByVersionId/{id}")]
    public async Task<ActionResult> GetNoteByVersionId(int id)
    {
        try
        {
            var note = await Context.Notes
                .Where(n => n.BookId == id)
                .FirstOrDefaultAsync();

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

    [HttpPut("UpdateNote")]
    public async Task<ActionResult> UpdateUser([FromBody] NoteDto noteDto)
    {
        try
        {
            var note = await Context.Notes!.FindAsync(noteDto.Id);

            if (note != null)
            {
                note.Content = noteDto.Content;

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