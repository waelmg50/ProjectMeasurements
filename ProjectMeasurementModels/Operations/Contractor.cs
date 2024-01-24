using System.ComponentModel.DataAnnotations;

namespace ProjectsMeasurements.Models.Operations
{
    public class Contractor : BaseModels.BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string ContractorNameEn { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? ContractorNameAr { get; set; }
    }
}
