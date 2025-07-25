namespace backend.DTOs;
using System;
using System.Collections.Generic;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; } // ???
}
