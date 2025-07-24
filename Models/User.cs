namespace backend.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [JsonIgnore]
    public ICollection<Version> UploadedBooks { get; set; }
    [JsonIgnore] 
    public ICollection<Annotation> Annotations { get; set; }
    [JsonIgnore]
    public ICollection<Note> Notes { get; set; }
}
