using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsMeasurements.Models.Security
{
    public class UsersGroup : BaseModels.BaseModel
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public int GroupID { get; set; }
        [ForeignKey("UserID")]
        public User? ParentUser { get; set; }
        [ForeignKey("GroupID")]
        public Group? ParentGroup { get; set; }
    }
}
