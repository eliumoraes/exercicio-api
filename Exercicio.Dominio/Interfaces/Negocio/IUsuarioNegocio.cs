using Exercicio.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio.Dominio.Interfaces.Negocio
{
    public interface IUsuarioNegocio
    {
        Task<Usuario> Inserir(UsuarioFront entidade);
        Task<Usuario> Alterar(UsuarioFront entidade);
        Task<dynamic> Login(UsuarioLogin entidade);
        Task<List<Usuario>> ConsultarLista();
        Task<bool> Excluir(int codigo);
    }
}
