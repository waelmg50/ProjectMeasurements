using Newtonsoft.Json;
using ProjectsMeasurements.Models.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsMeasurements.Models.BaseModels
{
    public class BaseModel
    {
        [Key]
        public int ID { get; set; }
        public int? LastUserID { get; set; }
        [Display(Name = "Last Update Date", Description ="The last update time for the current record")]
        [Column(nameof(LastUpdateDate), TypeName ="datetime")]
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
        
        [ForeignKey(nameof(LastUserID))]
        public User? LastUpdateUser { get; set; }
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
