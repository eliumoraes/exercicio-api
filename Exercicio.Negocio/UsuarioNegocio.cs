using Exercicio.Dominio.Entidades;
using Exercicio.Dominio.Entidades.DTO;
using Exercicio.Dominio.Interfaces.Negocio;
using Exercicio.Repositorio.Repositorios;
using Exercicio.ServicosExternos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exercicio.Negocio
{
    public class UsuarioNegocio : IUsuarioNegocio
    {
        private readonly DataContext _context;
        private readonly ILogger<Produto> _logger;
        private readonly IViaCep _viaCep;
        public UsuarioNegocio(
            DataContext context, 
            ILogger<Produto> logger,
            IViaCep viaCep)
        {
            _logger = logger;
            _context = context;
            _viaCep = viaCep;
        }
        public async Task<Usuario> Inserir(UsuarioFront entidade)
        {
            entidade.Codigo = 0;
            Usuario entidadeBanco = new Usuario(entidade);

            //Chamar função para preencher endereço.
            var resultado = _viaCep.ConsultarCep(entidade.Cep).Result;

            if(resultado != null)
            {
                var endereco = new Cep(resultado);

                entidadeBanco.Logradouro = endereco.Logradouro;
                entidadeBanco.Bairro = endereco.Bairro;
                entidadeBanco.Cidade = endereco.Cidade;                
            }

            

            try
            {
                _context.Usuario.Add(entidadeBanco);
                await _context.SaveChangesAsync();
                return entidadeBanco;
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Não foi possivel criar o usuário");
                _logger.LogError(ex.ToString());
                return entidadeBanco;

            }
        }
        public async Task<Usuario> Alterar(UsuarioFront entidade)
        {
            Usuario entidadeBanco = new Usuario(entidade);

            try
            {   
                _context.Entry<Usuario>(entidadeBanco).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entidadeBanco;
            }
            catch (DbUpdateConcurrencyException erroConcorrencia)
            {
                _logger.LogError("Erro de concorrência, registro já estava sendo atualizado.");
                _logger.LogError(erroConcorrencia.ToString());
                entidade.Codigo = 0;
                return (entidadeBanco);
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Não foi possivel altearar o usuário");
                _logger.LogError(ex.ToString());
                entidade.Codigo = 0;
                return (entidadeBanco);
            }
        }
        public async Task<dynamic> Login(UsuarioLogin entidade)
        {

            UsuarioFront usuarioFrontLogin = new UsuarioFront(entidade);
            Usuario entidadeLogin = new Usuario(usuarioFrontLogin);

            var usuario = await _context.Usuario
                .AsNoTracking()
                .Where(x=> x.Nome == entidadeLogin.Nome && x.Senha == entidadeLogin.Senha)
                .FirstOrDefaultAsync();

            if (usuario == null)
            {
                return new
                {
                    usuario = new Usuario { },
                    mensagem = "Não foi possível efetuar login, usuário ou senha incorretos."
                };
            }
            else
            {
                usuario.Senha = "********";
            }

            var token = "Bearer " + UsuarioTokenService.GenerateToken(usuario);
            return new 
            {
                usuario = usuario,
                token = token
            };
        }
        public async Task<List<Usuario>> ConsultarLista()
        {
            var usuarios = await _context.Usuario.AsNoTracking().ToListAsync();
            return usuarios;
        }
        public async Task<bool> Excluir(int codigo)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(x => x.Codigo == codigo);
            if (usuario == null)
                return false;
            try
            {
                _context.Usuario.Remove(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                _logger.LogDebug("Não foi possível remover o usuário");
                _logger.LogDebug(ex.ToString());
                return false;
            }
        }
    }
}
