using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectsMeasurements.DBContext;
using ProjectsMeasurements.Models.BaseModels;
using ProjectsMeasurements.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsMeasurements.Repositories
{
    public class RecursiveRepository<T> : Repository<T>, IRecursiveRepository<T> where T : BaseRecursiveModel
    {

        #region Constructor

        public RecursiveRepository(ProjectsMeasurementsDBContext Context) : base(Context) { }

        #endregion

        #region Methods

        public async Task<IEnumerable<T>> GetChilds(object ParentID)
        {
            if(ParentID == null)
                return await Get(x => x.ParentID == null, x => x.FullCode, 0, 1000, true);
            return await Get(x => x.ParentID == (int)ParentID, x => x.FullCode, 0, 1000, true);
        }
        public async Task UpdateFullCode(object key)
        {
            string? strTableName = _context?.Model.FindEntityType(typeof(T))?.GetTableName();
            if (strTableName == null)
                return;
            await ExecuteNoneQuery($"update {strTableName} set FullCode = right('000' + cast(Code as varchar(20)), 2) where (ParentID is null and FullCode <> right('000' + cast(Code as varchar(20)), 2))", System.Data.CommandType.Text);
            await ExecuteNoneQuery($"while(exists(select null from {strTableName} Child inner join {strTableName} Parent on Parent.ID = Child.ParentID where ((Child.FullCode <> Parent.FullCode + '-' + right('000' + cast(Child.Code as varchar(20)), 2) or Child.FullCode is null)))) update {strTableName} set FullCode = Parent.FullCode + '-' + right('000' + cast(Code as varchar(20)), 2) from {strTableName} as Child inner join (select ID, FullCode from {strTableName}) as Parent on Parent.ID = Child.ParentID where (Child.FullCode <> Parent.FullCode + '-' + right('000' + cast(Code as varchar(20)), 2) or Child.FullCode is null)", System.Data.CommandType.Text);
        }

        #endregion

    }
}
