using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesafioLike.Api.Dtos
{
    public class PerguntaDto
    {
        public int Id { get; set; } 
        public CategoriaDto Categoria { get; } 
        
        [Required(ErrorMessage="Descrição obrigatória")]
        [StringLength(100, ErrorMessage="Descrição, máximo de 100 caracteres")]
        public string Descricao { get; set; }   

        [Required(ErrorMessage="Opção de Resposta obrigatória.")]
        [StringLength(100, ErrorMessage="Opção de Resposta, máximo de 100 caracteres")]
        public string Opcao1 { get; set; }
        
        [Required(ErrorMessage="Opção de Resposta obrigatória.")]
        [StringLength(100, ErrorMessage="Opção de Resposta, máximo de 100 caracteres")]
        public string Opcao2 { get; set; }
        public bool Ativo { get; set; }
 //       public List<RespostaDto> Respostas { get; set; }
    }
}