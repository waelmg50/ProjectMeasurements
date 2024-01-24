using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsMeasurements.Models.BaseModels
{
    public class BaseRecursiveModel : BaseModel
    {
        [Required]
        [MaxLength(20)]
        [Column(nameof(Code), TypeName = "varchar")]
        public string Code { get; set; } = string.Empty;
        public int? ParentID { get; set; }
        [MaxLength(8000)]
        [Column(nameof(FullCode), TypeName = "varchar")]
        public string FullCode { get; set; } = string.Empty;
    }
}
