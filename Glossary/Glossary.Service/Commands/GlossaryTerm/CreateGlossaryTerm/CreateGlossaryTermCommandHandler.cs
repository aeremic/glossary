using Glossary.Service.Infrastructure;
using MediatR;

namespace Glossary.Service.Commands.GlossaryTerm.CreateGlossaryTerm;

public class CreateGlossaryTermCommandHandler : IRequestHandler<CreateGlossaryTermCommand, long>
{
    #region Properties

    private readonly Repository _repository;

    #endregion

    #region Constructors

    public CreateGlossaryTermCommandHandler(Repository repository)
    {
        _repository = repository;
    }

    #endregion

    #region Methods

    public async Task<long> Handle(CreateGlossaryTermCommand request, CancellationToken cancellationToken)
    {
        long result = default;

        var glossaryTerm = new Domains.GlossaryTerm
        {
            Term = request.Term,
            Definition = request.Definition
        };

        try
        {
            _repository.GlossaryTerms.Add(glossaryTerm);
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