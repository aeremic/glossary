using Glossary.Service.Common.Models;
using MediatR;

namespace Glossary.Service.Queries.GlossaryTerm.GetAllGlossaryTerms;

public class GetAllGlossaryTermsQuery : IRequest<List<GlossaryTermDto>>
{
}