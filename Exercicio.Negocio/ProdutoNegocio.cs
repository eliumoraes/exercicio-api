using Exercicio.Dominio.Entidades;
using Exercicio.Dominio.Interfaces.Negocio;
using Exercicio.Repositorio.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio.Negocio
{
    public class ProdutoNegocio : IProdutoNegocio
    {
        private readonly DataContext _context;
        private readonly ILogger<Produto> _logger;

        public ProdutoNegocio(DataContext context, ILogger<Produto> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<Produto>> ConsultarLista()
        {
            var produtos = await _context
                .Produto
                .Include(x => x.Categoria) //Fazendo Join para trazer a cat
                .AsNoTracking().ToListAsync();
            return produtos;
        }
        public async Task<Produto> Consultar(int codigo)
        {
            var produto = await _context
                .Produto
                .Include(x=> x.Categoria)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Codigo == codigo);
            return produto;
        }
        public async Task<List<Produto>> ConsultarPorCategoria(int codigo)
        {
            var produtos = await _context
                .Produto
                .Include(x => x.Categoria)
                .AsNoTracking()
                .Where(x => x.CategoriaCodigo == codigo)
                .ToListAsync();
            return produtos;
        }
        public async Task<Produto> Inserir(Produto entidade)
        {
            entidade.Codigo = 0;

            try
            {
                _context.Produto.Add(entidade);
                await _context.SaveChangesAsync();
                return entidade;
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Não foi possivel criar o produto");
                _logger.LogError(ex.ToString());
                return entidade;

            }
        }
        public async Task<Produto> Alterar(Produto entidade)
        {
            try
            {
                _context.Entry<Produto>(entidade).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entidade;
            }
            catch (DbUpdateConcurrencyException erroConcorrencia)
            {
                _logger.LogError("Erro de concorrência, registro já estava sendo atualizado.");
                _logger.LogError(erroConcorrencia.ToString());
                entidade.Codigo = 0;
                return (entidade);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Não foi alterar criar o produto");
                _logger.LogError(ex.ToString());
                entidade.Codigo = 0;
                return (entidade);

            }
        }
        public async Task<bool> Excluir(int codigo)
        {
            var produto = await _context.Produto.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (produto == null)
                return false;
            try
            {
                _context.Produto.Remove(produto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                _logger.LogDebug("Não foi possível remover o produto");
                _logger.LogDebug(ex.ToString());
                return false;
            }
        }


    }
}