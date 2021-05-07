using Exercicio.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Exercicio.Repositorio.Repositorios
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
