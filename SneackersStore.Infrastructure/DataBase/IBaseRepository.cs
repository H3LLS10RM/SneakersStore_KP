using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneackersStore.Infrastructure.DataBase
{
    internal interface IBaseRepository<TEntity>
    {
        TEntity GetById(long id);
        List<TEntity> GetList();
        TEntity Update(TEntity entity);
        TEntity Delete(long id);
        TEntity Add(TEntity entity);
        List<TEntity> Search(string search);

    }

}
