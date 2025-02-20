using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Entities
{
    public class Regiao : IEntity
    {
        public Regiao()
        {
        
        }

        public Regiao(string nome, bool ativo, ICollection<RegiaoCidade> regiaoCidades)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Ativo = ativo;
            RegiaoCidades = regiaoCidades;
        }

        public Guid Id { get; set; }

        public string Nome { get; set; }

        public bool Ativo { get; set; }

        public ICollection<RegiaoCidade> RegiaoCidades { get; set;} = new List<RegiaoCidade>();
    }
}
