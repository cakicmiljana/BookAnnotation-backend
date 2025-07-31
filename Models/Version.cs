namespace backend.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Version
{
    public int Id { get; set; }
    [JsonIgnore]
    public string Content { get; set; } = string.Empty;
    public string FileType { get; set; }
    public string Language { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int UserId { get; set; } = -1;
    public User User { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    [JsonIgnore]
    public ICollection<Annotation> Annotations { get; set; }
    [JsonIgnore]
    public ICollection<Note> Notes { get; set; }
}