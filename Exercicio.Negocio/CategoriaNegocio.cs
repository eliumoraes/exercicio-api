using Exercicio.Dominio.Entidades;
using Exercicio.Dominio.Interfaces.Negocio;
using Exercicio.Repositorio.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercicio.Negocio
{
    public class CategoriaNegocio : ICategoriaNegocio
    {
        private readonly DataContext _context;
        private readonly ILogger<Categoria> _logger;

        public CategoriaNegocio(DataContext context, ILogger<Categoria> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Categoria>> ConsultarLista()
        {
            var categorias = await _context.Categoria.AsNoTracking().ToListAsync();
            return categorias;
        }

        public async Task<Categoria> Consultar(int codigo)
        {
            var categoria = await _context.Categoria.AsNoTracking().FirstOrDefaultAsync(x => x.Codigo == codigo);
            return categoria;
        }   

        public async Task<Categoria> Inserir(Categoria entidade)
        {
            entidade.Codigo = 0;

            try
            {                
                _context.Categoria.Add(entidade);
                await _context.SaveChangesAsync();
                return entidade;
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Não foi possivel criar a categoria");
                _logger.LogError(ex.ToString());
                return entidade;
                
            }
            
        }

        public async Task<Categoria> Alterar(Categoria entidade)
        {
            try
            {
                _context.Entry<Categoria>(entidade).State = EntityState.Modified;
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
                _logger.LogError("Não foi possivel altearar a categoria");
                _logger.LogError(ex.ToString());
                entidade.Codigo = 0;
                return (entidade);

            }
            
        }

        public async Task<bool> Excluir(int codigo)
        {
            var categoria = await _context.Categoria.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (categoria == null)
                return false;
            try
            {
                _context.Categoria.Remove(categoria);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                _logger.LogDebug("Não foi possível remover a categoria");
                _logger.LogDebug(ex.ToString());
                return false;
            }
            
        }
    
    }
}
