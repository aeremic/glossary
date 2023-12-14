using Glossary.Service.Commands.GlossaryTerm.UpdateGlossaryTerm;
using Glossary.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace Glossary.UnitTests.Commands.GlossaryTerm.UpdateGlossaryTerm;

[TestFixture]
public class UpdateGlossaryTermCommandHandlerTest
{
    [Test]
    public async Task DeleteTest()
    {
        var dbContextOptions = new DbContextOptionsBuilder<Repository>()
            .UseInMemoryDatabase(databaseName: "updateGlossaryTermCommandHandlerTest_inMemory_db")
            .Options;

        await using var context = new Repository(dbContextOptions);
        context.GlossaryTerms.Add(new() { Term = "Term 1", Definition = "Definition 1" });
        context.GlossaryTerms.Add(new() { Term = "Term 2", Definition = "Definition 2" });
        await context.SaveChangesAsync();

        var handler = new UpdateGlossaryTermCommandHandler(context);
        var result =
            await handler.Handle(
                new UpdateGlossaryTermCommand { Id = 1, Term = "Term 1 Updated", Definition = "Definition 1"  },
                CancellationToken.None);

        result.ShouldBe(1);
    }
}