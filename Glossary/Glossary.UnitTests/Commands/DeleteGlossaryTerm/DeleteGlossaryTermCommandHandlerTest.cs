using Glossary.Service.Commands.GlossaryTerm.CreateGlossaryTerm;
using Glossary.Service.Commands.GlossaryTerm.DeleteGlossaryTerm;
using Glossary.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace Glossary.UnitTests.Commands.DeleteGlossaryTerm;

[TestFixture]
public class DeleteGlossaryTermCommandHandlerTest
{
    [Test]
    public async Task DeleteTest()
    {
        var dbContextOptions = new DbContextOptionsBuilder<Repository>()
            .UseInMemoryDatabase(databaseName: "glossary_db_InMemory")
            .Options;

        await using var context = new Repository(dbContextOptions);
        context.GlossaryTerms.Add(new() { Term = "Term 1", Definition = "Definition 1" });
        context.GlossaryTerms.Add(new() { Term = "Term 2", Definition = "Definition 2" });
        await context.SaveChangesAsync();

        var handler = new DeleteGlossaryTermCommandHandler(context);
        var result =
            await handler.Handle(
                new DeleteGlossaryTermCommand { Id = 2 },
                CancellationToken.None);

        result.ShouldBe(2);
    }
}