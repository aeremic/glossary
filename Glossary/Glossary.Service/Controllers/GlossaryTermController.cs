using Glossary.Service.Commands.GlossaryTerm.CreateGlossaryTerm;
using Glossary.Service.Commands.GlossaryTerm.DeleteGlossaryTerm;
using Glossary.Service.Commands.GlossaryTerm.UpdateGlossaryTerm;
using Glossary.Service.Common.Models;
using Glossary.Service.Queries.GlossaryTerm.GetAllGlossaryTerms;
using Glossary.Service.Queries.GlossaryTerm.GetGlossaryTerm;
using Glossary.Service.Queries.GlossaryTerm.GetSortedGlossaryTerms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Glossary.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GlossaryTermController : Controller
{
    #region Properties

    private readonly IMediator _mediator;

    #endregion

    #region Constructors

    public GlossaryTermController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Endpoint for getting glossary term by identifier.
    /// </summary>
    /// <param name="id">Glossary term identifier.</param>
    /// <returns>Glossary term data transfer object.</returns>
    [HttpGet("{id:long}")]
    public async Task<ActionResult<GlossaryTermDto>> GetGlossaryTerm(long id)
    {
        return await _mediator.Send(new GetGlossaryTermQuery { Id = id });
    }

    /// <summary>
    /// Endpoint for getting all available glossary terms.
    /// </summary>
    /// <returns>List of glossary terms.</returns>
    [HttpGet]
    public async Task<ActionResult<List<GlossaryTermDto>>> GetAllGlossaryTerms()
    {
        return await _mediator.Send(new GetAllGlossaryTermsQuery());
    }

    /// <summary>
    /// Endpoint for inserting glossary term into database.
    /// </summary>
    /// <param name="command">Command containing necessary glossary term values.</param>
    /// <returns>Identifier of inserted entity.</returns>
    [HttpPost]
    public async Task<ActionResult<long>> CreateGlossaryTerm(
        [FromBody] CreateGlossaryTermCommand command)
    {
        return await _mediator.Send(command);
    }
    
    /// <summary>
    /// Endpoint for updating glossary term.
    /// </summary>
    /// <param name="command">Command containing necessary glossary term values.</param>
    /// <returns>Identifier of updated entity.</returns>
    [HttpPut]
    public async Task<ActionResult<long>> UpdateGlossaryTerm(
        [FromBody] UpdateGlossaryTermCommand command)
    {
        return await _mediator.Send(command);
    }

    /// <summary>
    /// Endpoint for deleting glossary term by identifier.
    /// </summary>
    /// <param name="id">Glossary term identifier.</param>
    /// <returns>Identifier of deleted entity.</returns>
    [HttpDelete("{id:long}")]
    public async Task<ActionResult<long>> DeleteGlossaryTerm(long id)
    {
        return await _mediator.Send(new DeleteGlossaryTermCommand { Id = id });
    }

    /// <summary>
    /// Endpoint for getting all available glossary terms, sorted alphabetically by term and definition.
    /// </summary>
    /// <returns>List of sorted glossary terms.</returns>
    [HttpGet("[action]")]
    public async Task<ActionResult<List<GlossaryTermDto>>> GetSortedGlossaryTerms()
    {
        return await _mediator.Send(new GetSortedGlossaryTermsQuery());
    }

    #endregion
}