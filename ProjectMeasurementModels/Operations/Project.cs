using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsMeasurements.Models.Operations
{
    public class Project : BaseModels.BaseRecursiveModel
    {
        [Required]
        [MaxLength(100)]
        public string ProjectNameEn { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? ProjectNameAr { get; set; }
        [MaxLength(4000)]
        public string? ProjectDescription { get; set; }
        
        [ForeignKey(nameof(ParentID))]
        public Project? ParentProject { get; set; }
    }
}
