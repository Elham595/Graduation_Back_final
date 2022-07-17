using graduation_project.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using graduation_project.Const;
using graduation_project.NonDomainModels;


namespace graduation_project.Repository
{
    public class BaseRepositoryServices<TEntity, TDbContext> : IBaseRepositoryServices<TEntity>
        where TEntity : class
        where TDbContext : DbContext
    {
        private readonly TDbContext _TDbContext;

        public BaseRepositoryServices(TDbContext tdbcontext)
        {
            _TDbContext = tdbcontext;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            IQueryable<TEntity> TEntityList = _TDbContext.Set<TEntity>();

            return await TEntityList.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAll(int take)
        {
            IQueryable<TEntity> query = _TDbContext.Set<TEntity>();
            if (take > 0)
                query.Take(take);
            return await query.ToListAsync();

        }

        public async Task<IEnumerable<TEntity>> GetAll(int skip, int? take)
        {

            IQueryable<TEntity> query = _TDbContext.Set<TEntity>();
            if (skip > 0)
                query = query.Skip(skip);
            if (take.HasValue && take.Value > 0)
                query = query.Take(take.Value);

            return await query.ToListAsync();

        }
        public async Task<PagesInformation> GetPageInformation()
        {
            int TotalItmes = await _TDbContext.Set<TEntity>().CountAsync();
            double TotalPages = Math.Ceiling(TotalItmes * 1.0 / Paginations.NumberOfItems);
            PagesInformation pagesInformation = new PagesInformation { TotalPages = TotalPages, TotalItems = TotalItmes };
            return pagesInformation;
        }
        public async Task<PagesInformation> GetPageInformation(Expression<Func<TEntity,bool>> Criteria)
        {
           int TotalItmes =  await _TDbContext.Set<TEntity>().CountAsync(Criteria);
            double TotalPages = Math.Ceiling(TotalItmes*1.0 / Paginations.NumberOfItems);
            PagesInformation pagesInformation = new PagesInformation { TotalPages = TotalPages , TotalItems= TotalItmes};
            return  pagesInformation;
        }

      public async  Task<PagesInformation> GetPageInformation(Expression<Func<TEntity, bool>> Criteria, Expression<Func<TEntity, object>>[] Includes)
        {
            IQueryable<TEntity> query = _TDbContext.Set<TEntity>();
            foreach (var Include in Includes)
            {
                query = query.Include(Include);
            }
            int TotalItmes = await query.CountAsync(Criteria);
            double TotalPages = Math.Ceiling(TotalItmes * 1.0 / Paginations.NumberOfItems);
            PagesInformation pagesInformation = new PagesInformation { TotalPages = TotalPages, TotalItems = TotalItmes };
            return pagesInformation;
        }


        public async Task<IEnumerable<object>> GetAllPagination(int page, int numberOfItems, Expression<Func<TEntity, bool>>? criteria, Expression<Func<TEntity, object>>? selector)
        {
            if (numberOfItems > 0)
            {

                if (criteria is not null)
                {
                    var query = _TDbContext.Set<TEntity>().Where(criteria).Skip((page - 1) * numberOfItems).Take(numberOfItems);
                    if (selector is not null)
                    {
                        IEnumerable<object> list = await query.Select(selector).ToListAsync();
                        return list;
                    }
                    return await query.ToListAsync();
                }

                else
                {
                    var query = _TDbContext.Set<TEntity>().Skip((page - 1) * numberOfItems).Take(numberOfItems);
                    if (selector is not null)
                    {
                        IEnumerable<object> list = await query.Select(selector).ToListAsync();
                        return list;
                    }
                    return await query.ToListAsync();
                }
            }
            throw new ArgumentOutOfRangeException(nameof(numberOfItems), "Must Be Large Than 0 ");
        }

        public async Task<IEnumerable<TEntity>> GetAllPagination(int page, int numberOfItems, Expression<Func<TEntity, bool>>? criteria)
        {
            if (numberOfItems > 0)
            {

                if (criteria is not null)
                {
                    var query = _TDbContext.Set<TEntity>().Where(criteria).Skip((page - 1) * numberOfItems).Take(numberOfItems);
                    return await query.ToListAsync();
                }

                else
                {
                    var query = _TDbContext.Set<TEntity>().Skip((page - 1) * numberOfItems).Take(numberOfItems);
                    return await query.ToListAsync();
                }
            }
            throw new ArgumentOutOfRangeException(nameof(numberOfItems), "Must Be Large Than 0 ");
        }

        public async Task<IEnumerable<object>> GetAllPagination(int page, int numberOfItems, Expression<Func<TEntity, object>>? selector)
        {
            if (numberOfItems > 0)
            {
                IQueryable<TEntity> query = _TDbContext.Set<TEntity>().Skip((page - 1) * numberOfItems).Take(numberOfItems);

                if (selector is not null)
                {
                    IEnumerable<object> list = await query.Select(selector).ToListAsync();
                    return list;
                }
                return await query.ToListAsync();

            }
            throw new ArgumentOutOfRangeException(nameof(numberOfItems), "Must Be Large Than 0 ");
        }

        public async Task<IEnumerable<TEntity>> GetAllPagination(int page, int numberOfItems, Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, object>> orderSelector, string orderby = OrderByValues.Ascending)
        {
            if (numberOfItems > 0)
            {
                IQueryable<TEntity> query;
                if (orderby == OrderByValues.Descending)
                    query = _TDbContext.Set<TEntity>().Where(criteria).OrderByDescending(orderSelector).Skip((page - 1) * numberOfItems).Take(numberOfItems);
                else
                    query = _TDbContext.Set<TEntity>().Where(criteria).OrderByDescending(orderSelector).Skip((page - 1) * numberOfItems).Take(numberOfItems);

                return await query.ToListAsync();

            }
            throw new ArgumentOutOfRangeException(nameof(numberOfItems), "Must Be Large Than 0 ");
        }


        public async Task<IEnumerable<TEntity>> GetAllPagination(int page, int numberOfItems, Expression<Func<TEntity, object>> orderSelector, string orderby = OrderByValues.Ascending)
        {
            if (numberOfItems > 0)
            {
                IQueryable<TEntity> query;
                if (orderby == OrderByValues.Descending)
                    query = _TDbContext.Set<TEntity>().OrderByDescending(orderSelector).Skip((page - 1) * numberOfItems).Take(numberOfItems);
                else
                    query = _TDbContext.Set<TEntity>().OrderByDescending(orderSelector).Skip((page - 1) * numberOfItems).Take(numberOfItems);

                return await query.ToListAsync();

            }
            throw new ArgumentOutOfRangeException(nameof(numberOfItems), "Must Be Large Than 0 ");
        }



        public async Task<IEnumerable<TEntity>> GetAllPagination(int page, int numberOfItems, Expression<Func<TEntity, bool>> criteria, Expression<Func<TEntity, object>>[] Inckudes, Expression<Func<TEntity, object>> orderSelector, string orderby = OrderByValues.Ascending)
        {
            if (numberOfItems > 0)
            {
                IQueryable<TEntity> query= _TDbContext.Set<TEntity>();
                foreach (var included in Inckudes)
                {
                    query = query.Include(included);
                }
                if (orderby == OrderByValues.Descending)
                    query = query.Where(criteria).OrderByDescending(orderSelector).Skip((page - 1) * numberOfItems).Take(numberOfItems);
                else
                    query = _TDbContext.Set<TEntity>().Where(criteria).OrderByDescending(orderSelector).Skip((page - 1) * numberOfItems).Take(numberOfItems);

                return await query.ToListAsync();

            }
            throw new ArgumentOutOfRangeException(nameof(numberOfItems), "Must Be Large Than 0 ");
        }


        
        public async Task<IEnumerable<TEntity>> GetAllPagination(int page, int numberOfItems, Expression<Func<TEntity, object>>[] Inckudes, Expression<Func<TEntity, object>> orderSelector, string orderby = OrderByValues.Ascending)
        {

            if (numberOfItems > 0)
            {
                IQueryable<TEntity> query = _TDbContext.Set<TEntity>();
                foreach (var included in Inckudes)
                {
                    query = query.Include(included);
                }
                if (orderby == OrderByValues.Descending)
                    query = query.OrderByDescending(orderSelector).Skip((page - 1) * numberOfItems).Take(numberOfItems);
                else
                    query = _TDbContext.Set<TEntity>().OrderByDescending(orderSelector).Skip((page - 1) * numberOfItems).Take(numberOfItems);

                return await query.ToListAsync();

            }
            throw new ArgumentOutOfRangeException(nameof(numberOfItems), "Must Be Large Than 0 ");

        }


        public async  Task<IEnumerable<TEntity>> GetAllPagination(int page, int numberOfItems)
        {
            if (numberOfItems > 0)
            {
                List<TEntity> list = await _TDbContext.Set<TEntity>().Skip((page - 1) * numberOfItems).Take(numberOfItems).ToListAsync();

                return list;
            }
                throw new ArgumentOutOfRangeException(nameof(numberOfItems),"Must Be Large Than 0 ");
        }

        public async Task<IEnumerable<TEntity>> GetAllPagination(IQueryable<TEntity> query,int page, int numberOfItems)
        {
            if (numberOfItems > 0)
            {
                
                List<TEntity> list = await query.Skip((page - 1) * numberOfItems).Take(numberOfItems).ToListAsync();

                return list;
            }
            throw new ArgumentOutOfRangeException(nameof(numberOfItems), "Must Be Large Than 0 ");
        }

        public async Task<IEnumerable<object>> GetAllPagination(IQueryable<object> query, int page, int numberOfItems)
        {
            if (numberOfItems > 0)
            {

                List<object> list = await query.Skip((page - 1) * numberOfItems).Take(numberOfItems).ToListAsync();

                return list;
            }
            throw new ArgumentOutOfRangeException(nameof(numberOfItems), "Must Be Large Than 0 ");
        }


        public async Task<int> Remove(int id)
        {
            TEntity entity = await _TDbContext.Set<TEntity>().FindAsync(id);
            if (entity is null)
                return -1;
            _TDbContext.Set<TEntity>().Remove(entity);
            int status = await _TDbContext.SaveChangesAsync();
            return status;
        }

        public async Task<int> RemoveRange(Expression<Func<TEntity,bool>> lamda)
        {
            var entities =  _TDbContext.Set<TEntity>().Where(lamda);
            if (entities is null)
                return -1;
            _TDbContext.Set<TEntity>().RemoveRange(entities);
            int status = await _TDbContext.SaveChangesAsync();
            return status;
        }

        public async Task<int> Remove(TEntity entity)
        {
            _TDbContext.Set<TEntity>().Remove(entity);
            return await _TDbContext.SaveChangesAsync();
        }
        public async Task<int> Add(TEntity entity)
        {

            await _TDbContext.Set<TEntity>().AddAsync(entity);
            int status = await _TDbContext.SaveChangesAsync();

            return status;
        }

        public async Task<int> Update(TEntity entity)
        {
            _TDbContext.Set<TEntity>().Update(entity);
          int status = await  _TDbContext.SaveChangesAsync();
            return status;
        }
        public async Task<TEntity?> GetById(int id)
        {
            TEntity? entity = await _TDbContext.Set<TEntity>().FindAsync(id);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda)
        {
            return await _TDbContext.Set<TEntity>().Where(lamda).ToListAsync();
        }

       public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda, int take)
        {
            var query = _TDbContext.Set<TEntity>().Where(lamda);
            if(take>0)
                query = query.Take(take);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda, int take , int skip)
        {
            var query = _TDbContext.Set<TEntity>().Where(lamda);
            if (take > 0)
                query = query.Take(take);
            if (skip > 0)
                query= query.Skip(skip);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda, int take , Expression<Func<TEntity, object>> orderByProperty, string type = OrderByValues.Ascending)
        {
            var query = _TDbContext.Set<TEntity>().Where(lamda);
            if (orderByProperty is not null)
            {
                if (type == OrderByValues.Ascending)
                    query = query.OrderBy(orderByProperty);

                else
                    query = query.OrderByDescending(orderByProperty);
            }
            if (take > 0)
                query = query.Take(take);
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda, Expression<Func<TEntity, object>> orderByProperty, string type=OrderByValues.Ascending)
        {
            var query = _TDbContext.Set<TEntity>().Where(lamda);
            if (orderByProperty is not null)
            {
                if (type == OrderByValues.Ascending)
                    query=query.OrderBy(orderByProperty);

                else
                    query =query.OrderByDescending(orderByProperty);
            }


            return await query.ToListAsync();
        }
        
        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda, Expression<Func<TEntity, object>> orderByProperty, string type, int? take, int? skip)
        {
            var query = _TDbContext.Set<TEntity>().Where(lamda);
            if (orderByProperty is not null)
            {
                if (type == OrderByValues.Ascending)
                    query = query.OrderBy(orderByProperty);

                else
                    query = query.OrderByDescending(orderByProperty);
            }
            if(take.HasValue)
                query= query.Take(take.Value);
            if(skip.HasValue)
                query=query.Skip(skip.Value);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda, string[] Includes)
        {
            IQueryable<TEntity> query = _TDbContext.Set<TEntity>();
            if(Includes.Length > 0)
            {
                foreach(var include in Includes)
                {
                    query = query.Include(include);
                }
            }
            query = query.Where(lamda);
            return await query.ToListAsync();
        }

