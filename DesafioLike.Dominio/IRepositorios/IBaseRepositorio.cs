using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioLike.Dominio.IRepositorios
{
    public interface IBaseRepositorio<TEntity>:IDisposable where TEntity : class
    {
        void Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(int id);
        Task<IEnumerable<TEntity>> Obtertodos();
        void Atualizar(TEntity entity);
        void Remover(TEntity entity);
    }
}