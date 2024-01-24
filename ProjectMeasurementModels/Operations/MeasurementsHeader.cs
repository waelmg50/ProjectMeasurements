using ProjectsMeasurements.Models.BaseModels;
using ProjectsMeasurements.Models.BasicData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsMeasurements.Models.Operations
{
    public class MeasurementsHeader : BaseModel
    {

        [Required]
        public int MeasurementCode { get; set; }
        [Column(nameof(MeasurementDate), TypeName = "datetime")]
        public DateTime MeasurementDate { get; set; } = DateTime.Now;
        public int ProjectID { get; set; }
        public int OwnerID { get; set; }
        public int ContractorID { get; set; }
        public int MeasurementTypeID { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal MeasurementTotalPrice { get; set; }
        [MaxLength(4000)]
        public string? Remarks { get; set; }
        [ForeignKey(nameof(ProjectID))]
        public Project? MeasurementProject { get; set; }
        [ForeignKey(nameof(OwnerID))]
        public Owner? MeasurementOwner { get; set; }
        [ForeignKey(nameof(ContractorID))]
        public Contractor? MeasurementContractor { get; set; }
        [ForeignKey(nameof(MeasurementTypeID))]
        public MeasurementsType? MeasurementType { get; set; }

    }
}
