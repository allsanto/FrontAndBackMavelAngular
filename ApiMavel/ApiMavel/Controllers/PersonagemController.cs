using Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiMavel.Controllers
{
    [Route("api/[controller]")]
    public class PersonagemController : Controller
    {
        private readonly PersonagemBL _personagemBL;

        public PersonagemController(PersonagemBL personagemBL)
        {
            _personagemBL = personagemBL;
        }

        [HttpGet]
        [Route("getListaPersonagens")]
        [ProducesResponseType(typeof(Personagem), StatusCodes.Status200OK)] // Retorna o statuscode
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Retorna o statuscode
        public IActionResult GetListaPersonagens()
        {
            var lista = _personagemBL.ListaPersonagens();
            
            try
            {
                return Ok(lista);
            }
            catch (Exception)
            {
                return BadRequest("Erro! Consulte o administrador.");
            }
        }

        [HttpGet]
        [Route("getBuscaPersonagens")]
        [ProducesResponseType(typeof(Personagem), StatusCodes.Status200OK)] // Retorna o statuscode
        [ProducesResponseType(typeof(Exception), StatusCodes.Status404NotFound)] // Retorna o statuscode
        public IActionResult GetBuscaPersonagens(string nomePersonagem)
        {
            var lista = _personagemBL.BuscaPersonagens(nomePersonagem);

            try
            {
                return Ok(lista);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
