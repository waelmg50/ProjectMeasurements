using System.ComponentModel.DataAnnotations;

namespace ProjectsMeasurements.Models.Operations
{
    public class Owner : BaseModels.BaseModel
    {
        [Required][MaxLength(100)] public string OwnerNameEn { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? OwnerNameAr { get; set; }

    }
}
