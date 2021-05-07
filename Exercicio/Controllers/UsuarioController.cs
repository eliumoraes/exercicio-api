using Exercicio.Dominio.Entidades;
using Exercicio.Dominio.Interfaces.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exercicio.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<Produto> _logger;
        private readonly IUsuarioNegocio _usuarioNegocio;
        public UsuarioController(IUsuarioNegocio usuarioNegocio, ILogger<Produto> logger)
        {
            _logger = logger;
            _usuarioNegocio = usuarioNegocio;
        }
        
        /// <summary>
        /// Inserir usuário
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles ="administrador,gerente")]
        public async Task<ActionResult<UsuarioFront>> Post(
            [FromBody] UsuarioFront entidade)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Usuario entidadeRetorno = await _usuarioNegocio.Inserir(entidade);

            if (entidadeRetorno.Codigo == 0)
                return BadRequest(new { mensagem = "Não foi possivel criar o usuário" });
            else
                return Ok(entidadeRetorno);
        }

        /// <summary>
        /// Alterar usuário
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [HttpPut("{codigo:int}")]
        [Authorize(Roles ="administrador,gerente")]
        public async Task<ActionResult<UsuarioFront>> Put(int codigo, [FromBody] UsuarioFront entidade)
        {
            if (entidade.Codigo != codigo)
                return BadRequest(new { mensagem = "Você não pode alterar o código" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Usuario entidadeRetorno = await _usuarioNegocio.Alterar(entidade);

            if (entidadeRetorno.Codigo == 0)
                return BadRequest(new { mensagem = "Não foi possível atualizar a entidade" });
            else
                return Ok(entidade);
        }

        /// <summary>
        /// Excluir usuário
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpDelete("{codigo:int}")]
        [Authorize(Roles ="administrador,gerente")]
        public async Task<ActionResult<dynamic>> Delete(int codigo)
        {
            bool usuarioExcluido = await _usuarioNegocio.Excluir(codigo);
            if (!usuarioExcluido)
                return new { mensagem = "Não foi possivel excluir o usuário" };
            return new {mensagem = "Usuário excluído com sucesso."};
        }

        /// <summary>
        /// Efetuar login
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> Autenticar(
            [FromBody]UsuarioLogin entidade)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            dynamic usuarioRetorno = await _usuarioNegocio.Login(entidade);

            _logger.LogDebug("Como colocar aqui o usuarioRetorno?");

            return Ok(usuarioRetorno);
        }

        /// <summary>
        /// Consulta lista de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "administrador,gerente")]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            List<Usuario> listaDeUsuarios = await _usuarioNegocio.ConsultarLista();
            if (listaDeUsuarios.Count > 0)
                return Ok(listaDeUsuarios);
            else
                return NotFound();
        }

        [HttpGet("anonimo")]
        [AllowAnonymous]
        public string Anonimo()
        {
            return "Exemplo de usuário anônimo";
        }

        [HttpGet("autenticado")]
        [Authorize]
        public string Autenticado()
        {
            return "Exemplo de usuário autenticado";
        }

        [HttpGet("funcionario")]
        [Authorize(Roles = "funcionario")]
        public string Funcionario()
        {
            return "Exemplo de usuário funcionário";
        }

        [HttpGet("administrador")]
        [Authorize(Roles = "administrador")]
        public string Administrador()
        {
            return "Exemplo de usuário administrador";
        }
    }
}
