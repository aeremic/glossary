using AutoMapper;
using Glossary.Service.Common.Models;
using Glossary.Service.Infrastructure;
using Glossary.Service.Mapper;
using Glossary.Service.Queries.GlossaryTerm.GetGlossaryTerm;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace Glossary.UnitTests.Queries.GlossaryTerm.GetGlossaryTerm;

[TestFixture]
public class GetGlossaryTermQueryHandlerTest
{
    [Test]
    public async Task GetTypeTest()
    {
        var dbContextOptions = new DbContextOptionsBuilder<Repository>()
            .UseInMemoryDatabase(databaseName: "glossary_db_InMemory")
            .Options;

        IMapper mapper = new Mapper(new MapperConfiguration(cfg => 
            cfg.AddProfile(new GlossaryTermProfile())));

        await using var context = new Repository(dbContextOptions);
        context.GlossaryTerms.Add(new() { Term = "Term 1", Definition = "Definition 1" });
        await context.SaveChangesAsync();

        var handler = new GetGlossaryTermQueryHandler(context, mapper);
        var result = await handler.Handle(new GetGlossaryTermQuery { Id = 1 }, CancellationToken.None);

        result.ShouldBeOfType<GlossaryTermDto>();
    }

    [Test]
    public async Task GetValueTest()
    {
        var dbContextOptions = new DbContextOptionsBuilder<Repository>()
            .UseInMemoryDatabase(databaseName: "glossary_db_InMemory")
            .Options;

        IMapper mapper = new Mapper(new MapperConfiguration(cfg => 
            cfg.AddProfile(new GlossaryTermProfile())));

        await using var context = new Repository(dbContextOptions);
        context.GlossaryTerms.Add(new() { Term = "Term 1", Definition = "Definition 1" });
        await context.SaveChangesAsync();

        var handler = new GetGlossaryTermQueryHandler(context, mapper);
        var result = await handler.Handle(new GetGlossaryTermQuery { Id = 1 }, CancellationToken.None);

        result.ShouldNotBeNull();
        result.Id.ShouldBe(1);
        result.Term.ShouldBe("Term 1");
        result.Definition.ShouldBe("Definition 1");
    }
}