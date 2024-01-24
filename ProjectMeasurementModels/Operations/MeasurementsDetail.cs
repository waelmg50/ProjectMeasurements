using ProjectsMeasurements.Models.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsMeasurements.Models.Operations
{
    public class MeasurementsDetail : BaseModel
    {

        [Required]
        public int MeasurementHeaderID { get; set; }
        [Required]
        public int PlantDetailID { get; set; }
        [Required]
        public int PlantQuantity { get; set; } = 1;
        [Required]
        public decimal PlantUnitPrice { get; set; }
        [Required]
        [Column(nameof(PlantTotalPrice), TypeName = "decimal(18, 4)")]
        public decimal PlantTotalPrice { get; set; }
        [MaxLength(400)]
        public string? Remarks { get; set; }
        [ForeignKey(nameof(PlantDetailID))]
        public PlantsDetail? MeasurmentPlantDetail { get; set; }
        [ForeignKey(nameof(MeasurementHeaderID))]
        public MeasurementsHeader? MeasurementHeader { get; set; }

    }
}
