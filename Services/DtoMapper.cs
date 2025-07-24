using backend.Models;
using backend.DTOs;

public static class DtoMapper
{
    public static VersionDto ToDto(this backend.Models.Version version)
    {
        return new VersionDto
        {
            Id = version.Id,
            Content = version.Content,
            FileType = version.FileType,
            Language = version.Language,
            CreatedAt = version.CreatedAt,
            UserId = version.UserId,
            BookId = version.BookId
        };
    }

    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt
        };
    }

    public static NoteDto ToDto(this Note note)
    {
        return new NoteDto
        {
            Id = note.Id,
            BookId = note.BookId,
            UserId = note.UserId,
            Content = note.Content,
            CreatedAt = note.CreatedAt
        };
    }

    public static BookDto ToDto(this Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Country = book.Country,
            OriginalLanguage = book.OriginalLanguage,
            Pages = book.Pages,
            Description = book.Description,
            PublicationYear = book.PublicationYear
        };
    }

    public static AnnotationDto ToDto(this Annotation annotation)
    {
        return new AnnotationDto
        {
            Id = annotation.Id,
            BookId = annotation.BookId,
            UserId = annotation.UserId,
            StartOffset = annotation.StartOffset,
            EndOffset = annotation.EndOffset,
            Comment = annotation.Comment,
            Tag = annotation.Tag,
            CreatedAt = annotation.CreatedAt
        };
    }

    public static backend.Models.Version FromDto(this VersionDto dto)
    {
        return new backend.Models.Version
        {
            Id = dto.Id,
            Content = dto.Content,
            FileType = dto.FileType,
            Language = dto.Language,
            CreatedAt = dto.CreatedAt,
            BookId = dto.BookId,
            UserId = dto.UserId
        };
    }

    public static User FromDto(this UserDto dto)
    {
        return new User
        {
            Id = dto.Id,
            Username = dto.Username,
            Email = dto.Email,
            //PasswordHash = dto.PasswordHash,
            CreatedAt = dto.CreatedAt
        };
    }

    public static Note FromDto(this NoteDto dto)
    {
        return new Note
        {
            Id = dto.Id,
            BookId = dto.BookId,
            UserId = dto.UserId,
            Content = dto.Content,
            CreatedAt = dto.CreatedAt
        };
    }

    public static Book FromDto(this BookDto dto)
    {
        return new Book
        {
            Id = dto.Id,
            Title = dto.Title,
            Author = dto.Author,
            Country = dto.Country,
            OriginalLanguage = dto.OriginalLanguage,
            Pages = dto.Pages,
            Description = dto.Description,
            PublicationYear = dto.PublicationYear
        };
    }

    public static Annotation FromDto(this AnnotationDto dto)
    {
        return new Annotation
        {
            Id = dto.Id,
            BookId = dto.BookId,
            UserId = dto.UserId,
            StartOffset = dto.StartOffset,
            EndOffset = dto.EndOffset,
            Comment = dto.Comment,
            Tag = dto.Tag,
            CreatedAt = dto.CreatedAt
        };
    }
}
