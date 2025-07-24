namespace backend.DTOs;
using System;
using System.Collections.Generic;

public class AnnotationDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public int StartOffset { get; set; }
    public int EndOffset { get; set; }
    public string Comment { get; set; }
    public string Tag { get; set; }
    public DateTime CreatedAt { get; set; }
}
