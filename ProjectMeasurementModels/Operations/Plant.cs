using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectsMeasurements.Models.BaseModels;
using ProjectsMeasurements.Models.BasicData;

namespace ProjectsMeasurements.Models.Operations
{
    public class Plant : BaseModel
    {
        [Required]
        [Display(Name = "Plant Number", Prompt = "Enter plant number")]
        public int PlantNo { get; set; }
        [Required]
        [MaxLength(20)]
        [Display(Name = "Plant Code", Prompt = "Enter plant code")]
        public string PlantCode { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        [Display(Name = "Plant Name En", Prompt = "Enter plant english name")]
        public string? PlantNameEn { get; set; }
        [MaxLength(100)]
        [Display(Name = "Plant Name Ar", Prompt = "Enter plant arabic name")]
        public string? PlantNameAr { get; set; }
        [MaxLength(4000)]
        [Display(Name = "Plant Description", Prompt = "Enter plant description")]
        public string? PlantDescription { get; set; }
        [Required]
        public int PlantCategoryID { get; set; }
        [ForeignKey(nameof(PlantCategoryID))]
        [Display(Name = "Plant Category", Prompt = "Select plant category")]
        public PlantsCategory? CategoryOfPlant { get; set; }
        [Required]
        [MaxLength(8000)]
        [Column(nameof(PlantFullCode), TypeName = "varchar")]
        [Display(Name = "Plant Full  Code", Prompt = "Enter plant full code")]
        public string? PlantFullCode { get; set; }
    }
}