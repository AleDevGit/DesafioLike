using System.Threading.Tasks;
using DesafioLike.Dominio.Entidades;

namespace DesafioLike.Dominio.IRepositorios
{
    public interface ICategoriaRepositorio : IBaseRepositorio<Categoria>
    {
         Task<Categoria> ObterPorIdCategoria(int categoriaId);
         Task<Categoria[]> ObterTodosCategoria();
    }
}