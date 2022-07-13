using System.ComponentModel.DataAnnotations;

namespace webApiAlunos.Models
{
    public class Aluno
    {
        [Key]
        public int Id { get; set; }
        [Required]  // define que é um campo obrigatório
        [StringLength(80)] // define o limite da coluna varchar, poderia utilizar tbm "ErrorMessage = mensagem"
        public string Nome { get; set; }
        [EmailAddress] // define que deve ser um email
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public int Idade { get; set; }
    }
}
