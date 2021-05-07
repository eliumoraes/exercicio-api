using Exercicio.Dominio.Entidades;
using Exercicio.Dominio.Interfaces.Negocio;
using Exercicio.Repositorio.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio.Negocio
{
    public class StartNegocio : IStartNegocio
    {
        private readonly DataContext _context;
        public StartNegocio(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Essa função gera os dados iniciais para testar a API
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<dynamic>> Iniciar()
        {
            var administrador = new Usuario { Codigo = 1, Nome = "administrador", Senha = "123", Cargo = "administrador" };
            var gerente = new Usuario { Codigo = 2, Nome = "gerente", Senha = "123", Cargo = "gerente" };
            var funcionario = new Usuario { Codigo = 3, Nome = "funcionario", Senha = "123", Cargo = "funcionario" };
            var categoria = new Categoria { Codigo = 1, Titulo = "Não Perecíveis" };
            var produto = new Produto { Codigo = 1, Titulo = "Arroz branco", Categoria = categoria, Preco = 35, Descricao = "Arroz longo fino tipo 1"  };

            _context.Usuario.Add(administrador);
            _context.Usuario.Add(gerente);
            _context.Usuario.Add(funcionario);
            _context.Categoria.Add(categoria);
            _context.Produto.Add(produto);

            await _context.SaveChangesAsync();

            return new { mensagem = "Dados configurados com sucesso" };
        }
    }
}
