using AutoMapper;
using Glossary.Service.Common.Models;
using Glossary.Service.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Glossary.Service.Queries.GlossaryTerm.GetAllGlossaryTerms;

public class GetAllGlossaryTermsQueryHandler : IRequestHandler<GetAllGlossaryTermsQuery, List<GlossaryTermDto>>
{
    #region Properties

    private readonly Repository _repository;
    private readonly IMapper _mapper;

    #endregion

    #region Constructors

    public GetAllGlossaryTermsQueryHandler(Repository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    #endregion

    #region Methods

    public async Task<List<GlossaryTermDto>> Handle(GetAllGlossaryTermsQuery request,
        CancellationToken cancellationToken)
    {
        var result = new List<GlossaryTermDto>();
        try
        {
            var glossaryTerms = await _repository.GlossaryTerms
                .ToListAsync(cancellationToken);

            result = _mapper.Map<List<Domains.GlossaryTerm>, List<GlossaryTermDto>>(glossaryTerms);
        }
        catch (Exception ex)
        {
            // TODO: Log
        }

        return result;
    }

    #endregion
}