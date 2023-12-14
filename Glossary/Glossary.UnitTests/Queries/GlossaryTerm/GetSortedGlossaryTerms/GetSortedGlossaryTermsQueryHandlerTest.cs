using AutoMapper;
using Glossary.Service.Common.Models;
using Glossary.Service.Infrastructure;
using Glossary.Service.Mapper;
using Glossary.Service.Queries.GlossaryTerm.GetGlossaryTerm;
using Glossary.Service.Queries.GlossaryTerm.GetSortedGlossaryTerms;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace Glossary.UnitTests.Queries.GlossaryTerm.GetSortedGlossaryTerms;

[TestFixture]
public class GetSortedGlossaryTermsQueryHandlerTest
{
    [Test]
    public async Task GetSortedValuesTest()
    {
        var dbContextOptions = new DbContextOptionsBuilder<Repository>()
            .UseInMemoryDatabase(databaseName: "glossary_db_InMemory")
            .Options;

        IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            cfg.AddProfile(new GlossaryTermProfile())));

        await using var context = new Repository(dbContextOptions);
        context.GlossaryTerms.Add(new() { Term = "C Term", Definition = "A Definition" });
        context.GlossaryTerms.Add(new() { Term = "A Term", Definition = "A Definition" });
        context.GlossaryTerms.Add(new() { Term = "B Term", Definition = "B Definition" });
        context.GlossaryTerms.Add(new() { Term = "B Term", Definition = "A Definition" });
        await context.SaveChangesAsync();

        var handler = new GetSortedGlossaryTermsQueryHandler(context, mapper);
        var result = await handler.Handle(new GetSortedGlossaryTermsQuery(), CancellationToken.None);

        result.ShouldBeOfType<List<GlossaryTermDto>>();
        result.Count.ShouldBe(4);

        result[0].Id.ShouldBe(2);
        result[1].Id.ShouldBe(4);
        result[2].Id.ShouldBe(3);
        result[3].Id.ShouldBe(1);
    }
}