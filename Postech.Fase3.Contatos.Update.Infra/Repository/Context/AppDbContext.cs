using Microsoft.EntityFrameworkCore;
using Postech.Fase3.Contatos.Update.Domain.Entities;

namespace Postech.Fase3.Contatos.Update.Infra.Repository.Context;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public DbSet<Contato> Contatos { get; set; }
}