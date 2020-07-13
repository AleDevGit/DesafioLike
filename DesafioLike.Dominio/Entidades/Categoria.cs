using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesafioLike.Dominio.Entidades
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Descrição obrigatória")]
        [StringLength(100, ErrorMessage="Descrição, máximo de 100 caracteres")]
        public string Descricao { get; set; } 
        public bool Ativo { get; set; }
        public List<Pergunta> Perguntas { get; set; }
    
    }
}