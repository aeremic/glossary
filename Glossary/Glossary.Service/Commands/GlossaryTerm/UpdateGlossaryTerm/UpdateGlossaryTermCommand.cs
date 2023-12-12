using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Glossary.Service.Commands.GlossaryTerm.UpdateGlossaryTerm
{
    public class UpdateGlossaryTermCommand : IRequest<long>
    {
        [Required] public required long Id { get; set; }
        [Required] public required string Term { get; set; }
        [Required] public required string Definition { get; set; }
    }
}