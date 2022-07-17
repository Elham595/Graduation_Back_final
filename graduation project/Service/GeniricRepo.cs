using graduation_project.Data;
using graduation_project.Models;

namespace graduation_project.Service
{
    public class GeniricRepo<TResult> where TResult : class, IEntity
    {
        FashionDesignContext db;

        public TResult GetItem(int Id)
        {
           return db.Set<TResult>().FirstOrDefault(e => e.Id == Id);
        }

        public void InsertEntity(TResult entity)
        {
             db.Set<TResult>().Add(entity);
        }

    }
}
