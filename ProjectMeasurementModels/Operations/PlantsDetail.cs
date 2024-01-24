using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectsMeasurements.Models.BasicData;

namespace ProjectsMeasurements.Models.Operations
{
    public class PlantsDetail : BaseModels.BaseModel
    {
        [Required]
        public int PlantID { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 4)")]
        public decimal PlantHeight { get; set; }
        public int PlantHeihgtUnitID { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal PlantTrunkWidth { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal PlantDefaultPrice { get; set; }
        [ForeignKey(nameof(PlantHeihgtUnitID))]
        public Unit? PlantHeightUnit { get; set; }
        [ForeignKey(nameof(PlantID))]
        public Plant? ParentPlant { get; set; }

    }
}
