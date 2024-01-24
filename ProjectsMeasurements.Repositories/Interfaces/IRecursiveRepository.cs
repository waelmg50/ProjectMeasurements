using Microsoft.Data.SqlClient;
using ProjectsMeasurements.Models.BaseModels;
using ProjectsMeasurements.Models.Security;
using System.Data;
using System.Linq.Expressions;

namespace ProjectsMeasurements.Repositories.Interfaces
{

    public interface IRecursiveRepository<T> : IRepository<T> where T : BaseRecursiveModel
    {

        #region Select Methods

        Task<IEnumerable<T>> GetChilds(object ParentID);
        
        #endregion

        #region Data Operations

        Task UpdateFullCode(object key);
        
        #endregion

    }

}
