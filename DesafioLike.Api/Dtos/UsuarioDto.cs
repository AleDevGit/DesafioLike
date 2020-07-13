using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesafioLike.Api.Dtos
{
    public class UsuarioDto
    {
       public int Id { get; set;}

       [Required(ErrorMessage="CPF é obrigatório.")]
       public string CPF { get; set; }
       
       [Required(ErrorMessage="Senha é obrigatório.")]
       public string Senha { get; set; }
       
       [Required(ErrorMessage="Ano Nascimento é obrigatório.")] 
       public int AnoNasc { get; set; } 

       [Required(ErrorMessage="Sexo é obrigatório.")]
       public string Sexo { get; set; }
        
       public List<RespostaDto> Respostas { get; set; }
    }
}