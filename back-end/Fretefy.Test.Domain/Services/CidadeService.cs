using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;
using Fretefy.Test.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

        public async Task<CidadeModel> Get(Guid id)
        {
            var cidadeEntidade = await _cidadeRepository.Get(id);
            return new CidadeModel(cidadeEntidade);
        }

        public async Task<IEnumerable<CidadeModel>> List()
        {
            var lista = await _cidadeRepository.List();
            var cidades = lista.Select(x => new CidadeModel(x));
            return cidades;
        }

        public async Task<IEnumerable<CidadeModel>> ListByUf(string uf)
        {
            var lista = await _cidadeRepository.ListByUf(uf);
            var cidades = lista.Select(x => new CidadeModel(x));
            return cidades;
        }

        public async Task<IEnumerable<CidadeModel>> Query(string terms)
        {
            var lista = await _cidadeRepository.Query(terms);
            var cidades = lista.Select(x => new CidadeModel(x));
            return cidades;
        }
    }
}
