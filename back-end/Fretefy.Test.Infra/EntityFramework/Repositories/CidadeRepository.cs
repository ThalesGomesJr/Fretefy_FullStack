using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretefy.Test.Infra.EntityFramework.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private DbSet<Cidade> _dbSet;

        public CidadeRepository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<Cidade>();
        }
        public async Task<Cidade> Get(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<IEnumerable<Cidade>> List()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<Cidade>> ListByUf(string uf)
        {
            return await _dbSet.Where(w => EF.Functions.Like(w.UF, $"%{uf}%")).ToListAsync();
        }

        public async Task<IEnumerable<Cidade>> Query(string terms)
        {

            return await _dbSet.Where(w => EF.Functions.Like(w.Nome, $"%{terms}%") || EF.Functions.Like(w.UF, $"%{terms}%")).ToArrayAsync();
        }
    }
}
