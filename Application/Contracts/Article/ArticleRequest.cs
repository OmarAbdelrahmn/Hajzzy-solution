using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Article;

public record ArticleRequest
(
    [Length(3, 50)]
    [Required]
    string Name,
    [Required]
    [Length(3, 500)]
    string Description
    );