using System.Data;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using ProjectsMeasurements.Models.BaseModels;
using ProjectsMeasurements.Repositories.Interfaces;

namespace ProjectsMeasuremnts.Services.Interfaces
{
    public interface IBaseServiceInterface<T> where T : BaseModel
    {

        #region Members

        protected IUnitOfWork? Uow { get; set; }
        protected IRepository<T>? Repository { get; set; }

        #endregion

        #region Select Methods

        Task<T?>? Get(object id);
        Task<IEnumerable<T>>? GetAll();
        Task<bool> Any(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>>? Get(Func<T, object> sort, int skip, int take, bool ascending);
        Task<IEnumerable<T>>? GetQuery(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>>? Get(Expression<Func<T, bool>> predicate, Func<T, object> sort, int skip, int take, bool ascending);
        Task<string> GetData(string sqlStatement, CommandType commandType, params SqlParameter[] parameters);
        Task<string> GetDataSet(string sqlStatement, CommandType commandType, params SqlParameter[] parameters);
        Task<int> ExecuteNoneQuery(string sqlStatement, CommandType commandType, params SqlParameter[] parameters);

        #endregion

        #region Data Operations

        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task Update(T entity, object key);
        Task Remove(T entity);
        Task RemoveRange(IEnumerable<T> entities);

        #endregion

    }
}
