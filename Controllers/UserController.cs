namespace backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private BookDbContext Context { get; set; }

    public UserController(BookDbContext context)
    {
        Context = context;
    }

    [HttpPost("AddUser/{username}/{email}/{password}")]
    public async Task<ActionResult> AddUser(string username, string email, string password)
    {
        try
        {
            User user = new User();
            user.Username = username;
            user.Email = email;
            user.PasswordHash = password;

            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();
            return Ok($"User added with id {user.Id}.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetUser/{id}")]
    public async Task<ActionResult> GetUser(int id)
    {
        try
        {
            var user = await Context.Users.FindAsync(id);

            if (user != null)
                return Ok(user);
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Login/{username}/{password}")]
    public async Task<ActionResult> Login(string username, string password)
    {
        try
        {
            var user = await Context.Users
                .Where(u => u.Username == username && u.PasswordHash == password)
                .FirstOrDefaultAsync();

            if (user != null)
                return Ok(user);
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("UpdateUser/{id}/{username}/{email}/{password}")]
    public async Task<ActionResult> UpdateUser(int id, string username, string email, string password)
    {
        try
        {
            var user = await Context.Users!.FindAsync(id);

            if (user != null)
            {
                user.Username = username;
                user.Email = email;
                user.PasswordHash = password;

                await Context.SaveChangesAsync();
                return Ok(new { message = "User updated succesfully." });
            }
            else
                return BadRequest("UNSUCCESSFUL");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("DeleteUser/{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        try
        {
            var user = await Context.Users.FindAsync(id);

            if (user != null)
            {
                Context.Users.Remove(user);
                await Context.SaveChangesAsync();
                return Ok(new { message = "User deleted succesfully."});
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