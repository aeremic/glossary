using Glossary.Service.Common.Models;
using MediatR;

namespace Glossary.Service.Queries.GlossaryTerm.GetSortedGlossaryTerms;

public class GetSortedGlossaryTermsQuery : IRequest<List<GlossaryTermDto>>
{
}