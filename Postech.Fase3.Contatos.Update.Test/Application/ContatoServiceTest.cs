using Moq;
using Postech.Fase3.Contatos.Update.Application.DTO;
using Postech.Fase3.Contatos.Update.Application.Service;
using Postech.Fase3.Contatos.Update.Domain.Entities;
using Postech.Fase3.Contatos.Update.Infra.CrossCuting.Model;
using Postech.Fase3.Contatos.Update.Infra.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.Fase3.Contatos.Update.Test.Application;

public class ContatoServiceTest
{
    private readonly Contato _contato;
    private readonly Mock<IContatoRepository> contatoRepository;

    public ContatoServiceTest()
    {
        _contato = new Contato(Guid.NewGuid(), "nome Teste", "963333243", "teste@email.com.br", 11, DateTime.Now);

        contatoRepository = new Mock<IContatoRepository>();

        contatoRepository
        .Setup(x => x.ExisteAsync(_contato))
        .ReturnsAsync(true);
    }

    [Fact]
    public async Task ContatoService_Atualizar_ComSucesso()
    {
        //arrange
        contatoRepository
        .Setup(x => x.ExisteAsync(_contato))
        .ReturnsAsync(false);

        contatoRepository
            .Setup(x => x.Atualizar(_contato))
            .ReturnsAsync(_contato);

        var contatoService = new ContatoService(contatoRepository.Object);

        //act
        var contatoResult = await contatoService.AtualizarAsync(_contato);

        //assert
        Assert.True(contatoResult.IsSuccess);
        Assert.True(contatoResult.Data);
    }

    [Fact]
    public async Task ContatoService_Atualizar_ComErroContatoNaoExistente()
    {
        //arrange
        contatoRepository
            .Setup(x => x.ExisteAsync(It.IsAny<Contato>()))
            .ReturnsAsync(true);

        var contatoService = new ContatoService(contatoRepository.Object);

        //act
        var contatoResult = await contatoService.AtualizarAsync(_contato);

        //assert
        Assert.False(contatoResult.IsSuccess);
        var ex = Assert.IsType<ValidacaoException>(contatoResult.Error);
        Assert.Equal("Dados alterados já cadastrados para outro contato", ex.Message);
    }

    [Fact]
    public async Task ContatoService_Atualizar_ComErro()
    {
        //arrange
        contatoRepository
        .Setup(x => x.ExisteAsync(_contato))
        .ReturnsAsync(false);

        contatoRepository
            .Setup(x => x.Atualizar(It.IsAny<Contato>()))
            .ThrowsAsync(new Exception("Erro ao Atualizar"));

        var contatoService = new ContatoService(contatoRepository.Object);

        //act
        var contatoResult = await contatoService.AtualizarAsync(_contato);

        //assert
        Assert.False(contatoResult.IsSuccess);
        Assert.IsType<Exception>(contatoResult.Error);
    }
}
