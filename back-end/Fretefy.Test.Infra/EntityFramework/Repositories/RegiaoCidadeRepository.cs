using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class RegiaoCidadeRepository : IRegiaoCidadeRepository
    {
        private DbSet<RegiaoCidade> _dbSet;

        public RegiaoCidadeRepository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<RegiaoCidade>();
        }

        public async Task<bool> CidadeJaCadastrada(IEnumerable<RegiaoCidade> regiaoCidades) 
        {
            var cidadeIds = regiaoCidades.Select(rc => rc.CidadeId).ToList();
            return await _dbSet.Where(r => cidadeIds.Contains(r.CidadeId)).AnyAsync();          
        }        
    }
}
