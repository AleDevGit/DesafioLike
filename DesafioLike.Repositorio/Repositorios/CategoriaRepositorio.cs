using System.Linq;
using System.Threading.Tasks;
using DesafioLike.Dominio.Entidades;
using DesafioLike.Dominio.IRepositorios;
using DesafioLike.Repositorio.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioLike.Repositorio.Repositorios
{
    public class CategoriaRepositorio : BaseRepositorio<Categoria>, ICategoriaRepositorio
    {
        public CategoriaRepositorio(DataContext dataContext) : base(dataContext)
        {
            
        }
        public async Task<Categoria> ObterPorIdCategoria(int categoriaId)
        {
            IQueryable<Categoria> query = DataContext.Categorias;//.Include(a => a.Perguntas);
            query = query.AsNoTracking().OrderByDescending(x => x.Descricao)
            .Where(x => x.Id == categoriaId);
            
            return await query.FirstOrDefaultAsync(); 
        }

        public async Task<Categoria[]> ObterTodosCategoria()
        {
            IQueryable<Categoria> query = DataContext.Categorias.Include(a => a.Perguntas);
            query = query.AsNoTracking().OrderByDescending(x => x.Descricao);
            
            return await query.ToArrayAsync(); 
        }
    }
    
}