using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectsMeasurements.Models.BaseModels;

namespace ProjectsMeasurements.Models.Security
{
    public class Permission : BaseRecursiveModel
    {
        [Required]
        [MaxLength(100)]
        public string PermissionNameEn { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? PermissionNameAr { get; set; }
        [Required]
        public int PermissionTypeID { get; set; }
        [ForeignKey("PermissionTypeID")]
        public PermissionsType? TypeOfPermission { get; set; }
        [ForeignKey("ParentID")]
        public Permission? ParentPermission { get; set; }
    }
}
