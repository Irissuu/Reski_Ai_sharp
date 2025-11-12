namespace Reski.Application.DTO.Hateoas;

public record Link(string Rel, string Href, string Method = "GET");
