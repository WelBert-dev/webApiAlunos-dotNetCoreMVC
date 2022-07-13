using Microsoft.EntityFrameworkCore;
using webApiAlunos.Models;

namespace webApiAlunos.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Verifica se tem dados no "Aluno" e se não tiver inclui 2 novos
            // para atulizar essas infos utilizar: No Nuget: add-migration Nome-Da-Migration (add-migration PopulaTabela)
            // para rodar a migration criada: update-database

            modelBuilder.Entity<Aluno>().HasData(
                new Aluno
                {
                    Id = 1,
                    Nome = "Wellison da Cruz Bertelli",
                    Email = "wellison.bertelli@hotmail.com",
                    Idade = 22
                },
                new Aluno
                {
                    Id = 2,
                    Nome = "Sonin Bleinin",
                    Email = "sonin@hotmail.com",
                    Idade = 69
                }
                );
        }
    }
}
