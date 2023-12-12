using AutoMapper;
using Glossary.Service.Common.Models;
using Glossary.Service.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Glossary.Service.Queries.GlossaryTerm.GetGlossaryTerm;

public class GetGlossaryTermQueryHandler : IRequestHandler<GetGlossaryTermQuery, GlossaryTermDto>
{
    #region Properties

    private readonly Repository _repository;
    private readonly IMapper _mapper;

    #endregion

    #region Constructors

    public GetGlossaryTermQueryHandler(Repository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    #endregion

    #region Methods

    public async Task<GlossaryTermDto> Handle(GetGlossaryTermQuery request, CancellationToken cancellationToken)
    {
        var result = new GlossaryTermDto();
        try
        {
            var glossaryTerm = await _repository.GlossaryTerms.Where(glossaryTerm => glossaryTerm.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            result = _mapper.Map<GlossaryTermDto>(glossaryTerm);
        }
        catch (Exception ex)
        {
            // TODO: Log
        }

        return result;
    }

    #endregion
}