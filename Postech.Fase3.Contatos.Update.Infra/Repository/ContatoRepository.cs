using Microsoft.EntityFrameworkCore;
using Postech.Fase3.Contatos.Update.Domain.Entities;
using Postech.Fase3.Contatos.Update.Infra.Interface;
using Postech.Fase3.Contatos.Update.Infra.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.Fase3.Contatos.Update.Infra.Repository;

public class ContatoRepository(AppDBContext context) : IContatoRepository
{
    public async Task<Contato> Atualizar(Contato c)
    {
        context.Contatos.Update(c);
        await context.SaveChangesAsync();
        return c;
    }

    public async Task<bool> ExisteAsync(Contato c)
    {
        return await context.Contatos.AsNoTracking().AnyAsync(contato =>
            contato.ContatoId != c.ContatoId && contato.Nome.Equals(c.Nome) && contato.Telefone.Equals(c.Telefone) && contato.DddId.Equals(c.DddId));
    }
}