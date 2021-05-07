using Exercicio.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercicio.Dominio.Interfaces.Negocio
{
    public interface ICategoriaNegocio
    {
        Task<List<Categoria>> ConsultarLista(); 
        Task<Categoria> Consultar(int codigo);
        Task<Categoria> Inserir(Categoria entidade);
        Task<Categoria> Alterar(Categoria entidade);
        Task<bool> Excluir(int codigo);
    }
}
