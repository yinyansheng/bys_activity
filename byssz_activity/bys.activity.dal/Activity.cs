using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bys.activity.dal
{
    [Table("ba.activity")]
    public class Activity
    {
        [Key]
        public Guid ID { get; set; }
        
        [MaxLength(100)]
        public string Name { get; set; }
        public Guid ActivityTypeID { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        [MaxLength(500)]
        public String Address { get; set; }
        [MaxLength(100)]
        public string OriginatorAlias { get; set; }
        [MaxLength(1000)]
        public string Detail { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
