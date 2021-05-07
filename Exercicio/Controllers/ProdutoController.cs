using Exercicio.Dominio.Entidades;
using Exercicio.Dominio.Interfaces.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercicio.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoNegocio _produtoNegocio;
        public ProdutoController(IProdutoNegocio produtoNegocio)
        {
            _produtoNegocio = produtoNegocio;
        }
        
        /// <summary>
        /// Consultar lista de produtos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Produto>>> Get()
        {
            List<Produto> listaDeProdutos = await _produtoNegocio.ConsultarLista();
            if (listaDeProdutos.Count > 0)
                return Ok(listaDeProdutos);
            else
                return NotFound();
        }

        /// <summary>
        /// Consultar produto
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpGet("{codigo:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Produto>> Get(int codigo)
        {
            Produto produtoRetorno = await _produtoNegocio.Consultar(codigo);
            return Ok(produtoRetorno);
        }

        /// <summary>
        /// Consultar produtos por categoria
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpGet("categoria/{codigo:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Produto>>> GetListaCategoria(int codigo)
        {
            List<Produto> produtoRetorno = await _produtoNegocio
                .ConsultarPorCategoria(codigo);

            return Ok(produtoRetorno);
        }

        /// <summary>
        /// Inserir produto
        /// </summary>
        /// <param name="entidade"></param>
        [HttpPost]
        [Authorize(Roles = "administrador,gerente,funcionario")]
        public async Task<IActionResult> Post([FromBody] Produto entidade)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Produto entidadeRetorno = await _produtoNegocio.Inserir(entidade);

            if (entidadeRetorno.Codigo == 0)
                return BadRequest(new { mensagem = "Não foi possivel criar o produto" });
            else
                return Ok(entidadeRetorno);
        }

        // PUT api/<ProdutoController>/5
        /// <summary>
        /// Alterar produto
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="entidade"></param>
        [HttpPut("{codigo:int}")]
        [Authorize(Roles = "administrador,gerente,funcionario")]
        public async Task<IActionResult> Put(int codigo, [FromBody] Produto entidade)
        {
            //Verifica se o Código informado na URL é o mesmo do modelo (Corpo)
            //Não pode alterar o código
            if (entidade.Codigo != codigo)
                return NotFound(new { mensagem = "Você não pode alterar o código da entidade." });

            //Equipara com o ModelState, aplica as validações de entidade
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Produto entidadeRetorno = await _produtoNegocio.Alterar(entidade);

            if (entidadeRetorno.Codigo == 0)
                return BadRequest(new { mensagem = "Não foi possível atualizar a entidade" });
            else
                return Ok(entidade);
        }

        // DELETE api/<ProdutoController>/5
        /// <summary>
        /// Excluir produto
        /// </summary>
        /// <param name="codigo"></param>
        [HttpDelete("{codigo:int}")]
        [Authorize(Roles = "administrador,gerente,funcionario")]
        public async Task<IActionResult> Delete(int codigo)
        {
            bool excluirRetorno = await _produtoNegocio.Excluir(codigo);
            if (excluirRetorno)
                return Ok(new { mensagem = "Produto excluído!" });
            else
                return BadRequest(new { mensagem = "Não foi possível excluir o produto" });
        }
    }
}
