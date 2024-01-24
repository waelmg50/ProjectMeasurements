using ProjectsMeasurements.DBContext;
using ProjectsMeasurements.Models.BasicData;
using ProjectsMeasurements.Models.Operations;
using ProjectsMeasurements.Models.Security;
using ProjectsMeasurements.Repositories.Interfaces;

namespace ProjectsMeasurements.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Members

        readonly ProjectsMeasurementsDBContext _dataContext;
        IRecursiveRepository<PlantsCategory>? _repPlantsCategories;
        IRepository<Unit>? _repUnits;
        IRepository<Contractor>? _repContractors;
        IRepository<MeasurementsDetail>? _repMeasurementsDetails;
        IRepository<MeasurementsHeader>? _repMeasurementsHeaders;
        IRepository<Owner>? _repOwners;
        IRepository<Plant>? _repPlants;
        IRepository<PlantsDetail>? _repPlantsDetails;
        IRecursiveRepository<Project>? _repProjects;
        IRepository<Group>? _repGroups;
        IRepository<GroupsPermission>? _repGroupsPermissions;
        IRecursiveRepository<Permission>? _repPermissions;
        IRepository<PermissionsType>? _repPermissionsTypes;
        IRepository<User>? _repUsers;
        IRepository<UsersGroup>? _repUsersGroups;

        #endregion

        #region Constructor

        public UnitOfWork(ProjectsMeasurementsDBContext dataContext) => _dataContext = dataContext;

        #endregion

        #region Repositories

        public IRecursiveRepository<PlantsCategory> RepPlantsCategories { get => _repPlantsCategories ??= new RecursiveRepository<PlantsCategory>(_dataContext); }
        public IRepository<Unit> RepUnits { get => _repUnits ??= new Repository<Unit>(_dataContext); }
        public IRepository<Contractor> RepContractors { get => _repContractors ??= new Repository<Contractor>(_dataContext); }
        public IRepository<MeasurementsDetail> RepMeasurementsDetails { get => _repMeasurementsDetails ??= new Repository<MeasurementsDetail>(_dataContext); }
        public IRepository<MeasurementsHeader> RepMeasurementsHeaders { get => _repMeasurementsHeaders ??= new Repository<MeasurementsHeader>(_dataContext); }
        public IRepository<Owner> RepOwners { get => _repOwners ??= new Repository<Owner>(_dataContext); }
        public IRepository<Plant> RepPlants { get => _repPlants ??= new Repository<Plant>(_dataContext); }
        public IRepository<PlantsDetail> RepPlantsDetails { get => _repPlantsDetails ??= new Repository<PlantsDetail>(_dataContext); }
        public IRecursiveRepository<Project> RepProjects { get => _repProjects ??= new RecursiveRepository<Project>(_dataContext); }
        public IRepository<Group> RepGroups { get => _repGroups ??= new Repository<Group>(_dataContext); }
        public IRepository<GroupsPermission> RepGroupsPermissions { get => _repGroupsPermissions ??= new Repository<GroupsPermission>(_dataContext); }
        public IRecursiveRepository<Permission> RepPermissions { get => _repPermissions ??= new RecursiveRepository<Permission>(_dataContext); }
        public IRepository<PermissionsType> RepPermissionsTypes { get => _repPermissionsTypes ??= new Repository<PermissionsType>(_dataContext); }
        public IRepository<User> RepUsers { get => _repUsers ??= new Repository<User>(_dataContext); }
        public IRepository<UsersGroup> RepUsersGroups { get => _repUsersGroups ??= new Repository<UsersGroup>(_dataContext); }

        #endregion

        #region Methods

        public async Task Complete()
        {
            await _dataContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}