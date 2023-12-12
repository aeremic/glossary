namespace Glossary.Service.Common.Models;

public class GlossaryTermDto
{
    public long Id { get; set; }
    public string? Term { get; set; }
    public string? Definition { get; set; }
}