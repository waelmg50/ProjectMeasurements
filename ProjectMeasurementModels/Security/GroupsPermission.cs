using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectsMeasurements.Models.BaseModels;

namespace ProjectsMeasurements.Models.Security
{
    public class GroupsPermission : BaseModel
    {
        [Required]
        public int PermissionID { get; set; }
        [Required]
        public int GroupID { get; set; }
        [ForeignKey(nameof(PermissionID))]
        public Permission? ParentPermission { get; set; }
        [ForeignKey(nameof(GroupID))]
        public Group? ParentGroup { get; set; }
    }
}
