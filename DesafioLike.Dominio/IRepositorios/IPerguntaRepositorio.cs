using System.Threading.Tasks;
using DesafioLike.Dominio.Entidades;

namespace DesafioLike.Dominio.IRepositorios
{
    public interface IPerguntaRepositorio : IBaseRepositorio<Pergunta>
    {
        Task<Pergunta[]> GetAllPerguntaAsync();
        Task<Pergunta[]> ObterPorCategoriaId(int categoriaId);
        Task<Pergunta[]> ObterPorPerguntaId(int PerguntaId);
        Task<Pergunta> ObterProximaPergunta(int Id);

    }
}