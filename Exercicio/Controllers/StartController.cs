using Exercicio.Dominio.Entidades;
using Exercicio.Dominio.Interfaces.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartController : ControllerBase
    {
        private readonly IStartNegocio _startNegocio;
        public StartController(IStartNegocio startNegocio)
        {
            _startNegocio = startNegocio;
        }
        [HttpGet]
        public async Task<ActionResult<dynamic>> Get()
        {
            return _startNegocio.Iniciar();
        }

    }
}
