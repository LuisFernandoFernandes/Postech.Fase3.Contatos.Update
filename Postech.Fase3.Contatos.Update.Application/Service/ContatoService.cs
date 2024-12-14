using Postech.Fase3.Contatos.Update.Application.Interface;
using Postech.Fase3.Contatos.Update.Domain.Entities;
using Postech.Fase3.Contatos.Update.Infra.CrossCuting.Model;
using Postech.Fase3.Contatos.Update.Infra.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.Fase3.Contatos.Update.Application.Service;

public class ContatoService(IContatoRepository _contatoRepository) : IContatoService
{
    public async Task<ServiceResult<bool>> AtualizarAsync(Contato contato)
    {
        try
        {
            if (await _contatoRepository.ExisteAsync(contato))
                return new ServiceResult<bool>(new ValidacaoException("Dados alterados já cadastrados para outro contato"));

            await _contatoRepository.Atualizar(contato);

            return new ServiceResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new ServiceResult<bool>(ex);
        }
    }
}