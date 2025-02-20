using Fretefy.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface IRegiaoRepository
    {
        Task<Regiao> Get(Expression<Func<Regiao, bool>> expressao);
        Task<IEnumerable<Regiao>> List(string nome);
        Task Create(Regiao regiao);
        Task Update(Regiao regiao);
    }
}
