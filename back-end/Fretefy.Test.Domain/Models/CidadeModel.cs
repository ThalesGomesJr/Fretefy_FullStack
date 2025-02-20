using System;
using Fretefy.Test.Domain.Entities;

namespace Fretefy.Test.Domain.Models
{
    public class CidadeModel
    {

        public CidadeModel()
        {
        }

        public CidadeModel(Cidade cidadeEntidade)
        {
            Id = cidadeEntidade.Id;
            Nome = cidadeEntidade.Nome;
            UF = cidadeEntidade.UF;
        }

        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string UF { get; set; }
    }
}
