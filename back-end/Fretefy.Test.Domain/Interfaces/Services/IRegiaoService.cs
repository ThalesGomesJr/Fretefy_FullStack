using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces
{
    public interface IRegiaoService
    {
        Task<UpdateRegiaoModel> Get(Guid id);
        Task<IEnumerable<UpdateRegiaoModel>> List(string nome = null);
        Task<(bool Sucesso, string Mensagem)> Create(CreateRegiaoModel regiaoModelo);
        Task<(bool Sucesso, string Mensagem)> Update(UpdateRegiaoModel regiao);
        Task<bool> Activate(Guid id);
         Task<bool> Deactivate(Guid id);
    }
}
