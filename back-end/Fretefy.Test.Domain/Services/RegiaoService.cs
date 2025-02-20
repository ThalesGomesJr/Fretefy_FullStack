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
    public class RegiaoService : IRegiaoService
    {
        private readonly IRegiaoRepository _regiaoRepository;
        private readonly IRegiaoCidadeRepository _regiaoCidadeRepository;
        
        public RegiaoService(IRegiaoRepository regiaoRepository, IRegiaoCidadeRepository regiaoCidadeRepository)
        {
            _regiaoRepository = regiaoRepository;
            _regiaoCidadeRepository = regiaoCidadeRepository;
        }

        public async Task<UpdateRegiaoModel> Get(Guid id)
        {
            var regiaoEntidade = await _regiaoRepository.Get(x => x.Id == id); 
            if(regiaoEntidade is null)
                return null;
            
            return new UpdateRegiaoModel(regiaoEntidade); 
        }

        public async Task<IEnumerable<UpdateRegiaoModel>>  List(string nome = null)
        {
            var lista = await _regiaoRepository.List(nome);
            var regioes = lista.Select(x => new UpdateRegiaoModel(x));

            return regioes;
        }

        public async Task<(bool Sucesso, string Mensagem)> Create(CreateRegiaoModel regiaoModel)
        {
            try
            {
                var regiaoCidades = regiaoModel.RegiaoCidades.Select(x => new RegiaoCidade(x.Cidade.Id)).ToList();  

                if (await _regiaoRepository.RegiaoJaCadastrada(regiaoModel.Nome)){
                    return (false, "Já existe uma região com este nome.");
                }
                
                if(regiaoCidades.GroupBy(rc => rc.CidadeId).Any(g => g.Count() > 1))
                    return (false, "A mesma cidade não pode ser cadastrada duas vezes na mesma região.");
                
                if (await _regiaoCidadeRepository.CidadeJaCadastrada(regiaoCidades))
                    return (false, "A mesma cidade não pode ser cadastrada duas vezes em regiões diferentes.");

                var regiaoEntidade = new Regiao(regiaoModel.Nome, regiaoModel.Ativo, regiaoCidades);
                await _regiaoRepository.Create(regiaoEntidade);
                return (true, "Região cadastrada com sucesso.");
            } 
            catch(Exception e)
            {
                return (false, e.Message);
            }
        }

        public async Task<(bool Sucesso, string Mensagem)> Update(UpdateRegiaoModel regiaoModel)
        {
            try
            {
                var regiaoEntidade = await _regiaoRepository.Get(x => x.Id == regiaoModel.Id);
                
                if (regiaoEntidade is null)
                    return (false, "Região não encontrada.");

                var removidos = new List<RegiaoCidade>();
                foreach(var regiaoCidade in regiaoEntidade.RegiaoCidades)
                {
                    if (!regiaoModel.RegiaoCidades.Any(x => x.Id == regiaoCidade.Id))
                        removidos.Add(regiaoCidade);                
                }

                foreach(var regiaoCidade in regiaoModel.RegiaoCidades)
                {
                    if (regiaoCidade.Id is null){
                        regiaoEntidade.RegiaoCidades.Add(new RegiaoCidade(regiaoCidade.Cidade.Id));                
                    }
                }

                regiaoEntidade.RegiaoCidades = regiaoEntidade.RegiaoCidades.Where(x => !removidos.Any(r => r.Id == x.Id)).ToList(); 

                if (await _regiaoCidadeRepository.CidadeJaCadastrada(regiaoEntidade.RegiaoCidades))
                    return (false, "A mesma cidade não pode ser cadastrada duas vezes em regiões diferentes.");

                if(regiaoEntidade.RegiaoCidades.GroupBy(rc => rc.CidadeId).Any(g => g.Count() > 1))
                    return (false, "A mesma cidade não pode ser cadastrada duas vezes na mesma região.");

                
                await _regiaoRepository.Update(regiaoEntidade);
                return (true, "Região atualizada com sucesso.");
            }
            catch(Exception e)
            {
               return (false, e.Message);
            }
        }

        public async Task<bool> Activate(Guid id)
        {
            try
            {
                var regiaoEntidade = await _regiaoRepository.Get(x => x.Id == id);

                if (regiaoEntidade is null)
                    return false;

                regiaoEntidade.Ativo = true;

                await _regiaoRepository.Update(regiaoEntidade);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Deactivate(Guid id)
        {
            try
            {
                var regiaoEntidade = await _regiaoRepository.Get(x => x.Id == id);

                if (regiaoEntidade is null)
                    return false;

                regiaoEntidade.Ativo = false;

                await _regiaoRepository.Update(regiaoEntidade);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
