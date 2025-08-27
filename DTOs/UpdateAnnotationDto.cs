namespace backend.DTOs;
using System;
using System.Collections.Generic;

public class UpdateAnnotationDto
{
    public int Id { get; set; }
    public int StartOffset { get; set; }
    public int EndOffset { get; set; }
    public string Comment { get; set; }
    public string Tag { get; set; }
    public string Color { get; set; } = "lightblue";
}
