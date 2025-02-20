using Fretefy.Test.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface IRegiaoCidadeRepository
    {
        Task<bool> CidadeJaCadastrada(IEnumerable<RegiaoCidade> regiaoCidades);
    }
}
