using Microsoft.EntityFrameworkCore;
using webApiAlunos.Context;
using webApiAlunos.Models;

namespace webApiAlunos.Services
{
    public class AlunoServices : IAlunoServices
    {
        private readonly AppDbContext _context;

        public AlunoServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            try
            {
                return await _context.Alunos.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<Aluno> GetAluno(int id)
        {
            try
            {
                Aluno aluno;
                aluno = await _context.Alunos.FindAsync(id); // primeiro ele busca na memória, caso n encontre ele faz a query 
                return aluno;                               // ganhando assim desempenho
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<Aluno>> GetAlunosByNome(string nome)
        {
            try
            {
                IEnumerable<Aluno> alunos;
                if (!string.IsNullOrEmpty(nome))
                {
                    alunos = await _context.Alunos.Where(a => a.Nome.Contains(nome)).ToListAsync();
                }
                else
                {
                    alunos = await GetAlunos();
                }
                return alunos;
            }
            catch
            {
                throw;
            }
        }


        public async Task CreateAluno(Aluno aluno)
        {
            try
            {
                _context.Alunos.Add(aluno);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task UpdateAluno(Aluno aluno)
        {
            try
            {
                _context.Entry(aluno).State = EntityState.Modified; // faz ele atualizar 
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task DeleteAluno(Aluno aluno)
        {
            try
            {
                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
