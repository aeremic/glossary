using AutoMapper;
using Glossary.Service.Common.Models;
using Glossary.Service.Domains;
using Glossary.Service.Infrastructure;
using Glossary.Service.Queries.GlossaryTerm.GetGlossaryTerm;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Glossary.UnitTests.Tests.GetGlossaryTerm;

[TestFixture]
public class GetGlossaryTermQueryHandlerTest
{
    [Test]
    public async Task GetTest()
    {
        var expected = new GlossaryTermDto();
        var mockMapper = new Mock<IMapper>();
        
        mockMapper.Setup(x => x.Map<GlossaryTerm, GlossaryTermDto>(It.IsAny<GlossaryTerm>()))
            .Returns(expected); 
        
        var options = new DbContextOptionsBuilder<Repository>()
            .UseInMemoryDatabase(databaseName: "glossary_InMemory_db")
            .Options;

        await using var context = new Repository(options);
        context.GlossaryTerms.Add(new() { Id = 1, Term = "Term 1", Definition = "Definition 1" });
        await context.SaveChangesAsync();
        
        var handler = new GetGlossaryTermQueryHandler(context, mockMapper.Object);
        var result = await handler.Handle(new GetGlossaryTermQuery { Id = 1 }, CancellationToken.None);
        
        result.ShouldBeOfType<GlossaryTermDto>();
    }
}