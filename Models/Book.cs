namespace backend.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Country { get; set; }
    public string OriginalLanguage { get; set; }
    public int Pages { get; set; }
    public string Description { get; set; } = string.Empty;
    public string PublicationYear { get; set; }
    [JsonIgnore]
    public ICollection<Version> BookVersions { get; set; }
}