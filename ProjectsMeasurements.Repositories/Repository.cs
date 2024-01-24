using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProjectsMeasurements.DBContext;
using ProjectsMeasurements.Models.Security;
using ProjectsMeasurements.Models.BaseModels;
using ProjectsMeasurements.Repositories.Interfaces;

namespace ProjectsMeasurements.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {

        #region Members

        protected ProjectsMeasurementsDBContext _context;

        #endregion

        #region Constructor

        public Repository(ProjectsMeasurementsDBContext Context)
        {
            _context = Context;
        }

        #endregion

        #region Select Methods

        public virtual async Task<bool> Any(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }
        public virtual async Task<T?> Get(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> Get(Func<T, object> sort, int skip, int take, bool ascending)
        {
            return await Get(null, sort, skip, take, ascending);
        }
        public virtual async Task<IEnumerable<T>> Get(Expression<Func<T, bool>>? predicate, Func<T, object> sort, int skip, int take, bool ascending)
        {
            if (predicate == null)
            {
                if (sort == null)
                    return await _context.Set<T>().Skip(skip).Take(take).ToListAsync();
                else
                    return await Task.Run(() => ascending ? _context.Set<T>().OrderBy(sort).Skip(skip).Take(take).ToList() : _context.Set<T>().OrderByDescending(sort).Skip(skip).Take(take).ToList());
            }
            else
            {
                if (sort == null)
                    return await _context.Set<T>().Where(predicate).Skip(skip).Take(take).ToListAsync();
                else
                    return await Task.Run(() => ascending ? _context.Set<T>().Where(predicate).OrderBy(sort).Skip(skip).Take(take).ToList() : _context.Set<T>().Where(predicate).OrderByDescending(sort).Skip(skip).Take(take).ToList());
            }
        }
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public virtual async Task<IEnumerable<T>> GetQuery(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }
        public virtual async Task<string> GetData(string sqlStatement, CommandType commandType, params SqlParameter[] parameters)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(await _context.Database.ExecuteQueryAsync(sqlStatement, commandType, parameters));
        }
        public virtual async Task<string> GetDataSet(string sqlStatement, CommandType commandType, params SqlParameter[] parameters)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(await _context.Database.ExecuteQueryAsync(sqlStatement, commandType, parameters));
        }
        public virtual string GetSqlData(string sqlStatement, CommandType commandType, params SqlParameter[] parameters)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(_context.Database.ExecuteQuery(sqlStatement, commandType, parameters));
        }
        public virtual async Task<int> ExecuteNoneQuery(string sqlStatement, CommandType commandType, params SqlParameter[] parameters)
        {
            return await _context.Database.ExecuteNoneQyeryAsync(sqlStatement, commandType, parameters);
        }

        #endregion

        #region Data Operations

        public virtual async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public virtual async Task AddRange(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }
        public virtual async Task Update(T entity, object key)
        {
            await Task.Run(() =>
            {
                _context.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            });
        }
        public virtual async Task Remove(T entity)
        {
            await Task.Run(() => _context.Set<T>().Remove(entity));
        }
        public virtual async Task RemoveRange(IEnumerable<T> entities)
        {
            await Task.Run(() => _context.Set<T>().RemoveRange(entities));
        }

        async Task<(User?, DateTime)> IRepository<T>.GetLastUpdateInfo(int ID)
        {
            T? CurrentEntity = await Get(ID);
            if (CurrentEntity == null) 
                return (new User(), new DateTime());
            else
            {
                return (CurrentEntity.LastUpdateUser, CurrentEntity.LastUpdateDate);
            }
        }

        #endregion

    }
}
