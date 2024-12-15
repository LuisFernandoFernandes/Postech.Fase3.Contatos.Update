using Microsoft.EntityFrameworkCore;
using Postech.Fase3.Contatos.Update.Domain.Entities;
using Postech.Fase3.Contatos.Update.Infra.Repository;
using Postech.Fase3.Contatos.Update.Infra.Repository.Context;
using Postech.Fase3.Contatos.Update.Integracao.Test.Fixture;
using Xunit.Extensions.Ordering;

namespace Postech.Fase3.Contatos.Update.Integracao.Test.Infra;

[Collection(nameof(ContextDbCollection)), Order(2)]
public class ContatoRepositoryTest
{
    private readonly AppDBContext context;
    private readonly ContatoRepository repository;
    public ContatoRepositoryTest(ContextDbFixture fixture)
    {
        context = fixture.Context!;
        repository = new ContatoRepository(context);
    }

    [Fact]
    public async Task Atualizar_DeveAtualizarContato()
    {
        var contato = new Contato(Guid.NewGuid(), "Nome 1", "999878587", "teste@email.com.br", 11, DateTime.Now);
        context.Contatos.Add(contato);
        await context.SaveChangesAsync();
        context.Entry(contato).State = EntityState.Detached;

        contato = new Contato(contato.ContatoId, "Nome 2", "999878587", "teste@email.com.br", 11, contato.DataInclusao);
        var result = await repository.Atualizar(contato);

        Assert.NotNull(result);
        Assert.Equal("Nome 2", result.Nome);
        Assert.Equal("999878587", result.Telefone);
        Assert.Equal(11, result.DddId);
    }

    [Fact]
    public async Task Existe_DeveRetornarTrueSeContatoExiste()
    {
        // arrange
        var contatoExistente = new Contato(Guid.NewGuid(), "Nome 1", "999878587", "teste@exemplo.com.br", 11, DateTime.Now);
        context.Contatos.Add(contatoExistente);
        await context.SaveChangesAsync();

        // Cria um segundo contato com os mesmos Nome, Telefone e DddId
        var contatoNovo = new Contato(Guid.NewGuid(), "Nome 1", "999878587", "outro@exemplo.com.br", 11, DateTime.Now);

        // act
        var result = await repository.ExisteAsync(contatoNovo);

        // assert
        Assert.True(result);
    }

}
