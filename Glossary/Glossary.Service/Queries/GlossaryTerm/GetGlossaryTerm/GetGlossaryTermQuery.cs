using Glossary.Service.Common.Models;
using MediatR;

namespace Glossary.Service.Queries.GlossaryTerm.GetGlossaryTerm;

public class GetGlossaryTermQuery : IRequest<GlossaryTermDto>
{
    public long Id { get; init; }
}