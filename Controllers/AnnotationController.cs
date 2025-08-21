namespace backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using backend.DTOs;

[ApiController]
[Route("api/[controller]")]
public class AnnotationController : ControllerBase
{
    private BookDbContext Context { get; set; }

    public AnnotationController(BookDbContext context)
    {
        Context = context;
    }

    [HttpPost("AddAnnotation")]
    public async Task<ActionResult> AddAnnotation([FromBody] AnnotationDto dto)
    {
        try
        {
            Annotation annot = new Annotation();
            User user = await Context.Users.FindAsync(dto.UserId);
            Version version = await Context.Versions.FindAsync(dto.BookId);

            if (version != null && user != null)
            {
                annot.BookId = dto.BookId;
                annot.BookVersion = version;
                annot.UserId = dto.UserId;
                annot.User = user;
                annot.StartOffset = dto.StartOffset;
                annot.EndOffset = dto.EndOffset;
                annot.Comment = dto.Comment;
                annot.Tag = dto.Tag;
                annot.Color = dto.Color ?? "lightblue";

                await Context.Annotations.AddAsync(annot);
                await Context.SaveChangesAsync();
                return Ok(new { message = "Annotation added", id = annot.Id });
            }
            else
                return BadRequest("UNSUCCESSFUL.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException?.Message ?? ex.Message);
        }
    }

    [HttpGet("GetAnnotation/{id}")]
    public async Task<ActionResult> GetAnnotation(int id)
    {
        try
        {
            var annot = await Context.Annotations.FindAsync(id);

            if (annot != null)
                return Ok(annot);
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetAnnotationsByVersionId/{versionId}")]
    public async Task<ActionResult> GetAnnotationsByVersionId(int versionId)
    {
        try
        {
            var annotations = await Context.Annotations
                .Where(a => a.BookId == versionId)
                .ToListAsync();

            if (annotations != null)
                return Ok(annotations);
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("UpdateAnnotation/{id}/{start}/{end}/{comment}/{tag}")]
    public async Task<ActionResult> UpdateAnnotation(int id, int start, int end, string comment, string tag)
    {
        try
        {
            var annot = await Context.Annotations!.FindAsync(id);

            if (annot != null)
            {
                annot.StartOffset = start;
                annot.EndOffset = end;
                annot.Comment = comment;
                annot.Tag = tag;

                await Context.SaveChangesAsync();
                return Ok("Annotation updated successfully.");
            }
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("UpdateComment/{id}/{comment}")]
    public async Task<ActionResult> UpdateComment(int id, string comment)
    {
        try
        {
            var annot = await Context.Annotations!.FindAsync(id);

            if (annot != null)
            {
                annot.Comment = comment;

                await Context.SaveChangesAsync();
                return Ok("Annotation updated successfully.");
            }
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("UpdateTag/{id}/{tag}")]
    public async Task<ActionResult> UpdateTag(int id, string tag)
    {
        try
        {
            var annot = await Context.Annotations!.FindAsync(id);

            if (annot != null)
            {
                annot.Tag = tag;

                await Context.SaveChangesAsync();
                return Ok("Annotation updated successfully.");
            }
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("DeleteAnnotation/{id}")]
    public async Task<ActionResult> DeleteAnnotation(int id)
    {
        try
        {
            var annot = await Context.Annotations.FindAsync(id);

            if (annot != null)
            {
                Context.Annotations.Remove(annot);
                await Context.SaveChangesAsync();
                return Ok("Annotation deleted succesfully.");
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