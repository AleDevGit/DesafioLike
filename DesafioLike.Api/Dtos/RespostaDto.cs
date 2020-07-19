using System.ComponentModel.DataAnnotations;

namespace DesafioLike.Api.Dtos
{
    public class RespostaDto
    {
        public int Id { get; set; }
        public int PerguntaId { get; set; }
        public PerguntaDto Pergunta { get;  }
        public int UserId { get; set; }
        public UserDto User { get;  }

        [Required(ErrorMessage="Resposta do Usuário é obrigatória.")]
        [StringLength(100, ErrorMessage="Resposta do Usuário, máximo de 100 caracteres")]
        public string RespostaOpcao { get; set; }
    }
}