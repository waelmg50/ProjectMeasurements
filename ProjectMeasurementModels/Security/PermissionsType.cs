using System.ComponentModel.DataAnnotations;

namespace ProjectsMeasurements.Models.Security
{
    public class PermissionsType : BaseModels.BaseModel
    {
        [Required][MaxLength(100)] public string? PermissionTypeNameEn { get; set; }
        [MaxLength(100)]
        public string? PermissionTypeNameAr { get; set; }
    }
}