       public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda, Expression<Func<TEntity, object>>[] Includes)
        {
            IQueryable<TEntity> query = _TDbContext.Set<TEntity>();
            if(Includes.Length > 0)
            {
                foreach(var included in Includes)
                {
                    query= query.Include(included);
                }
            }
            query = query.Where(lamda);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> lamda, Expression<Func<TEntity, object>>[] Includes , int page , int NumberOfItems )
        {
            IQueryable<TEntity> query = _TDbContext.Set<TEntity>();
            if (Includes.Length > 0)
                foreach (var included in Includes)
                {
                    
                    query = query.Include(included);
                   
                }

            query= query.Where(lamda);  
            return await GetAllPagination(query,page,NumberOfItems);           
        }

        public async Task<TEntity?> FindOne(Expression<Func<TEntity, bool>> lamda, Expression<Func<TEntity, object>>[] Includes)
        {
            IQueryable<TEntity> query = _TDbContext.Set<TEntity>();
            if (Includes.Length > 0)
            {
                foreach (var included in Includes)
                {
                    query = query.Include(included);
                }
            }
            query = query.Where(lamda);
            return await query.FirstOrDefaultAsync();
        }


        public async Task<object?> FindOne(Expression<Func<TEntity, bool>> lamda, Expression<Func<TEntity, object>>[] Includes , Expression<Func<TEntity, object>> Selector)
        {
            IQueryable<TEntity> query = _TDbContext.Set<TEntity>();
            if (Includes.Length > 0)
            {
                foreach (var included in Includes)
                {
                    query = query.Include(included);
                }
            }
            var result = query.Where(lamda).Select(Selector);
            return await result.FirstOrDefaultAsync();
        }
        public async Task<TEntity> WhereOne(Expression<Func<TEntity, bool>> lamda)
        {
            TEntity entity = await _TDbContext.Set<TEntity>().Where(lamda).FirstOrDefaultAsync();

            return entity;

        }

        public async Task<object?> WhereOne(Expression<Func<TEntity, bool>> lamda, Expression<Func<TEntity, object>> selector)
        {
            object? obj = await _TDbContext.Set<TEntity>().Where(lamda).Select(selector).FirstOrDefaultAsync();

            return obj;

        }

        public async Task<int> WhereOne(Expression<Func<TEntity, bool>> lamda, Expression<Func<TEntity, int>> selector)
        {
            int Id = await _TDbContext.Set<TEntity>().Where(lamda).Select(selector).FirstOrDefaultAsync();

            return Id;

        }


        public IQueryable<TEntity> WhereMany(Expression<Func<TEntity, bool>> lamda)
        {
            var query = _TDbContext.Set<TEntity>().Where(lamda);

            return query;
        }

        public IQueryable<TEntity> WhereMany(Expression<Func<TEntity, bool>> lamda, int? take, int? skip)
        {
            var query = _TDbContext.Set<TEntity>().Where(lamda);
            if (take.HasValue)
                query = query.Take(take.Value);
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            return query;

        }

        public IQueryable<TEntity> WhereMany(Expression<Func<TEntity, bool>> lamda, int take)
        {
            var query = _TDbContext.Set<TEntity>().Where(lamda);
            if (take > 0)
                query = query.Take(take);
            return query;
        }

       public  string GetImagePath(string table ,string imageName)
        {
            string ImagePath="";
            if(table=="Design")
            {
              ImagePath= Path.Combine(StaticPath.DesignPath, imageName);
            }
            return ImagePath;
        }
    }
}
