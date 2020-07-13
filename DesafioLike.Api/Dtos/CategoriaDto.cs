using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesafioLike.Api.Dtos
{
    public class CategoriaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Descrição obrigatória")]
        [StringLength(100, ErrorMessage="Descrição, máximo de 100 caracteres")]
        public string Descricao { get; set; } 
        public bool Ativo { get; set; }
        public List<PerguntaDto> Perguntas { get; set; }
    
    }
}