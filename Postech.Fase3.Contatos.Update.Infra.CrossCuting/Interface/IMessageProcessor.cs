using Postech.Fase3.Contatos.Update.Infra.CrossCuting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.Fase3.Contatos.Update.Infra.CrossCuting.Interface;

public interface IMessageProcessor
{
    Task<ServiceResult<bool>> ProcessMessageAsync(string message);
}
