using Exercicio.Dominio.Entidades.Externas;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exercicio.ServicosExternos
{
    public class ViaCep : IViaCep
    {
        private string _endpoint;
        private HttpClient _httpClient;

        public ViaCep(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _endpoint = configuration.GetSection("ViaCep:endPoint").Value;
            _httpClient = httpClientFactory.CreateClient("HttpClient");

        }


        public async Task<ViaCepResponse> ConsultarCep(string cep)
        {
            var resultado = await _httpClient.GetAsync(string.Format(_endpoint, cep));

            if (resultado.IsSuccessStatusCode
                && resultado.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string json = await resultado.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<ViaCepResponse>(json);
            }
            return null;

        }
    }
}
