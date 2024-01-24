using System.ComponentModel.DataAnnotations;

namespace ProjectsMeasurements.Models.BasicData
{
    public class MeasurementsType : BaseModels.BaseModel
    {

        [Required]
        [MaxLength(100)]
        public string TypeNameEn { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? TypeNameAr { get; set; }

    }
}
