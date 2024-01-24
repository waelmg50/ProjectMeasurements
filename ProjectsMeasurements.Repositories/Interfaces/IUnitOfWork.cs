using ProjectsMeasurements.Models.BasicData;
using ProjectsMeasurements.Models.Operations;
using ProjectsMeasurements.Models.Security;

namespace ProjectsMeasurements.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        #region Repositories

        IRecursiveRepository<PlantsCategory> RepPlantsCategories { get; }
        IRepository<Unit> RepUnits { get; }
        IRepository<Contractor> RepContractors { get; }
        IRepository<MeasurementsDetail> RepMeasurementsDetails { get; }
        IRepository<MeasurementsHeader> RepMeasurementsHeaders { get; }
        IRepository<Owner> RepOwners { get; }
        IRepository<Plant> RepPlants { get; }
        IRepository<PlantsDetail> RepPlantsDetails { get; }
        IRecursiveRepository<Project> RepProjects { get; }
        IRepository<Group> RepGroups { get; }
        IRepository<GroupsPermission> RepGroupsPermissions { get; }
        IRecursiveRepository<Permission> RepPermissions { get; }
        IRepository<PermissionsType> RepPermissionsTypes { get; }
        IRepository<User> RepUsers { get; }
        IRepository<UsersGroup> RepUsersGroups { get; }
        
        #endregion

        #region Methods

        Task Complete();
       new void Dispose();

        #endregion

    }
}
