﻿using webApiAlunos.Models;

namespace webApiAlunos.Services
{
    public interface IAlunoServices
    {
        Task<IEnumerable<Aluno>> GetAlunos();

        Task<Aluno> GetAluno(int id);

        Task<IEnumerable<Aluno>> GetAlunosByNome(string nome);

        Task CreateAluno(Aluno aluno);

        Task UpdateAluno(Aluno aluno);

        Task DeleteAluno(Aluno aluno);
    }
}
