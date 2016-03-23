using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bys.activity.dal
{
    [Table("ba.activityType")]
    public class ActivityType
    {
        [Key]
        public Guid ID { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string PosterImagePath1 { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
