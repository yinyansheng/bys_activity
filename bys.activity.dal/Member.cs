using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bys.activity.dal
{
    [Table("ba.member")]
    public class Member
    {
        [Key]
        public Guid ID { get; set; }

        [MaxLength(100)]
        public string Alias { get; set; }

        [MaxLength(100)]
        public string DisplayName { get; set; }

        [MaxLength(200)]
        public string AvantarPath { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
