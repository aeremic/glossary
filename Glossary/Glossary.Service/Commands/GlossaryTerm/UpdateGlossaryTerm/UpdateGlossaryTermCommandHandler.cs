using Glossary.Service.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Glossary.Service.Commands.GlossaryTerm.UpdateGlossaryTerm;

public class UpdateGlossaryTermCommandHandler : IRequestHandler<UpdateGlossaryTermCommand, long>
{
    #region Properties

    private readonly Repository _repository;

    #endregion

    #region Constructors

    public UpdateGlossaryTermCommandHandler(Repository repository)
    {
        _repository = repository;
    }

    #endregion

    #region Methods

    public async Task<long> Handle(UpdateGlossaryTermCommand request, CancellationToken cancellationToken)
    {
        long result = default;

        var glossaryTerm = await _repository.GlossaryTerms.Where(glossaryTerm => glossaryTerm.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (glossaryTerm == null)
        {
            return result;
        }   

        try
        {
            glossaryTerm.Term = request.Term;
            glossaryTerm.Definition = request.Definition;
            
            await _repository.SaveChangesAsync(cancellationToken);

            result = glossaryTerm.Id;
        }
        catch (Exception ex)
        {
            // TODO: Log
        }

        return result;
    }

    #endregion
}