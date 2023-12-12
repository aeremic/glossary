using Glossary.Service.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Glossary.Service.Commands.GlossaryTerm.DeleteGlossaryTerm;

public class DeleteGlossaryTermCommandHandler : IRequestHandler<DeleteGlossaryTermCommand, long>
{
    #region Properties

    private readonly Repository _repository;
    private readonly Logger _logger;

    #endregion

    #region Constructors

    public DeleteGlossaryTermCommandHandler(Repository repository)
    {
        _repository = repository;
        _logger = LogManager.GetCurrentClassLogger();
    }

    #endregion

    #region Methods

    public async Task<long> Handle(DeleteGlossaryTermCommand request, CancellationToken cancellationToken)
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
            _repository.GlossaryTerms.Remove(glossaryTerm);
            await _repository.SaveChangesAsync(cancellationToken);

            result = glossaryTerm.Id;
        }
        catch (Exception ex)
        {
            // TODO: Log
            _logger.Error(ex);
        }

        return result;
    }

    #endregion
}