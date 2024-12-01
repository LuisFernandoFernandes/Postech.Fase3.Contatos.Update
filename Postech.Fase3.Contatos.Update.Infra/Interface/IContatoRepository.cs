using Postech.Fase3.Contatos.Update.Domain.Entities;

namespace Postech.Fase3.Contatos.Update.Infra.Interface;

public interface IContatoRepository
{
    Task<Contato> Atualizar(Contato c);
    Task<bool> ExisteAsync(Contato c);
}
