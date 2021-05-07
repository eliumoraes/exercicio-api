using Exercicio.Dominio.Interfaces.Negocio;
using Exercicio.Negocio;
using Exercicio.Repositorio.Repositorios;
using Exercicio.ServicosExternos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace Exercicio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InjecaoDeDependencia(services);

            services.AddRouting(options => options.LowercaseUrls = true); //Deixa as URL's com letras minúsculas

            services.AddHttpClient();

            // O método AddJsonOptions permite a customização das configurações de serialização
            // Ignorar propriedades nulas
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            //Permite requisições localhost para a API
            services.AddCors();

            ////Menor trafego de dados
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });            

            services.AddControllers();

            //services.AddResponseCaching(); //Deixa toda a api com cache!

            var key = Encoding.ASCII.GetBytes(Exercicio.Negocio.Settings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            //No fim do método para maior performance
            services.AddSwaggerGen();

            CarregarSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Exercicio - Eliu Moraes");
                c.RoutePrefix = "v1/api/swagger";
            });

            app.UseRouting();

            //Permite todas as origens, métodos e cabeçalhos.
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InjecaoDeDependencia(IServiceCollection services)
        {
            #region Negocio
            services.AddScoped<IProdutoNegocio, ProdutoNegocio>();
            services.AddScoped<ICategoriaNegocio, CategoriaNegocio>();
            services.AddScoped<IUsuarioNegocio, UsuarioNegocio>();
            services.AddScoped<IStartNegocio, StartNegocio>();
            #endregion Negocio

            #region ServicosExternos
            services.AddScoped<IViaCep, ViaCep>();
            #endregion ServicosExternos

            #region Repositorio
            //services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();

            //Utilizar o banco InMemory auxilia na hora de iniciar a criação da api, sem precisar do banco
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));

            //Banco de dados real
            //services.AddDbContext<DataContext>(
            //    opt => opt.UseSqlServer(
            //        Configuration.GetConnectionString("connectionString"),
            //        b => b.MigrationsAssembly("Exercicio")
            //        )
            //    );

            //AddScoped garante uma conexão com o banco por requisição.  || Se utilizar o AddDbContext não precisa do AddScoped no banco
            //services.AddScoped<DataContext, DataContext>();
            #endregion Repositorio
        }

        private void CarregarSwagger(IServiceCollection services)
        {
            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Api Exercicio - Eliu Moraes",
                    Version = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                    Description = "Api realizada para demonstrar os conhecimentos adquiridos.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Curso",
                        Email = string.Empty,
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "© Copyright Curso.Todos os Direitos Reservados.",
                    }
                });


                string caminhoAplicacao =
              PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc =
                Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);

            });
        }
    }
}
