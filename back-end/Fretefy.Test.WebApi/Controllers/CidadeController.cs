using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/cidade")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly ICidadeService _cidadeService;

        public CidadeController(ICidadeService cidadeService)
        {
            _cidadeService = cidadeService;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] string uf, [FromQuery] string terms)
        {
            IEnumerable<CidadeModel> cidades;

            if (!string.IsNullOrEmpty(terms))
                cidades = await _cidadeService.Query(terms);
            else if (!string.IsNullOrEmpty(uf))
                cidades = await _cidadeService.ListByUf(uf);
            else
                cidades = await _cidadeService.List();

            return Ok(cidades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var cidades = await _cidadeService.Get(id);
            return Ok(cidades);
        }
    }
}
