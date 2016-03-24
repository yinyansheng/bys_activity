using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bys.activity.dal
{
    [Table("ba.syslog")]
    public class SysLog
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Type { get; set; }
        [MaxLength(200)]
        public string UserName { get; set; }
        [MaxLength(50)]
        public string Area { get; set; }
        [MaxLength(200)]
        public string ControllerName { get; set; }
        [MaxLength(200)]
        public string ActionName { get; set; }
        [MaxLength(200)]
        public string MethodName { get; set; }
        [MaxLength(200)]
        public string ExecuteTime { get; set; }
        [MaxLength(500)]
        public string Msg { get; set; }
        public DateTime LogTime { get; set; }

    }
}
