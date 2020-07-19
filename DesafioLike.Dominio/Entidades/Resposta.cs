using DesafioLike.Dominio.Identity;

namespace DesafioLike.Dominio.Entidades
{
    public class Resposta
    {
        public int Id { get; set; }
        public int PerguntaId { get; set; }
        public Pergunta Pergunta { get;  }
        public int? UserId { get; set; }
        public User User { get;  }
        public string RespostaOpcao { get; set; }
 
    }
}