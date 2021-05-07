using Exercicio.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exercicio.Dominio.Interfaces.Negocio
{
    public interface IProdutoNegocio
    {
        Task<List<Produto>> ConsultarLista();
        Task<Produto> Consultar(int codigo);
        Task<List<Produto>> ConsultarPorCategoria(int codigo);
        Task<Produto> Inserir(Produto entidade);
        Task<Produto> Alterar(Produto entidade);
        Task<bool> Excluir(int codigo);
    }
}
