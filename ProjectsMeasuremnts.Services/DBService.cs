using System.Data;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using ProjectsMeasurements.DBContext;
using ProjectsMeasurements.Repositories;
using ProjectsMeasuremnts.Services.Interfaces;
using ProjectsMeasurements.Models.BaseModels;
using ProjectsMeasurements.Repositories.Interfaces;

namespace ProjectsMeasuremnts.Services
{
    public class DBService<T> : IBaseServiceInterface<T> where T : BaseModel
    {

        #region Members

        private IUnitOfWork? _uow;
        protected IRepository<T>? _rep;
        IUnitOfWork? IBaseServiceInterface<T>.Uow { get => _uow; set { _uow = value; } }
        IRepository<T>? IBaseServiceInterface<T>.Repository { get => _rep; set => _rep = value; }

        #endregion

        #region Constructor

        public DBService(IUnitOfWork? uow, ProjectsMeasurementsDBContext DBContext)
        {
            _uow = uow;
            _rep = new Repository<T>(DBContext);
        }

        #endregion

        #region  Select Methods

        public async Task<bool> Any(Expression<Func<T, bool>> predicate)
        {
            return await (_rep == null ? Task.FromResult(false) : _rep.Any(predicate));
        }
        public async Task<T?>? Get(object id)
        {
            if (_rep == null)
                return null;
            return await _rep.Get(id);
        }
        public async Task<IEnumerable<T>>? Get(Expression<Func<T, bool>> predicate, Func<T, object> sort, int skip, int take, bool ascending)
        {
            if (_rep == null)
                return Enumerable.Empty<T>();
            return await _rep.Get(predicate, sort, skip, take, ascending);
        }
        public async Task<string> GetData(string sqlStatement, CommandType commandType, params SqlParameter[] parameters)
        {
            return await (_rep == null ? Task.FromResult(string.Empty) : _rep.GetData(sqlStatement, commandType, parameters));
        }
        public async Task<string> GetDataSet(string sqlStatement, CommandType commandType, params SqlParameter[] parameters)
        {
            return await (_rep == null ? Task.FromResult(string.Empty) : _rep.GetDataSet(sqlStatement, commandType, parameters));
        }
        public async Task<IEnumerable<T>>? GetQuery(Expression<Func<T, bool>> predicate)
        {
            if (_rep == null)
                return Enumerable.Empty<T>();
            return await _rep.GetQuery(predicate);
        }
        public async Task<IEnumerable<T>>? GetAll()
        {
            if (_rep == null)
                return Enumerable.Empty<T>();
            return await _rep.GetAll();
        }
        public Task<IEnumerable<T>>? Get(Func<T, object> sort, int skip, int take, bool ascending)
        {
            return _rep?.Get(sort, skip, take, ascending);
        }

        #endregion

        #region Data Operations

        public async Task Add(T entity)
        {
            if (_rep != null)
                await _rep.Add(entity);
        }
        public async Task AddRange(IEnumerable<T> entities)
        {
            if (_rep != null)
                await _rep.AddRange(entities);
        }
        public async Task Remove(T entity)
        {
            if (_rep != null)
                await _rep.Remove(entity);
        }
        public async Task RemoveRange(IEnumerable<T> entities)
        {
            if (_rep != null)
                await _rep.RemoveRange(entities);
        }
        public async Task Update(T entity, object key)
        {
            if (_rep != null)
                await _rep.Update(entity, key);
        }
        public async Task<int> ExecuteNoneQuery(string sqlStatement, CommandType commandType, params SqlParameter[] parameters)
        {
            return await (_rep == null ? Task.FromResult(0) : _rep.ExecuteNoneQuery(sqlStatement, commandType, parameters));
        }

        #endregion

    }
}