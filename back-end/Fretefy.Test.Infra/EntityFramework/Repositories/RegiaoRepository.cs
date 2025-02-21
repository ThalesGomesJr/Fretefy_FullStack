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
    public class RegiaoRepository : IRegiaoRepository
    {
        private DbSet<Regiao> _dbSet;
        private readonly DbContext _context;

        public RegiaoRepository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<Regiao>();
            _context = dbContext;
        }

        public async Task<Regiao> Get(Expression<Func<Regiao, bool>> expressao)
        {
            return await _dbSet
                .AsSplitQuery()
                .Include(x => x.RegiaoCidades)
                    .ThenInclude(x => x.Cidade)
                .FirstOrDefaultAsync(expressao);
        }

        public async Task<IEnumerable<Regiao>> List(string nome)
        {
            if(!string.IsNullOrEmpty(nome)){
                return await _dbSet
                    .AsSplitQuery()
                    .Include(x => x.RegiaoCidades)
                        .ThenInclude(x => x.Cidade)
                    .Where(x => EF.Functions.Like(x.Nome, $"%{nome}%"))
                    .OrderBy(x => x.Nome)
                    .ToListAsync();
            }

            return await _dbSet
                .AsSplitQuery()
                .Include(x => x.RegiaoCidades)
                    .ThenInclude(x => x.Cidade)
                .OrderBy(x => x.Nome)
                .ToListAsync();
        }

        public async Task<bool> RegiaoJaCadastrada(string nome, Guid id) 
        {
            return await _dbSet.Where(x => x.Id != id).Where(x => x.Nome.ToLower() == nome.ToLower()).AnyAsync();
        }

        public async Task Create(Regiao regiao)
        {
            await _dbSet.AddAsync(regiao);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Regiao regiao)
        {
            foreach (var item in regiao.RegiaoCidades)
            {
                if(item.RegiaoId == Guid.Empty)
                    _context.Entry(item).State = EntityState.Added;
            }

            _dbSet.Update(regiao);
            await _context.SaveChangesAsync();
        }
    }
}
