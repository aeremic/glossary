using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Glossary.Service.Commands.GlossaryTerm.CreateGlossaryTerm;

public class CreateGlossaryTermCommand : IRequest<long>
{
    [Required] public required string Term { get; set; }
    [Required] public required string Definition { get; set; }
}