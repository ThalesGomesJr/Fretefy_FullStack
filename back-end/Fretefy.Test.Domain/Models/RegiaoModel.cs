using System;
using System.Collections.Generic;
using System.Linq;
using Fretefy.Test.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Fretefy.Test.Domain.Models
{
    public class CreateRegiaoModel
    {

        public CreateRegiaoModel()
        {
        }
        
        [Required(ErrorMessage = "O nome da região é obrigatório.")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O status é obrigatório.")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "A lista de cidades não pode estar vazia.")]
        [MinLength(1, ErrorMessage = "A lista deve ter pelo menos 1 cidade adicionada.")]
        public ICollection<CreateRegiaoCidadeModel> RegiaoCidades { get; set;} = new List<CreateRegiaoCidadeModel>();
    }

    public class UpdateRegiaoModel
    {

        public UpdateRegiaoModel()
        {
        }

        public UpdateRegiaoModel(Regiao regiaoEntidade)
        {
            Id = regiaoEntidade.Id;
            Nome = regiaoEntidade.Nome;
            Ativo = regiaoEntidade.Ativo;
            RegiaoCidades = regiaoEntidade.RegiaoCidades.Select(rc => new UpdateRegiaoCidadeModel(rc)).ToList();
        }

        [Required(ErrorMessage = "O ID é obrigatório.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome da região é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        public bool Ativo { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "A lista deve ter pelo menos 1 cidade adicionada.")]
        public ICollection<UpdateRegiaoCidadeModel> RegiaoCidades { get; set;} = new List<UpdateRegiaoCidadeModel>();
    }
}
