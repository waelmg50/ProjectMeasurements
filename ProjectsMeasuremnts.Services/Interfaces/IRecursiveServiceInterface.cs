using ProjectsMeasurements.Models.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsMeasuremnts.Services.Interfaces
{
    public interface IRecursiveServiceInterface<T> : IBaseServiceInterface<T> where T : BaseRecursiveModel
    {

        #region Select Methods

        Task<IEnumerable<T>> GetChilds(object ParentID);

        #endregion

        #region Data Operations

        Task UpdateFullCode(object key);

        #endregion

    }
}
