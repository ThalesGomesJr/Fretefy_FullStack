using ClosedXML.Excel;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/regiao")]
    [ApiController]
    public class RegiaoController : ControllerBase
    {
        private readonly IRegiaoService _regiaoService;

        public RegiaoController(IRegiaoService regiaoService)
        {
            _regiaoService = regiaoService;
        }

        [HttpGet("list/")]
        public async Task<IActionResult> List([FromQuery] string nome)
        {      
            var regioes = await _regiaoService.List(nome);
            return Ok(regioes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
             var regiao = await _regiaoService.Get(id);
            return Ok(regiao);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRegiaoModel regiaoModelo)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));

            var resultado = await _regiaoService.Create(regiaoModelo);
            if (resultado.Sucesso)
                return Ok(resultado);
            return StatusCode(500, resultado.Mensagem);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRegiaoModel regiaoModelo)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors));

            var resultado = await _regiaoService.Update(regiaoModelo);
            if (resultado.Sucesso)
                return Ok(resultado);
            return StatusCode(500, resultado.Mensagem);
        }

        [HttpPut("activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            var sucesso = await _regiaoService.Activate(id);
            if (sucesso)
                return Ok();
            return StatusCode(500);
        }


        [HttpPut("deactivate")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            var sucesso = await _regiaoService.Deactivate(id);
            if (sucesso)
                return Ok();
            return StatusCode(500);
        }


        [HttpGet("export-list")]
        public async Task<IActionResult> ExportList()
        {
            var regioes = (await _regiaoService.List()).ToList();
            return GerarFileXlsx(regioes);
        }
        
        private IActionResult GerarFileXlsx(List<UpdateRegiaoModel> regioes)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Regiões e Cidades");

                var headerRow = worksheet.Range("A1:C1");
                headerRow.Style.Font.Bold = true;
                headerRow.Style.Font.FontColor = XLColor.White;
                headerRow.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headerRow.Style.Fill.BackgroundColor = XLColor.DarkBlue;

                worksheet.Cell(1, 1).Value = "Região";
                worksheet.Cell(1, 2).Value = "Cidade";
                worksheet.Cell(1, 3).Value = "UF";

                int i = 2;
                foreach (var regiao in regioes)
                {
                    foreach (var cidade in regiao.RegiaoCidades)
                    {
                        worksheet.Cell(i, 1).Value = regiao.Nome;
                        worksheet.Cell(i, 2).Value = cidade.Cidade.Nome;
                        worksheet.Cell(i, 3).Value = cidade.Cidade.UF;

                        var rowColor = (i % 2 == 0) ? XLColor.LightGray : XLColor.Gray;
                        worksheet.Cell(i, 1).Style.Fill.BackgroundColor = rowColor;
                        worksheet.Cell(i, 2).Style.Fill.BackgroundColor = rowColor;
                        worksheet.Cell(i, 3).Style.Fill.BackgroundColor = rowColor;
                        i++;
                    }
                }

                worksheet.Columns().AdjustToContents();

                worksheet.RangeUsed().Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.RangeUsed().Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var bytes = stream.ToArray();
                    var base64String = Convert.ToBase64String(bytes);

                    return Ok(new { file = base64String });
                }
            }
        }
    }
}
