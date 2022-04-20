using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WorkManagement.Models;
using WorkManagement.Services;

namespace WorkManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OficinaController : ControllerBase
    {
        private IOficinaService _oficinaService;
        public OficinaController(IOficinaService oficinaService)
        {
            _oficinaService = oficinaService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Oficina>>> GetOficinas()
        {
            try
            {
                var oficinas = await _oficinaService.GetOficinas();
                return Ok(oficinas);
            }
            catch
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter oficinas");
            }
        }

        [HttpGet("CargaDisponivel")]
        public async Task<ActionResult<IAsyncEnumerable<Oficina>>> GetCargaDisponivel()
        {
            try
            {
                var carga = await _oficinaService.GetCargaDisponivel();
                return Ok(carga);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter Cargas disponíveis das oficinas");
            }
            
        }

        [HttpGet("{id:int}", Name = "GetOficina")]
        public async Task<ActionResult<Oficina>> GetOficinaPorId(int id)
        {
            try
            {
                var oficina = await _oficinaService.GetOficinaPorId(id);
                if (oficina == null)
                    return NotFound($"Não existe oficina com {id}");


                return Ok(oficina);
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpGet("FilterBy")]
        public async Task<ActionResult<IAsyncEnumerable<Oficina>>> GetOficinaPorData(DateTime data)
        {
            try
            {
                var oficinas = await _oficinaService.GetOficinaPorData(data);
                if (oficinas == null)
                {
                    return NotFound($"Não existe oficinas na data {data}");
                }

                return Ok(oficinas);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter esse agendamentos");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Oficina oficina)
        {
            try
            {
                await _oficinaService.CreateOficina(oficina);
                return CreatedAtRoute(nameof(GetOficinaPorId), new { id = oficina.Id }, oficina);
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Oficina oficina)
        {
            try
            {
                if (oficina.Id == id)
                {
                    await _oficinaService.UpdateOficina(oficina);
                    return Ok($"Aluno com id={id} foi atualizado ");
                }
                else
                {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var oficina = await _oficinaService.GetOficinaPorId(id);
                if (oficina != null)
                {
                    await _oficinaService.DeleteOficina(oficina);
                    return Ok($"Oficina de id={id} foi excluido.");
                }
                else
                {
                    return NotFound();
                }
            }
            catch
            {
                return BadRequest("Request Inválido");
            }
        }
    }
}
