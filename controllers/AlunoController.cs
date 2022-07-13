using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApiAlunos.Models;
using webApiAlunos.Services;

namespace webApiAlunos.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private IAlunoServices _alunoServices;

        public AlunoController(IAlunoServices alunoServices)
        {
            _alunoServices = alunoServices;
        }


        [HttpGet] // ActionResult: permite que retorne o tipo de response, ou qualquer outro resultado da action
                  // IAsyncEnumerable: com isto não teremos mais uma interação sincrona, afim de evitar erros
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                IEnumerable<Aluno> alunos = await _alunoServices.GetAlunos();
                return Ok(alunos);
            }
            catch
            {
                return BadRequest("Request Inválida");
            }
        }

        [HttpGet("AlunosPorNome")] // FromQuery: Informa que vamos pegar a informação via query string
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByName([FromQuery]string nome)
        {
            try
            {
                IEnumerable<Aluno> alunos = await _alunoServices.GetAlunosByNome(nome);

                if (alunos.Count() == 0)
                    return NotFound("Não existe alunos com o critério: " + nome);

                return Ok(alunos);
            }
            catch
            {
                return BadRequest("Request Inválida");
            }
        }

        [HttpGet("{id:int}", Name="GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            try
            {
                Aluno aluno = await _alunoServices.GetAluno(id);

                if (aluno == null)
                    return NotFound("Não existe aluno com o id: "+ id);

                return Ok(aluno);
            }
            catch
            {
                return BadRequest("Request Inválida");
            }
        }

        [HttpPost] // CreatedAtRoute: Vai retornar um 201 == recurso criado, e vai colocar no cabeçalho location a URI do objeto
                  //  Vai chamar o GetAluno (nameof é para indicar qual o name), 
                  // apos chamar o GetAluno ele vai retornar as infos do aluno recém criado
        public async Task<ActionResult> CreateAluno(Aluno aluno)
        {
            try
            {
                await _alunoServices.CreateAluno(aluno);
                return CreatedAtRoute(nameof(GetAluno), new {id = aluno.Id}, aluno);
            }
            catch
            {
                return BadRequest("Request Inválida");
            }
        }

        [HttpPut("{id:int}")] // FromBody: no corpo do request ele vai enviar o aluno
                 // NoContent == 204 ou seja Alteração realizada com sucess
                // Put == Idenpodente ou seja ele vai produzir o mesmo resultado
                 // mesmo que a solicitação seja feita varias vezes
        public async Task<ActionResult> UpdateAluno(int id, [FromBody]Aluno aluno)
        {
            try
            {
                if (aluno.Id == id)
                {
                    await _alunoServices.UpdateAluno(aluno);
                    // return NoContent();
                    return Ok($"O Aluno com id {aluno.Id} foi atualizado com SUCESSO! ;D");
                }
                else
                {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch
            {
                return BadRequest("Request Inválida");
            }
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAluno(int id)
        {
            try
            {
                Aluno aluno = await _alunoServices.GetAluno(id);

                if (aluno != null)
                {
                    await _alunoServices.DeleteAluno(aluno);
                    return Ok($"Aluno de id = {id} foi excluido com SUCESSO! ;D");
                }
                else
                {
                    return NotFound($"Aluno com id = {id} não encontrado! ;-;");
                }
            }
            catch
            {
                return BadRequest("Request Inválida");
            }
        }
    }
}
