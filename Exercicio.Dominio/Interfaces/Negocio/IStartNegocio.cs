using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Exercicio.Dominio.Interfaces.Negocio
{
    public interface IStartNegocio
    {
        public Task<ActionResult<dynamic>> Iniciar();
    }
}
