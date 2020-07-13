using System.Collections.Generic;

namespace DesafioLike.Dominio.Entidades
{
    public class Pergunta
    {
        public int Id { get; set; } 
        public int? CategoriaId { get; set; }   
        public Categoria Categoria { get; } 
        public string Descricao { get; set; }   
        public string Opcao1 { get; set; }
        public string Opcao2 { get; set; }
        public bool Ativo { get; set; }
        public List<Resposta> Respostas { get; set; }

    }
}