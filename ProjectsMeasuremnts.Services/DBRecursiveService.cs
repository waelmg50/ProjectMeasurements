using ProjectsMeasurements.DBContext;
using ProjectsMeasurements.Models.BaseModels;
using ProjectsMeasurements.Repositories;
using ProjectsMeasurements.Repositories.Interfaces;
using ProjectsMeasuremnts.Services.Interfaces;

namespace ProjectsMeasuremnts.Services
{
    public class DBRecursiveService<T> : DBService<T>, IRecursiveServiceInterface<T> where T : BaseRecursiveModel 
    {

        #region Constructor

        public DBRecursiveService(IUnitOfWork? uow, ProjectsMeasurementsDBContext DBContext) : base(uow, DBContext) { _rep = new RecursiveRepository<T>(DBContext); }

        #endregion

        #region Methods

        public async Task<IEnumerable<T>> GetChilds(object ParentID)
        {
            if (_rep is RecursiveRepository<T> repository)
                return await repository.GetChilds(ParentID);
            return Enumerable.Empty<T>();
        }
        public async Task UpdateFullCode(object key)
        {
            if (key == null)
                return;
            if (_rep is RecursiveRepository<T> repository)
                await repository.UpdateFullCode(key);
        }

        #endregion

    }
}
