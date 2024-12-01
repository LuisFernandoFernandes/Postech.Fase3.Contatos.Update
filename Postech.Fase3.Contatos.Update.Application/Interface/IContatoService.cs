using Postech.Fase3.Contatos.Update.Domain.Entities;
using Postech.Fase3.Contatos.Update.Infra.CrossCuting.Model;

namespace Postech.Fase3.Contatos.Update.Application.Interface;

public interface IContatoService
{
    Task<ServiceResult<bool>> AtualizarAsync(Contato contato);
}
