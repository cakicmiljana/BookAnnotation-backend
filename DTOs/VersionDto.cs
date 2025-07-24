namespace backend.DTOs;
using System;
using System.Collections.Generic;

public class VersionDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string FileType { get; set; }
    public string Language { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
}
