using Exercicio.Dominio.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exercicio.ServicosExternos
{
    public interface IViaCep
    {
        Task<ViaCepResponse> ConsultarCep(string cep);
    }
}
