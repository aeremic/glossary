using AutoMapper;
using Glossary.Service.Common.Models;
using Glossary.Service.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Glossary.Service.Queries.GlossaryTerm.GetSortedGlossaryTerms
{
    public class
        GetSortedGlossaryTermsQueryHandler : IRequestHandler<GetSortedGlossaryTermsQuery, List<GlossaryTermDto>>
    {
        #region Properties

        private readonly Repository _repository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public GetSortedGlossaryTermsQueryHandler(Repository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
                // TODO: Log
            }

            return result;
        }

        #endregion
    }
}