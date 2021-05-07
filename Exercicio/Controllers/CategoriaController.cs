using Exercicio.Dominio.Entidades;
using Exercicio.Dominio.Interfaces.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercicio.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaNegocio _categoriaNegocio;

        public CategoriaController(ICategoriaNegocio categoriaNegocio, ILogger<Categoria> logger)
        {
            _categoriaNegocio = categoriaNegocio;
        }
        
        /// <summary>
        /// Consultar lista de categorias
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)]  //Cria cache do método
        //[ResponseCache(Duration =0, Location =ResponseCacheLocation.None, NoStore =true)] //Desabilita o cache para o método.
        public async Task<ActionResult<List<Categoria>>> Get()
        {
            List<Categoria> listaDeCategorias = await _categoriaNegocio.ConsultarLista();
            if (listaDeCategorias.Count > 0)
                return Ok(listaDeCategorias);
            else
                return NotFound(); 
        }

        /// <summary>
        /// Consultar categoria
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{codigo:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Categoria>> Get(int codigo)
        {
            Categoria categoriaRetorno = await _categoriaNegocio.Consultar(codigo);
            return Ok(categoriaRetorno);
        }

        /// <summary>
        /// Inserir categoria
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "administrador,gerente,funcionario")]
        public async Task<ActionResult<Categoria>> Post(
            [FromBody] Categoria entidade)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Categoria entidadeRetorno = await _categoriaNegocio.Inserir(entidade);

            if (entidadeRetorno.Codigo == 0)
                return BadRequest(new { mensagem = "Não foi possivel criar a categoria" });
            else
                return Ok(entidadeRetorno);
        }

        /// <summary>
        /// Alterar categoria
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="entidade"></param>
        /// <returns></returns>
        [HttpPut("{codigo:int}")]
        [Authorize(Roles = "administrador,gerente,funcionario")]
        public async Task<ActionResult<Categoria>> Put(int codigo, [FromBody] Categoria entidade)
        {
            //Verifica se o Código informado na URL é o mesmo do modelo (Corpo)
            //Não pode alterar o código
            if (entidade.Codigo != codigo)
                return NotFound(new { mensagem = "Você não pode alterar o código da entidade." });

            //Equipara com o ModelState, aplica as validações de entidade
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Categoria entidadeRetorno = await _categoriaNegocio.Alterar(entidade);

            if (entidadeRetorno.Codigo == 0)
                return BadRequest(new { mensagem = "Não foi possível atualizar a entidade" });
            else
                return Ok(entidade);

        }

        /// <summary>
        /// Excluir categoria
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpDelete("{codigo:int}")]
        [Authorize(Roles = "administrador,gerente,funcionario")]
        public async Task<ActionResult<Categoria>> Delete(int codigo)
        {
            bool excluirRetorno = await _categoriaNegocio.Excluir(codigo);
            if (excluirRetorno)
                return Ok(new { mensagem = "Categoria excluída!" });
            else
                return BadRequest(new { mensagem = "Não foi possível excluir a categoria" });
        }

    }
}
