using graduation_project.Const;
using graduation_project.NonDomainModels;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace graduation_project.Repository
{
    public interface IBaseRepositoryServices<TEntity>
    {
         Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> GetAll(int take);

        Task<IEnumerable<TEntity>> GetAll(int skip, int? take);
        Task<PagesInformation> GetPageInformation();
        Task<PagesInformation> GetPageInformation(Expression<Func<TEntity, bool>> Criteria);

        Task<PagesInformation> GetPageInformation(Expression<Func<TEntity, bool>> Criteria, Expression<Func<TEntity, object>>[] Inckudes);


        Task<IEnumerable<object>> GetAllPagination(int page, int numberOfItems, Expression<Func<TEntity, bool>>? criteria, Expression<Func<TEntity, object>>? selector);

         Task<IEnumerable<TEntity>> GetAllPagination(int page, int numberOfItems, Expression<Func<TEntity, bool>>? criteria);

         Task<IEnumerable<object>> GetAllPagination(int page, int numberOfItems, Expression<Func<TEntity, object>>? selector);

        Task<IEnumerable<TEntity>> GetAllPagination(int page, int numberOfItems, Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, object>> orderSelector , string orderby = OrderByValues.Ascending);

        Task<IEnumerable<TEntity>> GetAllPagination(int page, int numberOfItems);

        Task<IEnumerable<TEntity>> GetAllPagination(int page, int numberOfItems, Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, object>>[]Inckudes, Expression<Func<TEntity, object>> orderSelector, string orderby = OrderByValues.Ascending);

        Task<IEnumerable<TEntity>> GetAllPagination(int page, int numberOfItems, Expression<Func<TEntity, object>> orderSelector, string orderby = OrderByValues.Ascending);
        Task<IEnumerable<TEntity>> GetAllPagination(int page, int numberOfItems, Expression<Func<TEntity, object>>[] Inckudes, Expression<Func<TEntity, object>> orderSelector, string orderby = OrderByValues.Ascending);

        Task<IEnumerable<TEntity>> GetAllPagination(IQueryable<TEntity> query, int page, int numberOfItems);

        Task<TEntity?> GetById(int id);

        Task<TEntity> WhereOne(Expression<Func<TEntity, bool>> lamda);
        Task<object?> WhereOne(Expression<Func<TEntity, bool>> lamda, Expression<Func<TEntity, object>> selector);

        Task<int> WhereOne(Expression<Func<TEntity, bool>> lamda, Expression<Func<TEntity, int>> selector);
        IQueryable<TEntity> WhereMany(Expression<Func<TEntity, bool>> lamda);

        IQueryable<TEntity> WhereMany(Expression<Func<TEntity, bool>> lamda,int? take , int? skip);

        IQueryable<TEntity> WhereMany(Expression<Func<TEntity, bool>> lamda, int take);


        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda,int take);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda, int take, int skip);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda , Expression<Func<TEntity, object>> orderByProperty, string type=OrderByValues.Ascending);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda,int take , Expression<Func<TEntity,object>> orderByProperty, string type = OrderByValues.Ascending);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda, Expression<Func<TEntity, object>> orderByProperty, string type, int? take , int? skip);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda, string[] Includes);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda, Expression<Func<TEntity, object>>[] Includes);
        Task<TEntity?> FindOne(Expression<Func<TEntity, bool>> lamda, Expression<Func<TEntity, object>>[] Includes);

        Task<object?> FindOne(Expression<Func<TEntity, bool>> lamda, Expression<Func<TEntity, object>>[] Includes, Expression<Func<TEntity, object>> Selector);

        Task<IEnumerable<object>> GetAllPagination(IQueryable<object> query, int page, int numberOfItems);

        Task<int> Add(TEntity tentity);
        Task<int> Remove(int id);
        Task<int> Remove(TEntity entity);

        Task<int> RemoveRange(Expression<Func<TEntity, bool>> lamda);
        Task<int> Update(TEntity entity);

        string GetImagePath(string table, string imageName);



    }
}
