using System;
using System.Collections.Generic;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Models;

namespace Fretefy.Test.Domain.Models
{
    public class CreateRegiaoCidadeModel
    {
        public CreateRegiaoCidadeModel()
        {
        }

        public CidadeModel Cidade { get; set; }
    }

    public class UpdateRegiaoCidadeModel : CreateRegiaoCidadeModel
    {
        public UpdateRegiaoCidadeModel()
        {
        }

        public UpdateRegiaoCidadeModel(RegiaoCidade regiaoCidadeEntidade)
        {
            Id = regiaoCidadeEntidade.Id;
            Cidade =  new CidadeModel(regiaoCidadeEntidade.Cidade);
        }

        public Guid? Id { get; set; }
    }   
}
