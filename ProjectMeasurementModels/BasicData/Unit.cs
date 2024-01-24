using System.ComponentModel.DataAnnotations;

namespace ProjectsMeasurements.Models.BasicData
{
    public class Unit : BaseModels.BaseModel
    {

        [Required]
        [MaxLength(100)]
        public string UnitNameEn { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? UnitNameAr { get; set; }

    }
}
