using System.Threading.Tasks;
using DesafioLike.Dominio.Entidades;

namespace DesafioLike.Dominio.IRepositorios
{
    public interface IRespostaRepositorio : IBaseRepositorio<Resposta>
    {
         Task<Resposta[]> GetAllRespostaAsync();
         Task<Resposta> ObterPorPerguntaId (int PerguntaId);
    }
}