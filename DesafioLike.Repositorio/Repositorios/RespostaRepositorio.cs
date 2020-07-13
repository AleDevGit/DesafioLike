using System.Linq;
using System.Threading.Tasks;
using DesafioLike.Dominio.Entidades;
using DesafioLike.Dominio.IRepositorios;
using DesafioLike.Repositorio.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioLike.Repositorio.Repositorios
{
    public class RespostaRepositorio: BaseRepositorio<Resposta>, IRespostaRepositorio
    {
        public RespostaRepositorio(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<Resposta[]>GetAllRespostaAsync(){

            IQueryable<Resposta> query = DataContext.Respostas
            .Include(e => e.Pergunta)
            .ThenInclude(x => x.Categoria);
            query = query.OrderByDescending(x=>x.Id);
            
            return await query.ToArrayAsync(); 
        }

        public async Task<Resposta> ObterPorPerguntaId(int PerguntaId)
        {
            IQueryable<Resposta> query = DataContext.Respostas
            .Include(e => e.Pergunta)
            .ThenInclude(x => x.Categoria);
            query = query.Where(x=>x.PerguntaId == PerguntaId).OrderByDescending(x=>x.Id);
            
            return await query.FirstOrDefaultAsync(); 
        }
    }
}