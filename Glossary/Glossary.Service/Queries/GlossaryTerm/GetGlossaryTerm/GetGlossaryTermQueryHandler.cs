using AutoMapper;
using Glossary.Service.Common.Models;
using Glossary.Service.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Glossary.Service.Queries.GlossaryTerm.GetGlossaryTerm;

public class GetGlossaryTermQueryHandler : IRequestHandler<GetGlossaryTermQuery, GlossaryTermDto>
{
    #region Properties

    private readonly Repository _repository;
    private readonly IMapper _mapper;
    private readonly Logger _logger;

    #endregion

    #region Constructors

    public GetGlossaryTermQueryHandler(Repository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = LogManager.GetCurrentClassLogger();
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
            _logger.Error(ex);
        }

        return result;
    }

    #endregion
}