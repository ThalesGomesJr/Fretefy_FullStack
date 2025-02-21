using Fretefy.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface ICidadeRepository
    {
        Task<Cidade> Get(Guid id);
        Task<IEnumerable<Cidade>> List();
        Task<IEnumerable<Cidade>> ListByUf(string uf);
        Task<IEnumerable<Cidade>> ListAvailable(IEnumerable<Guid> cidadesIds);
        Task<IEnumerable<Cidade>> Query(string terms);
    }
}
