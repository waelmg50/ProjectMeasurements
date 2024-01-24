using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsMeasurements.Models.BasicData
{
    [Table("PlantsCategories")]
    public class PlantsCategory : BaseModels.BaseRecursiveModel
    {
        [Required]
        [MaxLength(100)]
        [Display(Name ="English Name", Prompt ="Enter plant category english name")]
        public string CategoryNameEn { get; set; } = string.Empty;
        [MaxLength(100)]
        [Display(Name = "Arabic Name", Prompt = "Enter plant category arabic name")]
        public string? CategoryNameAr { get; set; }
        [MaxLength(4000)]
        [Display(Name = "Brief description", Prompt = "Enter a brief description about the plant category")]
        public string? CategoryDescription { get; set; }
        [ForeignKey("ParentID")]
        [Display(Name = "Parent Plant Category", Prompt ="Select plant category")]
        public PlantsCategory? ParentCategory { get; set; }
    }
}
