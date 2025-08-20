namespace backend.Models;
using System;
using System.Collections.Generic;

public class Annotation
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Version BookVersion { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int StartOffset { get; set; }
    public int EndOffset { get; set; }
    public string Comment { get; set; }
    public string Tag { get; set; }
    public string Color { get; set; } = "lightblue";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}