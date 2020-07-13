using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioLike.Dominio.IRepositorios;
using DesafioLike.Repositorio.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioLike.Repositorio.Repositorios
{
    public class BaseRepositorio<TEntity> : IBaseRepositorio<TEntity> where TEntity : class
    {
        protected readonly DataContext DataContext;

        public BaseRepositorio(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public void Adicionar(TEntity entity)
        {
            DataContext.Set<TEntity>().Add(entity);
            DataContext.SaveChanges();
        }

        public void Atualizar(TEntity entity)
        {
            DataContext.Set<TEntity>().Update(entity);
            DataContext.SaveChanges();
        }
        
        public async Task<TEntity> ObterPorId(int id)
        {
            return await DataContext.Set<TEntity>().FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> Obtertodos()
        {
            IEnumerable<TEntity> lists = await DataContext.Set<TEntity>().ToListAsync();
            return lists;
        }


        public void Remover(TEntity entity)
        {
            DataContext.Remove(entity);
            DataContext.SaveChanges();
        }

        public void Dispose()
        {
            DataContext.Dispose();
        }

    }
}