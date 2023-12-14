using Glossary.Service.Commands.GlossaryTerm.CreateGlossaryTerm;
using Glossary.Service.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace Glossary.UnitTests.Commands.GlossaryTerm.CreateGlossaryTerm;

[TestFixture]
public class CreateGlossaryTermCommandHandlerTest
{
    [Test]
    public async Task CreateTest()
    {
        var dbContextOptions = new DbContextOptionsBuilder<Repository>()
            .UseInMemoryDatabase(databaseName: "createGlossaryTermCommandHandlerTest_inMemory_db")
            .Options;

        await using var context = new Repository(dbContextOptions);

        var handler = new CreateGlossaryTermCommandHandler(context);
        var result =
            await handler.Handle(
                new CreateGlossaryTermCommand { Term = "Term 1", Definition = "Definition 1" },
                CancellationToken.None);

        result.ShouldBe(1);
    }
}