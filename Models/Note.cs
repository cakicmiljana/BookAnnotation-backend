namespace backend.Models;
using System;
using System.Collections.Generic;

public class Note
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Version BookVersion { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}