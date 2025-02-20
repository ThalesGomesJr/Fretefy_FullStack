using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces
{
    public interface ICidadeService
    {
        Task<CidadeModel> Get(Guid id);
        Task<IEnumerable<CidadeModel>> List();
        Task<IEnumerable<CidadeModel>> ListByUf(string uf);
        Task<IEnumerable<CidadeModel>> Query(string terms);
    }
}
