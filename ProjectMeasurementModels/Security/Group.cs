using System.ComponentModel.DataAnnotations;
using ProjectsMeasurements.Models.BaseModels;

namespace ProjectsMeasurements.Models.Security
{
    public class Group : BaseModel
    {
        [Required][MaxLength(100)] public string? GroupNameEn { get; set; }
        [MaxLength(100)]
        public string? GroupNameAr { get; set; }
        [MaxLength(2000)]
        public string? GroupDescription { get; set; }
    }
}
