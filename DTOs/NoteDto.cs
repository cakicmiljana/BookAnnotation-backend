namespace backend.DTOs;
using System;
using System.Collections.Generic;

public class NoteDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
}
