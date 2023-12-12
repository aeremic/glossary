using MediatR;

namespace Glossary.Service.Commands.GlossaryTerm.DeleteGlossaryTerm;

public class DeleteGlossaryTermCommand : IRequest<long>
{
    public long Id { get; init; }
}