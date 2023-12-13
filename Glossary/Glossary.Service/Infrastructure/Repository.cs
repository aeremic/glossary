using Glossary.Service.Domains;
using Microsoft.EntityFrameworkCore;

namespace Glossary.Service.Infrastructure;

public class Repository : DbContext
{
    #region Constructors

    public Repository(DbContextOptions options) : base(options)
    {
    }

    #endregion

    #region Entities

    public DbSet<GlossaryTerm> GlossaryTerms { get; set; } = null!;

    #endregion

    #region Configuration

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configuration setup
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Foreign keys setup
    }

    #endregion
}