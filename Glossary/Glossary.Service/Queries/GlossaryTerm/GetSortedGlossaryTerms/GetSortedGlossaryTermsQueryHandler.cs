using AutoMapper;
using Glossary.Service.Common.Models;
using Glossary.Service.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Glossary.Service.Queries.GlossaryTerm.GetSortedGlossaryTerms
{
    public class
        GetSortedGlossaryTermsQueryHandler : IRequestHandler<GetSortedGlossaryTermsQuery, List<GlossaryTermDto>>
    {
        #region Properties

        private readonly Repository _repository;
        private readonly IMapper _mapper;
        private readonly Logger _logger;

        #endregion

        #region Constructors

        public GetSortedGlossaryTermsQueryHandler(Repository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = LogManager.GetCurrentClassLogger();
        }

        #endregion

        #region Methods

        public async Task<List<GlossaryTermDto>> Handle(GetSortedGlossaryTermsQuery request,
            CancellationToken cancellationToken)
        {
            var result = new List<GlossaryTermDto>();
            try
            {
                var glossaryTerms = await _repository.GlossaryTerms
                    .OrderBy(glossaryTerm => glossaryTerm.Term)
                    .ThenBy(glossaryTerm => glossaryTerm.Definition)
                    .ToListAsync(cancellationToken);

                result = _mapper.Map<List<Domains.GlossaryTerm>, List<GlossaryTermDto>>(glossaryTerms);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return result;
        }

        #endregion
    }
}